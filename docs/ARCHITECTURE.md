# System Architecture — Egyptian Pharmacy POS

## Table of Contents

1. [Technology Stack](#1-technology-stack)
2. [Architectural Pattern](#2-architectural-pattern)
3. [Layer Responsibilities](#3-layer-responsibilities)
   - 3.1 [Data Access Layer (DAL)](#31-data-access-layer-dal)
   - 3.2 [Business Logic Layer (BLL)](#32-business-logic-layer-bll)
   - 3.3 [Presentation Layer (UI)](#33-presentation-layer-ui)
4. [Project Structure](#4-project-structure)
5. [Project Dependencies](#5-project-dependencies)
6. [Data Flow Diagrams](#6-data-flow-diagrams)
   - 6.1 [Standard Sale](#61-standard-sale)
   - 6.2 [Stock Receiving](#62-stock-receiving)
   - 6.3 [Return Processing](#63-return-processing)
7. [Database Design](#7-database-design)
   - 7.1 [Entity Overview](#71-entity-overview)
   - 7.2 [Entity Relationship Summary](#72-entity-relationship-summary)
8. [Cross-Cutting Concerns](#8-cross-cutting-concerns)

---

## 1. Technology Stack

| Concern | Technology |
|---|---|
| Language | C# (.NET) |
| Presentation | Windows Forms |
| Business Logic | C# class library |
| Data Access | EF Core (Code First) |
| Query Language | LINQ |
| Database | SQL Server |
| Deployment | Single machine, local SQL Server instance |

**SQL Server** is used to provide reliable transaction support, row-level locking during concurrent writes, and a clear migration path to a multi-terminal or networked configuration without changing application code.

EF Core is configured Code First. All schema changes are managed through migrations. No raw SQL is written in the application — all queries go through EF Core and LINQ.

---

## 2. Architectural Pattern

The system uses a strict **3-Tier Architecture** across four projects:

```
┌─────────────────────────────────┐
│     Presentation Layer (UI)     │  Windows Forms
│  Forms, Controls, Event Handlers│
└────────────────┬────────────────┘
                 │  calls BLL interfaces
                 ▼
┌─────────────────────────────────┐
│   Business Logic Layer (BLL)    │  C# Class Library
│   Rules, Validation, Workflows  │
└────────────────┬────────────────┘
                 │  calls DAL interfaces
                 ▼
┌─────────────────────────────────┐
│   Data Access Layer (DAL)       │  EF Core + LINQ
│   DbContext, Entities, Repos    │
└────────────────┬────────────────┘
                 │  reads/writes
                 ▼
┌─────────────────────────────────┐
│           SQL Server            │
└─────────────────────────────────┘

         ↑ all layers depend on ↑
┌─────────────────────────────────┐
│            Common               │  Shared primitives
│       Enums, Constants          │
└─────────────────────────────────┘
```

**Layer communication rules:**
- The UI calls the BLL through BLL interfaces only. It never references `DbContext` or any DAL type directly.
- The BLL calls the DAL through DAL repository interfaces only. It contains all business rules and is the sole decision-maker on whether an operation is valid.
- The DAL communicates with SQL Server only. It contains no business rules and applies no domain validation.
- Dependencies flow downward only. No layer references the layer above it.
- Shared primitives that are needed across layers live in `Common`, which has no dependencies of its own.

---

## 3. Layer Responsibilities

### 3.1 Data Access Layer (DAL)

**Responsibilities**
- Defines all EF Core entity models.
- Contains `PharmacyDbContext` with `DbSet` declarations. Entity configuration is applied via `modelBuilder.ApplyConfigurationsFromAssembly()` — no Fluent API in `OnModelCreating` directly.
- Defines one `IEntityTypeConfiguration<T>` class per entity in the `Configurations` folder, keeping each entity's mapping rules isolated and maintainable.
- Declares repository interfaces in the `Interfaces` folder. The BLL depends on these interfaces, not on concrete repository classes.
- Implements repository classes in the `Repositories` folder.
- Manages EF Core migrations and schema versioning against SQL Server.

**What the DAL does not do**
- It does not validate business rules — it does not check whether a `BatchItem` is expired before returning it.
- It does not calculate totals, apply discounts, or decide which batch to consume.
- It does not reference any Windows Forms type or UI concern.

**Key components**

| Component | Purpose |
|---|---|
| `PharmacyDbContext` | Single EF Core context. Discovers and applies all configurations automatically |
| `Configurations/` | One `IEntityTypeConfiguration<T>` class per entity. Owns all Fluent API mapping for that entity |
| `Interfaces/` | `IRepository<T>` base and entity-specific repository interfaces consumed by the BLL |
| `Repositories/` | Concrete implementations of repository interfaces |
| `Migrations/` | EF Core migration files managing all schema changes |

**DbContext lifetime**
A new `DbContext` instance is created per BLL service call and disposed after the operation completes. Long-lived contexts are avoided in Windows Forms applications to prevent stale tracking state accumulating across user interactions.

---

### 3.2 Business Logic Layer (BLL)

**Responsibilities**
- Declares service interfaces in the `Interfaces` folder. The UI depends on these interfaces, not on concrete service classes.
- Implements all domain rules and workflow logic in the `Services` folder.
- Validates all inputs before any write reaches the DAL.
- Selects the correct `BatchItem` for a sale using FIFO ordering and expiry filtering.
- Calculates invoice totals, discounts, and refund amounts.
- Enforces immutability rules on finalised invoices.
- Runs expiry threshold checks and returns alert data to the UI.
- Returns plain DTO objects to the UI — never raw EF Core entities.

**What the BLL does not do**
- It does not render anything or reference any Windows Forms type.
- It does not write SQL or construct raw queries.
- It does not make UI decisions — it returns results and errors; the UI decides how to display them.

**Key service interfaces and implementations**

| Interface | Service | Responsibility |
|---|---|---|
| `IBarcodeService` | `BarcodeService` | Check digit validation on scanned or entered barcodes |
| `IStockService` | `StockService` | BatchItem lookup, FIFO selection, expiry filtering, quantity checks |
| `IInvoiceService` | `InvoiceService` | Cart management, invoice finalisation, total calculation |
| `IReturnService` | `ReturnService` | Return quantity validation, refund calculation from InvoiceItem history |
| `IExpiryService` | `ExpiryService` | Threshold-based expiry queries, notification data preparation |
| `ICustomerService` | `CustomerService` | Customer lookup, creation, Name and Phone validation |

**Error handling**
The BLL returns `OperationResult` wrapper objects to the UI for expected validation failures (expired stock, missing customer data, oversell attempt). Exceptions propagate only for unexpected infrastructure failures.

---

### 3.3 Presentation Layer (UI)

**Responsibilities**
- Renders data returned by the BLL.
- Captures user input and passes it to BLL service interfaces.
- Enforces soft UI constraints (e.g., disabling "Complete Sale" until a customer is linked) as a usability layer on top of BLL hard enforcement.
- Displays expiry alerts on startup and in response to the manual check button.

**What the UI does not do**
- It does not calculate any value shown to the user. All numbers come from the BLL.
- It does not read from or write to the database directly.
- It does not contain conditional logic that substitutes for a missing BLL rule.
- It does not reference any DAL type directly.

**Key screens**

| Screen | Purpose |
|---|---|
| POS / Cart | Barcode scan, product lookup, discount entry, sale finalisation |
| Stock Receiving | Batch creation, BatchItem entry per product line |
| Invoice Search | Lookup by customer name, phone, or invoice ID |
| Returns | Load original invoice, select items and quantities to return |
| Expiry Dashboard | Displays near-expiry and expired stock from ExpiryService |
| Customer Management | Create and search customer records |

---

## 4. Project Structure

```
PharmacyPOS.sln
│
├── Common/                              # Shared primitives — no dependencies
│   └── Enums/
│       └── InvoiceStatus.cs
│
├── DAL/                                 # Data Access Layer
│   ├── Models/
│   │   ├── Customer.cs
│   │   ├── Supplier.cs
│   │   ├── Product.cs
│   │   ├── Batch.cs
│   │   ├── BatchItem.cs
│   │   ├── Invoice.cs
│   │   └── InvoiceItem.cs
│   ├── Context/
│   │   └── PharmacyDbContext.cs
│   ├── Configurations/                  # One class per entity — IEntityTypeConfiguration<T>
│   │   ├── CustomerConfiguration.cs
│   │   ├── SupplierConfiguration.cs
│   │   ├── ProductConfiguration.cs
│   │   ├── BatchConfiguration.cs
│   │   ├── BatchItemConfiguration.cs
│   │   ├── InvoiceConfiguration.cs
│   │   └── InvoiceItemConfiguration.cs
│   ├── Interfaces/                      # Repository interfaces consumed by BLL
│   │   ├── IRepository.cs
│   │   ├── IBatchItemRepository.cs
│   │   └── IInvoiceRepository.cs
│   ├── Repositories/                    # Concrete implementations
│   │   ├── BatchItemRepository.cs
│   │   └── InvoiceRepository.cs
│   └── Migrations/
│
├── BLL/                                 # Business Logic Layer
│   ├── Interfaces/                      # Service interfaces consumed by UI
│   │   ├── IBarcodeService.cs
│   │   ├── IStockService.cs
│   │   ├── IInvoiceService.cs
│   │   ├── IReturnService.cs
│   │   ├── IExpiryService.cs
│   │   └── ICustomerService.cs
│   ├── Services/                        # Concrete implementations
│   │   ├── BarcodeService.cs
│   │   ├── StockService.cs
│   │   ├── InvoiceService.cs
│   │   ├── ReturnService.cs
│   │   ├── ExpiryService.cs
│   │   └── CustomerService.cs
│   ├── DTOs/                            # Plain objects passed to the UI
│   │   ├── InvoiceDto.cs
│   │   ├── CartItemDto.cs
│   │   └── ExpiryAlertDto.cs
│   └── Results/
│       └── OperationResult.cs           # Success/failure wrapper returned by all services
│
└── UI/                                  # Presentation Layer
    ├── Forms/
    │   ├── POSForm.cs
    │   ├── StockReceivingForm.cs
    │   ├── InvoiceSearchForm.cs
    │   ├── ReturnsForm.cs
    │   ├── ExpiryDashboardForm.cs
    │   └── CustomerForm.cs
    └── Program.cs
```

---

## 5. Project Dependencies

```
Common      →  (none)
DAL         →  Common
BLL         →  Common, DAL (interfaces only)
UI          →  Common, BLL (interfaces only)
```

The UI has no reference to DAL. The BLL has no reference to any Windows Forms assembly. `Common` has no reference to any other project in the solution. This ensures the dependency graph has no cycles and each layer can be tested or replaced independently.

---

## 6. Data Flow Diagrams

### 6.1 Standard Sale

```
Cashier scans barcode
        │
        ▼
[UI] passes barcode string to IBarcodeService
        │
        ▼
[BLL: BarcodeService] validates check digit
        │ fail → OperationResult.Failure → UI displays error
        │ pass ↓
[BLL: StockService] calls IProductRepository / IBatchItemRepository
        │
        ▼
[DAL] returns BatchItems where:
      Product.Barcode matches
      ExpirationDate >= today
      QuantityRemaining > 0
        │
        ▼
[BLL] applies FIFO: selects BatchItem with earliest ExpirationDate
      maps to CartItemDto (name + MandatorySellingPrice)
      returns to UI
        │
        ▼
[UI] adds CartItemDto to cart. Cashier sets quantity and optional discount
        │
        ▼
Cashier links customer record, clicks Complete Sale
        │
        ▼
[UI] calls IInvoiceService.FinaliseInvoice(cart, customerId)
        │
        ▼
[BLL: CustomerService] verifies Customer.Name and Customer.Phone are non-empty
        │ fail → OperationResult.Failure → UI displays error
        │ pass ↓
[BLL: InvoiceService] calculates TotalAmount, TotalDiscount, NetAmount
      builds Invoice and InvoiceItem records
      copies MandatorySellingPrice → OriginalPrice on each InvoiceItem
      sets Invoice.Status = Finalised
      decrements QuantityRemaining on each consumed BatchItem
        │
        ▼
[DAL] persists all records in a single SQL Server transaction
        │
        ▼
[UI] receives OperationResult.Success, displays confirmation
```

### 6.2 Stock Receiving

```
Staff enters supplier delivery details
        │
        ▼
[UI] calls IStockService.CreateBatch(supplierId, purchaseDate, lines[])
        │
        ▼
[BLL: StockService] validates each line:
      ProductId must exist
      QuantityReceived > 0
      ExpirationDate > today
      CostPrice and MandatorySellingPrice > 0
        │ any fail → OperationResult.Failure → UI displays error
        │ all pass ↓
[BLL] constructs Batch header and one BatchItem per product line
      sets QuantityRemaining = QuantityReceived on each BatchItem
        │
        ▼
[DAL] persists Batch and all BatchItems in a single SQL Server transaction
```

### 6.3 Return Processing

```
Cashier retrieves original invoice by customer name, phone, or invoice ID
        │
        ▼
[UI] calls IInvoiceService.GetFinalisedInvoice(identifier)
        │
        ▼
[BLL: InvoiceService] queries IInvoiceRepository for Invoice where Status = Finalised
      maps result to InvoiceDto with InvoiceItem detail
      returns to UI
        │
        ▼
[UI] displays line items. Cashier selects item and enters return quantity
        │
        ▼
[UI] calls IReturnService.ProcessReturn(invoiceItemId, returnQuantity)
        │
        ▼
[BLL: ReturnService] validates:
      returnQuantity > 0
      returnQuantity <= InvoiceItem.Quantity - InvoiceItem.ReturnedQuantity
        │ fail → OperationResult.Failure → UI displays error
        │ pass ↓
[BLL] calculates refund = InvoiceItem.DiscountedPrice × returnQuantity
      increments InvoiceItem.ReturnedQuantity
      increments BatchItem.QuantityRemaining
        │
        ▼
[DAL] persists updated InvoiceItem and BatchItem in a single SQL Server transaction
        │
        ▼
[UI] receives OperationResult with refund amount, displays to cashier
```

---

## 7. Database Design

### 7.1 Entity Overview

| Entity | Represents | Key Fields |
|---|---|---|
| `Customer` | A pharmacy patient | `Name`, `Phone` |
| `Supplier` | A pharmaceutical distributor | `Name`, `Phone` |
| `Product` | A medication catalog entry — no price, no quantity | `Barcode`, `Name` |
| `Batch` | A supplier delivery header | `PurchaseDate`, `SupplierId` |
| `BatchItem` | A specific physical lot of a product | `MandatorySellingPrice`, `ExpirationDate`, `QuantityReceived`, `QuantityRemaining` |
| `Invoice` | A sale transaction header | `CustomerId`, `Status`, `NetAmount` |
| `InvoiceItem` | A line item on a sale | `BatchItemId`, `OriginalPrice`, `DiscountedPrice`, `ReturnedQuantity` |

Full field-level documentation for each entity is in [ENTITIES.md](./ENTITIES.md).

### 7.2 Entity Relationship Summary

```
Supplier ──< Batch ──< BatchItem >── Product
                            │
                            └──< InvoiceItem >── Invoice >── Customer
```

| Relationship | Cardinality |
|---|---|
| Supplier → Batches | One-to-Many |
| Batch → BatchItems | One-to-Many |
| Product → BatchItems | One-to-Many |
| BatchItem → InvoiceItems | One-to-Many |
| Invoice → InvoiceItems | One-to-Many |
| Customer → Invoices | One-to-Many |

All foreign keys are enforced at the SQL Server level via EF Core Fluent API configuration applied through the `Configurations` classes.

---

## 8. Cross-Cutting Concerns

### Transactions

Any operation that writes to more than one table is wrapped in a single EF Core transaction against SQL Server. This applies to invoice finalisation (Invoice + InvoiceItems + BatchItem quantity updates), batch receiving (Batch + BatchItems), and return processing (InvoiceItem + BatchItem). A partial write is never committed.

### Concurrency

`BatchItem.QuantityRemaining` is the field most at risk from concurrent writes in a future multi-terminal configuration. Before any multi-terminal deployment, a `rowversion` / `[Timestamp]` column will be added to `BatchItem` to enable EF Core optimistic concurrency — detecting and rejecting conflicting simultaneous decrements.

### Soft Deletes

No entity exposes a hard delete through the application. Entities referenced by historical records (`Customer`, `Product`, `BatchItem`, `Supplier`) are marked inactive via an `IsActive` flag rather than deleted. The BLL filters inactive records from operational queries while leaving all historical invoice data intact.

### No Raw SQL

All database interaction goes through EF Core and LINQ. Raw SQL is not used anywhere in the application. This ensures all queries are parameterised and validated at compile time through LINQ's type system.

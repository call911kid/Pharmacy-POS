# Entity Reference — Egyptian Pharmacy POS

## Table of Contents

1. [Customer](#1-customer)
2. [Supplier](#2-supplier)
3. [Product](#3-product)
4. [Batch](#4-batch)
5. [BatchItem](#5-batchitem)
6. [Invoice](#6-invoice)
7. [InvoiceItem](#7-invoiceitem)
8. [Entity Relationship Summary](#8-entity-relationship-summary)

---

## 1. Customer

Represents a pharmacy patient. Required on every invoice.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Name` | `string` | Required. Patient's full name |
| `Phone` | `string` | Required. Used for invoice retrieval and patient contact |
| `Invoices` | `ICollection<Invoice>` | Navigation. All invoices linked to this customer |

**Relationships:** One Customer → Many Invoices.

---

## 2. Supplier

Represents a pharmaceutical distributor or manufacturer from whom stock is purchased.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Name` | `string` | Supplier trading name |
| `Phone` | `string` | Contact number |
| `Batches` | `ICollection<Batch>` | Navigation. All purchase deliveries from this supplier |

**Relationships:** One Supplier → Many Batches.

---

## 3. Product

Represents a medication as a catalog entry. Holds static identity only — no price, no quantity.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Barcode` | `string` | Scanned identifier. Not guaranteed unique across price change cycles |
| `Name` | `string` | Commercial product name |
| `BatchItems` | `ICollection<BatchItem>` | Navigation. All stock lots of this product |

**Note:** Will reference a future `Medicine` entity holding clinical data — active ingredient, dosage form, category, and unit of measure.

**Relationships:** One Product → Many BatchItems.

---

## 4. Batch

Represents a single purchase delivery from a supplier — the header record that groups all product lines received in one transaction.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `PurchaseDate` | `DateTime` | Date the stock was received |
| `SupplierId` | `int` | Foreign key to Supplier |
| `Supplier` | `Supplier` | Navigation property |
| `BatchItems` | `ICollection<BatchItem>` | Navigation. All product lines in this delivery |

**Relationships:** Many Batches → One Supplier. One Batch → Many BatchItems.

---

## 5. BatchItem

The central stock entity. Represents a specific lot of a specific product received in a specific delivery. All dynamic, lot-level attributes — price, quantity, and expiry — live here.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `BatchId` | `int` | Foreign key to Batch |
| `Batch` | `Batch` | Navigation property |
| `ProductId` | `int` | Foreign key to Product |
| `Product` | `Product` | Navigation property |
| `QuantityReceived` | `int` | Immutable. Units received at time of purchase |
| `QuantityRemaining` | `int` | Mutable. Decremented on sale, incremented on return |
| `ExpirationDate` | `DateTime` | Lot-specific expiry date as printed on packaging |
| `CostPrice` | `decimal` | Purchase price per unit paid to the supplier |
| `MandatorySellingPrice` | `decimal` | Legally enforced selling price (التسعيرة الجبرية) for this lot. Set once at stock entry |

**Business rules:**
- `QuantityRemaining` cannot go below zero.
- `QuantityRemaining` cannot exceed `QuantityReceived`.
- Any `BatchItem` where `ExpirationDate` is before today is excluded from all sale workflows.
- Among eligible `BatchItems` for the same `Product`, the one with the earliest `ExpirationDate` is consumed first (FIFO).

**Relationships:** Many BatchItems → One Batch. Many BatchItems → One Product. One BatchItem → Many InvoiceItems.

---

## 6. Invoice

The sales transaction header. Represents a completed or in-progress sale to a specific customer.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `InvoiceDate` | `DateTime` | Date and time of the transaction |
| `CustomerId` | `int` | Foreign key to Customer. Required |
| `Customer` | `Customer` | Navigation property |
| `TotalAmount` | `decimal` | Sum of OriginalPrice × Quantity across all InvoiceItems |
| `TotalDiscount` | `decimal` | Sum of (OriginalPrice − DiscountedPrice) × Quantity across all InvoiceItems |
| `NetAmount` | `decimal` | Amount actually charged to the customer |
| `Status` | `InvoiceStatus` | Enum: Draft, Finalised, Cancelled |
| `InvoiceItems` | `ICollection<InvoiceItem>` | Navigation. All line items on this invoice |

**InvoiceStatus Enum**

| Value | Meaning |
|---|---|
| `Draft` | Invoice is being built. Line items can be added or removed |
| `Finalised` | Sale is complete. Record is immutable. Returns can be processed against this invoice |
| `Cancelled` | Transaction was voided. Read-only. Stock quantities are not affected |

**Business rules:**
- A linked `Customer` with non-empty `Name` and `Phone` is required before `Status` can advance from `Draft` to `Finalised`.
- `TotalAmount`, `TotalDiscount`, and `NetAmount` are calculated by the BLL at finalisation and written once. They are not recalculated after the invoice is `Finalised`.
- Only `Finalised` invoices can be used as the basis for a return.

**Relationships:** Many Invoices → One Customer. One Invoice → Many InvoiceItems.

---

## 7. InvoiceItem

A single line item on a sale invoice. Links a specific `BatchItem` — and therefore a specific lot with a specific legally enforced price — to a specific invoice. Freezes the exact prices paid at the moment of sale.

| Field | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `InvoiceId` | `int` | Foreign key to Invoice |
| `Invoice` | `Invoice` | Navigation property |
| `BatchItemId` | `int` | Foreign key to BatchItem. Records which exact lot was sold |
| `BatchItem` | `BatchItem` | Navigation property |
| `Quantity` | `int` | Units sold in this transaction |
| `ReturnedQuantity` | `int` | Units returned against this line to date. Defaults to 0 |
| `OriginalPrice` | `decimal` | MandatorySellingPrice copied from BatchItem at time of sale. Immutable after finalisation |
| `DiscountedPrice` | `decimal` | Actual per-unit price charged. Equals OriginalPrice if no discount was applied |

**Business rules:**
- `OriginalPrice` and `DiscountedPrice` are copied from the `BatchItem` at finalisation and never updated afterward.
- `ReturnedQuantity` cannot exceed `Quantity`.
- Refund amount for any return = `DiscountedPrice × ReturnedQuantity`. The BLL does not reference the current `BatchItem` price for this calculation.

**Relationships:** Many InvoiceItems → One Invoice. Many InvoiceItems → One BatchItem.

---

## 8. Entity Relationship Summary

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

All foreign keys are enforced at the SQL Server level via EF Core Fluent API configuration. No foreign key is nullable unless explicitly noted in the field definitions above.

# Business Overview — Egyptian Pharmacy POS

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Egyptian Market Challenges and Design Responses](#2-egyptian-market-challenges-and-design-responses)
   - 2.1 [Mandatory Pricing (التسعيرة الجبرية) and Barcode Limitations](#21-mandatory-pricing-التسعيرة-الجبرية-and-barcode-limitations)
   - 2.2 [Discounted Returns](#22-discounted-returns)
   - 2.3 [Expired Medications Tracking](#23-expired-medications-tracking)
   - 2.4 [Patient Data Capture Enforcement](#24-patient-data-capture-enforcement)
3. [System Entities — What They Represent](#3-system-entities--what-they-represent)
4. [Key Business Rules](#4-key-business-rules)
5. [Anticipated Constraints and Future Considerations](#5-anticipated-constraints-and-future-considerations)

---

## 1. Project Overview

This document describes the business logic, operational workflows, and rules for a desktop Pharmacy POS system targeting Egyptian retail pharmacies. The system handles two core workflows: stock management (receiving and tracking inventory by supplier delivery) and point-of-sale (selling, discounting, and returning medications).

Full field-level and schema detail for each entity is in [ENTITIES.md](./ENTITIES.md).

---

## 2. Egyptian Market Challenges and Design Responses

### 2.1 Mandatory Pricing (التسعيرة الجبرية) and Barcode Limitations

**The Problem**

Egyptian pharmaceutical law requires that all medications be sold at a government-mandated price (التسعيرة الجبرية). This price is printed on the physical packaging and is legally enforced at point of sale.

This creates a specific inventory problem: the mandated price changes over time. When the government updates the price for a drug, new stock arrives at the new price while old stock — still carrying the old price on its packaging — may remain on the shelf. Both lots share the same barcode. A system that looks up price by barcode alone cannot distinguish between them. Serving the new price against old stock either overcharges the customer or creates a discrepancy with the printed packaging, both of which are compliance failures.

Egyptian pharmacy barcodes compound this problem. They are not always unique to a product variant — the same barcode can appear on packaging from different manufacturers or across multiple price change cycles.

**The Design Response: Separating Product from BatchItem**

The system resolves this by separating the static identity of a medication from the dynamic attributes of a specific physical lot.

A **Product** is the medication as a catalog entry — its barcode and name. It holds no price and no quantity. It never changes based on market conditions.

A **BatchItem** is a specific physical lot of that product as received from a supplier on a specific date. It holds the expiry date, quantity on hand, cost price, and the legally enforced selling price that applied to that lot at the time of purchase. That price is set once, at stock entry, and never modified.

When a cashier scans a barcode, the system does not look up a price on the Product. It finds all lots of that product that have remaining stock and have not expired, then selects the lot with the earliest expiry date (FIFO). The price shown to the cashier is the legally enforced price recorded on that specific lot.

Two lots of the same drug can coexist in the system with different legally correct prices. The system serves each at the right price in the right order, without any manual intervention from the cashier.

---

### 2.2 Discounted Returns

**The Problem**

Pharmacies regularly apply promotional or manual discounts at point of sale. When a customer returns a discounted medication, the refund must equal exactly what the customer paid — not the current shelf price, and not the original undiscounted price. If the refund is calculated from the current shelf price and that price has since changed, the refund will be wrong. The only correct source of truth for a return is the original sale record.

**The Design Response: Price Freezing at the Moment of Sale**

Every line item on a sale invoice records two prices at the moment the sale is completed: the original shelf price before any discount, and the actual price the customer paid after discount. Once the sale is completed, these figures are permanent. No subsequent price change on the shelf, and no modification to any discount record, affects them.

**Return Workflow**

When a customer initiates a return, the cashier retrieves the original invoice by the customer's name, phone number, or invoice ID. The refund is calculated as the price the customer originally paid multiplied by the quantity being returned. The cashier has no input into the refund amount — it comes directly from the historical record. There is no calculation that can drift from what the customer actually paid.

---

### 2.3 Expired Medications Tracking

**The Problem**

Dispensing an expired medication is a patient safety failure and a regulatory violation. Manual shelf checks are unreliable in high-volume pharmacies. The system must surface expiry risks proactively, without waiting for a staff member to initiate a check.

**The Design Response: Lot-Level Expiry Monitoring**

Every lot of stock in the system carries its own expiry date. The system uses this date in two ways.

**Hard Block at Point of Sale**

When a cashier scans a product, the system only considers lots that have not expired. An expired lot is never surfaced as an available option. If a cashier attempts to add expired stock by any method, the system rejects it. Expired stock cannot enter a sale under any normal workflow.

**Proactive Notification**

On application startup, and at a configurable interval during the session, the system checks all lots whose expiry date falls within a configurable warning window (default: 30 days) and that still have quantity on hand. Any matches are surfaced as a notification listing the product name, remaining quantity, and exact expiry date. Staff can then act — returning stock to the supplier, promoting it for clearance, or scheduling disposal.

**On-Demand Check Button**

The UI exposes a dedicated control that triggers the same expiry check on demand. This is used at shift handover, during physical stock audits, or whenever a staff member needs immediate visibility into the full expiry status of all inventory.

---

### 2.4 Patient Data Capture Enforcement

**The Problem**

Every sale must be traceable to a specific patient. This supports two operational needs: retrieving the original invoice for a return, and contacting a patient in the event of a product recall or dispensing error. A sale record without a patient name and phone number cannot support either workflow.

**The Design Response: Required Patient Record on Every Sale**

Every invoice must be linked to a patient record before the sale can be completed. A patient record holds the patient's name and phone number. Both fields are required — neither can be empty. If either is missing, the system blocks the sale.

The UI enforces this as a workflow gate: the option to complete a sale is unavailable until a patient record is associated with the current invoice. The business logic layer enforces the same rule independently, so the block holds regardless of how the sale is initiated.

Returning patients can be retrieved by name or phone number, so a cashier does not need to re-enter data for repeat visits. The patient's full invoice history is accessible from their record, which also supports invoice retrieval for returns without requiring the cashier to know the invoice ID.

---

## 3. System Entities — What They Represent

This section describes the purpose of each entity in plain business terms. For field definitions, data types, and relationships, see [ENTITIES.md](./ENTITIES.md).

**Customer** — A pharmacy patient. Every sale is linked to a customer record. The customer's name and phone number are required before a sale can be completed.

**Supplier** — A pharmaceutical distributor or manufacturer. Stock deliveries are recorded against a supplier so that cost and sourcing history is traceable.

**Product** — A medication as a catalog entry. Identifies the medication by barcode and name. Holds no price and no quantity — those belong to the lot, not the product.

**Batch** — A single stock delivery from a supplier. Groups all product lines received in one purchase together under a single delivery record with a purchase date.

**BatchItem** — A specific physical lot of a product as received in a delivery. This is where price, quantity, and expiry date live. It is the unit the system uses to determine what is available for sale, at what price, and whether it has expired.

**Invoice** — A sale transaction. Links a set of sold items to a specific customer and records the total charged. An invoice moves through three states: Draft (being built), Finalised (sale complete, immutable), and Cancelled (voided).

**InvoiceItem** — A single line on a sale invoice. Records which specific lot was sold, how many units, and the exact prices — both before and after discount — that the customer was charged. This record is the authoritative source for any future return calculation.

---

## 4. Key Business Rules

All rules below are enforced in the Business Logic Layer. The UI may reflect them as soft constraints, but the business logic layer is the authoritative enforcement point in all cases.

| Rule | Description |
|---|---|
| Check digit validation | A barcode must pass check digit validation before any product lookup proceeds |
| No expired stock in sale | Any lot whose expiry date has passed is excluded from all point-of-sale queries |
| FIFO batch selection | Among available lots for the same product, the one with the earliest expiry date is consumed first |
| No oversell | The remaining quantity on a lot cannot be decremented below zero |
| Customer required | A sale cannot be completed without a linked patient record that has a non-empty name and phone number |
| Price freeze | The original and discounted prices on a sale line are recorded once at the moment of sale and never changed |
| Immutable completed sale | No field on a completed or cancelled invoice can be modified |
| Return quantity limit | The quantity returned against a sale line cannot exceed the quantity originally sold on that line |
| Refund from historical price | A refund is always calculated from the price recorded on the original sale line, never from the current shelf price |
| Expiry notification threshold | Lots expiring within the configured warning window are surfaced as alerts on startup and on demand |

---

## 5. Anticipated Constraints and Future Considerations

### Prescription-Only Medications

Certain drug categories in Egypt are dispensed only against a valid prescription. The system does not currently model this. Before dispensing prescription-only medications through the POS, a mechanism to record and verify prescription details at the point of sale will be required.

### Supplier Returns and Stock Adjustments

Stock levels change for reasons beyond sales: returns to suppliers, damaged stock write-offs, breakage, and periodic physical count corrections. Each of these changes the quantity on hand for a lot without being associated with a customer sale. A formal stock adjustment workflow — with a recorded reason and authorising staff member — is required to keep inventory accurate and auditable.

### Unit-of-Measure Selling

Some medications are sold by the strip, the tablet, or the vial rather than the full box. The current model tracks quantity in a single unit per lot. Handling sub-unit sales requires either a unit conversion mechanism or a policy decision to always sell at the base packaging unit.

### Hard Deletion Risk

Removing a patient, product, supplier, or stock lot that is referenced by a historical sale record will either break referential integrity or destroy historical data. Any entity referenced by historical records must be deactivated rather than deleted, remaining visible in historical context but excluded from active operational workflows.

### Multi-Terminal Operation

If the system is later expanded to multiple terminals sharing one database, simultaneous stock decrements against the same lot will require concurrency protection to prevent overselling.

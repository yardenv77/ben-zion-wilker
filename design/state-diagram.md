# PurchaseOrder State Diagram

**Source:** interactive SVG diagram provided directly by the team, embedded in `state-diagram.html` (open it in a browser; it also exports `.xmi` for Visual Paradigm). This document is the transition inventory derived from that source, produced for confirmation before implementation (per the course's state-diagram step) and kept as the design-level reference afterward.

Lifecycle of a `PurchaseOrder` (UC-03 Create Purchase Order). 11 states — 9 simple + 2 composite — one per `POStatus` literal (see `class-diagram.md` §1 and §5 for how this revised the original 5-value enum).

## 1. States

| State | `POStatus` value | Entry action |
|---|---|---|
| Draft | `Draft` | `setStatus('DRAFT')` |
| UnderApproval «composite» | `UnderApproval` | `setStatus('UNDER_APPROVAL')` |
| &nbsp;&nbsp;├─ PendingPMApproval | `PendingPMApproval` | `requestPMApproval()` → `setStatus(...)`; `notifyProjectManager()` |
| &nbsp;&nbsp;├─ PendingBudgetOverride | `PendingBudgetOverride` | `flagForOverride()` → `setStatus(...)`; `notifyCEO()` |
| Rejected | `Rejected` | `returnToAccountant()` → `setStatus(...)`; `notifyAccountant()` |
| InFulfillment «composite» | `InFulfillment` | `setStatus('IN_FULFILLMENT')` |
| &nbsp;&nbsp;├─ Sent | `Sent` | `markAsSent()` → `setStatus(...)`; `sendPOEmailToSupplier()` |
| &nbsp;&nbsp;├─ PartiallyReceived | `PartiallyReceived` | `setStatus('PARTIALLY_RECEIVED')` |
| Received | `Received` | `closeAsReceived()` → `setStatus(...)`; `setClosureReason('RECEIVED')` |
| Cancelled | `Cancelled` | `closeAsCancelled()` → `setStatus(...)`; `setClosureReason('CANCELLED')` |
| Archived | `Archived` | `setStatus('ARCHIVED')` (`closureReason` is preserved, not overwritten) |

`UnderApproval` has no inner initial pseudostate: at submission, BR-2 selects `PendingPMApproval` or `PendingBudgetOverride` directly (see t2a/t2b below). `InFulfillment` does have an inner initial pseudostate, always entering at `Sent`.

## 2. Transitions

| # | Source | Target | Trigger | Guard | Action / side effects |
|---|---|---|---|---|---|
| t0 | ● initial | Draft | — | — | — |
| t1 | Draft | Draft (self) | Line added or edited | — | `recalculateTotals()` — reads `PurchaseOrderLine.getLineTotal()` across lines |
| t2a | Draft | PendingBudgetOverride | Order submitted | BR-2: over budget | reads `BudgetLine` (remaining budget) to evaluate the guard; entry → `notifyCEO()` |
| t2b | Draft | PendingPMApproval | Order submitted | BR-2: within budget | reads `BudgetLine`; entry → `notifyProjectManager()` |
| t3 | UnderApproval (border — applies from either PendingPMApproval or PendingBudgetOverride) | Draft | Order withdrawn by accountant | — | — |
| t4a | PendingPMApproval | Rejected | Order rejected by PM | — | `recordRejection(reason, rejectedBy=PM)` — stores `rejectionReason` and `rejectedBy`; entry → `notifyAccountant()` |
| t4b | PendingBudgetOverride | Rejected | Order rejected by CEO | — | `recordRejection(reason, rejectedBy=CEO)` — stores `rejectionReason` and `rejectedBy`; entry → `notifyAccountant()` |
| t5 | Rejected | Draft | Revision started | — | — |
| t6 | Rejected | Cancelled | after(14 days from rejection) | BR-3 (automatic) | entry → `setClosureReason('CANCELLED')` |
| t7 | Draft | ◎ final | Order cancelled | BR-1: never submitted | `delete()` — removes the `PurchaseOrder` and cascades to its `PurchaseOrderLine` children (composition) |
| t8 | Draft | Cancelled | Order cancelled | BR-1: previously submitted (i.e. this Draft was reached via a Revision Started from Rejected) | entry → `setClosureReason('CANCELLED')` |
| t10 | PendingBudgetOverride | PendingPMApproval | Budget override approved by CEO | — | entry → `notifyProjectManager()` |
| t11 | PendingPMApproval | InFulfillment (enters at Sent) | Order approved by PM | — | entry → `sendPOEmailToSupplier()` — touches `Supplier`; this is UC-03's «include» UC "Send Purchase Order Email to Supplier" |
| t14 | Sent | PartiallyReceived | Delivery received | BR-5: lines remain open | `recordDelivery()` — updates `receivedQuantity` on the delivered `PurchaseOrderLine`(s) |
| t15 | PartiallyReceived | PartiallyReceived (self) | Delivery received | BR-5: lines remain open | `recordDelivery()` — same, on `PurchaseOrderLine` |
| t16 | InFulfillment (border — applies from either Sent or PartiallyReceived) | Received | Delivery received | BR-5: all lines fully received | `recordDelivery()` on `PurchaseOrderLine`; entry → `setClosureReason('RECEIVED')` |
| t17 | InFulfillment (border — applies from either sub-state) | Cancelled | Remaining order cancelled | BR-4: supplier agreed | entry → `setClosureReason('CANCELLED')` |
| t18 | Received | Archived | Fiscal year closed | — | `archive()` |
| t19 | Cancelled | Archived | Fiscal year closed | — | `archive()` |
| t20 | Archived | ◎ final | after(7 years) | — | `purge()` — cascades to `PurchaseOrderLine` |

**Approval order (confirmed final, matches the team's earlier submitted parts):** at submission, BR-2 checks the budget *before* either approver sees the order. An over-budget order goes to the CEO first (`PendingBudgetOverride`); only after the CEO approves the override does it move to `PendingPMApproval`. An order within budget skips the CEO and goes straight to `PendingPMApproval`. The PM's approval (t11) is always the last gate before `InFulfillment`. **`00e-use-cases.md`'s UC-03 MSS text needs to be aligned to this order** — flagged here, not rewritten, since editing UC spec behavioral text is a team decision.

## 3. Business Rules (source commentary)

- **BR-1:** An order that was never submitted may be deleted; a submitted order is cancelled and archived.
- **BR-2:** An order exceeding the remaining budget line needs a CEO budget override *before* PM approval (UC-03, step 8a).
- **BR-3:** A rejected order not revised within 14 days is cancelled automatically (supplier quotes are typically valid ~2 weeks).
- **BR-4:** Cancelling the remainder of a sent order requires supplier agreement.
- **BR-5:** An order is `Received` only when `receivedQuantity` equals `quantity` on every `PurchaseOrderLine`.
- **Retention:** 7 years — a purchase order is a financial record (bookkeeping regulations). `closureReason` is kept when `Archived` overwrites `status`.

## 4. New attributes introduced by this diagram

Per `class-diagram.md` §5's model-assumption note, this diagram revised `POStatus` from 5 to 11 values and added:

- `PurchaseOrder.rejectionReason : String`
- `PurchaseOrder.closureReason : ClosureReason` (new enum: `Received`, `Cancelled`)
- `PurchaseOrderLine.receivedQuantity : Real`

These are reflected in `class-diagram.md`, `CLAUDE.md`, and `scripts/create_database.sql`.

## 5. Revision: t4 split into t4a/t4b

The original single `t4` border transition (`UnderApproval` → `Rejected`, firing from either sub-state) was a valid UML shorthand for "the same rejection behavior applies regardless of which sub-state you're in" — but it meant *which* sub-state a given rejection actually came from was lost: nothing downstream (data or diagram) distinguished a PM rejection from a CEO rejection, even though the accept-path already distinguishes the two symmetric roles (`approvedBy` for the PM, `overrideApprovedBy` for the CEO). Split into `t4a` (PM rejects, from `PendingPMApproval`) and `t4b` (CEO rejects, from `PendingBudgetOverride`), each now also setting a new attribute:

- `PurchaseOrder.rejectedBy : Employee` — role `ProjectManager` on `t4a`, `CEO` on `t4b`; cleared by `t5` (Revision started), like `rejectionReason`.

Reflected in `CLAUDE.md`, `scripts/create_database.sql`, and `scripts/migrate_po_rejectedby.sql` (existing database).

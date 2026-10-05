# Domain Class Diagram

**Source:** interactive SVG diagram provided directly by the team, embedded in `class-diagram.html` (open it in a browser; it also exports `.xmi` for Visual Paradigm). This version supersedes the earlier 22-class/10-enumeration diagram: it was revised after `state-diagram.md` (the `PurchaseOrder` lifecycle) was designed, per the course's own convention that a state diagram is allowed to correct the class diagram it's derived from (see §5's `POStatus` note below) — plus a round of independent domain-modelling additions (business identifiers, the `Employee` role-association notes, `SubmittedDocument`, etc.).

Ben Tzion Wilker (1987) Ltd - Domain Class Diagram. Government-contractor construction & infrastructure ERP - System Analysis & Design course project. All 23 domain classes, 13 enumerations, and 1 external system are rendered on a single canvas, organized spatially into four domain clusters: Sales/Tendering (top-left, incl. Client), Procurement & Partners (top-right), Field Execution (left), Finance & Risk (bottom-right), with `Project` as the connecting hub and `MODPortal` on the outer edge. Green outline in the HTML marks the classes implemented in the programming part (see §5 model assumption on scope).

## 1. Enumerations

| Enumeration | Values |
|---|---|
| TenderStatus | Searching, InPreparation, Submitted, Won, Lost |
| ProjectStatus | InProgress, Completed, OnHold, Cancelled |
| PartnerStatus | Active, Inactive |
| POStatus | Draft, UnderApproval, PendingPMApproval, PendingBudgetOverride, Rejected, InFulfillment, Sent, PartiallyReceived, Received, Cancelled, Archived |
| ClosureReason | Received, Cancelled |
| EmployeeRole | SiteSupervisor, EquipmentManager, SafetyManager, ProjectManager, Accountant, FinanceOfficer, CEO, TenderCoordinator |
| EmployeeStatus | Active, OnVacation, Suspended, Terminated |
| WorkLogStatus | Submitted, UnderReview |
| EquipmentStatus | Available, InUse, UnderRepair |
| SecurityStatus | Active, Expired, Released |
| PaymentRequestStatus | Submitted, UnderReview, Approved, Rejected, Paid |
| DocumentType | Invoice, QuantityStatement, SupervisorApproval, SiteDiaryExtract, InsuranceCertificate |
| SupplierPaymentStatus | Pending, Paid, Overdue |

## 2. Domain Classes

### Cluster 1 - Sales / Tendering

| Class | Attributes | Methods |
|---|---|---|
| Client | name:String, contactPerson:String, phone:String, email:String, sector:String | getActiveProjects():List, getTotalRevenue():Money |
| Tender | tenderNumber:String, title:String, estimatedValue:Money, submissionDeadline:Date, publishedDate:Date, status:TenderStatus | daysUntilDeadline():Integer, isExpiringSoon():Boolean |
| Project | name:String, address:String, plannedStartDate:Date, plannedEndDate:Date, actualStartDate:Date, actualEndDate:Date, status:ProjectStatus | calculateProfitability():Money, isOverBudget():Boolean, getCurrentProgress():Real |

### Cluster 2 - Procurement & Partners

| Class | Attributes | Methods |
|---|---|---|
| BusinessPartner «abstract» | name:String, companyRegistrationNo:String, contactPerson:String, phone:String, email:String, rating:Real, status:PartnerStatus | isActive():Boolean, getEngagementHistory():List |
| Supplier (extends BusinessPartner) | - | getActivePriceQuotes():List, getPurchaseOrderCount():Integer |
| Subcontractor (extends BusinessPartner) | tradeSpecialty:String, dailyRate:Money | getRecentJobs():List, getAvgVariance():Real |
| TradeCategory | categoryName:String | getAverageQuotePrice():Money, getSupplierCount():Integer |
| SupplierPriceQuote | amount:Money, dateIssued:Date, validUntil:Date, isSelected:Boolean | isExpired():Boolean, isBestOffer():Boolean |
| PurchaseOrder | poNumber:String, orderDate:Date, totalAmount:Money, vatAmount:Money, status:POStatus, rejectionReason:String, closureReason:ClosureReason, rejectedAt:Date, archivedAt:Date, everSubmitted:Boolean | public (state-diagram events + guard queries): calculateTotal():Money, calculateVat():Money, exceedsBudget():Boolean, isFullyReceived():Boolean, addLine(line), editLine(line), submit(), withdraw(), approveBudgetOverride(ceo:Employee), approve(pm:Employee), reject(reason:String, by:Employee), revise(), cancel(), autoCancel(), receiveDelivery(line, qty), cancelRemaining(supplierAgreed:Boolean), archive(), purge(); private (entry wrappers + transition actions): setStatus(status), setClosureReason(reason), flagForOverride(), requestPMApproval(), returnToAccountant(), markAsSent(), closeAsReceived(), closeAsCancelled(), notifyCEO(), notifyProjectManager(), notifyAccountant(), sendPOEmailToSupplier(), recalculateTotals(), recordRejection(reason:String, by:Employee), recordDelivery(), delete() |
| PurchaseOrderLine | description:String, unitOfMeasure:String, quantity:Real, receivedQuantity:Real, unitPrice:Money | getLineTotal():Money, isPriced():Boolean, getRemainingQuantity():Real |

### Cluster 3 - Field Execution

| Class | Attributes | Methods |
|---|---|---|
| Employee | firstName:String, lastName:String, nationalId:String, role:EmployeeRole, dailyRate:Money, certificationNo:String, status:EmployeeStatus | isAvailable(date):Boolean, getFullName():String |
| DailyWorkLog | logDate:Date, plannedQuantity:Real, completedQuantity:Real, status:WorkLogStatus | isBelowThreshold():Boolean, calculateVariance():Real |
| Attendance «association class» | startTime:Time, endTime:Time, taskDescription:String | hoursWorked():Real, getDailyPay():Money, isFullDay():Boolean |
| Equipment | licenseNumber:String, equipmentType:String, description:String, dailyCost:Money, status:EquipmentStatus | isAvailable(date):Boolean, getUsageHistory():List |
| EquipmentUsage «association class» | hoursOperated:Real | getUsageCost():Money, isExcessiveUsage():Boolean |
| EquipmentAssignment «link class» | startDate:Date, endDate:Date | getDurationDays():Integer, isActive():Boolean |

### Cluster 4 - Finance & Risk

| Class | Attributes | Methods |
|---|---|---|
| BudgetLine | category:String, plannedAmount:Money, actualAmount:Money | getVariance():Money, isOverThreshold(percent):Boolean |
| FinancialSecurity «abstract» | amount:Money, issueDate:Date, expiryDate:Date, status:SecurityStatus | isExpiringSoon():Boolean, getRemainingDays():Integer |
| BankGuarantee (extends FinancialSecurity) | bankName:String, guaranteeNumber:String | isCallable():Boolean, renew():Boolean |
| InsurancePolicy (extends FinancialSecurity) | insurerName:String, policyNumber:String, coverageType:String | fileClaim(desc):Boolean, isCoverageActive():Boolean |
| PaymentRequest | amount:Money, submissionDate:Date, approvalDate:Date, status:PaymentRequestStatus | daysWaiting():Integer, isApproved():Boolean, getMissingDocuments():List |
| SubmittedDocument | type:DocumentType, receivedOn:Date | - |
| SupplierPayment | invoiceNumber:String, amount:Money, dueDate:Date, paidDate:Date, status:SupplierPaymentStatus | isOverdue():Boolean, getDaysOverdue():Integer |

### External system (not a domain class)

`MODPortal` «external system» - rendered as a styled stereotype box with a dashed dependency arrow from `Project` to `MODPortal`, tagged «depends on» / "syncs status & reports". It has no attributes/methods of its own - it represents the Ministry of Defense portal, which the company only reads from/writes to and does not manage.

## 3. Relationships and Multiplicities

| # | Relationship | Type | Multiplicity |
|---|---|---|---|
| 1 | Client - Tender | Association | Client 1 -- 0..* Tender |
| 2 | Tender - Project (labeled "becomes") | Association | Tender 1 -- 0..1 Project |
| 3 | Project --> MODPortal | Dependency («external system») | - |
| 4 | Supplier - SupplierPriceQuote | Association | Supplier 1 -- 0..* SupplierPriceQuote |
| 5 | TradeCategory - SupplierPriceQuote | Association | TradeCategory 1 -- 0..* SupplierPriceQuote |
| 6 | Tender - SupplierPriceQuote | Association | Tender 1 -- 0..* SupplierPriceQuote |
| 7 | BusinessPartner <\|-- Supplier | Generalization | - |
| 8 | BusinessPartner <\|-- Subcontractor | Generalization | - |
| 9 | Project - PurchaseOrder | Association | Project 1 -- 0..* PurchaseOrder |
| 10 | Supplier - PurchaseOrder | Association | Supplier 1 -- 0..* PurchaseOrder |
| 11 | PurchaseOrder *-- PurchaseOrderLine | Composition | PurchaseOrder 1 -- 1..* PurchaseOrderLine |
| 12 | Employee - Project {role=projectManager} | Association | Employee 1 -- 0..* Project |
| 13 | Employee - PurchaseOrder {role=createdBy} | Association | Employee 1 -- 0..* PurchaseOrder |
| 14 | Employee - PurchaseOrder {role=approvedBy} | Association | Employee 1 -- 0..* PurchaseOrder |
| 15 | Employee - PurchaseOrder {role=overrideApprovedBy} | Association | Employee 0..1 -- 0..* PurchaseOrder |
| 16 | Employee - DailyWorkLog {role=submittedBy} | Association | Employee 1 -- 0..* DailyWorkLog |
| 17 | Employee -- DailyWorkLog (via Attendance) | Association class (many-to-many) | Employee 0..* -- 0..* DailyWorkLog |
| 18 | Project -- DailyWorkLog | Association | Project 1 -- 0..* DailyWorkLog |
| 19 | Subcontractor -- DailyWorkLog (labeled "if subcontracted") | Association (optional) | Subcontractor 0..1 -- 0..* DailyWorkLog |
| 20 | Equipment -- DailyWorkLog (via EquipmentUsage) | Association class (many-to-many) | Equipment 0..* -- 0..* DailyWorkLog |
| 21 | Project -- EquipmentAssignment | Association | Project 1 -- 0..* EquipmentAssignment |
| 22 | Equipment -- EquipmentAssignment | Association | Equipment 1 -- 0..* EquipmentAssignment |
| 23 | Project *-- BudgetLine | Composition | Project 1 -- 1..* BudgetLine |
| 24 | Project -- FinancialSecurity | Association | Project 1 -- 0..* FinancialSecurity |
| 25 | FinancialSecurity <\|-- BankGuarantee | Generalization | - |
| 26 | FinancialSecurity <\|-- InsurancePolicy | Generalization | - |
| 27 | Project -- PaymentRequest | Association | Project 1 -- 0..* PaymentRequest |
| 28 | PaymentRequest *-- SubmittedDocument | Composition | PaymentRequest 1 -- 0..* SubmittedDocument |
| 29 | BusinessPartner -- SupplierPayment | Association | BusinessPartner 1 -- 0..* SupplierPayment |
| 30 | Employee - PurchaseOrder {role=rejectedBy} | Association | Employee 0..1 -- 0..* PurchaseOrder |

Relationships #9 (`Project`-`PurchaseOrder`) and #24 (`Project`-`FinancialSecurity`) resolve the two gaps previously flagged in `CLAUDE.md`'s "Known gaps in the class diagram" section (both were implied by UC-03 / UC-06 but absent from the relationship table). Both classes now carry the FK directly.

## 4. Association Classes, Mediator, and Role Rationale (source commentary, translated)

**a. Generalization - used twice:**
- `BusinessPartner` ← `Supplier`, `Subcontractor`: both sides share identity, contact details, rating and status, but differ in the nature of the engagement.
- `FinancialSecurity` ← `BankGuarantee`, `InsurancePolicy`: both sides share an identical lifecycle (issued → active → expired/released) and the same alerting logic, but differ in the issuing party and their unique fields.

**b. Composition - used three times:**
- `Project` ◆- `BudgetLine`: a budget line has no meaning or independent identity outside its project (multiplicity tightened to `1..*`: a project is expected to have at least one budget line from the start).
- `PurchaseOrder` ◆- `PurchaseOrderLine`: an order line does not exist and cannot be referenced outside its specific order.
- `PaymentRequest` ◆- `SubmittedDocument` (new): a submitted document has no meaning outside the payment request it was attached to; `getMissingDocuments()` derives the gap from which `DocumentType` values are present, replacing the former free-text `missingDocuments` attribute.

**c. Mediating class replacing a ternary association:**
- `SupplierPriceQuote` is now modeled as three separate binary associations (to `Supplier`, `Tender`, `TradeCategory`) rather than a single ternary association class. The price still depends on all three together; the mediating-class shape carries the same information without the ternary notation.

**d. Association classes - used twice:**
- `Attendance`, on the many-to-many relationship between `Employee` and `DailyWorkLog`: now stores `startTime`/`endTime` instead of a stored `hoursWorked` — `hoursWorked()` is derived, with the constraint `{endTime > startTime}`.
- `EquipmentUsage`, on the many-to-many relationship between `Equipment` and `DailyWorkLog`: unchanged, `hoursOperated` is a fact about the specific pair.

**e. Employee role associations (new):**
- `Employee`-`PurchaseOrder` now has four separate associations, one per role in UC-03: `createdBy` {role = Accountant, drafted the PO}, `approvedBy` {role = ProjectManager, approved it}, `overrideApprovedBy` {role = CEO, budget override, 0..1 since not every order needs one}, `rejectedBy` {role = ProjectManager on t4a or CEO on t4b, 0..1 -- set on entry to Rejected, cleared by Revision started; added by `state-diagram.md` §5}.
- `Employee`-`Project` has `projectManager` {role = ProjectManager}.
- `Employee`-`DailyWorkLog` has `submittedBy` {role = SiteSupervisor, the site supervisor who filed the log}, separate from the `Attendance` association class (every worker present that day).
- `EmployeeRole` is a single enumeration, not subclasses; a role gets its own association only where it acts on an object, constrained by `{role = ...}` in the source notes.

## 5. Model Assumptions (source commentary, translated)

- **Identifiers:** only numbers that someone outside the system refers to are modelled as attributes (`tenderNumber` = MOD tender number, `poNumber` = printed on the PO sent to the supplier, `invoiceNumber` = the supplier invoice, `licenseNumber` = vehicle/machine licence plate). Surrogate database keys stay in the database, not in the diagram — the old `<x>Id:String` placeholders were removed from every class for this reason (see `CLAUDE.md`'s Primary Key Strategy: they never were separate columns).
- **`POStatus` was revised from its original 5 values to the current 11** after `docs/design/state-diagram.md` (the `PurchaseOrder` lifecycle) was designed: `Approved` was removed (approval leads straight into `Sent`, not a separate persisted state), `PendingApproval` was renamed to `PendingPMApproval`, and `Draft`, `UnderApproval`, `InFulfillment`, `PartiallyReceived`, `Received`, `Cancelled` and `Archived` were added. `rejectionReason`, the new `ClosureReason` enumeration and `closureReason`, and `receivedQuantity` (on `PurchaseOrderLine`) were added for the same reason. This is the expected, intended outcome of the state-diagram step in this course, not a modeling inconsistency. `UnderApproval` and `InFulfillment` are composite/superstate values only: each is set by the superstate's entry action and immediately overwritten by the entry action of the inner state entered, so they are never actually persisted. `00e-use-cases.md`'s UC-03 MSS text needs alignment with the confirmed approval order (a budget-exceeding order goes to the CEO for override *before* PM approval, per BR-2).
- **`EmployeeRole` was widened from 3 field-workforce values to all 8 system-actor roles**, merging what was previously a separate, unenforced permissions list (`00e-use-cases.md` §8.1) directly onto `Employee`. This is a deliberate, confirmed decision (not a leftover inconsistency) needed so `Project.projectManager`, `PurchaseOrder.createdBy/approvedBy/overrideApprovedBy` and `DailyWorkLog.submittedBy` can reference real `Employee` rows.
- **`DailyWorkLog.status` uses the new `WorkLogStatus` enumeration** (`Submitted`, `UnderReview`), reversing the earlier decision to keep it a free `String`. Also a deliberate, confirmed decision.
- `SupplierPayment` is linked to the superclass `BusinessPartner` (rather than separately to `Supplier` and `Subcontractor`), so as not to duplicate the same relationship twice.
- `Equipment` and `Employee` are considered global company resources that do not belong exclusively to a single project. `Project`-`EquipmentAssignment` is a plain association (not aggregation as in the earlier diagram): equipment assignments belong to a project but do not compose it.
- VAT/currency rates are not detailed as a separate class - they are held as a simple `Money` attribute on every relevant class. `PurchaseOrder.totalAmount`/`vatAmount` are stored (they are columns on the `PurchaseOrder` table), but only ever written from `calculateTotal()`/`calculateVat()` over the order lines (`recalculateTotals()`), so the stored values cannot drift from the lines. (An earlier revision of this diagram removed them as purely derived; restored because the implementation persists them.)
- **`PurchaseOrder.rejectedAt`, `archivedAt`, `everSubmitted` and the `rejectedBy` association** were added during implementation of the state diagram: `rejectedAt`/`archivedAt` are the reference times of its two time events (`after(14 days)` → BR-3 `autoCancel()`, `after(7 years)` → `purge()`), `everSubmitted` is the BR-1 guard (delete a never-submitted Draft vs. cancel a previously-submitted one), and `rejectedBy` records which approver rejected the order (PM on t4a, CEO on t4b).
- `PurchaseOrder.attachments` (files attached to a purchase order, as mentioned in UC-03's MSS) is not detailed as a separate class at this stage - it is held as a future extension outside the scope of the current diagram.
- `Client` is linked to `Project` only through `Tender` (`Client 1--0..* Tender`, `Tender 1--0..1 Project`), and not through an additional direct relationship, so as not to create a redundant duplicate path in the diagram.
- The relationship `Subcontractor--DailyWorkLog` is optional (0..1 on the `Subcontractor` side), because a work log can belong entirely to the internal crew with no subcontractor involvement.
- `getAvgVariance()` (on `Subcontractor`) aggregates `calculateVariance()` across all of the subcontractor's logs, and supports the Extend relationship of UC-04 (Record Daily Work Log → Track Subcontractor Progress vs Plan).
- **Scope of the programming part:** the green outline in the HTML marks the classes used by the 3 selected use cases — UC-03 Create Purchase Order, UC-05 Profitability & Cash Flow Report, UC-06 Manage Guarantee & Insurance — and by the full CRUD screens (Employee): `Project`, `BusinessPartner`/`Supplier`, `PurchaseOrder`/`PurchaseOrderLine`, `Employee`, `BudgetLine`, `FinancialSecurity`/`BankGuarantee`/`InsurancePolicy`, `PaymentRequest`. Every other class is part of the full domain model and still has an entity class, a table and a basic maintenance screen (see `CLAUDE.md`'s "Project-Specific Decisions"), but none of the 3 selected UCs is built on it.

## 6. Legend

Association — | Composition ◆— | Generalization ▷— | Dependency (with «external system» stereotype for MODPortal). A dashed tether with a small diamond junction marks an association class attachment. Multiplicities are shown at both ends of a relationship; italicized method names denote operations; role names appear in green italics near the association end they qualify.

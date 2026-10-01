-- ============================================================================
-- create_database.sql
-- Ben Tzion Wilker (1987) Ltd. Project Management System
--
-- Creates all 23 domain-class tables from docs/design/class-diagram.md,
-- in the load order documented in docs/CLAUDE.md ("Domain Entities and
-- Load Order"). Run against the database configured for the mssql MCP
-- (see CLAUDE.md's "Database" section) -- Azure SQL does not support
-- CREATE DATABASE or USE from a script; the connection already targets
-- the right database, so neither statement appears here.
--
-- Conventions (see CLAUDE.md "Primary Key Strategy" / "Enumerations"):
--   * Every PK is INT NOT NULL PRIMARY KEY, assigned in C# (no IDENTITY).
--   * Enum-like attributes are NVARCHAR(20) + CHECK, no lookup tables.
--   * Table-per-subclass: a subclass table's PK is also its FK to the
--     parent table (reuses the parent's PK column name/value, not a new
--     surrogate) -- e.g. Supplier.business_partner_id, not supplier_id.
--   * Every "<x>Id:String" attribute in the class diagram is the analysis-
--     level placeholder for that table's surrogate INT PK; it is not a
--     second, separate business-id column. A handful of attributes are
--     genuine externally-visible business identifiers, not PK placeholders
--     (tenderNumber, poNumber, invoiceNumber, licenseNumber) -- these ARE
--     real columns, separate from the surrogate INT PK.
--   * All FKs use ON DELETE NO ACTION ON UPDATE NO ACTION.
--
-- The two previously-flagged gaps (PurchaseOrder -> Project, FinancialSecurity
-- -> Project) are resolved as of the updated class diagram: both now have a
-- direct FK to Project. FinancialSecurity (and its subclasses) moved from
-- Phase 1 to Phase 2 as a result, since it now depends on Project.
-- ============================================================================

-- ============================================================================
-- PHASE 1 -- Base entities (no FK to another domain entity)
-- ============================================================================

CREATE TABLE Client (
    client_id INT NOT NULL PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    contactPerson NVARCHAR(50) NOT NULL,
    phone NVARCHAR(50) NOT NULL,
    email NVARCHAR(50) NOT NULL,
    sector NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE TradeCategory (
    trade_category_id INT NOT NULL PRIMARY KEY,
    categoryName NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Employee (
    employee_id INT NOT NULL PRIMARY KEY,
    firstName NVARCHAR(50) NOT NULL,
    lastName NVARCHAR(50) NOT NULL,
    nationalId NVARCHAR(50) NOT NULL,
    role NVARCHAR(20) NOT NULL,
    dailyRate DECIMAL(10,2) NOT NULL,
    certificationNo NVARCHAR(50) NOT NULL, -- TODO: is a certification number required for every employee/role, or only some (e.g. EquipmentManager)? Diagram doesn't say.
    status NVARCHAR(20) NOT NULL,
    -- role: widened from 3 field-workforce values to all 8 system-actor roles, per
    -- the updated class diagram merging EmployeeRole with the system-access role list.
    CONSTRAINT CK_Employee_Role CHECK (role IN (N'SiteSupervisor', N'EquipmentManager', N'SafetyManager', N'ProjectManager', N'Accountant', N'FinanceOfficer', N'CEO', N'TenderCoordinator')),
    CONSTRAINT CK_Employee_Status CHECK (status IN (N'Active', N'OnVacation', N'Suspended', N'Terminated'))
);
GO

CREATE TABLE Equipment (
    equipment_id INT NOT NULL PRIMARY KEY,
    licenseNumber NVARCHAR(50) NOT NULL, -- genuine business identifier (vehicle/machine licence plate), not a PK placeholder
    equipmentType NVARCHAR(50) NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    dailyCost DECIMAL(10,2) NOT NULL,
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Equipment_Status CHECK (status IN (N'Available', N'InUse', N'UnderRepair'))
);
GO

-- BusinessPartner: abstract base for Supplier / Subcontractor (table-per-subclass).
CREATE TABLE BusinessPartner (
    business_partner_id INT NOT NULL PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    companyRegistrationNo NVARCHAR(50) NOT NULL,
    contactPerson NVARCHAR(50) NOT NULL,
    phone NVARCHAR(50) NOT NULL,
    email NVARCHAR(50) NOT NULL,
    rating FLOAT NOT NULL, -- TODO: should rating be nullable for a newly onboarded partner who hasn't been evaluated yet?
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_BusinessPartner_Status CHECK (status IN (N'Active', N'Inactive'))
);
GO

-- Supplier extends BusinessPartner: no unique attributes of its own in the diagram.
CREATE TABLE Supplier (
    business_partner_id INT NOT NULL PRIMARY KEY,
    CONSTRAINT FK_Supplier_BusinessPartner FOREIGN KEY (business_partner_id)
        REFERENCES BusinessPartner(business_partner_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Subcontractor extends BusinessPartner.
CREATE TABLE Subcontractor (
    business_partner_id INT NOT NULL PRIMARY KEY,
    tradeSpecialty NVARCHAR(50) NOT NULL,
    dailyRate DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Subcontractor_BusinessPartner FOREIGN KEY (business_partner_id)
        REFERENCES BusinessPartner(business_partner_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- DailyWorkLog.status uses the WorkLogStatus enumeration (Submitted, UnderReview),
-- per the updated class diagram (previously a deliberate free String).
CREATE TABLE DailyWorkLog (
    daily_work_log_id INT NOT NULL PRIMARY KEY,
    subcontractor_id INT NULL, -- nullable: relationship #16 is optional (0..1) -- a log can belong entirely to the internal crew with no subcontractor involved
    submitted_by_employee_id INT NOT NULL, -- role "submittedBy": the site supervisor who filed the log
    logDate DATETIME2 NOT NULL,
    plannedQuantity FLOAT NOT NULL,
    completedQuantity FLOAT NOT NULL,
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_DailyWorkLog_Status CHECK (status IN (N'Submitted', N'UnderReview')),
    CONSTRAINT FK_DailyWorkLog_Subcontractor FOREIGN KEY (subcontractor_id)
        REFERENCES Subcontractor(business_partner_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_DailyWorkLog_SubmittedBy FOREIGN KEY (submitted_by_employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- ============================================================================
-- PHASE 2 -- Entities with FK references
-- ============================================================================

CREATE TABLE Tender (
    tender_id INT NOT NULL PRIMARY KEY,
    tenderNumber NVARCHAR(50) NOT NULL, -- genuine business identifier (the MOD tender number), not a PK placeholder
    client_id INT NOT NULL,
    title NVARCHAR(50) NOT NULL,
    estimatedValue DECIMAL(10,2) NOT NULL,
    submissionDeadline DATETIME2 NOT NULL,
    publishedDate DATETIME2 NOT NULL,
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Tender_Status CHECK (status IN (N'Searching', N'InPreparation', N'Submitted', N'Won', N'Lost')),
    CONSTRAINT FK_Tender_Client FOREIGN KEY (client_id)
        REFERENCES Client(client_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

CREATE TABLE Project (
    project_id INT NOT NULL PRIMARY KEY,
    -- UNIQUE: relationship #2 is "Tender 1 -- 0..1 Project" (becomes) -- at most one Project per Tender.
    tender_id INT NOT NULL UNIQUE,
    project_manager_employee_id INT NOT NULL, -- role "projectManager"
    name NVARCHAR(50) NOT NULL,
    address NVARCHAR(50) NOT NULL,
    plannedStartDate DATETIME2 NOT NULL,
    plannedEndDate DATETIME2 NOT NULL,
    actualStartDate DATETIME2 NULL, -- nullable: unknown until the project actually starts
    actualEndDate DATETIME2 NULL,   -- nullable: unknown until the project actually finishes
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_Project_Status CHECK (status IN (N'InProgress', N'Completed', N'OnHold', N'Cancelled')),
    CONSTRAINT FK_Project_Tender FOREIGN KEY (tender_id)
        REFERENCES Tender(tender_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_Project_ProjectManager FOREIGN KEY (project_manager_employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- FinancialSecurity: abstract base for BankGuarantee / InsurancePolicy (table-per-subclass).
-- Moved here (Phase 2) because it now has a required FK to Project.
CREATE TABLE FinancialSecurity (
    financial_security_id INT NOT NULL PRIMARY KEY,
    project_id INT NOT NULL, -- resolves the previously-flagged gap: securities are managed per project (UC-06)
    amount DECIMAL(10,2) NOT NULL,
    issueDate DATETIME2 NOT NULL,
    expiryDate DATETIME2 NOT NULL,
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_FinancialSecurity_Status CHECK (status IN (N'Active', N'Expired', N'Released')),
    CONSTRAINT FK_FinancialSecurity_Project FOREIGN KEY (project_id)
        REFERENCES Project(project_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- BankGuarantee extends FinancialSecurity.
CREATE TABLE BankGuarantee (
    financial_security_id INT NOT NULL PRIMARY KEY,
    bankName NVARCHAR(50) NOT NULL,
    guaranteeNumber NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_BankGuarantee_FinancialSecurity FOREIGN KEY (financial_security_id)
        REFERENCES FinancialSecurity(financial_security_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- InsurancePolicy extends FinancialSecurity.
CREATE TABLE InsurancePolicy (
    financial_security_id INT NOT NULL PRIMARY KEY,
    insurerName NVARCHAR(50) NOT NULL,
    policyNumber NVARCHAR(50) NOT NULL,
    coverageType NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_InsurancePolicy_FinancialSecurity FOREIGN KEY (financial_security_id)
        REFERENCES FinancialSecurity(financial_security_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

CREATE TABLE BudgetLine (
    budget_line_id INT NOT NULL PRIMARY KEY,
    project_id INT NOT NULL,
    category NVARCHAR(50) NOT NULL,
    plannedAmount DECIMAL(10,2) NOT NULL,
    actualAmount DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_BudgetLine_Project FOREIGN KEY (project_id)
        REFERENCES Project(project_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

CREATE TABLE PaymentRequest (
    payment_request_id INT NOT NULL PRIMARY KEY,
    project_id INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    submissionDate DATETIME2 NOT NULL,
    approvalDate DATETIME2 NULL, -- nullable: not yet set while status is Submitted/UnderReview/Rejected
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_PaymentRequest_Status CHECK (status IN (N'Submitted', N'UnderReview', N'Approved', N'Rejected', N'Paid')),
    CONSTRAINT FK_PaymentRequest_Project FOREIGN KEY (project_id)
        REFERENCES Project(project_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- SubmittedDocument: composition child of PaymentRequest, replacing the former
-- free-text missingDocuments column (getMissingDocuments() now derives the gap
-- from which DocumentType values are missing among a request's documents).
CREATE TABLE SubmittedDocument (
    submitted_document_id INT NOT NULL PRIMARY KEY,
    payment_request_id INT NOT NULL,
    type NVARCHAR(20) NOT NULL,
    receivedOn DATETIME2 NOT NULL,
    CONSTRAINT CK_SubmittedDocument_Type CHECK (type IN (N'Invoice', N'QuantityStatement', N'SupervisorApproval', N'SiteDiaryExtract', N'InsuranceCertificate')),
    CONSTRAINT FK_SubmittedDocument_PaymentRequest FOREIGN KEY (payment_request_id)
        REFERENCES PaymentRequest(payment_request_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- status: revised from the original 5-value POStatus to the 11-value set
-- introduced by docs/design/state-diagram.md (PurchaseOrder lifecycle) --
-- see CLAUDE.md's Enumerations table for the full rationale.
CREATE TABLE PurchaseOrder (
    purchase_order_id INT NOT NULL PRIMARY KEY,
    poNumber NVARCHAR(50) NOT NULL, -- genuine business identifier (printed on the PO sent to the supplier), not a PK placeholder
    supplier_id INT NOT NULL, -- references Supplier(business_partner_id)
    project_id INT NOT NULL, -- resolves the previously-flagged gap: UC-03 has the Accountant select an active Project
    created_by_employee_id INT NOT NULL, -- role "createdBy" {role = Accountant}
    approved_by_employee_id INT NULL, -- role "approvedBy" {role = ProjectManager}; nullable until PM approval happens
    override_approved_by_employee_id INT NULL, -- role "overrideApprovedBy" {role = CEO}; only set when BR-2 required a budget override
    orderDate DATETIME2 NOT NULL,
    totalAmount DECIMAL(10,2) NOT NULL,
    vatAmount DECIMAL(10,2) NOT NULL,
    status NVARCHAR(30) NOT NULL, -- widened from the default NVARCHAR(20): 'PendingBudgetOverride' is 21 characters
    rejectionReason NVARCHAR(MAX) NULL, -- nullable: only set when status is/was Rejected (recordRejection(reason))
    closureReason NVARCHAR(20) NULL, -- nullable: only set on entry to Received/Cancelled, preserved through Archived
    rejectedAt DATETIME2 NULL, -- nullable: set on entry to Rejected, cleared on revise() back to Draft -- persists the BR-3 14-day auto-cancel guard across app restarts
    archivedAt DATETIME2 NULL, -- nullable: set on entry to Archived -- persists the 7-year purge() retention guard across app restarts
    everSubmitted BIT NOT NULL, -- BR-1 guard: has this order ever left Draft? False only for an order created via button_save (always Draft); true from creation for one created via the quick-create flow (sp_purchase_order_create_flow, which inserts straight into a Pending* status, never Draft) or the moment submit() runs -- persists the guard cancel() uses to decide delete-vs-Cancel across app restarts
    rejected_by_employee_id INT NULL, -- nullable: role "rejectedBy", set on entry to Rejected, cleared on revise() back to Draft -- whichever role actually rejected (ProjectManager from PendingPMApproval, CEO from PendingBudgetOverride); two distinct transitions into Rejected per design/state-diagram.html, previously indistinguishable from this column alone
    CONSTRAINT CK_PurchaseOrder_Status CHECK (status IN (N'Draft', N'UnderApproval', N'PendingPMApproval', N'PendingBudgetOverride', N'Rejected', N'InFulfillment', N'Sent', N'PartiallyReceived', N'Received', N'Cancelled', N'Archived')),
    CONSTRAINT CK_PurchaseOrder_ClosureReason CHECK (closureReason IS NULL OR closureReason IN (N'Received', N'Cancelled')),
    CONSTRAINT FK_PurchaseOrder_Supplier FOREIGN KEY (supplier_id)
        REFERENCES Supplier(business_partner_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PurchaseOrder_Project FOREIGN KEY (project_id)
        REFERENCES Project(project_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PurchaseOrder_CreatedBy FOREIGN KEY (created_by_employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PurchaseOrder_ApprovedBy FOREIGN KEY (approved_by_employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PurchaseOrder_OverrideApprovedBy FOREIGN KEY (override_approved_by_employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_PurchaseOrder_RejectedBy FOREIGN KEY (rejected_by_employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

CREATE TABLE PurchaseOrderLine (
    purchase_order_line_id INT NOT NULL PRIMARY KEY,
    purchase_order_id INT NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    unitOfMeasure NVARCHAR(50) NOT NULL,
    quantity FLOAT NOT NULL,
    unitPrice DECIMAL(10,2) NOT NULL,
    receivedQuantity FLOAT NOT NULL, -- added per state-diagram BR-5 (recordDelivery()); 0 until the first delivery is recorded
    CONSTRAINT FK_PurchaseOrderLine_PurchaseOrder FOREIGN KEY (purchase_order_id)
        REFERENCES PurchaseOrder(purchase_order_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

CREATE TABLE SupplierPayment (
    supplier_payment_id INT NOT NULL PRIMARY KEY,
    invoiceNumber NVARCHAR(50) NOT NULL, -- genuine business identifier (the supplier invoice number), not a PK placeholder
    -- References the superclass BusinessPartner, not Supplier/Subcontractor directly
    -- (docs/design/class-diagram.md Section 5 model assumption).
    business_partner_id INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    dueDate DATETIME2 NOT NULL,
    paidDate DATETIME2 NULL, -- nullable: not yet set while status is Pending/Overdue
    status NVARCHAR(20) NOT NULL,
    CONSTRAINT CK_SupplierPayment_Status CHECK (status IN (N'Pending', N'Paid', N'Overdue')),
    CONSTRAINT FK_SupplierPayment_BusinessPartner FOREIGN KEY (business_partner_id)
        REFERENCES BusinessPartner(business_partner_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- ============================================================================
-- PHASE 3 -- Association / link classes (loaded last: every side must already exist)
-- ============================================================================

-- Ternary association class: Supplier + Tender + TradeCategory.
CREATE TABLE SupplierPriceQuote (
    supplier_price_quote_id INT NOT NULL PRIMARY KEY,
    supplier_id INT NOT NULL, -- references Supplier(business_partner_id)
    tender_id INT NOT NULL,
    trade_category_id INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    dateIssued DATETIME2 NOT NULL,
    validUntil DATETIME2 NOT NULL,
    isSelected BIT NOT NULL,
    CONSTRAINT FK_SupplierPriceQuote_Supplier FOREIGN KEY (supplier_id)
        REFERENCES Supplier(business_partner_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_SupplierPriceQuote_Tender FOREIGN KEY (tender_id)
        REFERENCES Tender(tender_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_SupplierPriceQuote_TradeCategory FOREIGN KEY (trade_category_id)
        REFERENCES TradeCategory(trade_category_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Association class linking Employee <-> DailyWorkLog.
-- hoursWorked is no longer stored: it's derived from startTime/endTime (hoursWorked()).
CREATE TABLE Attendance (
    attendance_id INT NOT NULL PRIMARY KEY,
    employee_id INT NOT NULL,
    daily_work_log_id INT NOT NULL,
    startTime TIME NOT NULL,
    endTime TIME NOT NULL,
    taskDescription NVARCHAR(MAX) NOT NULL,
    CONSTRAINT CK_Attendance_Times CHECK (endTime > startTime),
    CONSTRAINT FK_Attendance_Employee FOREIGN KEY (employee_id)
        REFERENCES Employee(employee_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_Attendance_DailyWorkLog FOREIGN KEY (daily_work_log_id)
        REFERENCES DailyWorkLog(daily_work_log_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Association class linking Equipment <-> DailyWorkLog.
CREATE TABLE EquipmentUsage (
    equipment_usage_id INT NOT NULL PRIMARY KEY,
    equipment_id INT NOT NULL,
    daily_work_log_id INT NOT NULL,
    hoursOperated FLOAT NOT NULL,
    CONSTRAINT FK_EquipmentUsage_Equipment FOREIGN KEY (equipment_id)
        REFERENCES Equipment(equipment_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_EquipmentUsage_DailyWorkLog FOREIGN KEY (daily_work_log_id)
        REFERENCES DailyWorkLog(daily_work_log_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

-- Link class linking Project <-> Equipment (aggregation).
CREATE TABLE EquipmentAssignment (
    equipment_assignment_id INT NOT NULL PRIMARY KEY,
    project_id INT NOT NULL,
    equipment_id INT NOT NULL,
    startDate DATETIME2 NOT NULL,
    endDate DATETIME2 NULL, -- nullable: an active/ongoing assignment (isActive()) has no end date yet
    CONSTRAINT FK_EquipmentAssignment_Project FOREIGN KEY (project_id)
        REFERENCES Project(project_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_EquipmentAssignment_Equipment FOREIGN KEY (equipment_id)
        REFERENCES Equipment(equipment_id) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

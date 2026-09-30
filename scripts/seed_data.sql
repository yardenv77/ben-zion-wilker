-- ============================================================================
-- seed_data.sql
-- Ben Tzion Wilker (1987) Ltd. Project Management System
--
-- Realistic test/seed data for every table in scripts/create_database.sql,
-- inserted in the same load order documented in docs/CLAUDE.md ("Domain
-- Entities and Load Order"): Phase 1 base entities, Phase 2 FK entities,
-- Phase 3 association/link classes. Run against the database configured
-- for the mssql MCP (see CLAUDE.md's "Database" section).
--
-- Notes:
--   * All primary keys are explicit INTs (see CLAUDE.md "Primary Key
--     Strategy" -- no IDENTITY columns), numbered 1..N per table so FK
--     references below are easy to follow.
--   * Every enumeration value from CLAUDE.md's "Enumerations" table is
--     represented at least once across the relevant table, EXCEPT
--     POStatus's UnderApproval/InFulfillment: those are composite/superstate
--     values that are always immediately overwritten by an inner state's
--     entry action (docs/design/state-diagram.md), so they are never
--     actually persisted -- only the 9 leaf states appear here.
--   * No User/Login table exists in this schema -- per CLAUDE.md, "Login
--     is not a UC" and authentication is an NFR precondition, not a
--     domain class, so there are no passwords to seed here.
--   * This is test data for development/review, not production data.
-- ============================================================================

-- ============================================================================
-- PHASE 1 -- Base entities
-- ============================================================================

-- Client (6)
INSERT INTO Client (client_id, name, contactPerson, phone, email, sector) VALUES
(1, N'משרד הביטחון – אגף התשתיות', N'אלון פרידמן', N'03-6975432', N'alon.friedman@mod.gov.il', N'ציבורי - ביטחון'),
(2, N'רפאל מערכות לחימה מתקדמות בע"מ', N'מירב כהן-שגיא', N'04-8791234', N'merav.cohen@rafael.co.il', N'תעשייה ביטחונית'),
(3, N'רכבת ישראל בע"מ', N'יובל אשכנזי', N'03-6117890', N'yuval.ashkenazi@rail.co.il', N'תשתיות תחבורה'),
(4, N'עיריית תל אביב-יפו – אגף הנדסה', N'דנה לוי', N'03-5218765', N'dana.levy@tel-aviv.gov.il', N'ציבורי - רשות מקומית'),
(5, N'חברת נמלי ישראל בע"מ', N'רועי ברק', N'04-8351122', N'roi.barak@israports.co.il', N'תשתיות ימיות'),
(6, N'משרד הבינוי והשיכון', N'שירה מזרחי', N'02-5601987', N'shira.mizrahi@moch.gov.il', N'ציבורי - ממשלתי');
GO

-- TradeCategory (8)
INSERT INTO TradeCategory (trade_category_id, categoryName) VALUES
(1, N'עבודות עפר'),
(2, N'יציקות בטון'),
(3, N'עבודות חשמל'),
(4, N'עבודות אינסטלציה'),
(5, N'איטום וגגות'),
(6, N'עבודות אלומיניום וזיגוג'),
(7, N'עבודות טיח וגבס'),
(8, N'כבישים וריצוף');
GO

-- Employee (14) -- covers all EmployeeRole (8, incl. system-actor roles merged in per the
-- updated class diagram) and EmployeeStatus values. Rows 1-8 are field workforce
-- (unchanged); rows 9-14 are the office/management roles now needed as FK targets
-- for Project.projectManager, PurchaseOrder.createdBy/approvedBy/overrideApprovedBy
-- and DailyWorkLog.submittedBy.
INSERT INTO Employee (employee_id, firstName, lastName, nationalId, role, dailyRate, certificationNo, status) VALUES
(1, N'יוסי', N'אברהמי', N'021456789', N'SiteSupervisor', 850.00, N'CERT-45210', N'Active'),
(2, N'רונית', N'שמעוני', N'025874123', N'SiteSupervisor', 820.00, N'CERT-45298', N'Active'),
(3, N'דוד', N'מלכה', N'031258796', N'SiteSupervisor', 800.00, N'CERT-45311', N'OnVacation'),
(4, N'משה', N'בן-חמו', N'028745123', N'EquipmentManager', 780.00, N'CERT-33107', N'Active'),
(5, N'אורית', N'גבאי', N'033698521', N'EquipmentManager', 760.00, N'CERT-33189', N'Suspended'),
(6, N'אבי', N'טל', N'029631478', N'SafetyManager', 900.00, N'CERT-77452', N'Active'),
(7, N'נעמה', N'שרון', N'026985471', N'SafetyManager', 880.00, N'CERT-77519', N'Terminated'),
(8, N'איתן', N'ורדי', N'034127896', N'SiteSupervisor', 830.00, N'CERT-45367', N'Active'),
(9, N'אלי', N'רזניק', N'041236547', N'ProjectManager', 1100.00, N'CERT-90001', N'Active'),
(10, N'מיכל', N'אברמוב', N'042369871', N'ProjectManager', 1080.00, N'CERT-90002', N'Active'),
(11, N'רותם', N'סגל', N'043698521', N'Accountant', 950.00, N'CERT-90003', N'Active'),
(12, N'יעל', N'פרידמן', N'044785213', N'FinanceOfficer', 970.00, N'CERT-90004', N'Active'),
(13, N'בנצי', N'וילקר', N'045896321', N'CEO', 1500.00, N'CERT-90005', N'Active'),
(14, N'קרן', N'אדרי', N'046987412', N'TenderCoordinator', 920.00, N'CERT-90006', N'Active');
GO

-- Equipment (7) -- covers all EquipmentStatus values; licenseNumber is a genuine
-- business identifier (vehicle/machine licence plate), not a PK placeholder.
INSERT INTO Equipment (equipment_id, licenseNumber, equipmentType, description, dailyCost, status) VALUES
(1, N'12-345-67', N'מחפרון', N'מחפרון גלגלים JCB 3CX, שנת 2019', 950.00, N'Available'),
(2, N'23-456-78', N'דחפור', N'דחפור זחלים קטרפילר D6, שנת 2018', 1400.00, N'InUse'),
(3, N'34-567-89', N'מנוף', N'מנוף צריח ליברהר, גובה הרמה 40 מטר', 2200.00, N'InUse'),
(4, N'45-678-90', N'משאבת בטון', N'משאבת בטון נגררת פוצמייסטר', 1100.00, N'Available'),
(5, N'56-789-01', N'מלגזה', N'מלגזה טלסקופית מנג'' MT1440', 600.00, N'UnderRepair'),
(6, N'67-890-12', N'גנרטור', N'גנרטור דיזל ניידת 200KVA', 350.00, N'Available'),
(7, N'78-901-23', N'מכבש כביש', N'מכבש כביש ויברציוני בומאג', 700.00, N'UnderRepair');
GO

-- BusinessPartner (8) -- covers all PartnerStatus values; rows 1-4 become Suppliers, 5-8 become Subcontractors
INSERT INTO BusinessPartner (business_partner_id, name, companyRegistrationNo, contactPerson, phone, email, rating, status) VALUES
(1, N'בטונדע תעשיות בטון בע"מ', N'512345671', N'יעקב שטרן', N'08-9451236', N'orders@betondaa.co.il', 4.5, N'Active'),
(2, N'פלדות הצפון בע"מ', N'512345682', N'רמי אוחיון', N'04-6237891', N'sales@pladot-hatzafon.co.il', 4.2, N'Active'),
(3, N'חומרי בניין כרמל בע"מ', N'512345693', N'שלמה בכר', N'04-8124567', N'info@carmel-bp.co.il', 3.1, N'Inactive'),
(4, N'אלקטרו-סחר ציוד חשמל בע"מ', N'512345704', N'טליה נחום', N'03-5687412', N'sales@electro-sahar.co.il', 4.0, N'Active'),
(5, N'א.ב. עבודות עפר בע"מ', N'512345715', N'אריה בוזגלו', N'052-3451678', N'ab.earthworks@gmail.com', 4.3, N'Active'),
(6, N'חשמל ניר קבלני חשמל בע"מ', N'512345726', N'ניר אסולין', N'054-7896321', N'nir.elec@gmail.com', 4.6, N'Active'),
(7, N'איטום השרון בע"מ', N'512345737', N'בני כהן', N'09-8912345', N'benny@itum-hasharon.co.il', 2.8, N'Inactive'),
(8, N'גבס-פרו קבלני גבס וטיח', N'512345748', N'מוטי פרץ', N'050-6231478', N'moti.gypsum@gmail.com', 4.1, N'Active');
GO

-- Supplier (4) -- table-per-subclass: PK only, no unique attributes
INSERT INTO Supplier (business_partner_id) VALUES
(1), (2), (3), (4);
GO

-- Subcontractor (4)
INSERT INTO Subcontractor (business_partner_id, tradeSpecialty, dailyRate) VALUES
(5, N'עבודות עפר', 3200.00),
(6, N'עבודות חשמל', 2800.00),
(7, N'איטום וגגות', 2600.00),
(8, N'עבודות טיח וגבס', 2400.00);
GO

-- DailyWorkLog (12) -- subcontractor_id is nullable (relationship #16 is optional);
-- status uses the WorkLogStatus enumeration (Submitted, UnderReview); submitted_by_employee_id
-- is the site supervisor (role "submittedBy") who filed the log.
INSERT INTO DailyWorkLog (daily_work_log_id, subcontractor_id, submitted_by_employee_id, logDate, plannedQuantity, completedQuantity, status) VALUES
(1, 5, 1, '2026-01-12', 120, 115, N'Submitted'),
(2, NULL, 1, '2026-01-13', 80, 80, N'Submitted'),
(3, 6, 2, '2026-01-14', 60, 40, N'UnderReview'),
(4, NULL, 8, '2026-01-15', 100, 95, N'Submitted'),
(5, 8, 1, '2026-02-02', 50, 30, N'UnderReview'),
(6, NULL, 2, '2026-02-03', 70, 72, N'Submitted'),
(7, 5, 8, '2026-02-04', 110, 108, N'Submitted'),
(8, NULL, 1, '2026-02-05', 90, 85, N'Submitted'),
(9, 6, 2, '2026-03-01', 65, 20, N'UnderReview'),
(10, NULL, 3, '2026-03-02', 75, 75, N'Submitted'),
(11, 8, 8, '2026-03-03', 55, 50, N'UnderReview'),
(12, NULL, 1, '2026-03-04', 95, 93, N'Submitted');
GO

-- ============================================================================
-- PHASE 2 -- Entities with FK references
-- ============================================================================

-- Tender (9) -- covers all TenderStatus values; tenderNumber is a genuine business
-- identifier (the MOD/client-issued tender number); client_id references Client
INSERT INTO Tender (tender_id, tenderNumber, client_id, title, estimatedValue, submissionDeadline, publishedDate, status) VALUES
(1, N'MOD-2025-0341', 1, N'הקמת מחסן לוגיסטי – בסיס צריפין', 4200000.00, '2025-08-01', '2025-05-01', N'Won'),
(2, N'RAF-2025-0118', 2, N'שיפוץ מבנה מנהלה – רפאל חיפה', 1850000.00, '2025-07-15', '2025-04-10', N'Won'),
(3, N'RAIL-2025-0552', 3, N'הרחבת מסילת רכבת – קטע לוד-רמלה', 9600000.00, '2025-09-01', '2025-05-20', N'Won'),
(4, N'TLV-2025-0089', 4, N'שיקום כבישים – שכונת פלורנטין', 2750000.00, '2025-06-10', '2025-03-01', N'Won'),
(5, N'PORT-2025-0203', 5, N'בניית מזח נמל – הרחבת שטח מזרחי', 15300000.00, '2025-10-01', '2025-06-15', N'Won'),
(6, N'MOCH-2026-0071', 6, N'שיפוץ מבני ציבור – פרויקט דיור ציבורי', 3100000.00, '2026-11-01', '2026-08-01', N'InPreparation'),
(7, N'MOD-2026-0412', 1, N'הקמת גדר היקפית – מתקן ביטחוני דרום', 2200000.00, '2026-12-15', '2026-09-01', N'Searching'),
(8, N'TLV-2026-0155', 4, N'תשתיות ניקוז – אזור תעשייה חדש', 1650000.00, '2026-10-20', '2026-08-20', N'Submitted'),
(9, N'RAF-2024-0299', 2, N'הרחבת מבנה מחסנים – רפאל', 980000.00, '2025-02-01', '2024-11-01', N'Lost');
GO

-- Project (5) -- tender_id is UNIQUE (Tender 1 -- 0..1 Project); covers all ProjectStatus
-- values; project_manager_employee_id is the role "projectManager" (Employee 1--0..*).
INSERT INTO Project (project_id, tender_id, project_manager_employee_id, name, address, plannedStartDate, plannedEndDate, actualStartDate, actualEndDate, status) VALUES
(1, 1, 9, N'מחסן לוגיסטי – בסיס צריפין', N'בסיס צריפין, ראשל"צ', '2025-09-01', '2026-06-30', '2025-09-10', NULL, N'InProgress'),
(2, 2, 10, N'שיפוץ מנהלה – רפאל חיפה', N'מפעל רפאל, חיפה', '2025-08-01', '2026-01-31', '2025-08-05', '2026-02-10', N'Completed'),
(3, 3, 9, N'הרחבת מסילה – לוד-רמלה', N'מסילת רכבת, קטע לוד-רמלה', '2025-10-01', '2027-03-31', '2025-10-15', NULL, N'InProgress'),
(4, 4, 10, N'שיקום כבישים – פלורנטין', N'שכונת פלורנטין, תל אביב', '2025-07-01', '2026-02-28', '2025-07-10', NULL, N'OnHold'),
(5, 5, 9, N'מזח נמל – הרחבת שטח מזרחי', N'נמל אשדוד', '2025-11-01', '2027-06-30', NULL, NULL, N'Cancelled');
GO

-- FinancialSecurity (6) -- covers all SecurityStatus values; project_id resolves the
-- previously-flagged gap (securities are managed per project, UC-06); rows 1-3 become
-- BankGuarantees, 4-6 InsurancePolicies.
INSERT INTO FinancialSecurity (financial_security_id, project_id, amount, issueDate, expiryDate, status) VALUES
(1, 1, 250000.00, '2025-06-01', '2026-12-31', N'Active'),
(2, 2, 180000.00, '2023-01-15', '2025-01-15', N'Expired'),
(3, 3, 90000.00, '2024-03-01', '2025-03-01', N'Released'),
(4, 1, 500000.00, '2025-09-01', '2026-09-01', N'Active'),
(5, 4, 320000.00, '2023-05-01', '2024-05-01', N'Expired'),
(6, 5, 150000.00, '2024-01-01', '2025-01-01', N'Released');
GO

-- BankGuarantee (3)
INSERT INTO BankGuarantee (financial_security_id, bankName, guaranteeNumber) VALUES
(1, N'בנק הפועלים', N'GUA-2025-0112'),
(2, N'בנק לאומי', N'GUA-2023-0087'),
(3, N'בנק דיסקונט', N'GUA-2024-0033');
GO

-- InsurancePolicy (3)
INSERT INTO InsurancePolicy (financial_security_id, insurerName, policyNumber, coverageType) VALUES
(4, N'הפניקס חברה לביטוח', N'POL-778812', N'ביטוח עבודות קבלניות'),
(5, N'כלל ביטוח', N'POL-445521', N'ביטוח צד ג׳'),
(6, N'הראל ביטוח', N'POL-990087', N'ביטוח אחריות מקצועית');
GO

-- BudgetLine (10)
INSERT INTO BudgetLine (budget_line_id, project_id, category, plannedAmount, actualAmount) VALUES
(1, 1, N'עבודות עפר', 800000.00, 750000.00),
(2, 1, N'חומרי גלם', 1200000.00, 1260000.00),
(3, 2, N'כוח אדם', 400000.00, 395000.00),
(4, 2, N'קבלני משנה', 650000.00, 680000.00),
(5, 3, N'ציוד מכני', 1500000.00, 1420000.00),
(6, 3, N'עבודות עפר', 2000000.00, 2150000.00),
(7, 4, N'כבישים וריצוף', 900000.00, 910000.00),
(8, 4, N'כוח אדם', 350000.00, 340000.00),
(9, 5, N'חומרי גלם', 3000000.00, 500000.00),
(10, 5, N'קבלני משנה', 1800000.00, 200000.00);
GO

-- PaymentRequest (10) -- covers all PaymentRequestStatus values. missingDocuments is
-- no longer a column here: which documents are missing is now derived from
-- SubmittedDocument (getMissingDocuments()), seeded below.
INSERT INTO PaymentRequest (payment_request_id, project_id, amount, submissionDate, approvalDate, status) VALUES
(1, 1, 600000.00, '2026-02-01', '2026-02-15', N'Approved'),
(2, 1, 450000.00, '2026-05-01', NULL, N'UnderReview'),
(3, 2, 380000.00, '2025-11-01', '2025-11-20', N'Paid'),
(4, 2, 270000.00, '2025-12-15', '2026-01-05', N'Rejected'),
(5, 3, 1200000.00, '2026-01-10', '2026-01-25', N'Approved'),
(6, 3, 900000.00, '2026-04-01', NULL, N'Submitted'),
(7, 4, 310000.00, '2025-10-05', NULL, N'Rejected'),
(8, 4, 200000.00, '2026-01-20', '2026-02-01', N'Paid'),
(9, 5, 500000.00, '2025-12-01', NULL, N'UnderReview'),
(10, 1, 150000.00, '2026-06-01', NULL, N'Submitted');
GO

-- SubmittedDocument (22) -- composition child of PaymentRequest; request 4 is missing
-- SupervisorApproval and request 7 is missing QuantityStatement, matching why they
-- were rejected; covers all DocumentType values.
INSERT INTO SubmittedDocument (submitted_document_id, payment_request_id, type, receivedOn) VALUES
(1, 1, N'Invoice', '2026-01-28'),
(2, 1, N'QuantityStatement', '2026-01-29'),
(3, 1, N'SupervisorApproval', '2026-01-30'),
(4, 1, N'SiteDiaryExtract', '2026-01-30'),
(5, 2, N'Invoice', '2026-04-25'),
(6, 2, N'QuantityStatement', '2026-04-27'),
(7, 3, N'Invoice', '2025-10-25'),
(8, 3, N'QuantityStatement', '2025-10-27'),
(9, 3, N'SupervisorApproval', '2025-10-29'),
(10, 4, N'Invoice', '2025-12-10'),
(11, 4, N'QuantityStatement', '2025-12-11'),
(12, 5, N'Invoice', '2026-01-05'),
(13, 5, N'QuantityStatement', '2026-01-06'),
(14, 5, N'SupervisorApproval', '2026-01-07'),
(15, 5, N'InsuranceCertificate', '2026-01-07'),
(16, 6, N'Invoice', '2026-03-28'),
(17, 7, N'Invoice', '2025-09-30'),
(18, 7, N'SupervisorApproval', '2025-10-02'),
(19, 8, N'Invoice', '2026-01-15'),
(20, 8, N'QuantityStatement', '2026-01-16'),
(21, 8, N'SupervisorApproval', '2026-01-18'),
(22, 9, N'Invoice', '2025-11-25'),
(23, 9, N'QuantityStatement', '2025-11-27'),
(24, 10, N'Invoice', '2026-05-28');
GO

-- PurchaseOrder (10) -- poNumber is a genuine business identifier; project_id resolves
-- the previously-flagged gap; created_by/approved_by/override_approved_by are the
-- Employee-role associations from the updated class diagram. status covers all 9 leaf
-- POStatus values (UnderApproval/InFulfillment are transient superstate values and are
-- never actually persisted -- see the note at the top of this file).
INSERT INTO PurchaseOrder (purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason) VALUES
(1, N'PO-2025-0143', 1, 1, 11, 9, NULL, '2025-09-15', 180000.00, 27540.00, N'Sent', NULL, NULL),
(2, N'PO-2025-0156', 2, 2, 11, 10, NULL, '2025-10-01', 95000.00, 14535.00, N'Received', NULL, N'Received'),
(3, N'PO-2025-0161', 1, 1, 11, NULL, NULL, '2025-10-10', 220000.00, 33660.00, N'PendingPMApproval', NULL, NULL),
(4, N'PO-2025-0178', 4, 4, 11, 10, NULL, '2025-11-05', 64000.00, 9792.00, N'Sent', NULL, NULL),
(5, N'PO-2025-0122', 3, 3, 11, NULL, NULL, '2025-08-20', 45000.00, 6885.00, N'Rejected', N'המחיר חורג משמעותית מהצעת המחיר המקורית שאושרה', NULL),
(6, N'PO-2026-0033', 2, 2, 11, NULL, 13, '2026-01-05', 310000.00, 47430.00, N'PendingBudgetOverride', NULL, NULL),
(7, N'PO-2026-0058', 1, 1, 11, 9, 13, '2026-02-10', 128000.00, 19584.00, N'Archived', NULL, N'Received'),
(8, N'PO-2026-0071', 4, 4, 11, 10, NULL, '2026-02-20', 52000.00, 7956.00, N'PartiallyReceived', NULL, NULL),
(9, N'PO-2026-0084', 2, 2, 11, NULL, NULL, '2026-03-01', 175000.00, 26775.00, N'Draft', NULL, NULL),
(10, N'PO-2026-0097', 1, 1, 11, NULL, NULL, '2026-03-10', 99000.00, 15147.00, N'Cancelled', NULL, N'Cancelled');
GO

-- PurchaseOrderLine (16) -- PurchaseOrder 1 -- 1..* PurchaseOrderLine; receivedQuantity
-- reflects each order's status (0 until delivered, full for Received/Archived, partial
-- for PartiallyReceived, per BR-5).
INSERT INTO PurchaseOrderLine (purchase_order_line_id, purchase_order_id, description, unitOfMeasure, quantity, unitPrice, receivedQuantity) VALUES
(1, 1, N'מלט פורטלנד 50 ק"ג', N'שק', 2000, 32.50, 0),
(2, 1, N'ברזל זיון 12 מ"מ', N'טון', 15, 4200.00, 0),
(3, 2, N'פלדת בניין פרופיל IPE200', N'טון', 10, 5100.00, 10),
(4, 3, N'מלט פורטלנד 50 ק"ג', N'שק', 3500, 33.00, 0),
(5, 3, N'תוספי בטון אטימות', N'ליטר', 800, 18.50, 0),
(6, 4, N'לוח חשמל ראשי 400A', N'יחידה', 2, 12500.00, 0),
(7, 4, N'כבל חשמל NYY 3x2.5', N'מטר', 1200, 9.80, 0),
(8, 5, N'בלוקי בטון 20 ס"מ', N'יחידה', 5000, 6.40, 0),
(9, 6, N'ברזל זיון 16 מ"מ', N'טון', 25, 4350.00, 0),
(10, 6, N'מלט פורטלנד 50 ק"ג', N'שק', 4000, 32.80, 0),
(11, 7, N'אגרגט חצץ 0-25', N'טון', 600, 95.00, 600),
(12, 8, N'שקעים וממסרי חשמל', N'יחידה', 300, 45.00, 150),
(13, 8, N'כבל חשמל NYY 3x2.5', N'מטר', 900, 9.80, 450),
(14, 9, N'פלדת בניין פרופיל HEB160', N'טון', 18, 5300.00, 0),
(15, 10, N'מלט פורטלנד 50 ק"ג', N'שק', 2800, 33.20, 0),
(16, 10, N'ברזל זיון 10 מ"מ', N'טון', 8, 4150.00, 0);
GO

-- SupplierPayment (10) -- business_partner_id references the BusinessPartner superclass;
-- invoiceNumber is a genuine business identifier (the supplier invoice number); covers
-- all SupplierPaymentStatus values.
INSERT INTO SupplierPayment (supplier_payment_id, invoiceNumber, business_partner_id, amount, dueDate, paidDate, status) VALUES
(1, N'INV-4471', 1, 180000.00, '2025-10-15', '2025-10-14', N'Paid'),
(2, N'INV-4488', 2, 95000.00, '2025-11-01', NULL, N'Pending'),
(3, N'INV-4502', 5, 384000.00, '2026-02-01', NULL, N'Overdue'),
(4, N'INV-4519', 6, 280000.00, '2026-03-01', '2026-02-25', N'Paid'),
(5, N'INV-4527', 4, 64000.00, '2025-12-05', NULL, N'Overdue'),
(6, N'INV-4533', 3, 45000.00, '2025-09-20', '2025-09-19', N'Paid'),
(7, N'INV-4548', 8, 96000.00, '2026-04-01', NULL, N'Pending'),
(8, N'INV-4551', 2, 310000.00, '2026-04-15', NULL, N'Pending'),
(9, N'INV-4563', 7, 52000.00, '2025-08-10', NULL, N'Overdue'),
(10, N'INV-4579', 1, 128000.00, '2026-05-01', NULL, N'Pending');
GO

-- ============================================================================
-- PHASE 3 -- Association / link classes (loaded last)
-- ============================================================================

-- SupplierPriceQuote (12) -- ternary association class: Supplier + Tender + TradeCategory
INSERT INTO SupplierPriceQuote (supplier_price_quote_id, supplier_id, tender_id, trade_category_id, amount, dateIssued, validUntil, isSelected) VALUES
(1, 1, 1, 2, 210000.00, '2025-04-15', '2025-07-15', 1),
(2, 2, 1, 3, 185000.00, '2025-04-16', '2025-07-15', 1),
(3, 3, 1, 2, 225000.00, '2025-04-14', '2025-07-10', 0),
(4, 1, 2, 2, 140000.00, '2025-03-20', '2025-06-20', 1),
(5, 4, 2, 3, 88000.00, '2025-03-22', '2025-06-20', 1),
(6, 2, 3, 2, 980000.00, '2025-05-01', '2025-08-01', 1),
(7, 1, 3, 2, 1020000.00, '2025-05-02', '2025-08-01', 0),
(8, 4, 4, 3, 52000.00, '2025-02-10', '2025-05-10', 1),
(9, 3, 4, 8, 310000.00, '2025-02-12', '2025-05-10', 0),
(10, 2, 5, 2, 2200000.00, '2025-05-20', '2025-09-20', 0),
(11, 1, 6, 2, 310000.00, '2026-07-01', '2026-10-01', 0),
(12, 4, 8, 4, 64000.00, '2026-07-15', '2026-10-15', 0);
GO

-- Attendance (16) -- association class linking Employee <-> DailyWorkLog; hoursWorked
-- is no longer stored (it's derived from startTime/endTime), so every row starts its
-- shift at 07:00 and ends it after the same number of hours as the original data.
INSERT INTO Attendance (attendance_id, employee_id, daily_work_log_id, startTime, endTime, taskDescription) VALUES
(1, 1, 1, '07:00:00', '15:30:00', N'פיקוח על עבודות עפר וחפירה'),
(2, 1, 2, '07:00:00', '15:00:00', N'פיקוח יציקת יסודות'),
(3, 2, 3, '07:00:00', '14:30:00', N'פיקוח התקנת תשתית חשמל'),
(4, 8, 4, '07:00:00', '15:00:00', N'פיקוח כללי באתר'),
(5, 1, 5, '07:00:00', '15:00:00', N'פיקוח עבודות טיח'),
(6, 2, 6, '07:00:00', '13:30:00', N'בדיקת איכות יציקה'),
(7, 8, 7, '07:00:00', '15:30:00', N'פיקוח עבודות עפר'),
(8, 1, 8, '07:00:00', '15:00:00', N'פיקוח כללי'),
(9, 2, 9, '07:00:00', '11:00:00', N'עצירת עבודה עקב תקלת ציוד'),
(10, 3, 10, '07:00:00', '15:00:00', N'פיקוח באתר'),
(11, 8, 11, '07:00:00', '14:00:00', N'פיקוח עבודות טיח וגבס'),
(12, 1, 12, '07:00:00', '15:00:00', N'פיקוח סיום שלב'),
(13, 4, 1, '07:00:00', '15:00:00', N'ניהול ציוד מכני באתר'),
(14, 6, 3, '07:00:00', '10:00:00', N'בדיקת תקן בטיחות בעבודות חשמל'),
(15, 6, 9, '07:00:00', '09:30:00', N'חקירת אירוע בטיחות'),
(16, 4, 7, '07:00:00', '13:00:00', N'תיאום הפעלת ציוד כבד');
GO

-- EquipmentUsage (14) -- association class linking Equipment <-> DailyWorkLog
INSERT INTO EquipmentUsage (equipment_usage_id, equipment_id, daily_work_log_id, hoursOperated) VALUES
(1, 1, 1, 6.5),
(2, 2, 1, 7.0),
(3, 4, 2, 5.0),
(4, 3, 3, 4.5),
(5, 6, 4, 8.0),
(6, 1, 5, 5.5),
(7, 2, 6, 6.0),
(8, 3, 7, 7.5),
(9, 6, 8, 8.0),
(10, 5, 9, 1.5),
(11, 1, 10, 6.0),
(12, 4, 11, 4.0),
(13, 2, 12, 7.0),
(14, 7, 6, 3.5);
GO

-- EquipmentAssignment (10) -- link class between Project <-> Equipment (aggregation); endDate NULL = active assignment
INSERT INTO EquipmentAssignment (equipment_assignment_id, project_id, equipment_id, startDate, endDate) VALUES
(1, 1, 1, '2025-09-10', NULL),
(2, 1, 6, '2025-09-10', '2026-03-01'),
(3, 2, 4, '2025-08-05', '2026-01-15'),
(4, 3, 2, '2025-10-15', NULL),
(5, 3, 3, '2025-10-20', NULL),
(6, 4, 7, '2025-07-10', '2025-12-01'),
(7, 4, 5, '2025-09-01', NULL),
(8, 2, 6, '2025-08-01', '2025-08-04'),
(9, 5, 2, '2025-11-01', '2025-11-20'),
(10, 4, 1, '2026-01-05', NULL);
GO

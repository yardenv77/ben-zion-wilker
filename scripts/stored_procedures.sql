-- ============================================================================
-- stored_procedures.sql
-- Ben Tzion Wilker (1987) Ltd. Project Management System
--
-- Basic CRUD stored procedures for every table created by
-- scripts/create_database.sql. Mechanical only -- no business logic,
-- no report/aggregation procedures, no multi-table procedures, no
-- state-transition procedures. Run this AFTER create_database.sql.
--
-- Naming: sp_<entity>_create / _update / _delete / _get_all / _get_by_id,
-- where <entity> is the same snake_case token used in that table's PK
-- column name (e.g. Client -> client_id -> sp_client_*; BudgetLine ->
-- budget_line_id -> sp_budget_line_*). For the two table-per-subclass
-- pairs (Supplier/Subcontractor, BankGuarantee/InsurancePolicy), the
-- procedure name still follows the table name, but the PK parameter
-- name matches the actual (inherited) PK column -- business_partner_id
-- / financial_security_id, not a new surrogate -- per CLAUDE.md's
-- Inheritance (Table-per-Subclass) pattern.
--
-- CREATE PROCEDURE must be the only statement in its batch, so every
-- procedure below is separated by GO. GO is an SSMS/sqlcmd batch
-- separator, not T-SQL -- when running this through the mssql MCP,
-- split the file on GO and send each block as its own execute_sql call
-- (same constraint documented for create_database.sql).
--
-- No SCOPE_IDENTITY() anywhere: every _create procedure takes the
-- primary key as its first parameter, assigned in C# per CLAUDE.md's
-- Primary Key Strategy.
-- ============================================================================

-- ============================================================================
-- Client
-- ============================================================================

CREATE PROCEDURE sp_client_create
    @client_id INT,
    @name NVARCHAR(50),
    @contactPerson NVARCHAR(50),
    @phone NVARCHAR(50),
    @email NVARCHAR(50),
    @sector NVARCHAR(50)
AS
BEGIN
    INSERT INTO Client (client_id, name, contactPerson, phone, email, sector)
    VALUES (@client_id, @name, @contactPerson, @phone, @email, @sector);
END
GO

CREATE PROCEDURE sp_client_update
    @client_id INT,
    @name NVARCHAR(50),
    @contactPerson NVARCHAR(50),
    @phone NVARCHAR(50),
    @email NVARCHAR(50),
    @sector NVARCHAR(50)
AS
BEGIN
    UPDATE Client
    SET name = @name,
        contactPerson = @contactPerson,
        phone = @phone,
        email = @email,
        sector = @sector
    WHERE client_id = @client_id;
END
GO

CREATE PROCEDURE sp_client_delete
    @client_id INT
AS
BEGIN
    DELETE FROM Client
    WHERE client_id = @client_id;
END
GO

CREATE PROCEDURE sp_client_get_all
AS
BEGIN
    SELECT client_id, name, contactPerson, phone, email, sector
    FROM Client;
END
GO

CREATE PROCEDURE sp_client_get_by_id
    @client_id INT
AS
BEGIN
    SELECT client_id, name, contactPerson, phone, email, sector
    FROM Client
    WHERE client_id = @client_id;
END
GO

-- ============================================================================
-- TradeCategory
-- ============================================================================

CREATE PROCEDURE sp_trade_category_create
    @trade_category_id INT,
    @categoryName NVARCHAR(50)
AS
BEGIN
    INSERT INTO TradeCategory (trade_category_id, categoryName)
    VALUES (@trade_category_id, @categoryName);
END
GO

CREATE PROCEDURE sp_trade_category_update
    @trade_category_id INT,
    @categoryName NVARCHAR(50)
AS
BEGIN
    UPDATE TradeCategory
    SET categoryName = @categoryName
    WHERE trade_category_id = @trade_category_id;
END
GO

CREATE PROCEDURE sp_trade_category_delete
    @trade_category_id INT
AS
BEGIN
    DELETE FROM TradeCategory
    WHERE trade_category_id = @trade_category_id;
END
GO

CREATE PROCEDURE sp_trade_category_get_all
AS
BEGIN
    SELECT trade_category_id, categoryName
    FROM TradeCategory;
END
GO

CREATE PROCEDURE sp_trade_category_get_by_id
    @trade_category_id INT
AS
BEGIN
    SELECT trade_category_id, categoryName
    FROM TradeCategory
    WHERE trade_category_id = @trade_category_id;
END
GO

-- ============================================================================
-- Employee
-- ============================================================================

CREATE PROCEDURE sp_employee_create
    @employee_id INT,
    @firstName NVARCHAR(50),
    @lastName NVARCHAR(50),
    @nationalId NVARCHAR(50),
    @role NVARCHAR(20),
    @dailyRate DECIMAL(10,2),
    @certificationNo NVARCHAR(50),
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Employee (employee_id, firstName, lastName, nationalId, role, dailyRate, certificationNo, status)
    VALUES (@employee_id, @firstName, @lastName, @nationalId, @role, @dailyRate, @certificationNo, @status);
END
GO

CREATE PROCEDURE sp_employee_update
    @employee_id INT,
    @firstName NVARCHAR(50),
    @lastName NVARCHAR(50),
    @nationalId NVARCHAR(50),
    @role NVARCHAR(20),
    @dailyRate DECIMAL(10,2),
    @certificationNo NVARCHAR(50),
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE Employee
    SET firstName = @firstName,
        lastName = @lastName,
        nationalId = @nationalId,
        role = @role,
        dailyRate = @dailyRate,
        certificationNo = @certificationNo,
        status = @status
    WHERE employee_id = @employee_id;
END
GO

CREATE PROCEDURE sp_employee_delete
    @employee_id INT
AS
BEGIN
    DELETE FROM Employee
    WHERE employee_id = @employee_id;
END
GO

CREATE PROCEDURE sp_employee_get_all
AS
BEGIN
    SELECT employee_id, firstName, lastName, nationalId, role, dailyRate, certificationNo, status
    FROM Employee;
END
GO

CREATE PROCEDURE sp_employee_get_by_id
    @employee_id INT
AS
BEGIN
    SELECT employee_id, firstName, lastName, nationalId, role, dailyRate, certificationNo, status
    FROM Employee
    WHERE employee_id = @employee_id;
END
GO

-- ============================================================================
-- Equipment
-- ============================================================================

CREATE PROCEDURE sp_equipment_create
    @equipment_id INT,
    @licenseNumber NVARCHAR(50),
    @equipmentType NVARCHAR(50),
    @description NVARCHAR(MAX),
    @dailyCost DECIMAL(10,2),
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Equipment (equipment_id, licenseNumber, equipmentType, description, dailyCost, status)
    VALUES (@equipment_id, @licenseNumber, @equipmentType, @description, @dailyCost, @status);
END
GO

CREATE PROCEDURE sp_equipment_update
    @equipment_id INT,
    @licenseNumber NVARCHAR(50),
    @equipmentType NVARCHAR(50),
    @description NVARCHAR(MAX),
    @dailyCost DECIMAL(10,2),
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE Equipment
    SET licenseNumber = @licenseNumber,
        equipmentType = @equipmentType,
        description = @description,
        dailyCost = @dailyCost,
        status = @status
    WHERE equipment_id = @equipment_id;
END
GO

CREATE PROCEDURE sp_equipment_delete
    @equipment_id INT
AS
BEGIN
    DELETE FROM Equipment
    WHERE equipment_id = @equipment_id;
END
GO

CREATE PROCEDURE sp_equipment_get_all
AS
BEGIN
    SELECT equipment_id, licenseNumber, equipmentType, description, dailyCost, status
    FROM Equipment;
END
GO

CREATE PROCEDURE sp_equipment_get_by_id
    @equipment_id INT
AS
BEGIN
    SELECT equipment_id, licenseNumber, equipmentType, description, dailyCost, status
    FROM Equipment
    WHERE equipment_id = @equipment_id;
END
GO

-- ============================================================================
-- BusinessPartner
-- ============================================================================

CREATE PROCEDURE sp_business_partner_create
    @business_partner_id INT,
    @name NVARCHAR(50),
    @companyRegistrationNo NVARCHAR(50),
    @contactPerson NVARCHAR(50),
    @phone NVARCHAR(50),
    @email NVARCHAR(50),
    @rating FLOAT,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO BusinessPartner (business_partner_id, name, companyRegistrationNo, contactPerson, phone, email, rating, status)
    VALUES (@business_partner_id, @name, @companyRegistrationNo, @contactPerson, @phone, @email, @rating, @status);
END
GO

CREATE PROCEDURE sp_business_partner_update
    @business_partner_id INT,
    @name NVARCHAR(50),
    @companyRegistrationNo NVARCHAR(50),
    @contactPerson NVARCHAR(50),
    @phone NVARCHAR(50),
    @email NVARCHAR(50),
    @rating FLOAT,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE BusinessPartner
    SET name = @name,
        companyRegistrationNo = @companyRegistrationNo,
        contactPerson = @contactPerson,
        phone = @phone,
        email = @email,
        rating = @rating,
        status = @status
    WHERE business_partner_id = @business_partner_id;
END
GO

CREATE PROCEDURE sp_business_partner_delete
    @business_partner_id INT
AS
BEGIN
    DELETE FROM BusinessPartner
    WHERE business_partner_id = @business_partner_id;
END
GO

CREATE PROCEDURE sp_business_partner_get_all
AS
BEGIN
    SELECT business_partner_id, name, companyRegistrationNo, contactPerson, phone, email, rating, status
    FROM BusinessPartner;
END
GO

CREATE PROCEDURE sp_business_partner_get_by_id
    @business_partner_id INT
AS
BEGIN
    SELECT business_partner_id, name, companyRegistrationNo, contactPerson, phone, email, rating, status
    FROM BusinessPartner
    WHERE business_partner_id = @business_partner_id;
END
GO

-- ============================================================================
-- Supplier (extends BusinessPartner -- no attributes of its own;
-- sp_supplier_update has nothing to SET besides the key, so it is a
-- no-op provided only for interface consistency with every other entity)
-- ============================================================================

CREATE PROCEDURE sp_supplier_create
    @business_partner_id INT
AS
BEGIN
    INSERT INTO Supplier (business_partner_id)
    VALUES (@business_partner_id);
END
GO

CREATE PROCEDURE sp_supplier_update
    @business_partner_id INT
AS
BEGIN
    UPDATE Supplier
    SET business_partner_id = @business_partner_id
    WHERE business_partner_id = @business_partner_id;
END
GO

CREATE PROCEDURE sp_supplier_delete
    @business_partner_id INT
AS
BEGIN
    DELETE FROM Supplier
    WHERE business_partner_id = @business_partner_id;
END
GO

CREATE PROCEDURE sp_supplier_get_all
AS
BEGIN
    SELECT business_partner_id
    FROM Supplier;
END
GO

CREATE PROCEDURE sp_supplier_get_by_id
    @business_partner_id INT
AS
BEGIN
    SELECT business_partner_id
    FROM Supplier
    WHERE business_partner_id = @business_partner_id;
END
GO

-- ============================================================================
-- Subcontractor (extends BusinessPartner)
-- ============================================================================

CREATE PROCEDURE sp_subcontractor_create
    @business_partner_id INT,
    @tradeSpecialty NVARCHAR(50),
    @dailyRate DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Subcontractor (business_partner_id, tradeSpecialty, dailyRate)
    VALUES (@business_partner_id, @tradeSpecialty, @dailyRate);
END
GO

CREATE PROCEDURE sp_subcontractor_update
    @business_partner_id INT,
    @tradeSpecialty NVARCHAR(50),
    @dailyRate DECIMAL(10,2)
AS
BEGIN
    UPDATE Subcontractor
    SET tradeSpecialty = @tradeSpecialty,
        dailyRate = @dailyRate
    WHERE business_partner_id = @business_partner_id;
END
GO

CREATE PROCEDURE sp_subcontractor_delete
    @business_partner_id INT
AS
BEGIN
    DELETE FROM Subcontractor
    WHERE business_partner_id = @business_partner_id;
END
GO

CREATE PROCEDURE sp_subcontractor_get_all
AS
BEGIN
    SELECT business_partner_id, tradeSpecialty, dailyRate
    FROM Subcontractor;
END
GO

CREATE PROCEDURE sp_subcontractor_get_by_id
    @business_partner_id INT
AS
BEGIN
    SELECT business_partner_id, tradeSpecialty, dailyRate
    FROM Subcontractor
    WHERE business_partner_id = @business_partner_id;
END
GO

-- ============================================================================
-- DailyWorkLog
-- ============================================================================

CREATE PROCEDURE sp_daily_work_log_create
    @daily_work_log_id INT,
    @subcontractor_id INT,
    @submitted_by_employee_id INT,
    @logDate DATETIME2,
    @plannedQuantity FLOAT,
    @completedQuantity FLOAT,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO DailyWorkLog (daily_work_log_id, subcontractor_id, submitted_by_employee_id, logDate, plannedQuantity, completedQuantity, status)
    VALUES (@daily_work_log_id, @subcontractor_id, @submitted_by_employee_id, @logDate, @plannedQuantity, @completedQuantity, @status);
END
GO

CREATE PROCEDURE sp_daily_work_log_update
    @daily_work_log_id INT,
    @subcontractor_id INT,
    @submitted_by_employee_id INT,
    @logDate DATETIME2,
    @plannedQuantity FLOAT,
    @completedQuantity FLOAT,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE DailyWorkLog
    SET subcontractor_id = @subcontractor_id,
        submitted_by_employee_id = @submitted_by_employee_id,
        logDate = @logDate,
        plannedQuantity = @plannedQuantity,
        completedQuantity = @completedQuantity,
        status = @status
    WHERE daily_work_log_id = @daily_work_log_id;
END
GO

CREATE PROCEDURE sp_daily_work_log_delete
    @daily_work_log_id INT
AS
BEGIN
    DELETE FROM DailyWorkLog
    WHERE daily_work_log_id = @daily_work_log_id;
END
GO

CREATE PROCEDURE sp_daily_work_log_get_all
AS
BEGIN
    SELECT daily_work_log_id, subcontractor_id, submitted_by_employee_id, logDate, plannedQuantity, completedQuantity, status
    FROM DailyWorkLog;
END
GO

CREATE PROCEDURE sp_daily_work_log_get_by_id
    @daily_work_log_id INT
AS
BEGIN
    SELECT daily_work_log_id, subcontractor_id, submitted_by_employee_id, logDate, plannedQuantity, completedQuantity, status
    FROM DailyWorkLog
    WHERE daily_work_log_id = @daily_work_log_id;
END
GO

-- ============================================================================
-- FinancialSecurity
-- ============================================================================

CREATE PROCEDURE sp_financial_security_create
    @financial_security_id INT,
    @project_id INT,
    @amount DECIMAL(10,2),
    @issueDate DATETIME2,
    @expiryDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO FinancialSecurity (financial_security_id, project_id, amount, issueDate, expiryDate, status)
    VALUES (@financial_security_id, @project_id, @amount, @issueDate, @expiryDate, @status);
END
GO

CREATE PROCEDURE sp_financial_security_update
    @financial_security_id INT,
    @project_id INT,
    @amount DECIMAL(10,2),
    @issueDate DATETIME2,
    @expiryDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE FinancialSecurity
    SET project_id = @project_id,
        amount = @amount,
        issueDate = @issueDate,
        expiryDate = @expiryDate,
        status = @status
    WHERE financial_security_id = @financial_security_id;
END
GO

CREATE PROCEDURE sp_financial_security_delete
    @financial_security_id INT
AS
BEGIN
    DELETE FROM FinancialSecurity
    WHERE financial_security_id = @financial_security_id;
END
GO

CREATE PROCEDURE sp_financial_security_get_all
AS
BEGIN
    SELECT financial_security_id, project_id, amount, issueDate, expiryDate, status
    FROM FinancialSecurity;
END
GO

CREATE PROCEDURE sp_financial_security_get_by_id
    @financial_security_id INT
AS
BEGIN
    SELECT financial_security_id, project_id, amount, issueDate, expiryDate, status
    FROM FinancialSecurity
    WHERE financial_security_id = @financial_security_id;
END
GO

-- ============================================================================
-- BankGuarantee (extends FinancialSecurity)
-- ============================================================================

CREATE PROCEDURE sp_bank_guarantee_create
    @financial_security_id INT,
    @bankName NVARCHAR(50),
    @guaranteeNumber NVARCHAR(50)
AS
BEGIN
    INSERT INTO BankGuarantee (financial_security_id, bankName, guaranteeNumber)
    VALUES (@financial_security_id, @bankName, @guaranteeNumber);
END
GO

CREATE PROCEDURE sp_bank_guarantee_update
    @financial_security_id INT,
    @bankName NVARCHAR(50),
    @guaranteeNumber NVARCHAR(50)
AS
BEGIN
    UPDATE BankGuarantee
    SET bankName = @bankName,
        guaranteeNumber = @guaranteeNumber
    WHERE financial_security_id = @financial_security_id;
END
GO

CREATE PROCEDURE sp_bank_guarantee_delete
    @financial_security_id INT
AS
BEGIN
    DELETE FROM BankGuarantee
    WHERE financial_security_id = @financial_security_id;
END
GO

CREATE PROCEDURE sp_bank_guarantee_get_all
AS
BEGIN
    SELECT financial_security_id, bankName, guaranteeNumber
    FROM BankGuarantee;
END
GO

CREATE PROCEDURE sp_bank_guarantee_get_by_id
    @financial_security_id INT
AS
BEGIN
    SELECT financial_security_id, bankName, guaranteeNumber
    FROM BankGuarantee
    WHERE financial_security_id = @financial_security_id;
END
GO

-- ============================================================================
-- InsurancePolicy (extends FinancialSecurity)
-- ============================================================================

CREATE PROCEDURE sp_insurance_policy_create
    @financial_security_id INT,
    @insurerName NVARCHAR(50),
    @policyNumber NVARCHAR(50),
    @coverageType NVARCHAR(50)
AS
BEGIN
    INSERT INTO InsurancePolicy (financial_security_id, insurerName, policyNumber, coverageType)
    VALUES (@financial_security_id, @insurerName, @policyNumber, @coverageType);
END
GO

CREATE PROCEDURE sp_insurance_policy_update
    @financial_security_id INT,
    @insurerName NVARCHAR(50),
    @policyNumber NVARCHAR(50),
    @coverageType NVARCHAR(50)
AS
BEGIN
    UPDATE InsurancePolicy
    SET insurerName = @insurerName,
        policyNumber = @policyNumber,
        coverageType = @coverageType
    WHERE financial_security_id = @financial_security_id;
END
GO

CREATE PROCEDURE sp_insurance_policy_delete
    @financial_security_id INT
AS
BEGIN
    DELETE FROM InsurancePolicy
    WHERE financial_security_id = @financial_security_id;
END
GO

CREATE PROCEDURE sp_insurance_policy_get_all
AS
BEGIN
    SELECT financial_security_id, insurerName, policyNumber, coverageType
    FROM InsurancePolicy;
END
GO

CREATE PROCEDURE sp_insurance_policy_get_by_id
    @financial_security_id INT
AS
BEGIN
    SELECT financial_security_id, insurerName, policyNumber, coverageType
    FROM InsurancePolicy
    WHERE financial_security_id = @financial_security_id;
END
GO

-- ============================================================================
-- Tender
-- ============================================================================

CREATE PROCEDURE sp_tender_create
    @tender_id INT,
    @tenderNumber NVARCHAR(50),
    @client_id INT,
    @title NVARCHAR(50),
    @estimatedValue DECIMAL(10,2),
    @submissionDeadline DATETIME2,
    @publishedDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Tender (tender_id, tenderNumber, client_id, title, estimatedValue, submissionDeadline, publishedDate, status)
    VALUES (@tender_id, @tenderNumber, @client_id, @title, @estimatedValue, @submissionDeadline, @publishedDate, @status);
END
GO

CREATE PROCEDURE sp_tender_update
    @tender_id INT,
    @tenderNumber NVARCHAR(50),
    @client_id INT,
    @title NVARCHAR(50),
    @estimatedValue DECIMAL(10,2),
    @submissionDeadline DATETIME2,
    @publishedDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE Tender
    SET tenderNumber = @tenderNumber,
        client_id = @client_id,
        title = @title,
        estimatedValue = @estimatedValue,
        submissionDeadline = @submissionDeadline,
        publishedDate = @publishedDate,
        status = @status
    WHERE tender_id = @tender_id;
END
GO

CREATE PROCEDURE sp_tender_delete
    @tender_id INT
AS
BEGIN
    DELETE FROM Tender
    WHERE tender_id = @tender_id;
END
GO

CREATE PROCEDURE sp_tender_get_all
AS
BEGIN
    SELECT tender_id, tenderNumber, client_id, title, estimatedValue, submissionDeadline, publishedDate, status
    FROM Tender;
END
GO

CREATE PROCEDURE sp_tender_get_by_id
    @tender_id INT
AS
BEGIN
    SELECT tender_id, tenderNumber, client_id, title, estimatedValue, submissionDeadline, publishedDate, status
    FROM Tender
    WHERE tender_id = @tender_id;
END
GO

-- ============================================================================
-- Project
-- ============================================================================

CREATE PROCEDURE sp_project_create
    @project_id INT,
    @tender_id INT,
    @project_manager_employee_id INT,
    @name NVARCHAR(50),
    @address NVARCHAR(50),
    @plannedStartDate DATETIME2,
    @plannedEndDate DATETIME2,
    @actualStartDate DATETIME2,
    @actualEndDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO Project (project_id, tender_id, project_manager_employee_id, name, address, plannedStartDate, plannedEndDate, actualStartDate, actualEndDate, status)
    VALUES (@project_id, @tender_id, @project_manager_employee_id, @name, @address, @plannedStartDate, @plannedEndDate, @actualStartDate, @actualEndDate, @status);
END
GO

CREATE PROCEDURE sp_project_update
    @project_id INT,
    @tender_id INT,
    @project_manager_employee_id INT,
    @name NVARCHAR(50),
    @address NVARCHAR(50),
    @plannedStartDate DATETIME2,
    @plannedEndDate DATETIME2,
    @actualStartDate DATETIME2,
    @actualEndDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE Project
    SET tender_id = @tender_id,
        project_manager_employee_id = @project_manager_employee_id,
        name = @name,
        address = @address,
        plannedStartDate = @plannedStartDate,
        plannedEndDate = @plannedEndDate,
        actualStartDate = @actualStartDate,
        actualEndDate = @actualEndDate,
        status = @status
    WHERE project_id = @project_id;
END
GO

CREATE PROCEDURE sp_project_delete
    @project_id INT
AS
BEGIN
    DELETE FROM Project
    WHERE project_id = @project_id;
END
GO

CREATE PROCEDURE sp_project_get_all
AS
BEGIN
    SELECT project_id, tender_id, project_manager_employee_id, name, address, plannedStartDate, plannedEndDate, actualStartDate, actualEndDate, status
    FROM Project;
END
GO

CREATE PROCEDURE sp_project_get_by_id
    @project_id INT
AS
BEGIN
    SELECT project_id, tender_id, project_manager_employee_id, name, address, plannedStartDate, plannedEndDate, actualStartDate, actualEndDate, status
    FROM Project
    WHERE project_id = @project_id;
END
GO

-- ============================================================================
-- BudgetLine
-- ============================================================================

CREATE PROCEDURE sp_budget_line_create
    @budget_line_id INT,
    @project_id INT,
    @category NVARCHAR(50),
    @plannedAmount DECIMAL(10,2),
    @actualAmount DECIMAL(10,2)
AS
BEGIN
    INSERT INTO BudgetLine (budget_line_id, project_id, category, plannedAmount, actualAmount)
    VALUES (@budget_line_id, @project_id, @category, @plannedAmount, @actualAmount);
END
GO

CREATE PROCEDURE sp_budget_line_update
    @budget_line_id INT,
    @project_id INT,
    @category NVARCHAR(50),
    @plannedAmount DECIMAL(10,2),
    @actualAmount DECIMAL(10,2)
AS
BEGIN
    UPDATE BudgetLine
    SET project_id = @project_id,
        category = @category,
        plannedAmount = @plannedAmount,
        actualAmount = @actualAmount
    WHERE budget_line_id = @budget_line_id;
END
GO

CREATE PROCEDURE sp_budget_line_delete
    @budget_line_id INT
AS
BEGIN
    DELETE FROM BudgetLine
    WHERE budget_line_id = @budget_line_id;
END
GO

CREATE PROCEDURE sp_budget_line_get_all
AS
BEGIN
    SELECT budget_line_id, project_id, category, plannedAmount, actualAmount
    FROM BudgetLine;
END
GO

CREATE PROCEDURE sp_budget_line_get_by_id
    @budget_line_id INT
AS
BEGIN
    SELECT budget_line_id, project_id, category, plannedAmount, actualAmount
    FROM BudgetLine
    WHERE budget_line_id = @budget_line_id;
END
GO

-- ============================================================================
-- PaymentRequest
-- ============================================================================

CREATE PROCEDURE sp_payment_request_create
    @payment_request_id INT,
    @project_id INT,
    @amount DECIMAL(10,2),
    @submissionDate DATETIME2,
    @approvalDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO PaymentRequest (payment_request_id, project_id, amount, submissionDate, approvalDate, status)
    VALUES (@payment_request_id, @project_id, @amount, @submissionDate, @approvalDate, @status);
END
GO

CREATE PROCEDURE sp_payment_request_update
    @payment_request_id INT,
    @project_id INT,
    @amount DECIMAL(10,2),
    @submissionDate DATETIME2,
    @approvalDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE PaymentRequest
    SET project_id = @project_id,
        amount = @amount,
        submissionDate = @submissionDate,
        approvalDate = @approvalDate,
        status = @status
    WHERE payment_request_id = @payment_request_id;
END
GO

CREATE PROCEDURE sp_payment_request_delete
    @payment_request_id INT
AS
BEGIN
    DELETE FROM PaymentRequest
    WHERE payment_request_id = @payment_request_id;
END
GO

CREATE PROCEDURE sp_payment_request_get_all
AS
BEGIN
    SELECT payment_request_id, project_id, amount, submissionDate, approvalDate, status
    FROM PaymentRequest;
END
GO

CREATE PROCEDURE sp_payment_request_get_by_id
    @payment_request_id INT
AS
BEGIN
    SELECT payment_request_id, project_id, amount, submissionDate, approvalDate, status
    FROM PaymentRequest
    WHERE payment_request_id = @payment_request_id;
END
GO

-- ============================================================================
-- SubmittedDocument (composition child of PaymentRequest)
-- ============================================================================

CREATE PROCEDURE sp_submitted_document_create
    @submitted_document_id INT,
    @payment_request_id INT,
    @type NVARCHAR(20),
    @receivedOn DATETIME2
AS
BEGIN
    INSERT INTO SubmittedDocument (submitted_document_id, payment_request_id, type, receivedOn)
    VALUES (@submitted_document_id, @payment_request_id, @type, @receivedOn);
END
GO

CREATE PROCEDURE sp_submitted_document_update
    @submitted_document_id INT,
    @payment_request_id INT,
    @type NVARCHAR(20),
    @receivedOn DATETIME2
AS
BEGIN
    UPDATE SubmittedDocument
    SET payment_request_id = @payment_request_id,
        type = @type,
        receivedOn = @receivedOn
    WHERE submitted_document_id = @submitted_document_id;
END
GO

CREATE PROCEDURE sp_submitted_document_delete
    @submitted_document_id INT
AS
BEGIN
    DELETE FROM SubmittedDocument
    WHERE submitted_document_id = @submitted_document_id;
END
GO

CREATE PROCEDURE sp_submitted_document_get_all
AS
BEGIN
    SELECT submitted_document_id, payment_request_id, type, receivedOn
    FROM SubmittedDocument;
END
GO

CREATE PROCEDURE sp_submitted_document_get_by_id
    @submitted_document_id INT
AS
BEGIN
    SELECT submitted_document_id, payment_request_id, type, receivedOn
    FROM SubmittedDocument
    WHERE submitted_document_id = @submitted_document_id;
END
GO

-- ============================================================================
-- PurchaseOrder
-- ============================================================================

CREATE PROCEDURE sp_purchase_order_create
    @purchase_order_id INT,
    @poNumber NVARCHAR(50),
    @supplier_id INT,
    @project_id INT,
    @created_by_employee_id INT,
    @approved_by_employee_id INT,
    @override_approved_by_employee_id INT,
    @orderDate DATETIME2,
    @totalAmount DECIMAL(10,2),
    @vatAmount DECIMAL(10,2),
    @status NVARCHAR(30),
    @rejectionReason NVARCHAR(MAX),
    @closureReason NVARCHAR(20),
    @rejectedAt DATETIME2,
    @archivedAt DATETIME2,
    @everSubmitted BIT,
    @rejected_by_employee_id INT
AS
BEGIN
    INSERT INTO PurchaseOrder (purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted, rejected_by_employee_id)
    VALUES (@purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @approved_by_employee_id, @override_approved_by_employee_id, @orderDate, @totalAmount, @vatAmount, @status, @rejectionReason, @closureReason, @rejectedAt, @archivedAt, @everSubmitted, @rejected_by_employee_id);
END
GO

-- Deliberately excludes status/rejectionReason/closureReason/approved_by_employee_id/
-- override_approved_by_employee_id (step 7.4): those are state-machine-owned columns,
-- written only by the dedicated transition procedures below.
CREATE PROCEDURE sp_purchase_order_update
    @purchase_order_id INT,
    @poNumber NVARCHAR(50),
    @supplier_id INT,
    @project_id INT,
    @created_by_employee_id INT,
    @orderDate DATETIME2,
    @totalAmount DECIMAL(10,2),
    @vatAmount DECIMAL(10,2)
AS
BEGIN
    UPDATE PurchaseOrder
    SET poNumber = @poNumber,
        supplier_id = @supplier_id,
        project_id = @project_id,
        created_by_employee_id = @created_by_employee_id,
        orderDate = @orderDate,
        totalAmount = @totalAmount,
        vatAmount = @vatAmount
    WHERE purchase_order_id = @purchase_order_id;
END
GO

CREATE PROCEDURE sp_purchase_order_delete
    @purchase_order_id INT
AS
BEGIN
    DELETE FROM PurchaseOrder
    WHERE purchase_order_id = @purchase_order_id;
END
GO

CREATE PROCEDURE sp_purchase_order_get_all
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted, rejected_by_employee_id
    FROM PurchaseOrder;
END
GO

CREATE PROCEDURE sp_purchase_order_get_by_id
    @purchase_order_id INT
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted, rejected_by_employee_id
    FROM PurchaseOrder
    WHERE purchase_order_id = @purchase_order_id;
END
GO

-- ============================================================================
-- PurchaseOrderLine
-- ============================================================================

CREATE PROCEDURE sp_purchase_order_line_create
    @purchase_order_line_id INT,
    @purchase_order_id INT,
    @description NVARCHAR(MAX),
    @unitOfMeasure NVARCHAR(50),
    @quantity FLOAT,
    @unitPrice DECIMAL(10,2),
    @receivedQuantity FLOAT
AS
BEGIN
    INSERT INTO PurchaseOrderLine (purchase_order_line_id, purchase_order_id, description, unitOfMeasure, quantity, unitPrice, receivedQuantity)
    VALUES (@purchase_order_line_id, @purchase_order_id, @description, @unitOfMeasure, @quantity, @unitPrice, @receivedQuantity);
END
GO

-- Deliberately excludes receivedQuantity (step 7.4): state-machine-owned by
-- PurchaseOrder.receiveDelivery() (BR-5), written only by sp_purchase_order_receive_delivery.
CREATE PROCEDURE sp_purchase_order_line_update
    @purchase_order_line_id INT,
    @purchase_order_id INT,
    @description NVARCHAR(MAX),
    @unitOfMeasure NVARCHAR(50),
    @quantity FLOAT,
    @unitPrice DECIMAL(10,2)
AS
BEGIN
    UPDATE PurchaseOrderLine
    SET purchase_order_id = @purchase_order_id,
        description = @description,
        unitOfMeasure = @unitOfMeasure,
        quantity = @quantity,
        unitPrice = @unitPrice
    WHERE purchase_order_line_id = @purchase_order_line_id;
END
GO

CREATE PROCEDURE sp_purchase_order_line_delete
    @purchase_order_line_id INT
AS
BEGIN
    DELETE FROM PurchaseOrderLine
    WHERE purchase_order_line_id = @purchase_order_line_id;
END
GO

CREATE PROCEDURE sp_purchase_order_line_get_all
AS
BEGIN
    SELECT purchase_order_line_id, purchase_order_id, description, unitOfMeasure, quantity, unitPrice, receivedQuantity
    FROM PurchaseOrderLine;
END
GO

CREATE PROCEDURE sp_purchase_order_line_get_by_id
    @purchase_order_line_id INT
AS
BEGIN
    SELECT purchase_order_line_id, purchase_order_id, description, unitOfMeasure, quantity, unitPrice, receivedQuantity
    FROM PurchaseOrderLine
    WHERE purchase_order_line_id = @purchase_order_line_id;
END
GO

-- ============================================================================
-- SupplierPayment
-- ============================================================================

CREATE PROCEDURE sp_supplier_payment_create
    @supplier_payment_id INT,
    @invoiceNumber NVARCHAR(50),
    @business_partner_id INT,
    @amount DECIMAL(10,2),
    @dueDate DATETIME2,
    @paidDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO SupplierPayment (supplier_payment_id, invoiceNumber, business_partner_id, amount, dueDate, paidDate, status)
    VALUES (@supplier_payment_id, @invoiceNumber, @business_partner_id, @amount, @dueDate, @paidDate, @status);
END
GO

CREATE PROCEDURE sp_supplier_payment_update
    @supplier_payment_id INT,
    @invoiceNumber NVARCHAR(50),
    @business_partner_id INT,
    @amount DECIMAL(10,2),
    @dueDate DATETIME2,
    @paidDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE SupplierPayment
    SET invoiceNumber = @invoiceNumber,
        business_partner_id = @business_partner_id,
        amount = @amount,
        dueDate = @dueDate,
        paidDate = @paidDate,
        status = @status
    WHERE supplier_payment_id = @supplier_payment_id;
END
GO

CREATE PROCEDURE sp_supplier_payment_delete
    @supplier_payment_id INT
AS
BEGIN
    DELETE FROM SupplierPayment
    WHERE supplier_payment_id = @supplier_payment_id;
END
GO

CREATE PROCEDURE sp_supplier_payment_get_all
AS
BEGIN
    SELECT supplier_payment_id, invoiceNumber, business_partner_id, amount, dueDate, paidDate, status
    FROM SupplierPayment;
END
GO

CREATE PROCEDURE sp_supplier_payment_get_by_id
    @supplier_payment_id INT
AS
BEGIN
    SELECT supplier_payment_id, invoiceNumber, business_partner_id, amount, dueDate, paidDate, status
    FROM SupplierPayment
    WHERE supplier_payment_id = @supplier_payment_id;
END
GO

-- ============================================================================
-- SupplierPriceQuote (ternary association class: Supplier + Tender + TradeCategory)
-- ============================================================================

CREATE PROCEDURE sp_supplier_price_quote_create
    @supplier_price_quote_id INT,
    @supplier_id INT,
    @tender_id INT,
    @trade_category_id INT,
    @amount DECIMAL(10,2),
    @dateIssued DATETIME2,
    @validUntil DATETIME2,
    @isSelected BIT
AS
BEGIN
    INSERT INTO SupplierPriceQuote (supplier_price_quote_id, supplier_id, tender_id, trade_category_id, amount, dateIssued, validUntil, isSelected)
    VALUES (@supplier_price_quote_id, @supplier_id, @tender_id, @trade_category_id, @amount, @dateIssued, @validUntil, @isSelected);
END
GO

CREATE PROCEDURE sp_supplier_price_quote_update
    @supplier_price_quote_id INT,
    @supplier_id INT,
    @tender_id INT,
    @trade_category_id INT,
    @amount DECIMAL(10,2),
    @dateIssued DATETIME2,
    @validUntil DATETIME2,
    @isSelected BIT
AS
BEGIN
    UPDATE SupplierPriceQuote
    SET supplier_id = @supplier_id,
        tender_id = @tender_id,
        trade_category_id = @trade_category_id,
        amount = @amount,
        dateIssued = @dateIssued,
        validUntil = @validUntil,
        isSelected = @isSelected
    WHERE supplier_price_quote_id = @supplier_price_quote_id;
END
GO

CREATE PROCEDURE sp_supplier_price_quote_delete
    @supplier_price_quote_id INT
AS
BEGIN
    DELETE FROM SupplierPriceQuote
    WHERE supplier_price_quote_id = @supplier_price_quote_id;
END
GO

CREATE PROCEDURE sp_supplier_price_quote_get_all
AS
BEGIN
    SELECT supplier_price_quote_id, supplier_id, tender_id, trade_category_id, amount, dateIssued, validUntil, isSelected
    FROM SupplierPriceQuote;
END
GO

CREATE PROCEDURE sp_supplier_price_quote_get_by_id
    @supplier_price_quote_id INT
AS
BEGIN
    SELECT supplier_price_quote_id, supplier_id, tender_id, trade_category_id, amount, dateIssued, validUntil, isSelected
    FROM SupplierPriceQuote
    WHERE supplier_price_quote_id = @supplier_price_quote_id;
END
GO

-- ============================================================================
-- Attendance (association class: Employee <-> DailyWorkLog)
-- ============================================================================

CREATE PROCEDURE sp_attendance_create
    @attendance_id INT,
    @employee_id INT,
    @daily_work_log_id INT,
    @startTime TIME,
    @endTime TIME,
    @taskDescription NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Attendance (attendance_id, employee_id, daily_work_log_id, startTime, endTime, taskDescription)
    VALUES (@attendance_id, @employee_id, @daily_work_log_id, @startTime, @endTime, @taskDescription);
END
GO

CREATE PROCEDURE sp_attendance_update
    @attendance_id INT,
    @employee_id INT,
    @daily_work_log_id INT,
    @startTime TIME,
    @endTime TIME,
    @taskDescription NVARCHAR(MAX)
AS
BEGIN
    UPDATE Attendance
    SET employee_id = @employee_id,
        daily_work_log_id = @daily_work_log_id,
        startTime = @startTime,
        endTime = @endTime,
        taskDescription = @taskDescription
    WHERE attendance_id = @attendance_id;
END
GO

CREATE PROCEDURE sp_attendance_delete
    @attendance_id INT
AS
BEGIN
    DELETE FROM Attendance
    WHERE attendance_id = @attendance_id;
END
GO

CREATE PROCEDURE sp_attendance_get_all
AS
BEGIN
    SELECT attendance_id, employee_id, daily_work_log_id, startTime, endTime, taskDescription
    FROM Attendance;
END
GO

CREATE PROCEDURE sp_attendance_get_by_id
    @attendance_id INT
AS
BEGIN
    SELECT attendance_id, employee_id, daily_work_log_id, startTime, endTime, taskDescription
    FROM Attendance
    WHERE attendance_id = @attendance_id;
END
GO

-- ============================================================================
-- EquipmentUsage (association class: Equipment <-> DailyWorkLog)
-- ============================================================================

CREATE PROCEDURE sp_equipment_usage_create
    @equipment_usage_id INT,
    @equipment_id INT,
    @daily_work_log_id INT,
    @hoursOperated FLOAT
AS
BEGIN
    INSERT INTO EquipmentUsage (equipment_usage_id, equipment_id, daily_work_log_id, hoursOperated)
    VALUES (@equipment_usage_id, @equipment_id, @daily_work_log_id, @hoursOperated);
END
GO

CREATE PROCEDURE sp_equipment_usage_update
    @equipment_usage_id INT,
    @equipment_id INT,
    @daily_work_log_id INT,
    @hoursOperated FLOAT
AS
BEGIN
    UPDATE EquipmentUsage
    SET equipment_id = @equipment_id,
        daily_work_log_id = @daily_work_log_id,
        hoursOperated = @hoursOperated
    WHERE equipment_usage_id = @equipment_usage_id;
END
GO

CREATE PROCEDURE sp_equipment_usage_delete
    @equipment_usage_id INT
AS
BEGIN
    DELETE FROM EquipmentUsage
    WHERE equipment_usage_id = @equipment_usage_id;
END
GO

CREATE PROCEDURE sp_equipment_usage_get_all
AS
BEGIN
    SELECT equipment_usage_id, equipment_id, daily_work_log_id, hoursOperated
    FROM EquipmentUsage;
END
GO

CREATE PROCEDURE sp_equipment_usage_get_by_id
    @equipment_usage_id INT
AS
BEGIN
    SELECT equipment_usage_id, equipment_id, daily_work_log_id, hoursOperated
    FROM EquipmentUsage
    WHERE equipment_usage_id = @equipment_usage_id;
END
GO

-- ============================================================================
-- EquipmentAssignment (link class: Project <-> Equipment)
-- ============================================================================

CREATE PROCEDURE sp_equipment_assignment_create
    @equipment_assignment_id INT,
    @project_id INT,
    @equipment_id INT,
    @startDate DATETIME2,
    @endDate DATETIME2
AS
BEGIN
    INSERT INTO EquipmentAssignment (equipment_assignment_id, project_id, equipment_id, startDate, endDate)
    VALUES (@equipment_assignment_id, @project_id, @equipment_id, @startDate, @endDate);
END
GO

CREATE PROCEDURE sp_equipment_assignment_update
    @equipment_assignment_id INT,
    @project_id INT,
    @equipment_id INT,
    @startDate DATETIME2,
    @endDate DATETIME2
AS
BEGIN
    UPDATE EquipmentAssignment
    SET project_id = @project_id,
        equipment_id = @equipment_id,
        startDate = @startDate,
        endDate = @endDate
    WHERE equipment_assignment_id = @equipment_assignment_id;
END
GO

CREATE PROCEDURE sp_equipment_assignment_delete
    @equipment_assignment_id INT
AS
BEGIN
    DELETE FROM EquipmentAssignment
    WHERE equipment_assignment_id = @equipment_assignment_id;
END
GO

CREATE PROCEDURE sp_equipment_assignment_get_all
AS
BEGIN
    SELECT equipment_assignment_id, project_id, equipment_id, startDate, endDate
    FROM EquipmentAssignment;
END
GO

CREATE PROCEDURE sp_equipment_assignment_get_by_id
    @equipment_assignment_id INT
AS
BEGIN
    SELECT equipment_assignment_id, project_id, equipment_id, startDate, endDate
    FROM EquipmentAssignment
    WHERE equipment_assignment_id = @equipment_assignment_id;
END
GO

-- ============================================================================
-- PurchaseOrder state transitions (course step 7.3, docs/design/state-diagram.md)
--
-- Distinct from the generic sp_purchase_order_update above: these procedures
-- are called ONLY from PurchaseOrder.cs's transition methods (submit(),
-- approve(), etc.), never from the CRUD update path. Each wraps its writes in
-- BEGIN TRAN / COMMIT TRAN with ROLLBACK on error, per the step's instructions
-- -- the guard itself is evaluated in C# before the procedure is called.
-- ============================================================================

CREATE PROCEDURE sp_purchase_order_submit
    @purchase_order_id INT,
    @new_status NVARCHAR(30)
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder SET status = @new_status, everSubmitted = 1 WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_withdraw
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder SET status = N'Draft' WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_reject
    @purchase_order_id INT,
    @rejectionReason NVARCHAR(MAX),
    @rejectedAt DATETIME2,
    @rejected_by_employee_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Rejected',
            rejectionReason = @rejectionReason,
            rejectedAt = @rejectedAt,
            rejected_by_employee_id = @rejected_by_employee_id
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- Clears rejectedAt/rejected_by_employee_id along with the status flip back to Draft --
-- a PO revised out of Rejected is no longer subject to the BR-3 auto-cancel guard, and
-- a fresh rejection (if it happens again) will set both fields anew.
CREATE PROCEDURE sp_purchase_order_revise
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Draft',
            rejectedAt = NULL,
            rejected_by_employee_id = NULL
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_auto_cancel
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Cancelled',
            closureReason = N'Cancelled'
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_cancel
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Cancelled',
            closureReason = N'Cancelled'
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- BR-1 (never submitted): deletes the order and its lines together -- the only
-- transition that removes rows instead of changing status.
CREATE PROCEDURE sp_purchase_order_cancel_delete
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        DELETE FROM PurchaseOrderLine WHERE purchase_order_id = @purchase_order_id;
        DELETE FROM PurchaseOrder WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_approve_budget_override
    @purchase_order_id INT,
    @override_approved_by_employee_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'PendingPMApproval',
            override_approved_by_employee_id = @override_approved_by_employee_id
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_approve
    @purchase_order_id INT,
    @approved_by_employee_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Sent',
            approved_by_employee_id = @approved_by_employee_id
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- Touches both PurchaseOrderLine (receivedQuantity) and PurchaseOrder (status,
-- closureReason) atomically -- BR-5.
CREATE PROCEDURE sp_purchase_order_receive_delivery
    @purchase_order_id INT,
    @purchase_order_line_id INT,
    @receivedQuantity FLOAT,
    @new_status NVARCHAR(30),
    @closureReason NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrderLine
        SET receivedQuantity = @receivedQuantity
        WHERE purchase_order_line_id = @purchase_order_line_id;
        UPDATE PurchaseOrder
        SET status = @new_status,
            closureReason = @closureReason
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE sp_purchase_order_cancel_remaining
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Cancelled',
            closureReason = N'Cancelled'
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- closureReason is NOT touched here -- it must survive from Received/Cancelled
-- into Archived (docs/design/state-diagram.md).
CREATE PROCEDURE sp_purchase_order_archive
    @purchase_order_id INT,
    @archivedAt DATETIME2
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Archived',
            archivedAt = @archivedAt
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- 7-year retention: deletes the order and its lines together, like sp_purchase_order_cancel_delete.
CREATE PROCEDURE sp_purchase_order_purge
    @purchase_order_id INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        DELETE FROM PurchaseOrderLine WHERE purchase_order_id = @purchase_order_id;
        DELETE FROM PurchaseOrder WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- ============================================================================
-- UC-05 report (course step 8.2, docs/00e-use-cases.md UC-05)
--
-- First-pass scope only: per-project profitability (revenue, actual cost,
-- net profit, margin %) over a date range, optionally filtered to one
-- project. The monthly cash-flow trend breakdown from UC-05's MSS step 7 is
-- deliberately deferred to a later pass, per the team's own "simplest report
-- first" decision under step 8.1.
--
-- Revenue and cost are aggregated in separate CTEs before being joined to
-- Project, so a project with N PaymentRequests and M BudgetLines doesn't
-- produce N*M duplicated rows.
--
-- Revenue = SUM(PaymentRequest.amount) for status IN ('Approved','Paid') rows whose
-- approvalDate falls in [@date_from, @date_to].
-- Actual cost = SUM(BudgetLine.actualAmount) for the project -- BudgetLine
-- has no date column, so this is the project's cumulative actual cost as of
-- now, not scoped to the date range (there is no finer-grained cost data to
-- filter by; flagged here rather than silently treated as period-accurate).
-- ============================================================================

-- ============================================================================
-- UC-03 Create Purchase Order -- transactional flow (docs/00e-use-cases.md
-- UC-03 MSS steps 1-9: the Accountant builds a PurchaseOrder + its
-- PurchaseOrderLine items and submits it as one atomic action).
--
-- Deliberate deviation from the comment on the PurchaseOrder state-transition
-- procedures above ("the guard itself is evaluated in C# before the procedure
-- is called"): those guard single-entity status transitions where C# already
-- holds current state in memory. This procedure guards a multi-entity CREATE
-- where the authoritative data (remaining budget, current supplier status)
-- lives in the database and could differ from whatever the panel last
-- loaded, so every guard is re-checked here, server-side, as the last line
-- of defense -- nothing is left for Employee.createPurchaseOrder() to
-- "remember" to validate.
--
-- Guards enforced, in order, BEFORE any row is written:
--   1. Supplier exists and Supplier.status = 'Active'   (UC-03 ext. 4a)
--   2. Project exists                                   (UC-03 pre-cond. 3)
--   3. Project has at least one BudgetLine               (UC-03 pre-cond. 3)
--   4. @lines has at least one row                        (UC-03 MSS step 5)
--   5. Every line has quantity > 0 and unitPrice >= 0     (basic validity)
--
-- Budget check (UC-03 MSS step 8 / ext. 8a) is NOT a hard guard: it branches
-- the resulting status between 'PendingPMApproval' (within remaining budget)
-- and 'PendingBudgetOverride' (exceeds it, routed to the CEO) -- both are
-- valid, committed outcomes; this mirrors how sp_purchase_order_approve_
-- budget_override already handles the CEO-override branch later in the
-- lifecycle. Remaining budget = SUM(plannedAmount - actualAmount) over the
-- project's BudgetLine rows.
--
-- VAT: the class diagram has no VatRate entity/setting, so @vat_rate is a
-- procedure-local constant (17%, assumption -- flagged, not hidden).
-- totalAmount/vatAmount are (re)computed here from @lines, not trusted from
-- the client, since the guard depends on them being correct.
--
-- Ordering inside the transaction: PurchaseOrder (parent) is inserted before
-- PurchaseOrderLine (children, FK to purchase_order_id) -- the only order
-- the FK allows.
--
-- purchase_order_id and every purchase_order_line_id in @lines must already
-- be assigned by C# before calling this procedure, per CLAUDE.md's Primary
-- Key Strategy (getNextPurchaseOrderId() once, getNextPurchaseOrderLineId()
-- once per line, incrementing locally between calls).
-- ============================================================================

CREATE TYPE PurchaseOrderLineTableType AS TABLE (
    purchase_order_line_id INT NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    unitOfMeasure NVARCHAR(50) NOT NULL,
    quantity FLOAT NOT NULL,
    unitPrice DECIMAL(10,2) NOT NULL
);
GO

CREATE PROCEDURE sp_purchase_order_create_flow
    @purchase_order_id INT,
    @poNumber NVARCHAR(50),
    @supplier_id INT,
    @project_id INT,
    @created_by_employee_id INT,
    @orderDate DATETIME2,
    @lines PurchaseOrderLineTableType READONLY,
    @result_status NVARCHAR(30) OUTPUT,
    @result_total_amount DECIMAL(18,2) OUTPUT,
    @result_vat_amount DECIMAL(18,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @vat_rate DECIMAL(5,4) = 0.17; -- assumption: no VatRate entity in the class diagram

    -- ---- Guard 1: Supplier must exist and be Active (UC-03 ext. 4a) ----
    IF NOT EXISTS (
        SELECT 1 FROM Supplier s
        JOIN BusinessPartner bp ON bp.business_partner_id = s.business_partner_id
        WHERE s.business_partner_id = @supplier_id
    )
    BEGIN
        RAISERROR(N'sp_purchase_order_create_flow: supplier %d does not exist', 16, 1, @supplier_id);
        RETURN;
    END

    IF EXISTS (
        SELECT 1 FROM BusinessPartner
        WHERE business_partner_id = @supplier_id AND status <> N'Active'
    )
    BEGIN
        RAISERROR(N'sp_purchase_order_create_flow: supplier %d is not Active', 16, 1, @supplier_id);
        RETURN;
    END

    -- ---- Guard 2: Project must exist (UC-03 pre-cond. 3) ----
    IF NOT EXISTS (SELECT 1 FROM Project WHERE project_id = @project_id)
    BEGIN
        RAISERROR(N'sp_purchase_order_create_flow: project %d does not exist', 16, 1, @project_id);
        RETURN;
    END

    -- ---- Guard 3: Project must have at least one BudgetLine (UC-03 pre-cond. 3) ----
    IF NOT EXISTS (SELECT 1 FROM BudgetLine WHERE project_id = @project_id)
    BEGIN
        RAISERROR(N'sp_purchase_order_create_flow: project %d has no BudgetLine', 16, 1, @project_id);
        RETURN;
    END

    -- ---- Guard 4: at least one PO line (UC-03 MSS step 5) ----
    IF NOT EXISTS (SELECT 1 FROM @lines)
    BEGIN
        RAISERROR(N'sp_purchase_order_create_flow: no line items supplied', 16, 1);
        RETURN;
    END

    -- ---- Guard 5: every line is valid ----
    IF EXISTS (SELECT 1 FROM @lines WHERE quantity <= 0 OR unitPrice < 0)
    BEGIN
        RAISERROR(N'sp_purchase_order_create_flow: every line must have quantity > 0 and unitPrice >= 0', 16, 1);
        RETURN;
    END

    -- ---- Compute totals from the lines (server-side -- not trusted from the client) ----
    DECLARE @subtotal DECIMAL(18,2);
    SELECT @subtotal = SUM(quantity * unitPrice) FROM @lines;
    DECLARE @vat_amount DECIMAL(18,2) = ROUND(@subtotal * @vat_rate, 2);
    DECLARE @total_amount DECIMAL(18,2) = @subtotal + @vat_amount;

    -- ---- Budget branch (UC-03 MSS step 8 / ext. 8a) -- a status branch, not a guard ----
    DECLARE @remaining_budget DECIMAL(18,2);
    SELECT @remaining_budget = SUM(plannedAmount - actualAmount)
    FROM BudgetLine
    WHERE project_id = @project_id;

    DECLARE @status NVARCHAR(30) =
        CASE WHEN @total_amount > @remaining_budget THEN N'PendingBudgetOverride'
             ELSE N'PendingPMApproval'
        END;

    -- ---- Write phase: PurchaseOrder + all PurchaseOrderLine rows, atomically ----
    BEGIN TRY
        BEGIN TRAN;

        -- everSubmitted = 1: this flow inserts straight into a Pending* status, never
        -- Draft, so by the time this row exists it has already (functionally) left Draft --
        -- the BR-1 guard cancel() reads must not treat it as "never submitted".
        INSERT INTO PurchaseOrder (
            purchase_order_id, poNumber, supplier_id, project_id,
            created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id,
            orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, everSubmitted
        )
        VALUES (
            @purchase_order_id, @poNumber, @supplier_id, @project_id,
            @created_by_employee_id, NULL, NULL,
            @orderDate, @total_amount, @vat_amount, @status, NULL, NULL, 1
        );

        INSERT INTO PurchaseOrderLine (
            purchase_order_line_id, purchase_order_id, description,
            unitOfMeasure, quantity, unitPrice, receivedQuantity
        )
        SELECT
            purchase_order_line_id, @purchase_order_id, description,
            unitOfMeasure, quantity, unitPrice, 0
        FROM @lines;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH

    SET @result_status = @status;
    SET @result_total_amount = @total_amount;
    SET @result_vat_amount = @vat_amount;
END
GO

CREATE PROCEDURE sp_report_project_profitability
    @date_from DATETIME2,
    @date_to DATETIME2,
    @project_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    WITH Revenue AS (
        SELECT pr.project_id, SUM(pr.amount) AS revenue
        FROM PaymentRequest pr
        WHERE pr.status IN (N'Approved', N'Paid') -- Paid comes after Approved; same rule as PaymentRequest.isApproved()
          AND pr.approvalDate >= @date_from
          AND pr.approvalDate <= @date_to
        GROUP BY pr.project_id
    ),
    Cost AS (
        SELECT bl.project_id, SUM(bl.actualAmount) AS actualCost
        FROM BudgetLine bl
        GROUP BY bl.project_id
    )
    SELECT
        p.project_id AS [מזהה_פרויקט],
        p.name AS [שם_פרויקט],
        ISNULL(r.revenue, 0) AS [הכנסה],
        ISNULL(c.actualCost, 0) AS [עלות_בפועל],
        ISNULL(r.revenue, 0) - ISNULL(c.actualCost, 0) AS [רווח_נטו],
        CASE WHEN ISNULL(r.revenue, 0) = 0 THEN NULL
             ELSE ROUND((ISNULL(r.revenue, 0) - ISNULL(c.actualCost, 0)) / r.revenue * 100, 2)
        END AS [אחוז_רווחיות]
    FROM Project p
    LEFT JOIN Revenue r ON r.project_id = p.project_id
    LEFT JOIN Cost c ON c.project_id = p.project_id
    WHERE (@project_id IS NULL OR p.project_id = @project_id)
      AND (r.revenue IS NOT NULL OR c.actualCost IS NOT NULL)
    ORDER BY p.project_id;
END
GO

-- ============================================================================
-- UC-05 report, second part: monthly cash flow (docs/00e-use-cases.md UC-05
-- MSS step 7, "monthly cash flow trends"). Same filters as
-- sp_report_project_profitability, one row per month that has any movement.
--
-- Cash in  = PaymentRequest.amount, status IN ('Approved','Paid'), by the month
--            of approvalDate (same rule as the profitability report and
--            PaymentRequest.isApproved()).
-- Cash out = PurchaseOrder.totalAmount for orders that passed PM approval and
--            were not cancelled -- Sent, PartiallyReceived, Received, or Archived
--            with closureReason 'Received' -- by the month of orderDate.
--            PurchaseOrder rather than SupplierPayment: SupplierPayment links to
--            a BusinessPartner only (class diagram #29), not to a Project, so it
--            could not honour the project filter.
-- Cumulative balance = running SUM of the monthly net (window function).
-- ============================================================================

CREATE PROCEDURE sp_report_monthly_cash_flow
    @date_from DATETIME2,
    @date_to DATETIME2,
    @project_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    WITH Movements AS (
        SELECT DATEFROMPARTS(YEAR(pr.approvalDate), MONTH(pr.approvalDate), 1) AS month_start,
               pr.amount AS cash_in,
               CAST(0 AS DECIMAL(18,2)) AS cash_out
        FROM PaymentRequest pr
        WHERE pr.status IN (N'Approved', N'Paid')
          AND pr.approvalDate >= @date_from
          AND pr.approvalDate <= @date_to
          AND (@project_id IS NULL OR pr.project_id = @project_id)
        UNION ALL
        SELECT DATEFROMPARTS(YEAR(po.orderDate), MONTH(po.orderDate), 1),
               CAST(0 AS DECIMAL(18,2)),
               po.totalAmount
        FROM PurchaseOrder po
        WHERE (po.status IN (N'Sent', N'PartiallyReceived', N'Received')
               OR (po.status = N'Archived' AND po.closureReason = N'Received'))
          AND po.orderDate >= @date_from
          AND po.orderDate <= @date_to
          AND (@project_id IS NULL OR po.project_id = @project_id)
    ),
    Monthly AS (
        SELECT month_start, SUM(cash_in) AS cash_in, SUM(cash_out) AS cash_out
        FROM Movements
        GROUP BY month_start
    )
    SELECT
        FORMAT(month_start, 'MM/yyyy') AS [חודש],
        cash_in AS [הכנסות],
        cash_out AS [הוצאות],
        cash_in - cash_out AS [תזרים_נטו],
        SUM(cash_in - cash_out) OVER (ORDER BY month_start ROWS UNBOUNDED PRECEDING) AS [יתרה_מצטברת]
    FROM Monthly
    ORDER BY month_start;
END
GO

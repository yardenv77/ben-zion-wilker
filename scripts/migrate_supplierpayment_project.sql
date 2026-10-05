-- Migration: add SupplierPayment.project_id -- relationship "Project 1 -- 0..* SupplierPayment".
-- UC-05's monthly cash flow has to filter cash out by project, and SupplierPayment was
-- linked only to the BusinessPartner it pays (class diagram #29), so a payment could not
-- be attributed to any project. Also adds four more paid invoices (rows 11-14) so the cash
-- flow has real cash out across several months, and switches sp_report_monthly_cash_flow's
-- cash out from approved purchase orders to paid supplier payments.
--
-- create_database.sql, stored_procedures.sql and seed_data.sql already carry the fixed
-- definitions and data for a fresh database; this script brings an EXISTING database up to
-- the same shape.
--
-- Run this once against the project database via the mssql MCP, then verify with:
--   SELECT supplier_payment_id, business_partner_id, project_id, status, paidDate FROM SupplierPayment;

ALTER TABLE SupplierPayment ADD project_id INT NULL;
GO

-- Same project per payment as seed_data.sql: a supplier's payment goes to the project of the
-- order it pays for; a subcontractor's to the project matching its trade.
UPDATE SupplierPayment SET project_id = CASE supplier_payment_id
    WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 WHEN 4 THEN 1 WHEN 5 THEN 4
    WHEN 6 THEN 3 WHEN 7 THEN 2 WHEN 8 THEN 2 WHEN 9 THEN 2 WHEN 10 THEN 1 END;
GO

ALTER TABLE SupplierPayment ALTER COLUMN project_id INT NOT NULL;
GO

ALTER TABLE SupplierPayment ADD CONSTRAINT FK_SupplierPayment_Project FOREIGN KEY (project_id)
    REFERENCES Project(project_id) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

INSERT INTO SupplierPayment (supplier_payment_id, invoiceNumber, business_partner_id, project_id, amount, dueDate, paidDate, status) VALUES
(11, N'INV-4584', 5, 3, 210000.00, '2025-11-30', '2025-11-28', N'Paid'),
(12, N'INV-4590', 4, 4, 43009.20, '2026-01-15', '2026-01-12', N'Paid'),
(13, N'INV-4596', 6, 3, 165000.00, '2026-01-31', '2026-01-29', N'Paid'),
(14, N'INV-4602', 8, 2, 48000.00, '2026-03-31', '2026-03-30', N'Paid');
GO

ALTER PROCEDURE sp_supplier_payment_create
    @supplier_payment_id INT,
    @invoiceNumber NVARCHAR(50),
    @business_partner_id INT,
    @project_id INT,
    @amount DECIMAL(10,2),
    @dueDate DATETIME2,
    @paidDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    INSERT INTO SupplierPayment (supplier_payment_id, invoiceNumber, business_partner_id, project_id, amount, dueDate, paidDate, status)
    VALUES (@supplier_payment_id, @invoiceNumber, @business_partner_id, @project_id, @amount, @dueDate, @paidDate, @status);
END
GO

ALTER PROCEDURE sp_supplier_payment_update
    @supplier_payment_id INT,
    @invoiceNumber NVARCHAR(50),
    @business_partner_id INT,
    @project_id INT,
    @amount DECIMAL(10,2),
    @dueDate DATETIME2,
    @paidDate DATETIME2,
    @status NVARCHAR(20)
AS
BEGIN
    UPDATE SupplierPayment
    SET invoiceNumber = @invoiceNumber,
        business_partner_id = @business_partner_id,
        project_id = @project_id,
        amount = @amount,
        dueDate = @dueDate,
        paidDate = @paidDate,
        status = @status
    WHERE supplier_payment_id = @supplier_payment_id;
END
GO

ALTER PROCEDURE sp_supplier_payment_get_all
AS
BEGIN
    SELECT supplier_payment_id, invoiceNumber, business_partner_id, amount, dueDate, paidDate, status, project_id
    FROM SupplierPayment;
END
GO

ALTER PROCEDURE sp_supplier_payment_get_by_id
    @supplier_payment_id INT
AS
BEGIN
    SELECT supplier_payment_id, invoiceNumber, business_partner_id, amount, dueDate, paidDate, status, project_id
    FROM SupplierPayment
    WHERE supplier_payment_id = @supplier_payment_id;
END
GO

ALTER PROCEDURE sp_report_monthly_cash_flow
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
        SELECT DATEFROMPARTS(YEAR(sp.paidDate), MONTH(sp.paidDate), 1),
               CAST(0 AS DECIMAL(18,2)),
               sp.amount
        FROM SupplierPayment sp
        WHERE sp.status = N'Paid'
          AND sp.paidDate >= @date_from
          AND sp.paidDate <= @date_to
          AND (@project_id IS NULL OR sp.project_id = @project_id)
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

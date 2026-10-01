-- Migration: persist PurchaseOrder.everSubmitted (previously an in-memory-only BR-1 guard
-- field -- "has this order ever left Draft?" -- used by cancel() to decide whether to hard-
-- delete a never-submitted Draft or mark a previously-submitted one Cancelled instead. Like
-- rejectedAt/archivedAt before it, this reset to false on every app restart, so a PO that had
-- genuinely been submitted (then withdrawn or revised back to Draft) would be wrongly
-- hard-deleted by cancel() after any restart instead of being marked Cancelled.
--
-- create_database.sql and stored_procedures.sql already carry the fixed definitions for a
-- fresh database; this script brings an EXISTING database up to the same shape.
--
-- Run this once against the project database via the mssql MCP, then verify with:
--   SELECT purchase_order_id, status, everSubmitted FROM PurchaseOrder ORDER BY purchase_order_id;

ALTER TABLE PurchaseOrder ADD
    everSubmitted BIT NOT NULL CONSTRAINT DF_PurchaseOrder_everSubmitted DEFAULT 0;
GO

-- Backfill: every row NOT currently in Draft has, by definition, left Draft at least once.
-- Rows currently in Draft keep the column's default (0) -- correct for a row that was truly
-- never submitted; a Draft row that had previously been submitted-then-withdrawn-or-revised
-- can't be distinguished from one that never left Draft using only the data this migration
-- has access to, so this is the best available backfill, not a guess the app will keep making
-- going forward (submit() now persists the column itself).
UPDATE PurchaseOrder SET everSubmitted = 1 WHERE status <> N'Draft';
GO

ALTER PROCEDURE sp_purchase_order_create
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
    @everSubmitted BIT
AS
BEGIN
    INSERT INTO PurchaseOrder (purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted)
    VALUES (@purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @approved_by_employee_id, @override_approved_by_employee_id, @orderDate, @totalAmount, @vatAmount, @status, @rejectionReason, @closureReason, @rejectedAt, @archivedAt, @everSubmitted);
END
GO

ALTER PROCEDURE sp_purchase_order_get_all
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted
    FROM PurchaseOrder;
END
GO

ALTER PROCEDURE sp_purchase_order_get_by_id
    @purchase_order_id INT
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted
    FROM PurchaseOrder
    WHERE purchase_order_id = @purchase_order_id;
END
GO

ALTER PROCEDURE sp_purchase_order_submit
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

ALTER PROCEDURE sp_purchase_order_create_flow
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

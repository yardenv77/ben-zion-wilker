-- Migration: add PurchaseOrder.rejectedBy -- records WHICH role actually rejected an order
-- (the ProjectManager from PendingPMApproval, or the CEO from PendingBudgetOverride). Two
-- distinct transitions converge on Rejected (design/state-diagram.html), and without this
-- column there was no way to tell them apart after the fact, unlike the accept-path's
-- approvedBy/overrideApprovedBy, which already distinguish PM vs. CEO.
--
-- create_database.sql and stored_procedures.sql already carry the fixed definitions for a
-- fresh database; this script brings an EXISTING database up to the same shape.
--
-- Run this once against the project database via the mssql MCP, then verify with:
--   SELECT purchase_order_id, status, rejectionReason, rejected_by_employee_id FROM PurchaseOrder WHERE status = N'Rejected';

ALTER TABLE PurchaseOrder ADD
    rejected_by_employee_id INT NULL
        CONSTRAINT FK_PurchaseOrder_RejectedBy FOREIGN KEY REFERENCES Employee(employee_id);
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
    @everSubmitted BIT,
    @rejected_by_employee_id INT
AS
BEGIN
    INSERT INTO PurchaseOrder (purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted, rejected_by_employee_id)
    VALUES (@purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @approved_by_employee_id, @override_approved_by_employee_id, @orderDate, @totalAmount, @vatAmount, @status, @rejectionReason, @closureReason, @rejectedAt, @archivedAt, @everSubmitted, @rejected_by_employee_id);
END
GO

ALTER PROCEDURE sp_purchase_order_get_all
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted, rejected_by_employee_id
    FROM PurchaseOrder;
END
GO

ALTER PROCEDURE sp_purchase_order_get_by_id
    @purchase_order_id INT
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, everSubmitted, rejected_by_employee_id
    FROM PurchaseOrder
    WHERE purchase_order_id = @purchase_order_id;
END
GO

ALTER PROCEDURE sp_purchase_order_reject
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

ALTER PROCEDURE sp_purchase_order_revise
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

-- Backfill: the one existing Rejected row (id=5, per earlier verification this session)
-- predates this column and can't be attributed retroactively with certainty -- left NULL
-- rather than guessed. Going forward, every new rejection fills it in via reject().

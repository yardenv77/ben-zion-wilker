-- Migration: persist PurchaseOrder.rejectedAt / archivedAt (previously in-memory-only
-- guard fields for the BR-3 14-day auto-cancel guard and the 7-year purge() retention
-- guard -- both reset to null on every app restart, silently disabling both guards
-- after a restart). create_database.sql and stored_procedures.sql already carry the
-- fixed definitions for a fresh database; this script brings an EXISTING database
-- (already created and seeded) up to the same shape, since CREATE TABLE/CREATE
-- PROCEDURE cannot simply be re-run against objects that already exist.
--
-- Run this once against the project database via the mssql MCP, then verify with:
--   EXEC sp_help 'PurchaseOrder';
--   SELECT purchase_order_id, status, rejectionReason, rejectedAt, closureReason, archivedAt FROM PurchaseOrder;

ALTER TABLE PurchaseOrder ADD
    rejectedAt DATETIME2 NULL,
    archivedAt DATETIME2 NULL;
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
    @archivedAt DATETIME2
AS
BEGIN
    INSERT INTO PurchaseOrder (purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt)
    VALUES (@purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @approved_by_employee_id, @override_approved_by_employee_id, @orderDate, @totalAmount, @vatAmount, @status, @rejectionReason, @closureReason, @rejectedAt, @archivedAt);
END
GO

ALTER PROCEDURE sp_purchase_order_get_all
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt
    FROM PurchaseOrder;
END
GO

ALTER PROCEDURE sp_purchase_order_get_by_id
    @purchase_order_id INT
AS
BEGIN
    SELECT purchase_order_id, poNumber, supplier_id, project_id, created_by_employee_id, approved_by_employee_id, override_approved_by_employee_id, orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt
    FROM PurchaseOrder
    WHERE purchase_order_id = @purchase_order_id;
END
GO

ALTER PROCEDURE sp_purchase_order_reject
    @purchase_order_id INT,
    @rejectionReason NVARCHAR(MAX),
    @rejectedAt DATETIME2
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN;
        UPDATE PurchaseOrder
        SET status = N'Rejected',
            rejectionReason = @rejectionReason,
            rejectedAt = @rejectedAt
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
            rejectedAt = NULL
        WHERE purchase_order_id = @purchase_order_id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

ALTER PROCEDURE sp_purchase_order_archive
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

-- Backfill for rows already in the Rejected or Archived state before this migration
-- ran (their rejectedAt/archivedAt would otherwise stay NULL forever, since those
-- columns are only set going forward by reject()/archive()). Approximated with the
-- current time, since the real transition time was never recorded -- this is the best
-- available value, and it only affects pre-existing rows, not rows created from now on.
UPDATE PurchaseOrder SET rejectedAt = SYSDATETIME() WHERE status = N'Rejected' AND rejectedAt IS NULL;
UPDATE PurchaseOrder SET archivedAt = SYSDATETIME() WHERE status = N'Archived' AND archivedAt IS NULL;
GO

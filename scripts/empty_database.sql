-- ============================================================================
-- empty_database.sql
-- Ben Tzion Wilker (1987) Ltd. Project Management System
--
-- Empties every table's DATA while keeping the schema itself intact (no
-- DROP/CREATE here -- see drop_database.sql / create_database.sql for that).
-- Useful for resetting to a clean, empty database before re-running
-- seed_data.sql, without having to rebuild the schema from scratch.
--
-- Deletes in the EXACT REVERSE of the creation order in create_database.sql
-- (children before parents): every FK in this schema is declared
-- ON DELETE NO ACTION, so SQL Server refuses a DELETE that would orphan a
-- child row -- deleting parents first would fail with an FK violation.
--
-- Run order relative to the other scripts in this folder:
--   1. (schema already exists from a previous create_database.sql run)
--   2. empty_database.sql   <-- this script (clears all data)
--   3. seed_data.sql        (optional: repopulate with seed data)
-- ============================================================================

-- ============================================================================
-- PHASE 3 -- Association / link classes (deleted first: nothing depends on them)
-- ============================================================================

DELETE FROM EquipmentAssignment;
GO

DELETE FROM EquipmentUsage;
GO

DELETE FROM Attendance;
GO

DELETE FROM SupplierPriceQuote;
GO

-- ============================================================================
-- PHASE 2 -- Entities with FK references (reverse order)
-- ============================================================================

DELETE FROM SupplierPayment;
GO

DELETE FROM PurchaseOrderLine;
GO

DELETE FROM PurchaseOrder;
GO

DELETE FROM SubmittedDocument;
GO

DELETE FROM PaymentRequest;
GO

DELETE FROM BudgetLine;
GO

DELETE FROM InsurancePolicy;
GO

DELETE FROM BankGuarantee;
GO

DELETE FROM FinancialSecurity;
GO

DELETE FROM Project;
GO

DELETE FROM Tender;
GO

-- ============================================================================
-- PHASE 1 -- Base entities (reverse order)
-- ============================================================================

DELETE FROM DailyWorkLog;
GO

DELETE FROM Subcontractor;
GO

DELETE FROM Supplier;
GO

DELETE FROM BusinessPartner;
GO

DELETE FROM Equipment;
GO

DELETE FROM Employee;
GO

DELETE FROM TradeCategory;
GO

DELETE FROM Client;
GO

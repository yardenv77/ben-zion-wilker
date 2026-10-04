-- ============================================================================
-- drop_database.sql
-- Ben Tzion Wilker (1987) Ltd. Project Management System
--
-- Drops every table created by create_database.sql -- the ENTIRE schema,
-- not just its data. This is IRREVERSIBLE: once run, nothing from this
-- database remains. create_database.sql (+ stored_procedures.sql +
-- seed_data.sql) must be re-run afterward to restore the database to a
-- usable state.
--
-- Drops in the EXACT REVERSE of the creation order in create_database.sql
-- (children before parents): every FK in this schema is declared
-- ON DELETE NO ACTION, so dropping a parent table before its child's FK
-- is removed would fail -- dropping the child table first removes the FK
-- along with it, so parents can safely be dropped afterward.
--
-- Run order relative to the other scripts in this folder:
--   1. drop_database.sql       <-- this script (drops everything -- irreversible)
--   2. create_database.sql     (rebuilds the schema from scratch)
--   3. stored_procedures.sql   (recreates all stored procedures)
--   4. seed_data.sql           (optional: repopulate with seed data)
-- ============================================================================

-- ============================================================================
-- PHASE 3 -- Association / link classes (dropped first: nothing depends on them)
-- ============================================================================

DROP TABLE EquipmentAssignment;
GO

DROP TABLE EquipmentUsage;
GO

DROP TABLE Attendance;
GO

DROP TABLE SupplierPriceQuote;
GO

-- ============================================================================
-- PHASE 2 -- Entities with FK references (reverse order)
-- ============================================================================

DROP TABLE SupplierPayment;
GO

DROP TABLE PurchaseOrderLine;
GO

DROP TABLE PurchaseOrder;
GO

DROP TABLE SubmittedDocument;
GO

DROP TABLE PaymentRequest;
GO

DROP TABLE BudgetLine;
GO

DROP TABLE InsurancePolicy;
GO

DROP TABLE BankGuarantee;
GO

DROP TABLE FinancialSecurity;
GO

DROP TABLE Project;
GO

DROP TABLE Tender;
GO

-- ============================================================================
-- PHASE 1 -- Base entities (reverse order)
-- ============================================================================

DROP TABLE DailyWorkLog;
GO

DROP TABLE Subcontractor;
GO

DROP TABLE Supplier;
GO

DROP TABLE BusinessPartner;
GO

DROP TABLE Equipment;
GO

DROP TABLE Employee;
GO

DROP TABLE TradeCategory;
GO

DROP TABLE Client;
GO

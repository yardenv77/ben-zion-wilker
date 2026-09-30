using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BenZionVilker
{
    static class Program
    {
        // =====================================================================
        // In-memory lists -- one static list per domain entity, per PATTERNS.md.
        // Supplier/Subcontractor share Program.BusinessPartners (BusinessPartner
        // is the table-per-subclass base); BankGuarantee/InsurancePolicy share
        // Program.FinancialSecurities the same way.
        // =====================================================================

        // Phase 1 -- base entities
        public static List<Client> Clients;
        public static List<TradeCategory> TradeCategories;
        public static List<Employee> Employees;
        public static List<Equipment> Equipments;
        public static List<BusinessPartner> BusinessPartners;
        public static List<DailyWorkLog> DailyWorkLogs;

        // Phase 2 -- entities with FK references
        public static List<Tender> Tenders;
        public static List<Project> Projects;
        public static List<FinancialSecurity> FinancialSecurities;   // FK: Project (moved from Phase 1)
        public static List<BudgetLine> BudgetLines;
        public static List<PaymentRequest> PaymentRequests;
        public static List<SubmittedDocument> SubmittedDocuments;
        public static List<PurchaseOrder> PurchaseOrders;
        public static List<PurchaseOrderLine> PurchaseOrderLines;
        public static List<SupplierPayment> SupplierPayments;

        // Phase 3 -- association / link classes
        public static List<SupplierPriceQuote> SupplierPriceQuotes;
        public static List<Attendance> Attendances;
        public static List<EquipmentUsage> EquipmentUsages;
        public static List<EquipmentAssignment> EquipmentAssignments;

        // =====================================================================
        // Loads every list from the database, in the load order documented in
        // CLAUDE.md ("Domain Entities and Load Order"): base entities first,
        // then entities with FK references, then association classes last.
        // =====================================================================
        public static void initLists()
        {
            // Phase 1
            Client.initClients();
            TradeCategory.initTradeCategories();
            Employee.initEmployees();
            Equipment.initEquipments();
            BusinessPartner.initBusinessPartners();       // Supplier / Subcontractor
            DailyWorkLog.initDailyWorkLogs();              // FK: Subcontractor, Employee

            // Phase 2
            Tender.initTenders();                          // FK: Client
            Project.initProjects();                        // FK: Tender, Employee
            FinancialSecurity.initFinancialSecurities();   // BankGuarantee / InsurancePolicy; FK: Project
            BudgetLine.initBudgetLines();                  // FK: Project
            PaymentRequest.initPaymentRequests();          // FK: Project
            SubmittedDocument.initSubmittedDocuments();     // FK: PaymentRequest
            PurchaseOrder.initPurchaseOrders();             // FK: Supplier, Project, Employee
            PurchaseOrderLine.initPurchaseOrderLines();     // FK: PurchaseOrder
            SupplierPayment.initSupplierPayments();         // FK: BusinessPartner

            // Phase 3
            SupplierPriceQuote.initSupplierPriceQuotes();   // Supplier + Tender + TradeCategory
            Attendance.initAttendances();                   // Employee + DailyWorkLog
            EquipmentUsage.initEquipmentUsages();            // Equipment + DailyWorkLog
            EquipmentAssignment.initEquipmentAssignments();  // Project + Equipment
        }

        // =====================================================================
        // Entry point. No credential-holding entity exists in the domain model
        // (see CLAUDE.md, "Entry Flow"), so mainForm opens directly to
        // MainMenuPanel -- there is no LoginPanel.
        //
        // initLists() is wrapped in a retry loop: SQL_CON.execute_query no longer
        // swallows connection failures (see its own comment), so a transient DB
        // blip during startup now surfaces here as a clear Hebrew message with a
        // retry option, instead of crashing on an unhandled NullReferenceException
        // the moment the first initXxx() call hit while(rdr.Read()) on a null reader.
        // =====================================================================
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            while (true)
            {
                try
                {
                    initLists();
                    break;
                }
                catch (Exception ex)
                {
                    DialogResult choice = MessageBox.Show(
                        "טעינת הנתונים מבסיס הנתונים נכשלה:\n" + ex.Message +
                        "\n\nיש לבדוק את החיבור לרשת ולנסות שוב.",
                        "שגיאת התחברות לבסיס הנתונים",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (choice != DialogResult.Retry)
                        return; // יציאה נקייה -- לא ממשיכים עם רשימות חלקיות/ריקות
                }
            }

            Application.Run(new mainForm());
        }
    }
}

using System;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// תפריט הבית של המערכת. אין מסך Login (ראו CLAUDE.md, "Entry Flow") —
    /// זהו המסך הראשון שנטען. כפתורי UC-01/03/04/05/06 מחוברים למסכים אמיתיים; UC-05
    /// (דוח) מחובר למסך קריאה-בלבד (ProjectProfitabilityReportPanel), לא מסך CRUD.
    /// שאר הכפתורים (Client, TradeCategory, Equipment,
    /// Subcontractor, Tender, Project, BudgetLine, PaymentRequest, PurchaseOrderLine,
    /// SupplierPayment, SupplierPriceQuote, Attendance, EquipmentUsage, EquipmentAssignment)
    /// מחוברים למסכי CRUD שנוספו עבור ישויות ללא UC מפורט, לפי בקשת המשתמש להשלים מסך לכל
    /// ישות במודל התחום המלא (design/class-diagram.md).
    /// </summary>
    public partial class MainMenuPanel : UserControl
    {
        public MainMenuPanel()
        {
            InitializeComponent();

            Theme.ApplyPanelBackground(this);
            Theme.ApplyTitle(label_title);
            Theme.CenterHorizontally(label_title, this.Width);

            // Column width (280) matches the button block's own Size -- see MainMenuPanel.Designer.cs.
            const int columnWidth = 280;
            Theme.ApplySectionLabel(label_sectionProcurement, Theme.CategoryProcurement);
            Theme.CenterHorizontally(label_sectionProcurement, 30, columnWidth);
            Theme.ApplySectionLabel(label_sectionFieldWork, Theme.CategoryField);
            Theme.CenterHorizontally(label_sectionFieldWork, 360, columnWidth);
            Theme.ApplySectionLabel(label_sectionFinance, Theme.CategoryFinance);
            Theme.CenterHorizontally(label_sectionFinance, 690, columnWidth);

            // Column 1 (x=30): Procurement -- Purchasing Manager / Accountant
            foreach (Button b in new[] { button_manageSuppliers, button_manageSubcontractors, button_manageTradeCategories,
                button_createPurchaseOrder, button_managePurchaseOrderLines, button_manageSupplierPayments, button_manageSupplierPriceQuotes })
                Theme.ApplyMenuButton(b, Theme.CategoryProcurement, Theme.CategoryProcurementBg);

            // Column 2 (x=360): Field execution -- Site Supervisor / Equipment Manager / Tender Coordinator
            foreach (Button b in new[] { button_manageEmployees, button_dailyWorkLog, button_manageAttendance,
                button_manageEquipment, button_manageEquipmentUsage, button_manageEquipmentAssignments, button_manageTenders })
                Theme.ApplyMenuButton(b, Theme.CategoryField, Theme.CategoryFieldBg);

            // Column 3 (x=690): Finance & projects -- Project Manager / CEO / Finance Officer
            foreach (Button b in new[] { button_manageProjects, button_manageClients, button_manageBudgetLines,
                button_managePaymentRequests, button_profitabilityReport, button_manageSecurities })
                Theme.ApplyMenuButton(b, Theme.CategoryFinance, Theme.CategoryFinanceBg);
        }

        // UC-01: Manage Supplier (Purchasing Manager)
        private void button_manageSuppliers_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SupplierPanel());
        }

        // Purchasing Manager -- no separate UC spec (BusinessPartner subclass, same as Supplier)
        private void button_manageSubcontractors_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SubcontractorPanel());
        }

        // Purchasing Manager -- no separate UC spec
        private void button_manageTradeCategories_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new TradeCategoryPanel());
        }

        // UC-02: Manage Employee (Site Supervisor)
        private void button_manageEmployees_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new EmployeePanel());
        }

        // UC-03: Create Purchase Order (Accountant)
        private void button_createPurchaseOrder_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new PurchaseOrderPanel());
        }

        // Accountant -- no separate UC spec (line items of UC-03's Purchase Order)
        private void button_managePurchaseOrderLines_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new PurchaseOrderLinePanel());
        }

        // Accountant -- no separate UC spec
        private void button_manageSupplierPayments_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SupplierPaymentPanel());
        }

        // Purchasing Manager -- no separate UC spec (ternary association class)
        private void button_manageSupplierPriceQuotes_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new SupplierPriceQuotePanel());
        }

        // UC-04: Record Daily Work Log (Site Supervisor)
        private void button_dailyWorkLog_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new DailyWorkLogPanel());
        }

        // Site Supervisor -- no separate UC spec (Employee x DailyWorkLog association)
        private void button_manageAttendance_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new AttendancePanel());
        }

        // Equipment Manager -- no separate UC spec
        private void button_manageEquipment_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new EquipmentPanel());
        }

        // Equipment Manager -- no separate UC spec (Equipment x DailyWorkLog association)
        private void button_manageEquipmentUsage_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new EquipmentUsagePanel());
        }

        // Equipment Manager -- no separate UC spec (Project x Equipment link class)
        private void button_manageEquipmentAssignments_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new EquipmentAssignmentPanel());
        }

        // Tender Coordinator -- no separate UC spec
        private void button_manageTenders_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new TenderPanel());
        }

        // Project Manager -- no separate UC spec
        private void button_manageProjects_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new ProjectPanel());
        }

        // Tender Coordinator -- no separate UC spec
        private void button_manageClients_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new ClientPanel());
        }

        // Project Manager -- no separate UC spec
        private void button_manageBudgetLines_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new BudgetLinePanel());
        }

        // Project Manager -- no separate UC spec
        private void button_managePaymentRequests_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new PaymentRequestPanel());
        }

        // UC-05: Generate Profitability & Cash Flow Report (CEO) -- read-only report (step 8.3)
        private void button_profitabilityReport_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new ProjectProfitabilityReportPanel());
        }

        // UC-06: Manage Guarantee & Insurance (Finance Officer)
        private void button_manageSecurities_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new FinancialSecurityPanel());
        }
    }
}

namespace BenZionVilker
{
    partial class MainMenuPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.label_title = new System.Windows.Forms.Label();
            this.label_sectionProcurement = new System.Windows.Forms.Label();
            this.label_sectionFieldWork = new System.Windows.Forms.Label();
            this.label_sectionFinance = new System.Windows.Forms.Label();
            this.button_manageSuppliers = new System.Windows.Forms.Button();
            this.button_manageSubcontractors = new System.Windows.Forms.Button();
            this.button_manageTradeCategories = new System.Windows.Forms.Button();
            this.button_createPurchaseOrder = new System.Windows.Forms.Button();
            this.button_managePurchaseOrderLines = new System.Windows.Forms.Button();
            this.button_manageSupplierPayments = new System.Windows.Forms.Button();
            this.button_manageSupplierPriceQuotes = new System.Windows.Forms.Button();
            this.button_manageEmployees = new System.Windows.Forms.Button();
            this.button_dailyWorkLog = new System.Windows.Forms.Button();
            this.button_manageAttendance = new System.Windows.Forms.Button();
            this.button_manageEquipment = new System.Windows.Forms.Button();
            this.button_manageEquipmentUsage = new System.Windows.Forms.Button();
            this.button_manageEquipmentAssignments = new System.Windows.Forms.Button();
            this.button_manageTenders = new System.Windows.Forms.Button();
            this.button_manageProjects = new System.Windows.Forms.Button();
            this.button_manageClients = new System.Windows.Forms.Button();
            this.button_manageBudgetLines = new System.Windows.Forms.Button();
            this.button_managePaymentRequests = new System.Windows.Forms.Button();
            this.button_profitabilityReport = new System.Windows.Forms.Button();
            this.button_manageSecurities = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(330, 15);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(340, 44);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "תפריט ראשי";
            //
            // label_sectionProcurement
            //
            this.label_sectionProcurement.AutoSize = true;
            this.label_sectionProcurement.Location = new System.Drawing.Point(30,72);
            this.label_sectionProcurement.Name = "label_sectionProcurement";
            this.label_sectionProcurement.Size = new System.Drawing.Size(120, 21);
            this.label_sectionProcurement.TabIndex = 21;
            this.label_sectionProcurement.Text = "רכש וספקים";
            //
            // label_sectionFieldWork
            //
            this.label_sectionFieldWork.AutoSize = true;
            this.label_sectionFieldWork.Location = new System.Drawing.Point(360,72);
            this.label_sectionFieldWork.Name = "label_sectionFieldWork";
            this.label_sectionFieldWork.Size = new System.Drawing.Size(150, 21);
            this.label_sectionFieldWork.TabIndex = 22;
            this.label_sectionFieldWork.Text = "עבודה בשטח וציוד";
            //
            // label_sectionFinance
            //
            this.label_sectionFinance.AutoSize = true;
            this.label_sectionFinance.Location = new System.Drawing.Point(690,72);
            this.label_sectionFinance.Name = "label_sectionFinance";
            this.label_sectionFinance.Size = new System.Drawing.Size(140, 21);
            this.label_sectionFinance.TabIndex = 23;
            this.label_sectionFinance.Text = "פרויקטים וכספים";
            //
            // Column 1 (x=40): Procurement -- Purchasing Manager / Accountant
            //
            // button_manageSuppliers (UC-01)
            //
            this.button_manageSuppliers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageSuppliers.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageSuppliers.Location = new System.Drawing.Point(30,100);
            this.button_manageSuppliers.Name = "button_manageSuppliers";
            this.button_manageSuppliers.Size = new System.Drawing.Size(280, 50);
            this.button_manageSuppliers.TabIndex = 1;
            this.button_manageSuppliers.Text = "ניהול ספקים";
            this.button_manageSuppliers.UseVisualStyleBackColor = true;
            this.button_manageSuppliers.Click += new System.EventHandler(this.button_manageSuppliers_Click);
            //
            // button_manageSubcontractors
            //
            this.button_manageSubcontractors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageSubcontractors.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageSubcontractors.Location = new System.Drawing.Point(30,158);
            this.button_manageSubcontractors.Name = "button_manageSubcontractors";
            this.button_manageSubcontractors.Size = new System.Drawing.Size(280, 50);
            this.button_manageSubcontractors.TabIndex = 2;
            this.button_manageSubcontractors.Text = "ניהול קבלני משנה";
            this.button_manageSubcontractors.UseVisualStyleBackColor = true;
            this.button_manageSubcontractors.Click += new System.EventHandler(this.button_manageSubcontractors_Click);
            //
            // button_manageTradeCategories
            //
            this.button_manageTradeCategories.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageTradeCategories.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageTradeCategories.Location = new System.Drawing.Point(30,216);
            this.button_manageTradeCategories.Name = "button_manageTradeCategories";
            this.button_manageTradeCategories.Size = new System.Drawing.Size(280, 50);
            this.button_manageTradeCategories.TabIndex = 3;
            this.button_manageTradeCategories.Text = "ניהול תחומי עיסוק";
            this.button_manageTradeCategories.UseVisualStyleBackColor = true;
            this.button_manageTradeCategories.Click += new System.EventHandler(this.button_manageTradeCategories_Click);
            //
            // button_createPurchaseOrder (UC-03)
            //
            this.button_createPurchaseOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_createPurchaseOrder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_createPurchaseOrder.Location = new System.Drawing.Point(30,274);
            this.button_createPurchaseOrder.Name = "button_createPurchaseOrder";
            this.button_createPurchaseOrder.Size = new System.Drawing.Size(280, 50);
            this.button_createPurchaseOrder.TabIndex = 4;
            this.button_createPurchaseOrder.Text = "יצירת הזמנת רכש";
            this.button_createPurchaseOrder.UseVisualStyleBackColor = true;
            this.button_createPurchaseOrder.Click += new System.EventHandler(this.button_createPurchaseOrder_Click);
            //
            // button_managePurchaseOrderLines
            //
            this.button_managePurchaseOrderLines.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_managePurchaseOrderLines.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_managePurchaseOrderLines.Location = new System.Drawing.Point(30,332);
            this.button_managePurchaseOrderLines.Name = "button_managePurchaseOrderLines";
            this.button_managePurchaseOrderLines.Size = new System.Drawing.Size(280, 50);
            this.button_managePurchaseOrderLines.TabIndex = 5;
            this.button_managePurchaseOrderLines.Text = "שורות הזמנת רכש";
            this.button_managePurchaseOrderLines.UseVisualStyleBackColor = true;
            this.button_managePurchaseOrderLines.Click += new System.EventHandler(this.button_managePurchaseOrderLines_Click);
            //
            // button_manageSupplierPayments
            //
            this.button_manageSupplierPayments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageSupplierPayments.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageSupplierPayments.Location = new System.Drawing.Point(30,390);
            this.button_manageSupplierPayments.Name = "button_manageSupplierPayments";
            this.button_manageSupplierPayments.Size = new System.Drawing.Size(280, 50);
            this.button_manageSupplierPayments.TabIndex = 6;
            this.button_manageSupplierPayments.Text = "תשלומים לספקים";
            this.button_manageSupplierPayments.UseVisualStyleBackColor = true;
            this.button_manageSupplierPayments.Click += new System.EventHandler(this.button_manageSupplierPayments_Click);
            //
            // button_manageSupplierPriceQuotes
            //
            this.button_manageSupplierPriceQuotes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageSupplierPriceQuotes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageSupplierPriceQuotes.Location = new System.Drawing.Point(30,448);
            this.button_manageSupplierPriceQuotes.Name = "button_manageSupplierPriceQuotes";
            this.button_manageSupplierPriceQuotes.Size = new System.Drawing.Size(280, 50);
            this.button_manageSupplierPriceQuotes.TabIndex = 7;
            this.button_manageSupplierPriceQuotes.Text = "הצעות מחיר מספקים";
            this.button_manageSupplierPriceQuotes.UseVisualStyleBackColor = true;
            this.button_manageSupplierPriceQuotes.Click += new System.EventHandler(this.button_manageSupplierPriceQuotes_Click);
            //
            // Column 2 (x=370): Field execution -- Site Supervisor / Equipment Manager / Tender Coordinator
            //
            // button_manageEmployees (UC-02)
            //
            this.button_manageEmployees.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageEmployees.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageEmployees.Location = new System.Drawing.Point(360,100);
            this.button_manageEmployees.Name = "button_manageEmployees";
            this.button_manageEmployees.Size = new System.Drawing.Size(280, 50);
            this.button_manageEmployees.TabIndex = 8;
            this.button_manageEmployees.Text = "ניהול עובדים";
            this.button_manageEmployees.UseVisualStyleBackColor = true;
            this.button_manageEmployees.Click += new System.EventHandler(this.button_manageEmployees_Click);
            //
            // button_dailyWorkLog (UC-04)
            //
            this.button_dailyWorkLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_dailyWorkLog.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_dailyWorkLog.Location = new System.Drawing.Point(360,158);
            this.button_dailyWorkLog.Name = "button_dailyWorkLog";
            this.button_dailyWorkLog.Size = new System.Drawing.Size(280, 50);
            this.button_dailyWorkLog.TabIndex = 9;
            this.button_dailyWorkLog.Text = "יומן עבודה יומי";
            this.button_dailyWorkLog.UseVisualStyleBackColor = true;
            this.button_dailyWorkLog.Click += new System.EventHandler(this.button_dailyWorkLog_Click);
            //
            // button_manageAttendance
            //
            this.button_manageAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageAttendance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageAttendance.Location = new System.Drawing.Point(360,216);
            this.button_manageAttendance.Name = "button_manageAttendance";
            this.button_manageAttendance.Size = new System.Drawing.Size(280, 50);
            this.button_manageAttendance.TabIndex = 10;
            this.button_manageAttendance.Text = "נוכחות עובדים";
            this.button_manageAttendance.UseVisualStyleBackColor = true;
            this.button_manageAttendance.Click += new System.EventHandler(this.button_manageAttendance_Click);
            //
            // button_manageEquipment
            //
            this.button_manageEquipment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageEquipment.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageEquipment.Location = new System.Drawing.Point(360,274);
            this.button_manageEquipment.Name = "button_manageEquipment";
            this.button_manageEquipment.Size = new System.Drawing.Size(280, 50);
            this.button_manageEquipment.TabIndex = 11;
            this.button_manageEquipment.Text = "ניהול ציוד";
            this.button_manageEquipment.UseVisualStyleBackColor = true;
            this.button_manageEquipment.Click += new System.EventHandler(this.button_manageEquipment_Click);
            //
            // button_manageEquipmentUsage
            //
            this.button_manageEquipmentUsage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageEquipmentUsage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageEquipmentUsage.Location = new System.Drawing.Point(360,332);
            this.button_manageEquipmentUsage.Name = "button_manageEquipmentUsage";
            this.button_manageEquipmentUsage.Size = new System.Drawing.Size(280, 50);
            this.button_manageEquipmentUsage.TabIndex = 12;
            this.button_manageEquipmentUsage.Text = "שימוש בציוד";
            this.button_manageEquipmentUsage.UseVisualStyleBackColor = true;
            this.button_manageEquipmentUsage.Click += new System.EventHandler(this.button_manageEquipmentUsage_Click);
            //
            // button_manageEquipmentAssignments
            //
            this.button_manageEquipmentAssignments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageEquipmentAssignments.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageEquipmentAssignments.Location = new System.Drawing.Point(360,390);
            this.button_manageEquipmentAssignments.Name = "button_manageEquipmentAssignments";
            this.button_manageEquipmentAssignments.Size = new System.Drawing.Size(280, 50);
            this.button_manageEquipmentAssignments.TabIndex = 13;
            this.button_manageEquipmentAssignments.Text = "שיבוץ ציוד לפרויקטים";
            this.button_manageEquipmentAssignments.UseVisualStyleBackColor = true;
            this.button_manageEquipmentAssignments.Click += new System.EventHandler(this.button_manageEquipmentAssignments_Click);
            //
            // button_manageTenders
            //
            this.button_manageTenders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageTenders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageTenders.Location = new System.Drawing.Point(360,448);
            this.button_manageTenders.Name = "button_manageTenders";
            this.button_manageTenders.Size = new System.Drawing.Size(280, 50);
            this.button_manageTenders.TabIndex = 14;
            this.button_manageTenders.Text = "ניהול מכרזים";
            this.button_manageTenders.UseVisualStyleBackColor = true;
            this.button_manageTenders.Click += new System.EventHandler(this.button_manageTenders_Click);
            //
            // Column 3 (x=700): Finance & projects -- Project Manager / CEO / Finance Officer
            //
            // button_manageProjects
            //
            this.button_manageProjects.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageProjects.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageProjects.Location = new System.Drawing.Point(690,100);
            this.button_manageProjects.Name = "button_manageProjects";
            this.button_manageProjects.Size = new System.Drawing.Size(280, 50);
            this.button_manageProjects.TabIndex = 15;
            this.button_manageProjects.Text = "ניהול פרויקטים";
            this.button_manageProjects.UseVisualStyleBackColor = true;
            this.button_manageProjects.Click += new System.EventHandler(this.button_manageProjects_Click);
            //
            // button_manageClients
            //
            this.button_manageClients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageClients.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageClients.Location = new System.Drawing.Point(690,158);
            this.button_manageClients.Name = "button_manageClients";
            this.button_manageClients.Size = new System.Drawing.Size(280, 50);
            this.button_manageClients.TabIndex = 16;
            this.button_manageClients.Text = "ניהול לקוחות";
            this.button_manageClients.UseVisualStyleBackColor = true;
            this.button_manageClients.Click += new System.EventHandler(this.button_manageClients_Click);
            //
            // button_manageBudgetLines
            //
            this.button_manageBudgetLines.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageBudgetLines.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageBudgetLines.Location = new System.Drawing.Point(690,216);
            this.button_manageBudgetLines.Name = "button_manageBudgetLines";
            this.button_manageBudgetLines.Size = new System.Drawing.Size(280, 50);
            this.button_manageBudgetLines.TabIndex = 17;
            this.button_manageBudgetLines.Text = "שורות תקציב";
            this.button_manageBudgetLines.UseVisualStyleBackColor = true;
            this.button_manageBudgetLines.Click += new System.EventHandler(this.button_manageBudgetLines_Click);
            //
            // button_managePaymentRequests
            //
            this.button_managePaymentRequests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_managePaymentRequests.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_managePaymentRequests.Location = new System.Drawing.Point(690,274);
            this.button_managePaymentRequests.Name = "button_managePaymentRequests";
            this.button_managePaymentRequests.Size = new System.Drawing.Size(280, 50);
            this.button_managePaymentRequests.TabIndex = 18;
            this.button_managePaymentRequests.Text = "בקשות תשלום מלקוח";
            this.button_managePaymentRequests.UseVisualStyleBackColor = true;
            this.button_managePaymentRequests.Click += new System.EventHandler(this.button_managePaymentRequests_Click);
            //
            // button_profitabilityReport (UC-05)
            //
            this.button_profitabilityReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_profitabilityReport.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_profitabilityReport.Location = new System.Drawing.Point(690,332);
            this.button_profitabilityReport.Name = "button_profitabilityReport";
            this.button_profitabilityReport.Size = new System.Drawing.Size(280, 50);
            this.button_profitabilityReport.TabIndex = 19;
            this.button_profitabilityReport.Text = "דוח רווחיות ותזרים מזומנים";
            this.button_profitabilityReport.UseVisualStyleBackColor = true;
            this.button_profitabilityReport.Click += new System.EventHandler(this.button_profitabilityReport_Click);
            //
            // button_manageSecurities (UC-06)
            //
            this.button_manageSecurities.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_manageSecurities.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button_manageSecurities.Location = new System.Drawing.Point(690,390);
            this.button_manageSecurities.Name = "button_manageSecurities";
            this.button_manageSecurities.Size = new System.Drawing.Size(280, 50);
            this.button_manageSecurities.TabIndex = 20;
            this.button_manageSecurities.Text = "ניהול ערבויות וביטוחים";
            this.button_manageSecurities.UseVisualStyleBackColor = true;
            this.button_manageSecurities.Click += new System.EventHandler(this.button_manageSecurities_Click);
            //
            // MainMenuPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_manageSecurities);
            this.Controls.Add(this.button_profitabilityReport);
            this.Controls.Add(this.button_managePaymentRequests);
            this.Controls.Add(this.button_manageBudgetLines);
            this.Controls.Add(this.button_manageClients);
            this.Controls.Add(this.button_manageProjects);
            this.Controls.Add(this.button_manageTenders);
            this.Controls.Add(this.button_manageEquipmentAssignments);
            this.Controls.Add(this.button_manageEquipmentUsage);
            this.Controls.Add(this.button_manageEquipment);
            this.Controls.Add(this.button_manageAttendance);
            this.Controls.Add(this.button_dailyWorkLog);
            this.Controls.Add(this.button_manageEmployees);
            this.Controls.Add(this.button_manageSupplierPriceQuotes);
            this.Controls.Add(this.button_manageSupplierPayments);
            this.Controls.Add(this.button_managePurchaseOrderLines);
            this.Controls.Add(this.button_createPurchaseOrder);
            this.Controls.Add(this.button_manageTradeCategories);
            this.Controls.Add(this.button_manageSubcontractors);
            this.Controls.Add(this.button_manageSuppliers);
            this.Controls.Add(this.label_sectionFinance);
            this.Controls.Add(this.label_sectionFieldWork);
            this.Controls.Add(this.label_sectionProcurement);
            this.Controls.Add(this.label_title);
            this.Name = "MainMenuPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_sectionProcurement;
        private System.Windows.Forms.Label label_sectionFieldWork;
        private System.Windows.Forms.Label label_sectionFinance;
        private System.Windows.Forms.Button button_manageSuppliers;
        private System.Windows.Forms.Button button_manageSubcontractors;
        private System.Windows.Forms.Button button_manageTradeCategories;
        private System.Windows.Forms.Button button_createPurchaseOrder;
        private System.Windows.Forms.Button button_managePurchaseOrderLines;
        private System.Windows.Forms.Button button_manageSupplierPayments;
        private System.Windows.Forms.Button button_manageSupplierPriceQuotes;
        private System.Windows.Forms.Button button_manageEmployees;
        private System.Windows.Forms.Button button_dailyWorkLog;
        private System.Windows.Forms.Button button_manageAttendance;
        private System.Windows.Forms.Button button_manageEquipment;
        private System.Windows.Forms.Button button_manageEquipmentUsage;
        private System.Windows.Forms.Button button_manageEquipmentAssignments;
        private System.Windows.Forms.Button button_manageTenders;
        private System.Windows.Forms.Button button_manageProjects;
        private System.Windows.Forms.Button button_manageClients;
        private System.Windows.Forms.Button button_manageBudgetLines;
        private System.Windows.Forms.Button button_managePaymentRequests;
        private System.Windows.Forms.Button button_profitabilityReport;
        private System.Windows.Forms.Button button_manageSecurities;
    }
}

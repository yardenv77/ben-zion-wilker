namespace BenZionVilker
{
    partial class PurchaseOrderPanel
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
            this.dataGridView_purchaseOrders = new System.Windows.Forms.DataGridView();
            this.label_purchaseOrderId = new System.Windows.Forms.Label();
            this.textBox_purchaseOrderId = new System.Windows.Forms.TextBox();
            this.label_poNumber = new System.Windows.Forms.Label();
            this.textBox_poNumber = new System.Windows.Forms.TextBox();
            this.label_supplier = new System.Windows.Forms.Label();
            this.comboBox_supplier = new System.Windows.Forms.ComboBox();
            this.label_project = new System.Windows.Forms.Label();
            this.comboBox_project = new System.Windows.Forms.ComboBox();
            this.label_orderDate = new System.Windows.Forms.Label();
            this.textBox_orderDate = new System.Windows.Forms.TextBox();
            this.label_totalAmount = new System.Windows.Forms.Label();
            this.textBox_totalAmount = new System.Windows.Forms.TextBox();
            this.label_vatAmount = new System.Windows.Forms.Label();
            this.textBox_vatAmount = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.label_createdBy = new System.Windows.Forms.Label();
            this.comboBox_createdBy = new System.Windows.Forms.ComboBox();
            this.label_approvedBy = new System.Windows.Forms.Label();
            this.comboBox_approvedBy = new System.Windows.Forms.ComboBox();
            this.label_overrideApprovedBy = new System.Windows.Forms.Label();
            this.comboBox_overrideApprovedBy = new System.Windows.Forms.ComboBox();
            this.label_rejectionReason = new System.Windows.Forms.Label();
            this.textBox_rejectionReason = new System.Windows.Forms.TextBox();
            this.label_closureReason = new System.Windows.Forms.Label();
            this.comboBox_closureReason = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_updateDetails = new System.Windows.Forms.Button();
            this.button_submit = new System.Windows.Forms.Button();
            this.button_withdraw = new System.Windows.Forms.Button();
            this.button_reject = new System.Windows.Forms.Button();
            this.button_revise = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_approveBudgetOverride = new System.Windows.Forms.Button();
            this.button_approve = new System.Windows.Forms.Button();
            this.button_cancelRemaining = new System.Windows.Forms.Button();
            this.button_archive = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.label_quickCreateSection = new System.Windows.Forms.Label();
            this.label_newLineDescription = new System.Windows.Forms.Label();
            this.label_newLineUnitOfMeasure = new System.Windows.Forms.Label();
            this.label_newLineQuantity = new System.Windows.Forms.Label();
            this.label_newLineUnitPrice = new System.Windows.Forms.Label();
            this.textBox_newLineDescription = new System.Windows.Forms.TextBox();
            this.textBox_newLineUnitOfMeasure = new System.Windows.Forms.TextBox();
            this.textBox_newLineQuantity = new System.Windows.Forms.TextBox();
            this.textBox_newLineUnitPrice = new System.Windows.Forms.TextBox();
            this.button_addLineToQueue = new System.Windows.Forms.Button();
            this.dataGridView_newLines = new System.Windows.Forms.DataGridView();
            this.button_removeSelectedLine = new System.Windows.Forms.Button();
            this.button_quickCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_purchaseOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_newLines)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(350, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(300, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "יצירת הזמנת רכש";
            //
            // dataGridView_purchaseOrders
            //
            this.dataGridView_purchaseOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_purchaseOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_purchaseOrders.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_purchaseOrders.Name = "dataGridView_purchaseOrders";
            this.dataGridView_purchaseOrders.ReadOnly = true;
            this.dataGridView_purchaseOrders.RowTemplate.Height = 24;
            this.dataGridView_purchaseOrders.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_purchaseOrders.TabIndex = 1;
            this.dataGridView_purchaseOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_purchaseOrders_CellClick);
            //
            // label_purchaseOrderId
            //
            this.label_purchaseOrderId.AutoSize = true;
            this.label_purchaseOrderId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_purchaseOrderId.Location = new System.Drawing.Point(820, 260);
            this.label_purchaseOrderId.Name = "label_purchaseOrderId";
            this.label_purchaseOrderId.Size = new System.Drawing.Size(90, 17);
            this.label_purchaseOrderId.TabIndex = 2;
            this.label_purchaseOrderId.Text = "מזהה הזמנה";
            //
            // textBox_purchaseOrderId
            //
            this.textBox_purchaseOrderId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_purchaseOrderId.Location = new System.Drawing.Point(550, 257);
            this.textBox_purchaseOrderId.Name = "textBox_purchaseOrderId";
            this.textBox_purchaseOrderId.ReadOnly = true;
            this.textBox_purchaseOrderId.Size = new System.Drawing.Size(250, 25);
            this.textBox_purchaseOrderId.TabIndex = 3;
            this.textBox_purchaseOrderId.TabStop = false;
            //
            // label_poNumber (col A)
            //
            this.label_poNumber.AutoSize = true;
            this.label_poNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_poNumber.Location = new System.Drawing.Point(820, 295);
            this.label_poNumber.Name = "label_poNumber";
            this.label_poNumber.Size = new System.Drawing.Size(90, 17);
            this.label_poNumber.TabIndex = 4;
            this.label_poNumber.Text = "מספר הזמנה";
            //
            // textBox_poNumber
            //
            this.textBox_poNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_poNumber.Location = new System.Drawing.Point(550, 292);
            this.textBox_poNumber.Name = "textBox_poNumber";
            this.textBox_poNumber.Size = new System.Drawing.Size(250, 25);
            this.textBox_poNumber.TabIndex = 5;
            //
            // label_supplier (col A)
            //
            this.label_supplier.AutoSize = true;
            this.label_supplier.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_supplier.Location = new System.Drawing.Point(820, 330);
            this.label_supplier.Name = "label_supplier";
            this.label_supplier.Size = new System.Drawing.Size(48, 17);
            this.label_supplier.TabIndex = 6;
            this.label_supplier.Text = "ספק";
            //
            // comboBox_supplier
            //
            this.comboBox_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_supplier.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_supplier.FormattingEnabled = true;
            this.comboBox_supplier.Location = new System.Drawing.Point(550, 327);
            this.comboBox_supplier.Name = "comboBox_supplier";
            this.comboBox_supplier.Size = new System.Drawing.Size(250, 25);
            this.comboBox_supplier.TabIndex = 7;
            //
            // label_project (col A)
            //
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_project.Location = new System.Drawing.Point(820, 365);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(60, 17);
            this.label_project.TabIndex = 8;
            this.label_project.Text = "פרויקט";
            //
            // comboBox_project
            //
            this.comboBox_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_project.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_project.FormattingEnabled = true;
            this.comboBox_project.Location = new System.Drawing.Point(550, 362);
            this.comboBox_project.Name = "comboBox_project";
            this.comboBox_project.Size = new System.Drawing.Size(250, 25);
            this.comboBox_project.TabIndex = 9;
            //
            // label_orderDate (col A)
            //
            this.label_orderDate.AutoSize = true;
            this.label_orderDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_orderDate.Location = new System.Drawing.Point(820, 400);
            this.label_orderDate.Name = "label_orderDate";
            this.label_orderDate.Size = new System.Drawing.Size(80, 17);
            this.label_orderDate.TabIndex = 10;
            this.label_orderDate.Text = "תאריך הזמנה";
            //
            // textBox_orderDate
            //
            this.textBox_orderDate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_orderDate.Location = new System.Drawing.Point(550, 397);
            this.textBox_orderDate.Name = "textBox_orderDate";
            this.textBox_orderDate.Size = new System.Drawing.Size(250, 25);
            this.textBox_orderDate.TabIndex = 11;
            //
            // label_totalAmount (col A)
            //
            this.label_totalAmount.AutoSize = true;
            this.label_totalAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_totalAmount.Location = new System.Drawing.Point(820, 435);
            this.label_totalAmount.Name = "label_totalAmount";
            this.label_totalAmount.Size = new System.Drawing.Size(76, 17);
            this.label_totalAmount.TabIndex = 12;
            this.label_totalAmount.Text = "סכום כולל";
            //
            // textBox_totalAmount
            //
            this.textBox_totalAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_totalAmount.Location = new System.Drawing.Point(550, 432);
            this.textBox_totalAmount.Name = "textBox_totalAmount";
            this.textBox_totalAmount.Size = new System.Drawing.Size(250, 25);
            this.textBox_totalAmount.TabIndex = 13;
            //
            // label_vatAmount (col A)
            //
            this.label_vatAmount.AutoSize = true;
            this.label_vatAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_vatAmount.Location = new System.Drawing.Point(820, 470);
            this.label_vatAmount.Name = "label_vatAmount";
            this.label_vatAmount.Size = new System.Drawing.Size(76, 17);
            this.label_vatAmount.TabIndex = 14;
            this.label_vatAmount.Text = "סכום מע\"מ";
            //
            // textBox_vatAmount
            //
            this.textBox_vatAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_vatAmount.Location = new System.Drawing.Point(550, 467);
            this.textBox_vatAmount.Name = "textBox_vatAmount";
            this.textBox_vatAmount.Size = new System.Drawing.Size(250, 25);
            this.textBox_vatAmount.TabIndex = 15;
            //
            // label_status (col A)
            //
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_status.Location = new System.Drawing.Point(820, 505);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(48, 17);
            this.label_status.TabIndex = 16;
            this.label_status.Text = "סטטוס";
            //
            // comboBox_status
            //
            this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_status.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Location = new System.Drawing.Point(550, 502);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(250, 25);
            this.comboBox_status.TabIndex = 17;
            //
            // label_createdBy (col B)
            //
            this.label_createdBy.AutoSize = true;
            this.label_createdBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_createdBy.Location = new System.Drawing.Point(430, 260);
            this.label_createdBy.Name = "label_createdBy";
            this.label_createdBy.Size = new System.Drawing.Size(70, 17);
            this.label_createdBy.TabIndex = 18;
            this.label_createdBy.Text = "נוצר על ידי";
            //
            // comboBox_createdBy
            //
            this.comboBox_createdBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_createdBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_createdBy.FormattingEnabled = true;
            this.comboBox_createdBy.Location = new System.Drawing.Point(190, 257);
            this.comboBox_createdBy.Name = "comboBox_createdBy";
            this.comboBox_createdBy.Size = new System.Drawing.Size(230, 25);
            this.comboBox_createdBy.TabIndex = 19;
            //
            // label_approvedBy (col B)
            //
            this.label_approvedBy.AutoSize = true;
            this.label_approvedBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_approvedBy.Location = new System.Drawing.Point(430, 295);
            this.label_approvedBy.Name = "label_approvedBy";
            this.label_approvedBy.Size = new System.Drawing.Size(70, 17);
            this.label_approvedBy.TabIndex = 20;
            this.label_approvedBy.Text = "אושר על ידי";
            //
            // comboBox_approvedBy
            //
            this.comboBox_approvedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_approvedBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_approvedBy.FormattingEnabled = true;
            this.comboBox_approvedBy.Location = new System.Drawing.Point(190, 292);
            this.comboBox_approvedBy.Name = "comboBox_approvedBy";
            this.comboBox_approvedBy.Size = new System.Drawing.Size(230, 25);
            this.comboBox_approvedBy.TabIndex = 21;
            //
            // label_overrideApprovedBy (col B)
            //
            this.label_overrideApprovedBy.AutoSize = true;
            this.label_overrideApprovedBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_overrideApprovedBy.Location = new System.Drawing.Point(430, 330);
            this.label_overrideApprovedBy.Name = "label_overrideApprovedBy";
            this.label_overrideApprovedBy.Size = new System.Drawing.Size(120, 17);
            this.label_overrideApprovedBy.TabIndex = 22;
            this.label_overrideApprovedBy.Text = "אישר חריגת תקציב";
            //
            // comboBox_overrideApprovedBy
            //
            this.comboBox_overrideApprovedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_overrideApprovedBy.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_overrideApprovedBy.FormattingEnabled = true;
            this.comboBox_overrideApprovedBy.Location = new System.Drawing.Point(190, 327);
            this.comboBox_overrideApprovedBy.Name = "comboBox_overrideApprovedBy";
            this.comboBox_overrideApprovedBy.Size = new System.Drawing.Size(230, 25);
            this.comboBox_overrideApprovedBy.TabIndex = 23;
            //
            // label_rejectionReason (col B)
            //
            this.label_rejectionReason.AutoSize = true;
            this.label_rejectionReason.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_rejectionReason.Location = new System.Drawing.Point(430, 365);
            this.label_rejectionReason.Name = "label_rejectionReason";
            this.label_rejectionReason.Size = new System.Drawing.Size(80, 17);
            this.label_rejectionReason.TabIndex = 24;
            this.label_rejectionReason.Text = "סיבת דחייה";
            //
            // textBox_rejectionReason
            //
            this.textBox_rejectionReason.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_rejectionReason.Location = new System.Drawing.Point(190, 362);
            this.textBox_rejectionReason.Name = "textBox_rejectionReason";
            this.textBox_rejectionReason.Size = new System.Drawing.Size(230, 25);
            this.textBox_rejectionReason.TabIndex = 25;
            //
            // label_closureReason (col B)
            //
            this.label_closureReason.AutoSize = true;
            this.label_closureReason.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_closureReason.Location = new System.Drawing.Point(430, 400);
            this.label_closureReason.Name = "label_closureReason";
            this.label_closureReason.Size = new System.Drawing.Size(80, 17);
            this.label_closureReason.TabIndex = 26;
            this.label_closureReason.Text = "סיבת סגירה";
            //
            // comboBox_closureReason
            //
            this.comboBox_closureReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_closureReason.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_closureReason.FormattingEnabled = true;
            this.comboBox_closureReason.Location = new System.Drawing.Point(190, 397);
            this.comboBox_closureReason.Name = "comboBox_closureReason";
            this.comboBox_closureReason.Size = new System.Drawing.Size(230, 25);
            this.comboBox_closureReason.TabIndex = 27;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 690);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 28;
            this.button_save.Text = "שמירה";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            //
            // button_updateDetails -- non-state-machine CRUD fields only (poNumber, supplier,
            // project, createdBy, dates, amounts). Does NOT touch status/rejectionReason/
            // closureReason/approvedBy/overrideApprovedBy (step 7.4) -- those change only
            // via the verb buttons below.
            //
            this.button_updateDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_updateDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_updateDetails.Location = new System.Drawing.Point(610, 690);
            this.button_updateDetails.Name = "button_updateDetails";
            this.button_updateDetails.Size = new System.Drawing.Size(130, 42);
            this.button_updateDetails.TabIndex = 40;
            this.button_updateDetails.Text = "עדכון פרטים";
            this.button_updateDetails.UseVisualStyleBackColor = true;
            this.button_updateDetails.Click += new System.EventHandler(this.button_updateDetails_Click);
            //
            // Verb buttons (step 7.5) -- one per user-triggered transition in
            // docs/design/state-diagram.md. autoCancel()/purge() are system-triggered
            // (BR-3 / 7-year retention) and deliberately get no button here.
            //
            // button_submit (t2a/t2b)
            //
            this.button_submit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_submit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_submit.Location = new System.Drawing.Point(790, 545);
            this.button_submit.Name = "button_submit";
            this.button_submit.Size = new System.Drawing.Size(140, 38);
            this.button_submit.TabIndex = 29;
            this.button_submit.Text = "שלח לאישור";
            this.button_submit.UseVisualStyleBackColor = true;
            this.button_submit.Click += new System.EventHandler(this.button_submit_Click);
            //
            // button_withdraw (t3)
            //
            this.button_withdraw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_withdraw.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_withdraw.Location = new System.Drawing.Point(630, 545);
            this.button_withdraw.Name = "button_withdraw";
            this.button_withdraw.Size = new System.Drawing.Size(140, 38);
            this.button_withdraw.TabIndex = 30;
            this.button_withdraw.Text = "משוך הזמנה";
            this.button_withdraw.UseVisualStyleBackColor = true;
            this.button_withdraw.Click += new System.EventHandler(this.button_withdraw_Click);
            //
            // button_reject (t4)
            //
            this.button_reject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_reject.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_reject.Location = new System.Drawing.Point(470, 545);
            this.button_reject.Name = "button_reject";
            this.button_reject.Size = new System.Drawing.Size(140, 38);
            this.button_reject.TabIndex = 31;
            this.button_reject.Text = "דחה";
            this.button_reject.UseVisualStyleBackColor = true;
            this.button_reject.Click += new System.EventHandler(this.button_reject_Click);
            //
            // button_revise (t5)
            //
            this.button_revise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_revise.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_revise.Location = new System.Drawing.Point(790, 590);
            this.button_revise.Name = "button_revise";
            this.button_revise.Size = new System.Drawing.Size(140, 38);
            this.button_revise.TabIndex = 32;
            this.button_revise.Text = "תקן (חזרה לטיוטה)";
            this.button_revise.UseVisualStyleBackColor = true;
            this.button_revise.Click += new System.EventHandler(this.button_revise_Click);
            //
            // button_cancel (t7/t8)
            //
            this.button_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_cancel.Location = new System.Drawing.Point(630, 590);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(140, 38);
            this.button_cancel.TabIndex = 33;
            this.button_cancel.Text = "בטל הזמנה";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            //
            // button_approveBudgetOverride (t10)
            //
            this.button_approveBudgetOverride.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_approveBudgetOverride.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_approveBudgetOverride.Location = new System.Drawing.Point(470, 590);
            this.button_approveBudgetOverride.Name = "button_approveBudgetOverride";
            this.button_approveBudgetOverride.Size = new System.Drawing.Size(140, 38);
            this.button_approveBudgetOverride.TabIndex = 34;
            this.button_approveBudgetOverride.Text = "אשר חריגת תקציב";
            this.button_approveBudgetOverride.UseVisualStyleBackColor = true;
            this.button_approveBudgetOverride.Click += new System.EventHandler(this.button_approveBudgetOverride_Click);
            //
            // button_approve (t11)
            //
            this.button_approve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_approve.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_approve.Location = new System.Drawing.Point(790, 635);
            this.button_approve.Name = "button_approve";
            this.button_approve.Size = new System.Drawing.Size(140, 38);
            this.button_approve.TabIndex = 35;
            this.button_approve.Text = "אשר הזמנה";
            this.button_approve.UseVisualStyleBackColor = true;
            this.button_approve.Click += new System.EventHandler(this.button_approve_Click);
            //
            // button_cancelRemaining (t17)
            //
            this.button_cancelRemaining.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancelRemaining.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_cancelRemaining.Location = new System.Drawing.Point(630, 635);
            this.button_cancelRemaining.Name = "button_cancelRemaining";
            this.button_cancelRemaining.Size = new System.Drawing.Size(140, 38);
            this.button_cancelRemaining.TabIndex = 36;
            this.button_cancelRemaining.Text = "בטל יתרה";
            this.button_cancelRemaining.UseVisualStyleBackColor = true;
            this.button_cancelRemaining.Click += new System.EventHandler(this.button_cancelRemaining_Click);
            //
            // button_archive (t18/t19)
            //
            this.button_archive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_archive.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_archive.Location = new System.Drawing.Point(470, 635);
            this.button_archive.Name = "button_archive";
            this.button_archive.Size = new System.Drawing.Size(140, 38);
            this.button_archive.TabIndex = 37;
            this.button_archive.Text = "העבר לארכיון";
            this.button_archive.UseVisualStyleBackColor = true;
            this.button_archive.Click += new System.EventHandler(this.button_archive_Click);
            //
            // button_delete
            //
            this.button_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_delete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_delete.Location = new System.Drawing.Point(460, 690);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(110, 42);
            this.button_delete.TabIndex = 38;
            this.button_delete.Text = "מחיקה";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            //
            // button_back
            //
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_back.Location = new System.Drawing.Point(320, 690);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 39;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // Quick-create-with-lines section (docs/00e-use-cases.md UC-03 MSS 1-9,
            // atomic alternative to button_save + PurchaseOrderLinePanel + button_submit).
            // Reuses textBox_poNumber/comboBox_supplier/comboBox_project/comboBox_createdBy/
            // textBox_orderDate above as the header inputs -- only the pending-lines list
            // and the two buttons below are new. totalAmount/vatAmount are deliberately
            // NOT read from textBox_totalAmount/textBox_vatAmount here: sp_purchase_order_
            // create_flow computes them server-side from the queued lines.
            //
            // label_quickCreateSection
            //
            this.label_quickCreateSection.AutoSize = true;
            this.label_quickCreateSection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label_quickCreateSection.Location = new System.Drawing.Point(200, 745);
            this.label_quickCreateSection.Name = "label_quickCreateSection";
            this.label_quickCreateSection.Size = new System.Drawing.Size(560, 20);
            this.label_quickCreateSection.TabIndex = 41;
            this.label_quickCreateSection.Text = "יצירה מהירה עם שורות (זרימה אטומית — בדיקות ספק ותקציב בשרת)";
            //
            // label_newLineDescription
            //
            this.label_newLineDescription.AutoSize = true;
            this.label_newLineDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_newLineDescription.Location = new System.Drawing.Point(820, 780);
            this.label_newLineDescription.Name = "label_newLineDescription";
            this.label_newLineDescription.Size = new System.Drawing.Size(40, 15);
            this.label_newLineDescription.TabIndex = 42;
            this.label_newLineDescription.Text = "תיאור";
            //
            // label_newLineUnitOfMeasure
            //
            this.label_newLineUnitOfMeasure.AutoSize = true;
            this.label_newLineUnitOfMeasure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_newLineUnitOfMeasure.Location = new System.Drawing.Point(650, 780);
            this.label_newLineUnitOfMeasure.Name = "label_newLineUnitOfMeasure";
            this.label_newLineUnitOfMeasure.Size = new System.Drawing.Size(60, 15);
            this.label_newLineUnitOfMeasure.TabIndex = 43;
            this.label_newLineUnitOfMeasure.Text = "יח\' מידה";
            //
            // label_newLineQuantity
            //
            this.label_newLineQuantity.AutoSize = true;
            this.label_newLineQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_newLineQuantity.Location = new System.Drawing.Point(540, 780);
            this.label_newLineQuantity.Name = "label_newLineQuantity";
            this.label_newLineQuantity.Size = new System.Drawing.Size(40, 15);
            this.label_newLineQuantity.TabIndex = 44;
            this.label_newLineQuantity.Text = "כמות";
            //
            // label_newLineUnitPrice
            //
            this.label_newLineUnitPrice.AutoSize = true;
            this.label_newLineUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_newLineUnitPrice.Location = new System.Drawing.Point(420, 780);
            this.label_newLineUnitPrice.Name = "label_newLineUnitPrice";
            this.label_newLineUnitPrice.Size = new System.Drawing.Size(60, 15);
            this.label_newLineUnitPrice.TabIndex = 45;
            this.label_newLineUnitPrice.Text = "מחיר יח\'";
            //
            // textBox_newLineDescription
            //
            this.textBox_newLineDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox_newLineDescription.Location = new System.Drawing.Point(770, 800);
            this.textBox_newLineDescription.Name = "textBox_newLineDescription";
            this.textBox_newLineDescription.Size = new System.Drawing.Size(180, 23);
            this.textBox_newLineDescription.TabIndex = 46;
            //
            // textBox_newLineUnitOfMeasure
            //
            this.textBox_newLineUnitOfMeasure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox_newLineUnitOfMeasure.Location = new System.Drawing.Point(650, 800);
            this.textBox_newLineUnitOfMeasure.Name = "textBox_newLineUnitOfMeasure";
            this.textBox_newLineUnitOfMeasure.Size = new System.Drawing.Size(100, 23);
            this.textBox_newLineUnitOfMeasure.TabIndex = 47;
            //
            // textBox_newLineQuantity
            //
            this.textBox_newLineQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox_newLineQuantity.Location = new System.Drawing.Point(540, 800);
            this.textBox_newLineQuantity.Name = "textBox_newLineQuantity";
            this.textBox_newLineQuantity.Size = new System.Drawing.Size(90, 23);
            this.textBox_newLineQuantity.TabIndex = 48;
            //
            // textBox_newLineUnitPrice
            //
            this.textBox_newLineUnitPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBox_newLineUnitPrice.Location = new System.Drawing.Point(420, 800);
            this.textBox_newLineUnitPrice.Name = "textBox_newLineUnitPrice";
            this.textBox_newLineUnitPrice.Size = new System.Drawing.Size(100, 23);
            this.textBox_newLineUnitPrice.TabIndex = 49;
            //
            // button_addLineToQueue
            //
            this.button_addLineToQueue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_addLineToQueue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_addLineToQueue.Location = new System.Drawing.Point(290, 797);
            this.button_addLineToQueue.Name = "button_addLineToQueue";
            this.button_addLineToQueue.Size = new System.Drawing.Size(110, 32);
            this.button_addLineToQueue.TabIndex = 50;
            this.button_addLineToQueue.Text = "הוסף שורה";
            this.button_addLineToQueue.UseVisualStyleBackColor = true;
            this.button_addLineToQueue.Click += new System.EventHandler(this.button_addLineToQueue_Click);
            //
            // dataGridView_newLines
            //
            this.dataGridView_newLines.AllowUserToAddRows = false;
            this.dataGridView_newLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_newLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_newLines.Location = new System.Drawing.Point(50, 840);
            this.dataGridView_newLines.Name = "dataGridView_newLines";
            this.dataGridView_newLines.ReadOnly = true;
            this.dataGridView_newLines.RowTemplate.Height = 22;
            this.dataGridView_newLines.Size = new System.Drawing.Size(900, 120);
            this.dataGridView_newLines.TabIndex = 51;
            //
            // button_removeSelectedLine
            //
            this.button_removeSelectedLine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_removeSelectedLine.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button_removeSelectedLine.Location = new System.Drawing.Point(650, 975);
            this.button_removeSelectedLine.Name = "button_removeSelectedLine";
            this.button_removeSelectedLine.Size = new System.Drawing.Size(150, 38);
            this.button_removeSelectedLine.TabIndex = 52;
            this.button_removeSelectedLine.Text = "הסר שורה נבחרת";
            this.button_removeSelectedLine.UseVisualStyleBackColor = true;
            this.button_removeSelectedLine.Click += new System.EventHandler(this.button_removeSelectedLine_Click);
            //
            // button_quickCreate
            //
            this.button_quickCreate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_quickCreate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_quickCreate.Location = new System.Drawing.Point(270, 975);
            this.button_quickCreate.Name = "button_quickCreate";
            this.button_quickCreate.Size = new System.Drawing.Size(360, 38);
            this.button_quickCreate.TabIndex = 53;
            this.button_quickCreate.Text = "יצירה מהירה עם שורות (זרימה אטומית)";
            this.button_quickCreate.UseVisualStyleBackColor = true;
            this.button_quickCreate.Click += new System.EventHandler(this.button_quickCreate_Click);
            //
            // PurchaseOrderPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_quickCreate);
            this.Controls.Add(this.button_removeSelectedLine);
            this.Controls.Add(this.dataGridView_newLines);
            this.Controls.Add(this.button_addLineToQueue);
            this.Controls.Add(this.textBox_newLineUnitPrice);
            this.Controls.Add(this.textBox_newLineQuantity);
            this.Controls.Add(this.textBox_newLineUnitOfMeasure);
            this.Controls.Add(this.textBox_newLineDescription);
            this.Controls.Add(this.label_newLineUnitPrice);
            this.Controls.Add(this.label_newLineQuantity);
            this.Controls.Add(this.label_newLineUnitOfMeasure);
            this.Controls.Add(this.label_newLineDescription);
            this.Controls.Add(this.label_quickCreateSection);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_archive);
            this.Controls.Add(this.button_cancelRemaining);
            this.Controls.Add(this.button_approve);
            this.Controls.Add(this.button_approveBudgetOverride);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_revise);
            this.Controls.Add(this.button_reject);
            this.Controls.Add(this.button_withdraw);
            this.Controls.Add(this.button_submit);
            this.Controls.Add(this.button_updateDetails);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_closureReason);
            this.Controls.Add(this.label_closureReason);
            this.Controls.Add(this.textBox_rejectionReason);
            this.Controls.Add(this.label_rejectionReason);
            this.Controls.Add(this.comboBox_overrideApprovedBy);
            this.Controls.Add(this.label_overrideApprovedBy);
            this.Controls.Add(this.comboBox_approvedBy);
            this.Controls.Add(this.label_approvedBy);
            this.Controls.Add(this.comboBox_createdBy);
            this.Controls.Add(this.label_createdBy);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.textBox_vatAmount);
            this.Controls.Add(this.label_vatAmount);
            this.Controls.Add(this.textBox_totalAmount);
            this.Controls.Add(this.label_totalAmount);
            this.Controls.Add(this.textBox_orderDate);
            this.Controls.Add(this.label_orderDate);
            this.Controls.Add(this.comboBox_project);
            this.Controls.Add(this.label_project);
            this.Controls.Add(this.comboBox_supplier);
            this.Controls.Add(this.label_supplier);
            this.Controls.Add(this.textBox_poNumber);
            this.Controls.Add(this.label_poNumber);
            this.Controls.Add(this.textBox_purchaseOrderId);
            this.Controls.Add(this.label_purchaseOrderId);
            this.Controls.Add(this.dataGridView_purchaseOrders);
            this.Controls.Add(this.label_title);
            this.Name = "PurchaseOrderPanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 1040);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_purchaseOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_newLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_purchaseOrders;
        private System.Windows.Forms.Label label_purchaseOrderId;
        private System.Windows.Forms.TextBox textBox_purchaseOrderId;
        private System.Windows.Forms.Label label_poNumber;
        private System.Windows.Forms.TextBox textBox_poNumber;
        private System.Windows.Forms.Label label_supplier;
        private System.Windows.Forms.ComboBox comboBox_supplier;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.ComboBox comboBox_project;
        private System.Windows.Forms.Label label_orderDate;
        private System.Windows.Forms.TextBox textBox_orderDate;
        private System.Windows.Forms.Label label_totalAmount;
        private System.Windows.Forms.TextBox textBox_totalAmount;
        private System.Windows.Forms.Label label_vatAmount;
        private System.Windows.Forms.TextBox textBox_vatAmount;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.ComboBox comboBox_status;
        private System.Windows.Forms.Label label_createdBy;
        private System.Windows.Forms.ComboBox comboBox_createdBy;
        private System.Windows.Forms.Label label_approvedBy;
        private System.Windows.Forms.ComboBox comboBox_approvedBy;
        private System.Windows.Forms.Label label_overrideApprovedBy;
        private System.Windows.Forms.ComboBox comboBox_overrideApprovedBy;
        private System.Windows.Forms.Label label_rejectionReason;
        private System.Windows.Forms.TextBox textBox_rejectionReason;
        private System.Windows.Forms.Label label_closureReason;
        private System.Windows.Forms.ComboBox comboBox_closureReason;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_updateDetails;
        private System.Windows.Forms.Button button_submit;
        private System.Windows.Forms.Button button_withdraw;
        private System.Windows.Forms.Button button_reject;
        private System.Windows.Forms.Button button_revise;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_approveBudgetOverride;
        private System.Windows.Forms.Button button_approve;
        private System.Windows.Forms.Button button_cancelRemaining;
        private System.Windows.Forms.Button button_archive;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Label label_quickCreateSection;
        private System.Windows.Forms.Label label_newLineDescription;
        private System.Windows.Forms.Label label_newLineUnitOfMeasure;
        private System.Windows.Forms.Label label_newLineQuantity;
        private System.Windows.Forms.Label label_newLineUnitPrice;
        private System.Windows.Forms.TextBox textBox_newLineDescription;
        private System.Windows.Forms.TextBox textBox_newLineUnitOfMeasure;
        private System.Windows.Forms.TextBox textBox_newLineQuantity;
        private System.Windows.Forms.TextBox textBox_newLineUnitPrice;
        private System.Windows.Forms.Button button_addLineToQueue;
        private System.Windows.Forms.DataGridView dataGridView_newLines;
        private System.Windows.Forms.Button button_removeSelectedLine;
        private System.Windows.Forms.Button button_quickCreate;
    }
}

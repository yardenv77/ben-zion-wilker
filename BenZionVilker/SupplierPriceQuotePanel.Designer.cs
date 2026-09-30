namespace BenZionVilker
{
    partial class SupplierPriceQuotePanel
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
            this.dataGridView_quotes = new System.Windows.Forms.DataGridView();
            this.label_supplierPriceQuoteId = new System.Windows.Forms.Label();
            this.textBox_supplierPriceQuoteId = new System.Windows.Forms.TextBox();
            this.label_supplier = new System.Windows.Forms.Label();
            this.comboBox_supplier = new System.Windows.Forms.ComboBox();
            this.label_tender = new System.Windows.Forms.Label();
            this.comboBox_tender = new System.Windows.Forms.ComboBox();
            this.label_tradeCategory = new System.Windows.Forms.Label();
            this.comboBox_tradeCategory = new System.Windows.Forms.ComboBox();
            this.label_amount = new System.Windows.Forms.Label();
            this.textBox_amount = new System.Windows.Forms.TextBox();
            this.label_dateIssued = new System.Windows.Forms.Label();
            this.textBox_dateIssued = new System.Windows.Forms.TextBox();
            this.label_validUntil = new System.Windows.Forms.Label();
            this.textBox_validUntil = new System.Windows.Forms.TextBox();
            this.checkBox_isSelected = new System.Windows.Forms.CheckBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_quotes)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(300, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(400, 36);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "ניהול הצעות מחיר מספקים";
            //
            // dataGridView_quotes
            //
            this.dataGridView_quotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_quotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_quotes.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_quotes.Name = "dataGridView_quotes";
            this.dataGridView_quotes.ReadOnly = true;
            this.dataGridView_quotes.RowTemplate.Height = 24;
            this.dataGridView_quotes.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_quotes.TabIndex = 1;
            this.dataGridView_quotes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_quotes_CellClick);
            //
            // label_supplierPriceQuoteId
            //
            this.label_supplierPriceQuoteId.AutoSize = true;
            this.label_supplierPriceQuoteId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_supplierPriceQuoteId.Location = new System.Drawing.Point(820, 260);
            this.label_supplierPriceQuoteId.Name = "label_supplierPriceQuoteId";
            this.label_supplierPriceQuoteId.Size = new System.Drawing.Size(80, 17);
            this.label_supplierPriceQuoteId.TabIndex = 2;
            this.label_supplierPriceQuoteId.Text = "מזהה הצעה";
            //
            // textBox_supplierPriceQuoteId
            //
            this.textBox_supplierPriceQuoteId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_supplierPriceQuoteId.Location = new System.Drawing.Point(550, 257);
            this.textBox_supplierPriceQuoteId.Name = "textBox_supplierPriceQuoteId";
            this.textBox_supplierPriceQuoteId.ReadOnly = true;
            this.textBox_supplierPriceQuoteId.Size = new System.Drawing.Size(250, 25);
            this.textBox_supplierPriceQuoteId.TabIndex = 3;
            this.textBox_supplierPriceQuoteId.TabStop = false;
            //
            // label_supplier
            //
            this.label_supplier.AutoSize = true;
            this.label_supplier.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_supplier.Location = new System.Drawing.Point(820, 295);
            this.label_supplier.Name = "label_supplier";
            this.label_supplier.Size = new System.Drawing.Size(48, 17);
            this.label_supplier.TabIndex = 4;
            this.label_supplier.Text = "ספק";
            //
            // comboBox_supplier
            //
            this.comboBox_supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_supplier.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_supplier.FormattingEnabled = true;
            this.comboBox_supplier.Location = new System.Drawing.Point(550, 292);
            this.comboBox_supplier.Name = "comboBox_supplier";
            this.comboBox_supplier.Size = new System.Drawing.Size(250, 25);
            this.comboBox_supplier.TabIndex = 5;
            //
            // label_tender
            //
            this.label_tender.AutoSize = true;
            this.label_tender.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_tender.Location = new System.Drawing.Point(820, 330);
            this.label_tender.Name = "label_tender";
            this.label_tender.Size = new System.Drawing.Size(48, 17);
            this.label_tender.TabIndex = 6;
            this.label_tender.Text = "מכרז";
            //
            // comboBox_tender
            //
            this.comboBox_tender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tender.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_tender.FormattingEnabled = true;
            this.comboBox_tender.Location = new System.Drawing.Point(550, 327);
            this.comboBox_tender.Name = "comboBox_tender";
            this.comboBox_tender.Size = new System.Drawing.Size(250, 25);
            this.comboBox_tender.TabIndex = 7;
            //
            // label_tradeCategory
            //
            this.label_tradeCategory.AutoSize = true;
            this.label_tradeCategory.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_tradeCategory.Location = new System.Drawing.Point(820, 365);
            this.label_tradeCategory.Name = "label_tradeCategory";
            this.label_tradeCategory.Size = new System.Drawing.Size(76, 17);
            this.label_tradeCategory.TabIndex = 8;
            this.label_tradeCategory.Text = "תחום עיסוק";
            //
            // comboBox_tradeCategory
            //
            this.comboBox_tradeCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tradeCategory.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_tradeCategory.FormattingEnabled = true;
            this.comboBox_tradeCategory.Location = new System.Drawing.Point(550, 362);
            this.comboBox_tradeCategory.Name = "comboBox_tradeCategory";
            this.comboBox_tradeCategory.Size = new System.Drawing.Size(250, 25);
            this.comboBox_tradeCategory.TabIndex = 9;
            //
            // label_amount
            //
            this.label_amount.AutoSize = true;
            this.label_amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_amount.Location = new System.Drawing.Point(820, 400);
            this.label_amount.Name = "label_amount";
            this.label_amount.Size = new System.Drawing.Size(48, 17);
            this.label_amount.TabIndex = 10;
            this.label_amount.Text = "סכום";
            //
            // textBox_amount
            //
            this.textBox_amount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_amount.Location = new System.Drawing.Point(550, 397);
            this.textBox_amount.Name = "textBox_amount";
            this.textBox_amount.Size = new System.Drawing.Size(250, 25);
            this.textBox_amount.TabIndex = 11;
            //
            // label_dateIssued
            //
            this.label_dateIssued.AutoSize = true;
            this.label_dateIssued.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_dateIssued.Location = new System.Drawing.Point(820, 435);
            this.label_dateIssued.Name = "label_dateIssued";
            this.label_dateIssued.Size = new System.Drawing.Size(70, 17);
            this.label_dateIssued.TabIndex = 12;
            this.label_dateIssued.Text = "תאריך הצעה";
            //
            // textBox_dateIssued
            //
            this.textBox_dateIssued.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_dateIssued.Location = new System.Drawing.Point(550, 432);
            this.textBox_dateIssued.Name = "textBox_dateIssued";
            this.textBox_dateIssued.Size = new System.Drawing.Size(250, 25);
            this.textBox_dateIssued.TabIndex = 13;
            //
            // label_validUntil
            //
            this.label_validUntil.AutoSize = true;
            this.label_validUntil.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_validUntil.Location = new System.Drawing.Point(820, 470);
            this.label_validUntil.Name = "label_validUntil";
            this.label_validUntil.Size = new System.Drawing.Size(64, 17);
            this.label_validUntil.TabIndex = 14;
            this.label_validUntil.Text = "תאריך תוקף";
            //
            // textBox_validUntil
            //
            this.textBox_validUntil.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_validUntil.Location = new System.Drawing.Point(550, 467);
            this.textBox_validUntil.Name = "textBox_validUntil";
            this.textBox_validUntil.Size = new System.Drawing.Size(250, 25);
            this.textBox_validUntil.TabIndex = 15;
            //
            // checkBox_isSelected
            //
            this.checkBox_isSelected.AutoSize = true;
            this.checkBox_isSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_isSelected.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.checkBox_isSelected.Location = new System.Drawing.Point(550, 505);
            this.checkBox_isSelected.Name = "checkBox_isSelected";
            this.checkBox_isSelected.Size = new System.Drawing.Size(150, 21);
            this.checkBox_isSelected.TabIndex = 16;
            this.checkBox_isSelected.Text = "הצעה נבחרת";
            this.checkBox_isSelected.UseVisualStyleBackColor = true;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 17;
            this.button_save.Text = "שמירה";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            //
            // button_update
            //
            this.button_update.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_update.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_update.Location = new System.Drawing.Point(610, 560);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(110, 42);
            this.button_update.TabIndex = 18;
            this.button_update.Text = "עדכון";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            //
            // button_delete
            //
            this.button_delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_delete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_delete.Location = new System.Drawing.Point(470, 560);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(110, 42);
            this.button_delete.TabIndex = 19;
            this.button_delete.Text = "מחיקה";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            //
            // button_back
            //
            this.button_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_back.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_back.Location = new System.Drawing.Point(330, 560);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(110, 42);
            this.button_back.TabIndex = 20;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // SupplierPriceQuotePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.checkBox_isSelected);
            this.Controls.Add(this.textBox_validUntil);
            this.Controls.Add(this.label_validUntil);
            this.Controls.Add(this.textBox_dateIssued);
            this.Controls.Add(this.label_dateIssued);
            this.Controls.Add(this.textBox_amount);
            this.Controls.Add(this.label_amount);
            this.Controls.Add(this.comboBox_tradeCategory);
            this.Controls.Add(this.label_tradeCategory);
            this.Controls.Add(this.comboBox_tender);
            this.Controls.Add(this.label_tender);
            this.Controls.Add(this.comboBox_supplier);
            this.Controls.Add(this.label_supplier);
            this.Controls.Add(this.textBox_supplierPriceQuoteId);
            this.Controls.Add(this.label_supplierPriceQuoteId);
            this.Controls.Add(this.dataGridView_quotes);
            this.Controls.Add(this.label_title);
            this.Name = "SupplierPriceQuotePanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_quotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_quotes;
        private System.Windows.Forms.Label label_supplierPriceQuoteId;
        private System.Windows.Forms.TextBox textBox_supplierPriceQuoteId;
        private System.Windows.Forms.Label label_supplier;
        private System.Windows.Forms.ComboBox comboBox_supplier;
        private System.Windows.Forms.Label label_tender;
        private System.Windows.Forms.ComboBox comboBox_tender;
        private System.Windows.Forms.Label label_tradeCategory;
        private System.Windows.Forms.ComboBox comboBox_tradeCategory;
        private System.Windows.Forms.Label label_amount;
        private System.Windows.Forms.TextBox textBox_amount;
        private System.Windows.Forms.Label label_dateIssued;
        private System.Windows.Forms.TextBox textBox_dateIssued;
        private System.Windows.Forms.Label label_validUntil;
        private System.Windows.Forms.TextBox textBox_validUntil;
        private System.Windows.Forms.CheckBox checkBox_isSelected;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
    }
}

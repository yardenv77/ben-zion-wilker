namespace BenZionVilker
{
    partial class PurchaseOrderLinePanel
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
            this.dataGridView_lines = new System.Windows.Forms.DataGridView();
            this.label_purchaseOrderLineId = new System.Windows.Forms.Label();
            this.textBox_purchaseOrderLineId = new System.Windows.Forms.TextBox();
            this.label_purchaseOrder = new System.Windows.Forms.Label();
            this.comboBox_purchaseOrder = new System.Windows.Forms.ComboBox();
            this.label_description = new System.Windows.Forms.Label();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.label_unitOfMeasure = new System.Windows.Forms.Label();
            this.textBox_unitOfMeasure = new System.Windows.Forms.TextBox();
            this.label_quantity = new System.Windows.Forms.Label();
            this.textBox_quantity = new System.Windows.Forms.TextBox();
            this.label_unitPrice = new System.Windows.Forms.Label();
            this.textBox_unitPrice = new System.Windows.Forms.TextBox();
            this.label_receivedQuantity = new System.Windows.Forms.Label();
            this.textBox_receivedQuantity = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_back = new System.Windows.Forms.Button();
            this.label_receiveQty = new System.Windows.Forms.Label();
            this.textBox_receiveQty = new System.Windows.Forms.TextBox();
            this.button_receiveDelivery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_lines)).BeginInit();
            this.SuspendLayout();
            //
            // label_title
            //
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.label_title.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_title.Location = new System.Drawing.Point(320, 10);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(360, 40);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "שורות הזמנת רכש";
            //
            // dataGridView_lines
            //
            this.dataGridView_lines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_lines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_lines.Location = new System.Drawing.Point(50, 60);
            this.dataGridView_lines.Name = "dataGridView_lines";
            this.dataGridView_lines.ReadOnly = true;
            this.dataGridView_lines.RowTemplate.Height = 24;
            this.dataGridView_lines.Size = new System.Drawing.Size(900, 180);
            this.dataGridView_lines.TabIndex = 1;
            this.dataGridView_lines.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_lines_CellClick);
            //
            // label_purchaseOrderLineId
            //
            this.label_purchaseOrderLineId.AutoSize = true;
            this.label_purchaseOrderLineId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_purchaseOrderLineId.Location = new System.Drawing.Point(820, 260);
            this.label_purchaseOrderLineId.Name = "label_purchaseOrderLineId";
            this.label_purchaseOrderLineId.Size = new System.Drawing.Size(80, 17);
            this.label_purchaseOrderLineId.TabIndex = 2;
            this.label_purchaseOrderLineId.Text = "מזהה שורה";
            //
            // textBox_purchaseOrderLineId
            //
            this.textBox_purchaseOrderLineId.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_purchaseOrderLineId.Location = new System.Drawing.Point(550, 257);
            this.textBox_purchaseOrderLineId.Name = "textBox_purchaseOrderLineId";
            this.textBox_purchaseOrderLineId.ReadOnly = true;
            this.textBox_purchaseOrderLineId.Size = new System.Drawing.Size(250, 25);
            this.textBox_purchaseOrderLineId.TabIndex = 3;
            this.textBox_purchaseOrderLineId.TabStop = false;
            //
            // label_purchaseOrder
            //
            this.label_purchaseOrder.AutoSize = true;
            this.label_purchaseOrder.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_purchaseOrder.Location = new System.Drawing.Point(820, 295);
            this.label_purchaseOrder.Name = "label_purchaseOrder";
            this.label_purchaseOrder.Size = new System.Drawing.Size(90, 17);
            this.label_purchaseOrder.TabIndex = 4;
            this.label_purchaseOrder.Text = "הזמנת רכש";
            //
            // comboBox_purchaseOrder
            //
            this.comboBox_purchaseOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_purchaseOrder.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBox_purchaseOrder.FormattingEnabled = true;
            this.comboBox_purchaseOrder.Location = new System.Drawing.Point(550, 292);
            this.comboBox_purchaseOrder.Name = "comboBox_purchaseOrder";
            this.comboBox_purchaseOrder.Size = new System.Drawing.Size(250, 25);
            this.comboBox_purchaseOrder.TabIndex = 5;
            //
            // label_description
            //
            this.label_description.AutoSize = true;
            this.label_description.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_description.Location = new System.Drawing.Point(820, 330);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(48, 17);
            this.label_description.TabIndex = 6;
            this.label_description.Text = "תיאור";
            //
            // textBox_description
            //
            this.textBox_description.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_description.Location = new System.Drawing.Point(550, 327);
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(250, 25);
            this.textBox_description.TabIndex = 7;
            //
            // label_unitOfMeasure
            //
            this.label_unitOfMeasure.AutoSize = true;
            this.label_unitOfMeasure.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_unitOfMeasure.Location = new System.Drawing.Point(820, 365);
            this.label_unitOfMeasure.Name = "label_unitOfMeasure";
            this.label_unitOfMeasure.Size = new System.Drawing.Size(80, 17);
            this.label_unitOfMeasure.TabIndex = 8;
            this.label_unitOfMeasure.Text = "יחידת מידה";
            //
            // textBox_unitOfMeasure
            //
            this.textBox_unitOfMeasure.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_unitOfMeasure.Location = new System.Drawing.Point(550, 362);
            this.textBox_unitOfMeasure.Name = "textBox_unitOfMeasure";
            this.textBox_unitOfMeasure.Size = new System.Drawing.Size(250, 25);
            this.textBox_unitOfMeasure.TabIndex = 9;
            //
            // label_quantity
            //
            this.label_quantity.AutoSize = true;
            this.label_quantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_quantity.Location = new System.Drawing.Point(820, 400);
            this.label_quantity.Name = "label_quantity";
            this.label_quantity.Size = new System.Drawing.Size(48, 17);
            this.label_quantity.TabIndex = 10;
            this.label_quantity.Text = "כמות";
            //
            // textBox_quantity
            //
            this.textBox_quantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_quantity.Location = new System.Drawing.Point(550, 397);
            this.textBox_quantity.Name = "textBox_quantity";
            this.textBox_quantity.Size = new System.Drawing.Size(250, 25);
            this.textBox_quantity.TabIndex = 11;
            //
            // label_unitPrice
            //
            this.label_unitPrice.AutoSize = true;
            this.label_unitPrice.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_unitPrice.Location = new System.Drawing.Point(820, 435);
            this.label_unitPrice.Name = "label_unitPrice";
            this.label_unitPrice.Size = new System.Drawing.Size(80, 17);
            this.label_unitPrice.TabIndex = 12;
            this.label_unitPrice.Text = "מחיר יחידה";
            //
            // textBox_unitPrice
            //
            this.textBox_unitPrice.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_unitPrice.Location = new System.Drawing.Point(550, 432);
            this.textBox_unitPrice.Name = "textBox_unitPrice";
            this.textBox_unitPrice.Size = new System.Drawing.Size(250, 25);
            this.textBox_unitPrice.TabIndex = 13;
            //
            // label_receivedQuantity
            //
            this.label_receivedQuantity.AutoSize = true;
            this.label_receivedQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_receivedQuantity.Location = new System.Drawing.Point(820, 470);
            this.label_receivedQuantity.Name = "label_receivedQuantity";
            this.label_receivedQuantity.Size = new System.Drawing.Size(80, 17);
            this.label_receivedQuantity.TabIndex = 14;
            this.label_receivedQuantity.Text = "כמות שהתקבלה";
            //
            // textBox_receivedQuantity
            //
            this.textBox_receivedQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_receivedQuantity.Location = new System.Drawing.Point(550, 467);
            this.textBox_receivedQuantity.Name = "textBox_receivedQuantity";
            this.textBox_receivedQuantity.Size = new System.Drawing.Size(250, 25);
            this.textBox_receivedQuantity.TabIndex = 15;
            //
            // button_save
            //
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_save.Location = new System.Drawing.Point(750, 560);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 42);
            this.button_save.TabIndex = 16;
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
            this.button_update.TabIndex = 17;
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
            this.button_delete.TabIndex = 18;
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
            this.button_back.TabIndex = 19;
            this.button_back.Text = "חזרה";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            //
            // label_receiveQty
            //
            this.label_receiveQty.AutoSize = true;
            this.label_receiveQty.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label_receiveQty.Location = new System.Drawing.Point(820, 505);
            this.label_receiveQty.Name = "label_receiveQty";
            this.label_receiveQty.Size = new System.Drawing.Size(100, 17);
            this.label_receiveQty.TabIndex = 20;
            this.label_receiveQty.Text = "כמות לקבלה עכשיו";
            //
            // textBox_receiveQty
            //
            this.textBox_receiveQty.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox_receiveQty.Location = new System.Drawing.Point(550, 502);
            this.textBox_receiveQty.Name = "textBox_receiveQty";
            this.textBox_receiveQty.Size = new System.Drawing.Size(250, 25);
            this.textBox_receiveQty.TabIndex = 21;
            //
            // button_receiveDelivery
            //
            this.button_receiveDelivery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_receiveDelivery.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button_receiveDelivery.Location = new System.Drawing.Point(550, 610);
            this.button_receiveDelivery.Name = "button_receiveDelivery";
            this.button_receiveDelivery.Size = new System.Drawing.Size(310, 42);
            this.button_receiveDelivery.TabIndex = 22;
            this.button_receiveDelivery.Text = "רישום קבלת אספקה";
            this.button_receiveDelivery.UseVisualStyleBackColor = true;
            this.button_receiveDelivery.Click += new System.EventHandler(this.button_receiveDelivery_Click);
            //
            // PurchaseOrderLinePanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_receiveDelivery);
            this.Controls.Add(this.textBox_receiveQty);
            this.Controls.Add(this.label_receiveQty);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox_receivedQuantity);
            this.Controls.Add(this.label_receivedQuantity);
            this.Controls.Add(this.textBox_unitPrice);
            this.Controls.Add(this.label_unitPrice);
            this.Controls.Add(this.textBox_quantity);
            this.Controls.Add(this.label_quantity);
            this.Controls.Add(this.textBox_unitOfMeasure);
            this.Controls.Add(this.label_unitOfMeasure);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.label_description);
            this.Controls.Add(this.comboBox_purchaseOrder);
            this.Controls.Add(this.label_purchaseOrder);
            this.Controls.Add(this.textBox_purchaseOrderLineId);
            this.Controls.Add(this.label_purchaseOrderLineId);
            this.Controls.Add(this.dataGridView_lines);
            this.Controls.Add(this.label_title);
            this.Name = "PurchaseOrderLinePanel";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_lines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.DataGridView dataGridView_lines;
        private System.Windows.Forms.Label label_purchaseOrderLineId;
        private System.Windows.Forms.TextBox textBox_purchaseOrderLineId;
        private System.Windows.Forms.Label label_purchaseOrder;
        private System.Windows.Forms.ComboBox comboBox_purchaseOrder;
        private System.Windows.Forms.Label label_description;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.Label label_unitOfMeasure;
        private System.Windows.Forms.TextBox textBox_unitOfMeasure;
        private System.Windows.Forms.Label label_quantity;
        private System.Windows.Forms.TextBox textBox_quantity;
        private System.Windows.Forms.Label label_unitPrice;
        private System.Windows.Forms.TextBox textBox_unitPrice;
        private System.Windows.Forms.Label label_receivedQuantity;
        private System.Windows.Forms.TextBox textBox_receivedQuantity;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Label label_receiveQty;
        private System.Windows.Forms.TextBox textBox_receiveQty;
        private System.Windows.Forms.Button button_receiveDelivery;
    }
}

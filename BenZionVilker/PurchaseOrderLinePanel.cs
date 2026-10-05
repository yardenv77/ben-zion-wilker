using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך שורות הזמנת רכש — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// UC-03 (יצירת הזמנת רכש) מתאר הוספת שורות פריטים כחלק מה-MSS (שלב 5); כאן זה ממומש
    /// כמסך CRUD נפרד ל-PurchaseOrderLine, בדומה לשאר ישויות ה-FK, במקום כרשת מקוננת
    /// בתוך PurchaseOrderPanel. receivedQuantity הוא תצוגה בלבד (ReadOnly) -- משתנה
    /// אך ורק דרך PurchaseOrder.receiveDelivery() (BR-5), שממומש כאן בכפתור "רישום קבלת
    /// אספקה" (שלב 7.5) ולא דרך העדכון הגנרי (שלב 7.4).
    /// </summary>
    public partial class PurchaseOrderLinePanel : UserControl
    {
        private PurchaseOrderLine selectedLine;

        public PurchaseOrderLinePanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_lines);

            foreach (PurchaseOrder po in Program.PurchaseOrders)
                comboBox_purchaseOrder.Items.Add(po.getPurchaseOrderId() + " - " + po.getSupplier().getName());

            textBox_receivedQuantity.ReadOnly = true;

            loadLines();
        }

        private void loadLines()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("purchaseOrderLineId", typeof(int));
            dt.Columns.Add("purchaseOrder", typeof(int));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("unitOfMeasure", typeof(string));
            dt.Columns.Add("quantity", typeof(double));
            dt.Columns.Add("unitPrice", typeof(decimal));
            dt.Columns.Add("receivedQuantity", typeof(double));

            foreach (PurchaseOrderLine line in Program.PurchaseOrderLines)
            {
                dt.Rows.Add(line.getPurchaseOrderLineId(), line.getPurchaseOrder().getPurchaseOrderId(), line.getDescription(),
                    line.getUnitOfMeasure(), line.getQuantity(), line.getUnitPrice(), line.getReceivedQuantity());
            }

            dataGridView_lines.DataSource = dt;
            dataGridView_lines.Columns["unitPrice"].DefaultCellStyle.Format = "N2";

            dataGridView_lines.Columns["purchaseOrderLineId"].HeaderText = "מס'";
            dataGridView_lines.Columns["purchaseOrder"].HeaderText = "הזמנת רכש";
            dataGridView_lines.Columns["description"].HeaderText = "תיאור";
            dataGridView_lines.Columns["unitOfMeasure"].HeaderText = "יח' מידה";
            dataGridView_lines.Columns["quantity"].HeaderText = "כמות";
            dataGridView_lines.Columns["unitPrice"].HeaderText = "מחיר יחידה";
            dataGridView_lines.Columns["receivedQuantity"].HeaderText = "כמות שהתקבלה";
        }

        private void dataGridView_lines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_lines.Rows[e.RowIndex].Cells["purchaseOrderLineId"].Value.ToString());
            selectedLine = PurchaseOrderLine.seekPurchaseOrderLine(id);
            if (selectedLine == null) return;

            textBox_purchaseOrderLineId.Text = selectedLine.getPurchaseOrderLineId().ToString();
            comboBox_purchaseOrder.Text = selectedLine.getPurchaseOrder().getPurchaseOrderId() + " - " + selectedLine.getPurchaseOrder().getSupplier().getName();
            textBox_description.Text = selectedLine.getDescription();
            textBox_unitOfMeasure.Text = selectedLine.getUnitOfMeasure();
            textBox_quantity.Text = selectedLine.getQuantity().ToString();
            textBox_unitPrice.Text = selectedLine.getUnitPrice().ToString();
            textBox_receivedQuantity.Text = selectedLine.getReceivedQuantity().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_purchaseOrder.Text))
            {
                MessageBox.Show("יש לבחור הזמנת רכש", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_description.Text))
            {
                MessageBox.Show("יש להזין תיאור פריט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_unitOfMeasure.Text))
            {
                MessageBox.Show("יש להזין יחידת מידה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!double.TryParse(textBox_quantity.Text, out double quantity) || quantity <= 0)
            {
                MessageBox.Show("יש להזין כמות תקינה (גדולה מ-0)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_unitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("יש להזין מחיר יחידה תקין (לא שלילי)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private PurchaseOrder resolveSelectedPurchaseOrder()
        {
            int id = int.Parse(comboBox_purchaseOrder.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return PurchaseOrder.seekPurchaseOrder(id);
        }

        private void clearForm()
        {
            selectedLine = null;
            textBox_purchaseOrderLineId.Text = "";
            comboBox_purchaseOrder.SelectedIndex = -1;
            comboBox_purchaseOrder.Text = "";
            textBox_description.Text = "";
            textBox_unitOfMeasure.Text = "";
            textBox_quantity.Text = "";
            textBox_unitPrice.Text = "";
            textBox_receivedQuantity.Text = "0";
            textBox_receiveQty.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = PurchaseOrderLine.getNextPurchaseOrderLineId();
            // A new line always starts at 0 received (BR-5) -- receivedQuantity changes
            // only through PurchaseOrder.receiveDelivery(), never at line creation.
            PurchaseOrder po = resolveSelectedPurchaseOrder();
            PurchaseOrderLine line = new PurchaseOrderLine(id, po, textBox_description.Text, textBox_unitOfMeasure.Text,
                double.Parse(textBox_quantity.Text), decimal.Parse(textBox_unitPrice.Text), 0, false);

            // t1 in the state diagram: the order adds the line and recalculates its totals
            try
            {
                po.addLine(line);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
                return;
            }
            if (!Program.PurchaseOrderLines.Contains(line)) return;

            MessageBox.Show("שורת הפריט נשמרה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadLines();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedLine == null)
            {
                MessageBox.Show("יש לבחור שורה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            // Checked here, before any field changes, so a refused edit leaves the line untouched
            PurchaseOrder po = resolveSelectedPurchaseOrder();
            if (selectedLine.getPurchaseOrder().getStatus() != POStatus.Draft || po.getStatus() != POStatus.Draft)
            {
                MessageBox.Show("ניתן לערוך שורות רק בהזמנה שנמצאת בטיוטה", "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
                return;
            }

            selectedLine.setPurchaseOrder(po);
            selectedLine.setDescription(textBox_description.Text);
            selectedLine.setUnitOfMeasure(textBox_unitOfMeasure.Text);
            selectedLine.setQuantity(double.Parse(textBox_quantity.Text));
            selectedLine.setUnitPrice(decimal.Parse(textBox_unitPrice.Text));

            // t1 in the state diagram: the order saves the line and recalculates its totals
            try
            {
                if (!po.editLine(selectedLine)) return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("שורת הפריט עודכנה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadLines();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedLine == null)
            {
                MessageBox.Show("יש לבחור שורה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את שורת הפריט?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedLine.deletePurchaseOrderLine()) return;
            clearForm();
            loadLines();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }

        private void button_receiveDelivery_Click(object sender, EventArgs e)
        {
            if (selectedLine == null)
            {
                MessageBox.Show("יש לבחור שורה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!double.TryParse(textBox_receiveQty.Text, out double qty))
            {
                MessageBox.Show("יש להזין כמות תקינה לקבלה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            try
            {
                selectedLine.getPurchaseOrder().receiveDelivery(selectedLine, qty);
                MessageBox.Show("קבלת האספקה נרשמה בהצלחה", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadLines();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }
    }
}

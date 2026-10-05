using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול תשלומים לספקים — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-SupplierPayment ב-00e-use-cases.md; נבנה לפי אותה תבנית כמו EmployeePanel.
    /// businessPartner מפנה ל-BusinessPartner (ספק או קבלן משנה כאחד), לפי design/class-diagram.md
    /// Section 5 -- ראו הערה ב-SupplierPayment.cs.
    /// </summary>
    public partial class SupplierPaymentPanel : UserControl
    {
        private SupplierPayment selectedSupplierPayment;

        public SupplierPaymentPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_supplierPayments);

            foreach (BusinessPartner bp in Program.BusinessPartners)
                comboBox_businessPartner.Items.Add(bp.getBusinessPartnerId() + " - " + bp.getName());
            foreach (Project p in Program.Projects)
                comboBox_project.Items.Add(p.getProjectId() + " - " + p.getName());
            foreach (SupplierPaymentStatus s in Enum.GetValues(typeof(SupplierPaymentStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadSupplierPayments();
        }

        private void loadSupplierPayments()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("supplierPaymentId", typeof(int));
            dt.Columns.Add("invoiceNumber", typeof(string));
            dt.Columns.Add("businessPartner", typeof(string));
            dt.Columns.Add("project", typeof(string));
            dt.Columns.Add("amount", typeof(decimal));
            dt.Columns.Add("dueDate", typeof(DateTime));
            dt.Columns.Add("paidDate", typeof(string));
            dt.Columns.Add("status", typeof(string));

            foreach (SupplierPayment sp in Program.SupplierPayments)
            {
                dt.Rows.Add(sp.getSupplierPaymentId(), sp.getInvoiceNumber(), sp.getBusinessPartner().getName(), sp.getProject().getName(), sp.getAmount(), sp.getDueDate(),
                    sp.getPaidDate().HasValue ? sp.getPaidDate().Value.ToString("yyyy-MM-dd") : "", sp.getStatus().ToString());
            }

            dataGridView_supplierPayments.DataSource = dt;
            dataGridView_supplierPayments.Columns["amount"].DefaultCellStyle.Format = "N2";
            dataGridView_supplierPayments.Columns["dueDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_supplierPayments.Columns["supplierPaymentId"].HeaderText = "מס'";
            dataGridView_supplierPayments.Columns["invoiceNumber"].HeaderText = "מס' חשבונית";
            dataGridView_supplierPayments.Columns["businessPartner"].HeaderText = "ספק/קבלן משנה";
            dataGridView_supplierPayments.Columns["project"].HeaderText = "פרויקט";
            dataGridView_supplierPayments.Columns["amount"].HeaderText = "סכום";
            dataGridView_supplierPayments.Columns["dueDate"].HeaderText = "תאריך פירעון";
            dataGridView_supplierPayments.Columns["paidDate"].HeaderText = "תאריך תשלום";
            dataGridView_supplierPayments.Columns["status"].HeaderText = "סטטוס";
        }

        private void dataGridView_supplierPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_supplierPayments.Rows[e.RowIndex].Cells["supplierPaymentId"].Value.ToString());
            selectedSupplierPayment = SupplierPayment.seekSupplierPayment(id);
            if (selectedSupplierPayment == null) return;

            textBox_supplierPaymentId.Text = selectedSupplierPayment.getSupplierPaymentId().ToString();
            textBox_invoiceNumber.Text = selectedSupplierPayment.getInvoiceNumber();
            comboBox_businessPartner.Text = selectedSupplierPayment.getBusinessPartner().getBusinessPartnerId() + " - " + selectedSupplierPayment.getBusinessPartner().getName();
            textBox_amount.Text = selectedSupplierPayment.getAmount().ToString();
            textBox_dueDate.Text = selectedSupplierPayment.getDueDate().ToString("yyyy-MM-dd");
            textBox_paidDate.Text = selectedSupplierPayment.getPaidDate().HasValue ? selectedSupplierPayment.getPaidDate().Value.ToString("yyyy-MM-dd") : "";
            comboBox_status.Text = selectedSupplierPayment.getStatus().ToString();
            comboBox_project.Text = selectedSupplierPayment.getProject().getProjectId() + " - " + selectedSupplierPayment.getProject().getName();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_invoiceNumber.Text))
            {
                MessageBox.Show("יש להזין מספר חשבונית", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_businessPartner.Text))
            {
                MessageBox.Show("יש לבחור ספק/קבלן משנה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (comboBox_project.SelectedIndex < 0)
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_amount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("יש להזין סכום תקין (גדול מ-0)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_dueDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך פירעון תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox_paidDate.Text) && !DateTime.TryParse(textBox_paidDate.Text, out _))
            {
                MessageBox.Show("תאריך תשלום אינו תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            // A Paid payment needs its payment date: UC-05's monthly cash flow places it by that date
            bool isPaid = comboBox_status.Text == SupplierPaymentStatus.Paid.ToString();
            if (isPaid && string.IsNullOrWhiteSpace(textBox_paidDate.Text))
            {
                MessageBox.Show("תשלום בסטטוס Paid חייב תאריך תשלום", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!isPaid && !string.IsNullOrWhiteSpace(textBox_paidDate.Text))
            {
                MessageBox.Show("תאריך תשלום מוזן רק לתשלום בסטטוס Paid", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private BusinessPartner resolveSelectedBusinessPartner()
        {
            int id = int.Parse(comboBox_businessPartner.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return BusinessPartner.seekBusinessPartner(id);
        }

        private Project resolveSelectedProject()
        {
            int id = int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Project.seekProject(id);
        }

        private void clearForm()
        {
            selectedSupplierPayment = null;
            textBox_supplierPaymentId.Text = "";
            textBox_invoiceNumber.Text = "";
            comboBox_businessPartner.SelectedIndex = -1;
            comboBox_businessPartner.Text = "";
            comboBox_project.SelectedIndex = -1;
            textBox_amount.Text = "";
            textBox_dueDate.Text = "";
            textBox_paidDate.Text = "";
            comboBox_status.SelectedIndex = 0;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = SupplierPayment.getNextSupplierPaymentId();
            SupplierPaymentStatus status = (SupplierPaymentStatus)Enum.Parse(typeof(SupplierPaymentStatus), comboBox_status.Text);
            DateTime? paidDate = string.IsNullOrWhiteSpace(textBox_paidDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_paidDate.Text);

            SupplierPayment supplierPayment = new SupplierPayment(id, textBox_invoiceNumber.Text, resolveSelectedBusinessPartner(), resolveSelectedProject(), decimal.Parse(textBox_amount.Text),
                DateTime.Parse(textBox_dueDate.Text), paidDate, status, true);

            if (!Program.SupplierPayments.Contains(supplierPayment)) return;

            MessageBox.Show("התשלום נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSupplierPayments();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedSupplierPayment == null)
            {
                MessageBox.Show("יש לבחור תשלום מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedSupplierPayment.setInvoiceNumber(textBox_invoiceNumber.Text);
            selectedSupplierPayment.setBusinessPartner(resolveSelectedBusinessPartner());
            selectedSupplierPayment.setProject(resolveSelectedProject());
            selectedSupplierPayment.setAmount(decimal.Parse(textBox_amount.Text));
            selectedSupplierPayment.setDueDate(DateTime.Parse(textBox_dueDate.Text));
            selectedSupplierPayment.setPaidDate(string.IsNullOrWhiteSpace(textBox_paidDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_paidDate.Text));
            selectedSupplierPayment.setStatus((SupplierPaymentStatus)Enum.Parse(typeof(SupplierPaymentStatus), comboBox_status.Text));
            if (!selectedSupplierPayment.updateSupplierPayment()) return;

            MessageBox.Show("התשלום עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSupplierPayments();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedSupplierPayment == null)
            {
                MessageBox.Show("יש לבחור תשלום מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את התשלום?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedSupplierPayment.deleteSupplierPayment()) return;
            clearForm();
            loadSupplierPayments();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

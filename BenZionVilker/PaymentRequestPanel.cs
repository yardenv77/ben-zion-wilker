using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול בקשות תשלום מלקוח — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-PaymentRequest ב-00e-use-cases.md; נבנה לפי אותה תבנית כמו EmployeePanel.
    /// approvalDate/missingDocuments אופציונליים (PaymentRequest.cs) -- שדה ריק = DBNull.
    /// </summary>
    public partial class PaymentRequestPanel : UserControl
    {
        private PaymentRequest selectedPaymentRequest;

        public PaymentRequestPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_paymentRequests);

            foreach (Project p in Program.Projects)
                comboBox_project.Items.Add(p.getProjectId() + " - " + p.getName());
            foreach (PaymentRequestStatus s in Enum.GetValues(typeof(PaymentRequestStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadPaymentRequests();
        }

        private void loadPaymentRequests()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("paymentRequestId", typeof(int));
            dt.Columns.Add("project", typeof(string));
            dt.Columns.Add("amount", typeof(decimal));
            dt.Columns.Add("submissionDate", typeof(DateTime));
            dt.Columns.Add("approvalDate", typeof(string));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("missingDocuments", typeof(string));

            foreach (PaymentRequest pr in Program.PaymentRequests)
            {
                dt.Rows.Add(pr.getPaymentRequestId(), pr.getProject().getName(), pr.getAmount(), pr.getSubmissionDate(),
                    pr.getApprovalDate().HasValue ? pr.getApprovalDate().Value.ToString("yyyy-MM-dd") : "",
                    pr.getStatus().ToString(), string.Join(", ", pr.getMissingDocuments()));
            }

            dataGridView_paymentRequests.DataSource = dt;
            dataGridView_paymentRequests.Columns["amount"].DefaultCellStyle.Format = "N2";
            dataGridView_paymentRequests.Columns["submissionDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_paymentRequests.Columns["paymentRequestId"].HeaderText = "מס'";
            dataGridView_paymentRequests.Columns["project"].HeaderText = "פרויקט";
            dataGridView_paymentRequests.Columns["amount"].HeaderText = "סכום";
            dataGridView_paymentRequests.Columns["submissionDate"].HeaderText = "תאריך הגשה";
            dataGridView_paymentRequests.Columns["approvalDate"].HeaderText = "תאריך אישור";
            dataGridView_paymentRequests.Columns["status"].HeaderText = "סטטוס";
            dataGridView_paymentRequests.Columns["missingDocuments"].HeaderText = "מסמכים חסרים";
        }

        private void dataGridView_paymentRequests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_paymentRequests.Rows[e.RowIndex].Cells["paymentRequestId"].Value.ToString());
            selectedPaymentRequest = PaymentRequest.seekPaymentRequest(id);
            if (selectedPaymentRequest == null) return;

            textBox_paymentRequestId.Text = selectedPaymentRequest.getPaymentRequestId().ToString();
            comboBox_project.Text = selectedPaymentRequest.getProject().getProjectId() + " - " + selectedPaymentRequest.getProject().getName();
            textBox_amount.Text = selectedPaymentRequest.getAmount().ToString();
            textBox_submissionDate.Text = selectedPaymentRequest.getSubmissionDate().ToString("yyyy-MM-dd");
            textBox_approvalDate.Text = selectedPaymentRequest.getApprovalDate().HasValue ? selectedPaymentRequest.getApprovalDate().Value.ToString("yyyy-MM-dd") : "";
            comboBox_status.Text = selectedPaymentRequest.getStatus().ToString();
            textBox_missingDocuments.Text = string.Join(", ", selectedPaymentRequest.getMissingDocuments());
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_project.Text))
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_amount.Text, out _))
            {
                MessageBox.Show("יש להזין סכום תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_submissionDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך הגשה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox_approvalDate.Text) && !DateTime.TryParse(textBox_approvalDate.Text, out _))
            {
                MessageBox.Show("תאריך אישור אינו תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Project resolveSelectedProject()
        {
            int id = int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Project.seekProject(id);
        }

        private void clearForm()
        {
            selectedPaymentRequest = null;
            textBox_paymentRequestId.Text = "";
            comboBox_project.SelectedIndex = -1;
            comboBox_project.Text = "";
            textBox_amount.Text = "";
            textBox_submissionDate.Text = "";
            textBox_approvalDate.Text = "";
            comboBox_status.SelectedIndex = 0;
            textBox_missingDocuments.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = PaymentRequest.getNextPaymentRequestId();
            PaymentRequestStatus status = (PaymentRequestStatus)Enum.Parse(typeof(PaymentRequestStatus), comboBox_status.Text);
            DateTime? approvalDate = string.IsNullOrWhiteSpace(textBox_approvalDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_approvalDate.Text);

            new PaymentRequest(id, resolveSelectedProject(), decimal.Parse(textBox_amount.Text),
                DateTime.Parse(textBox_submissionDate.Text), approvalDate, status, true);

            MessageBox.Show("בקשת התשלום נשמרה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadPaymentRequests();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedPaymentRequest == null)
            {
                MessageBox.Show("יש לבחור בקשת תשלום מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedPaymentRequest.setProject(resolveSelectedProject());
            selectedPaymentRequest.setAmount(decimal.Parse(textBox_amount.Text));
            selectedPaymentRequest.setSubmissionDate(DateTime.Parse(textBox_submissionDate.Text));
            selectedPaymentRequest.setApprovalDate(string.IsNullOrWhiteSpace(textBox_approvalDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_approvalDate.Text));
            selectedPaymentRequest.setStatus((PaymentRequestStatus)Enum.Parse(typeof(PaymentRequestStatus), comboBox_status.Text));
            selectedPaymentRequest.updatePaymentRequest();

            MessageBox.Show("בקשת התשלום עודכנה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadPaymentRequests();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedPaymentRequest == null)
            {
                MessageBox.Show("יש לבחור בקשת תשלום מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את בקשת התשלום?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedPaymentRequest.deletePaymentRequest();
            clearForm();
            loadPaymentRequests();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

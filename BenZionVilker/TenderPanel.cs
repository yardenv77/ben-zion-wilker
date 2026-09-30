using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול מכרזים — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-Tender ב-00e-use-cases.md; נבנה לפי אותה תבנית כמו EmployeePanel.
    /// תאריכים מוזנים כטקסט בפורמט yyyy-MM-dd, כמו יתר שדות הטקסט במסך (אין DateTimePicker
    /// בתבנית הקיימת).
    /// </summary>
    public partial class TenderPanel : UserControl
    {
        private Tender selectedTender;

        public TenderPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_tenders);

            foreach (Client c in Program.Clients)
                comboBox_client.Items.Add(c.getClientId() + " - " + c.getName());
            foreach (TenderStatus s in Enum.GetValues(typeof(TenderStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadTenders();
        }

        private void loadTenders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tenderId", typeof(int));
            dt.Columns.Add("tenderNumber", typeof(string));
            dt.Columns.Add("client", typeof(string));
            dt.Columns.Add("title", typeof(string));
            dt.Columns.Add("estimatedValue", typeof(decimal));
            dt.Columns.Add("submissionDeadline", typeof(DateTime));
            dt.Columns.Add("publishedDate", typeof(DateTime));
            dt.Columns.Add("status", typeof(string));

            foreach (Tender t in Program.Tenders)
            {
                dt.Rows.Add(t.getTenderId(), t.getTenderNumber(), t.getClient().getName(), t.getTitle(), t.getEstimatedValue(),
                    t.getSubmissionDeadline(), t.getPublishedDate(), t.getStatus().ToString());
            }

            dataGridView_tenders.DataSource = dt;
            dataGridView_tenders.Columns["estimatedValue"].DefaultCellStyle.Format = "N2";
            dataGridView_tenders.Columns["submissionDeadline"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView_tenders.Columns["publishedDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_tenders.Columns["tenderId"].HeaderText = "מס'";
            dataGridView_tenders.Columns["tenderNumber"].HeaderText = "מס' מכרז";
            dataGridView_tenders.Columns["client"].HeaderText = "לקוח";
            dataGridView_tenders.Columns["title"].HeaderText = "כותרת";
            dataGridView_tenders.Columns["estimatedValue"].HeaderText = "שווי משוער";
            dataGridView_tenders.Columns["submissionDeadline"].HeaderText = "מועד הגשה";
            dataGridView_tenders.Columns["publishedDate"].HeaderText = "תאריך פרסום";
            dataGridView_tenders.Columns["status"].HeaderText = "סטטוס";
        }

        private void dataGridView_tenders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_tenders.Rows[e.RowIndex].Cells["tenderId"].Value.ToString());
            selectedTender = Tender.seekTender(id);
            if (selectedTender == null) return;

            textBox_tenderId.Text = selectedTender.getTenderId().ToString();
            textBox_tenderNumber.Text = selectedTender.getTenderNumber();
            comboBox_client.Text = selectedTender.getClient().getClientId() + " - " + selectedTender.getClient().getName();
            textBox_title.Text = selectedTender.getTitle();
            textBox_estimatedValue.Text = selectedTender.getEstimatedValue().ToString();
            textBox_submissionDeadline.Text = selectedTender.getSubmissionDeadline().ToString("yyyy-MM-dd");
            textBox_publishedDate.Text = selectedTender.getPublishedDate().ToString("yyyy-MM-dd");
            comboBox_status.Text = selectedTender.getStatus().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_tenderNumber.Text))
            {
                MessageBox.Show("יש להזין מספר מכרז", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (comboBox_client.SelectedIndex < 0 && string.IsNullOrWhiteSpace(comboBox_client.Text))
            {
                MessageBox.Show("יש לבחור לקוח", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_title.Text))
            {
                MessageBox.Show("יש להזין כותרת מכרז", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_estimatedValue.Text, out _))
            {
                MessageBox.Show("יש להזין שווי משוער תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_submissionDeadline.Text, out _))
            {
                MessageBox.Show("יש להזין מועד הגשה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_publishedDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך פרסום תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Client resolveSelectedClient()
        {
            int id = int.Parse(comboBox_client.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Client.seekClient(id);
        }

        private void clearForm()
        {
            selectedTender = null;
            textBox_tenderId.Text = "";
            textBox_tenderNumber.Text = "";
            comboBox_client.SelectedIndex = -1;
            comboBox_client.Text = "";
            textBox_title.Text = "";
            textBox_estimatedValue.Text = "";
            textBox_submissionDeadline.Text = "";
            textBox_publishedDate.Text = "";
            comboBox_status.SelectedIndex = 0;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = Tender.getNextTenderId();
            TenderStatus status = (TenderStatus)Enum.Parse(typeof(TenderStatus), comboBox_status.Text);
            new Tender(id, textBox_tenderNumber.Text, resolveSelectedClient(), textBox_title.Text, decimal.Parse(textBox_estimatedValue.Text),
                DateTime.Parse(textBox_submissionDeadline.Text), DateTime.Parse(textBox_publishedDate.Text), status, true);

            MessageBox.Show("המכרז נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadTenders();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedTender == null)
            {
                MessageBox.Show("יש לבחור מכרז מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedTender.setTenderNumber(textBox_tenderNumber.Text);
            selectedTender.setClient(resolveSelectedClient());
            selectedTender.setTitle(textBox_title.Text);
            selectedTender.setEstimatedValue(decimal.Parse(textBox_estimatedValue.Text));
            selectedTender.setSubmissionDeadline(DateTime.Parse(textBox_submissionDeadline.Text));
            selectedTender.setPublishedDate(DateTime.Parse(textBox_publishedDate.Text));
            selectedTender.setStatus((TenderStatus)Enum.Parse(typeof(TenderStatus), comboBox_status.Text));
            selectedTender.updateTender();

            MessageBox.Show("המכרז עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadTenders();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedTender == null)
            {
                MessageBox.Show("יש לבחור מכרז מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את המכרז?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedTender.deleteTender();
            clearForm();
            loadTenders();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

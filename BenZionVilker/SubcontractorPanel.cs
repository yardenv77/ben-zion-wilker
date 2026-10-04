using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול קבלני משנה — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-Subcontractor ב-00e-use-cases.md; נבנה לפי אותה תבנית כמו SupplierPanel,
    /// שכן שתיהן תת-מחלקות של BusinessPartner (ראו CLAUDE.md, Inheritance -- Table-per-Subclass).
    /// </summary>
    public partial class SubcontractorPanel : UserControl
    {
        private Subcontractor selectedSubcontractor;

        public SubcontractorPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_subcontractors);

            foreach (PartnerStatus s in Enum.GetValues(typeof(PartnerStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadSubcontractors();
        }

        private void loadSubcontractors()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("businessPartnerId", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("companyRegistrationNo", typeof(string));
            dt.Columns.Add("contactPerson", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("rating", typeof(double));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("tradeSpecialty", typeof(string));
            dt.Columns.Add("dailyRate", typeof(decimal));

            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (!(bp is Subcontractor sub)) continue;
                dt.Rows.Add(sub.getBusinessPartnerId(), sub.getName(), sub.getCompanyRegistrationNo(), sub.getContactPerson(),
                    sub.getPhone(), sub.getEmail(), sub.getRating(), sub.getStatus().ToString(), sub.getTradeSpecialty(), sub.getDailyRate());
            }

            dataGridView_subcontractors.DataSource = dt;
            dataGridView_subcontractors.Columns["dailyRate"].DefaultCellStyle.Format = "N2";

            dataGridView_subcontractors.Columns["businessPartnerId"].HeaderText = "מס'";
            dataGridView_subcontractors.Columns["name"].HeaderText = "שם";
            dataGridView_subcontractors.Columns["companyRegistrationNo"].HeaderText = "ח.פ.";
            dataGridView_subcontractors.Columns["contactPerson"].HeaderText = "איש קשר";
            dataGridView_subcontractors.Columns["phone"].HeaderText = "טלפון";
            dataGridView_subcontractors.Columns["email"].HeaderText = "דוא\"ל";
            dataGridView_subcontractors.Columns["rating"].HeaderText = "דירוג";
            dataGridView_subcontractors.Columns["status"].HeaderText = "סטטוס";
            dataGridView_subcontractors.Columns["tradeSpecialty"].HeaderText = "תחום התמחות";
            dataGridView_subcontractors.Columns["dailyRate"].HeaderText = "תעריף יומי";
        }

        private void dataGridView_subcontractors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_subcontractors.Rows[e.RowIndex].Cells["businessPartnerId"].Value.ToString());
            selectedSubcontractor = BusinessPartner.seekBusinessPartner(id) as Subcontractor;
            if (selectedSubcontractor == null) return;

            textBox_businessPartnerId.Text = selectedSubcontractor.getBusinessPartnerId().ToString();
            textBox_name.Text = selectedSubcontractor.getName();
            textBox_companyRegistrationNo.Text = selectedSubcontractor.getCompanyRegistrationNo();
            textBox_contactPerson.Text = selectedSubcontractor.getContactPerson();
            textBox_phone.Text = selectedSubcontractor.getPhone();
            textBox_email.Text = selectedSubcontractor.getEmail();
            textBox_rating.Text = selectedSubcontractor.getRating().ToString();
            comboBox_status.Text = selectedSubcontractor.getStatus().ToString();
            textBox_tradeSpecialty.Text = selectedSubcontractor.getTradeSpecialty();
            textBox_dailyRate.Text = selectedSubcontractor.getDailyRate().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_name.Text))
            {
                MessageBox.Show("יש להזין שם קבלן משנה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_companyRegistrationNo.Text))
            {
                MessageBox.Show("יש להזין מספר ח.פ.", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_contactPerson.Text))
            {
                MessageBox.Show("יש להזין איש קשר", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_phone.Text))
            {
                MessageBox.Show("יש להזין טלפון", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_email.Text))
            {
                MessageBox.Show("יש להזין דוא\"ל", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!double.TryParse(textBox_rating.Text, out _))
            {
                MessageBox.Show("יש להזין דירוג תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_tradeSpecialty.Text))
            {
                MessageBox.Show("יש להזין תחום התמחות", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_dailyRate.Text, out _))
            {
                MessageBox.Show("יש להזין תעריף יומי תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void clearForm()
        {
            selectedSubcontractor = null;
            textBox_businessPartnerId.Text = "";
            textBox_name.Text = "";
            textBox_companyRegistrationNo.Text = "";
            textBox_contactPerson.Text = "";
            textBox_phone.Text = "";
            textBox_email.Text = "";
            textBox_rating.Text = "5";
            comboBox_status.SelectedIndex = 0;
            textBox_tradeSpecialty.Text = "";
            textBox_dailyRate.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = BusinessPartner.getNextBusinessPartnerId();
            PartnerStatus status = (PartnerStatus)Enum.Parse(typeof(PartnerStatus), comboBox_status.Text);
            Subcontractor sub = new Subcontractor(id, textBox_name.Text, textBox_companyRegistrationNo.Text, textBox_contactPerson.Text,
                textBox_phone.Text, textBox_email.Text, double.Parse(textBox_rating.Text), status,
                textBox_tradeSpecialty.Text, decimal.Parse(textBox_dailyRate.Text), true);

            if (!Program.BusinessPartners.Contains(sub)) return;

            MessageBox.Show("קבלן המשנה נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSubcontractors();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedSubcontractor == null)
            {
                MessageBox.Show("יש לבחור קבלן משנה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedSubcontractor.setName(textBox_name.Text);
            selectedSubcontractor.setCompanyRegistrationNo(textBox_companyRegistrationNo.Text);
            selectedSubcontractor.setContactPerson(textBox_contactPerson.Text);
            selectedSubcontractor.setPhone(textBox_phone.Text);
            selectedSubcontractor.setEmail(textBox_email.Text);
            selectedSubcontractor.setRating(double.Parse(textBox_rating.Text));
            selectedSubcontractor.setStatus((PartnerStatus)Enum.Parse(typeof(PartnerStatus), comboBox_status.Text));
            selectedSubcontractor.setTradeSpecialty(textBox_tradeSpecialty.Text);
            selectedSubcontractor.setDailyRate(decimal.Parse(textBox_dailyRate.Text));
            if (!selectedSubcontractor.updateSubcontractor()) return;

            MessageBox.Show("קבלן המשנה עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSubcontractors();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedSubcontractor == null)
            {
                MessageBox.Show("יש לבחור קבלן משנה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את קבלן המשנה?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedSubcontractor.deleteSubcontractor()) return;
            clearForm();
            loadSubcontractors();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

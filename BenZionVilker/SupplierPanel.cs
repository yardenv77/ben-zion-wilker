using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול ספקים (UC-01) — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// לפי 00e-use-cases.md UC-01: מחיקה היא סופטית (סטטוס Inactive), לכן כפתור המחיקה
    /// כאן מעדכן את הסטטוס במקום להסיר את הרשומה — בדומה ל-Scenario C ב-UC-01.
    /// </summary>
    public partial class SupplierPanel : UserControl
    {
        private Supplier selectedSupplier;

        public SupplierPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_suppliers);

            foreach (PartnerStatus s in Enum.GetValues(typeof(PartnerStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadSuppliers();
        }

        private void loadSuppliers()
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

            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (!(bp is Supplier sup)) continue;
                dt.Rows.Add(sup.getBusinessPartnerId(), sup.getName(), sup.getCompanyRegistrationNo(), sup.getContactPerson(),
                    sup.getPhone(), sup.getEmail(), sup.getRating(), sup.getStatus().ToString());
            }

            dataGridView_suppliers.DataSource = dt;
            dataGridView_suppliers.Columns["businessPartnerId"].HeaderText = "מס'";
            dataGridView_suppliers.Columns["name"].HeaderText = "שם ספק";
            dataGridView_suppliers.Columns["companyRegistrationNo"].HeaderText = "ח.פ.";
            dataGridView_suppliers.Columns["contactPerson"].HeaderText = "איש קשר";
            dataGridView_suppliers.Columns["phone"].HeaderText = "טלפון";
            dataGridView_suppliers.Columns["email"].HeaderText = "דוא\"ל";
            dataGridView_suppliers.Columns["rating"].HeaderText = "דירוג";
            dataGridView_suppliers.Columns["status"].HeaderText = "סטטוס";
        }

        private void dataGridView_suppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_suppliers.Rows[e.RowIndex].Cells["businessPartnerId"].Value.ToString());
            selectedSupplier = BusinessPartner.seekBusinessPartner(id) as Supplier;
            if (selectedSupplier == null) return;

            textBox_businessPartnerId.Text = selectedSupplier.getBusinessPartnerId().ToString();
            textBox_name.Text = selectedSupplier.getName();
            textBox_companyRegistrationNo.Text = selectedSupplier.getCompanyRegistrationNo();
            textBox_contactPerson.Text = selectedSupplier.getContactPerson();
            textBox_phone.Text = selectedSupplier.getPhone();
            textBox_email.Text = selectedSupplier.getEmail();
            textBox_rating.Text = selectedSupplier.getRating().ToString();
            comboBox_status.Text = selectedSupplier.getStatus().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_name.Text))
            {
                MessageBox.Show("יש להזין שם ספק", "שגיאה", MessageBoxButtons.OK);
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
            if (!double.TryParse(textBox_rating.Text, out double rating))
            {
                MessageBox.Show("יש להזין דירוג תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (rating < 0 || rating > 5)
            {
                MessageBox.Show("הדירוג חייב להיות בין 0 ל-5", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void clearForm()
        {
            selectedSupplier = null;
            textBox_businessPartnerId.Text = "";
            textBox_name.Text = "";
            textBox_companyRegistrationNo.Text = "";
            textBox_contactPerson.Text = "";
            textBox_phone.Text = "";
            textBox_email.Text = "";
            textBox_rating.Text = "5";
            comboBox_status.SelectedIndex = 0;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = BusinessPartner.getNextBusinessPartnerId();
            PartnerStatus status = (PartnerStatus)Enum.Parse(typeof(PartnerStatus), comboBox_status.Text);
            Supplier sup = new Supplier(id, textBox_name.Text, textBox_companyRegistrationNo.Text, textBox_contactPerson.Text,
                textBox_phone.Text, textBox_email.Text, double.Parse(textBox_rating.Text), status, true);

            if (!Program.BusinessPartners.Contains(sup)) return;

            MessageBox.Show("הספק נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSuppliers();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedSupplier == null)
            {
                MessageBox.Show("יש לבחור ספק מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedSupplier.setName(textBox_name.Text);
            selectedSupplier.setCompanyRegistrationNo(textBox_companyRegistrationNo.Text);
            selectedSupplier.setContactPerson(textBox_contactPerson.Text);
            selectedSupplier.setPhone(textBox_phone.Text);
            selectedSupplier.setEmail(textBox_email.Text);
            selectedSupplier.setRating(double.Parse(textBox_rating.Text));
            selectedSupplier.setStatus((PartnerStatus)Enum.Parse(typeof(PartnerStatus), comboBox_status.Text));
            if (!selectedSupplier.updateSupplier()) return;

            MessageBox.Show("הספק עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSuppliers();
        }

        // מחיקה רכה (UC-01 Scenario C) -- הסטטוס מתעדכן ל-Inactive במקום הסרת הרשומה.
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedSupplier == null)
            {
                MessageBox.Show("יש לבחור ספק מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם להפוך את הספק ללא פעיל?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedSupplier.setStatus(PartnerStatus.Inactive);
            if (!selectedSupplier.updateSupplier()) return;
            clearForm();
            loadSuppliers();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול ערבויות וביטוחים (UC-06) — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// לפי CLAUDE.md, UC-06 ממומש כ-UC יחיד המכסה את שתי תת-המחלקות BankGuarantee ו-InsurancePolicy
    /// (Inheritance -- Table-per-Subclass), ולא כשני מסכים נפרדים. שדה "סוג" קובע איזו תת-מחלקה
    /// נוצרת; שדות ייעודיים לסוג האחר נשארים ריקים ואינם בשימוש.
    /// </summary>
    public partial class FinancialSecurityPanel : UserControl
    {
        private const string TypeGuarantee = "ערבות בנקאית";
        private const string TypePolicy = "פוליסת ביטוח";

        private FinancialSecurity selectedSecurity;

        public FinancialSecurityPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_securities);

            comboBox_type.Items.Add(TypeGuarantee);
            comboBox_type.Items.Add(TypePolicy);
            comboBox_type.SelectedIndex = 0;

            foreach (SecurityStatus s in Enum.GetValues(typeof(SecurityStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            foreach (Project proj in Program.Projects)
                comboBox_project.Items.Add(proj.getProjectId() + " - " + proj.getName());

            loadSecurities();
        }

        private void loadSecurities()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("financialSecurityId", typeof(int));
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("amount", typeof(decimal));
            dt.Columns.Add("issueDate", typeof(DateTime));
            dt.Columns.Add("expiryDate", typeof(DateTime));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("alertStatus", typeof(string));
            dt.Columns.Add("details", typeof(string));

            foreach (FinancialSecurity fs in Program.FinancialSecurities)
            {
                string type, details;
                if (fs is BankGuarantee bg)
                {
                    type = TypeGuarantee;
                    details = bg.getBankName() + " / " + bg.getGuaranteeNumber();
                }
                else if (fs is InsurancePolicy ip)
                {
                    type = TypePolicy;
                    details = ip.getInsurerName() + " / " + ip.getPolicyNumber() + " / " + ip.getCoverageType();
                }
                else continue;

                // UC-06 30-day alert (docs/00e-use-cases.md, Part 1 problem #3): a dedicated
                // column, not a reuse of "status" -- a security can be SecurityStatus.Active
                // and still be days from expiring, which the status column alone can't show.
                // Driven by the real expiryDate, not by `status`: nothing in this system flips
                // status to Expired automatically as time passes, so a stale "Active" record
                // whose date has already passed must still be caught here (the exact silent-
                // lapse scenario this alert exists to prevent). Released is the one status
                // trusted, since it's always a deliberate Finance Officer action -- but it
                // gets its own label, not "בתוקף": a released security isn't "valid right now"
                // just because it was properly closed out before its date, even if that date
                // has since passed (e.g. a returned bank guarantee with a 2025 expiry date).
                string alertStatus;
                if (fs.getStatus() == SecurityStatus.Released)
                    alertStatus = "שוחרר";
                else if (fs.getRemainingDays() < 0)
                    alertStatus = "פג תוקף";
                else if (fs.isExpiringSoon())
                    alertStatus = "מתקרב לתפוגה";
                else
                    alertStatus = "בתוקף";

                dt.Rows.Add(fs.getFinancialSecurityId(), type, fs.getAmount(), fs.getIssueDate(), fs.getExpiryDate(), fs.getStatus().ToString(), alertStatus, details);
            }

            dataGridView_securities.DataSource = dt;
            dataGridView_securities.Columns["amount"].DefaultCellStyle.Format = "N2";
            dataGridView_securities.Columns["issueDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView_securities.Columns["expiryDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_securities.Columns["financialSecurityId"].HeaderText = "מס'";
            dataGridView_securities.Columns["type"].HeaderText = "סוג";
            dataGridView_securities.Columns["amount"].HeaderText = "סכום";
            dataGridView_securities.Columns["issueDate"].HeaderText = "תאריך הנפקה";
            dataGridView_securities.Columns["expiryDate"].HeaderText = "תאריך תפוגה";
            dataGridView_securities.Columns["status"].HeaderText = "סטטוס";
            dataGridView_securities.Columns["alertStatus"].HeaderText = "התראה";
            dataGridView_securities.Columns["details"].HeaderText = "פרטים";

            Theme.ApplyStatusBadgeColumn(dataGridView_securities, "alertStatus");
        }

        private void dataGridView_securities_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_securities.Rows[e.RowIndex].Cells["financialSecurityId"].Value.ToString());
            selectedSecurity = FinancialSecurity.seekFinancialSecurity(id);
            if (selectedSecurity == null) return;

            textBox_financialSecurityId.Text = selectedSecurity.getFinancialSecurityId().ToString();
            comboBox_project.Text = selectedSecurity.getProject().getProjectId() + " - " + selectedSecurity.getProject().getName();
            textBox_amount.Text = selectedSecurity.getAmount().ToString();
            textBox_issueDate.Text = selectedSecurity.getIssueDate().ToString("yyyy-MM-dd");
            textBox_expiryDate.Text = selectedSecurity.getExpiryDate().ToString("yyyy-MM-dd");
            comboBox_status.Text = selectedSecurity.getStatus().ToString();

            textBox_bankName.Text = "";
            textBox_guaranteeNumber.Text = "";
            textBox_insurerName.Text = "";
            textBox_policyNumber.Text = "";
            textBox_coverageType.Text = "";

            if (selectedSecurity is BankGuarantee bg)
            {
                comboBox_type.Text = TypeGuarantee;
                textBox_bankName.Text = bg.getBankName();
                textBox_guaranteeNumber.Text = bg.getGuaranteeNumber();
            }
            else if (selectedSecurity is InsurancePolicy ip)
            {
                comboBox_type.Text = TypePolicy;
                textBox_insurerName.Text = ip.getInsurerName();
                textBox_policyNumber.Text = ip.getPolicyNumber();
                textBox_coverageType.Text = ip.getCoverageType();
            }
        }

        private Project resolveSelectedProject()
        {
            int id = int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Project.seekProject(id);
        }

        private bool validateFields()
        {
            if (comboBox_project.SelectedIndex < 0 && string.IsNullOrWhiteSpace(comboBox_project.Text))
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_amount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("יש להזין סכום תקין (גדול מ-0)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_issueDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך הנפקה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_expiryDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך תפוגה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (DateTime.Parse(textBox_issueDate.Text) > DateTime.Parse(textBox_expiryDate.Text))
            {
                MessageBox.Show("תאריך התפוגה לא יכול להיות לפני תאריך ההנפקה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (comboBox_type.Text == TypeGuarantee)
            {
                if (string.IsNullOrWhiteSpace(textBox_bankName.Text) || string.IsNullOrWhiteSpace(textBox_guaranteeNumber.Text))
                {
                    MessageBox.Show("יש להזין שם בנק ומספר ערבות", "שגיאה", MessageBoxButtons.OK);
                    return false;
                }
                foreach (FinancialSecurity fs in Program.FinancialSecurities)
                {
                    if (fs != selectedSecurity && fs is BankGuarantee bg && bg.getGuaranteeNumber() == textBox_guaranteeNumber.Text)
                    {
                        MessageBox.Show("קיימת כבר ערבות עם מספר זה", "שגיאה", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox_insurerName.Text) || string.IsNullOrWhiteSpace(textBox_policyNumber.Text) || string.IsNullOrWhiteSpace(textBox_coverageType.Text))
                {
                    MessageBox.Show("יש להזין מבטח, מספר פוליסה וסוג כיסוי", "שגיאה", MessageBoxButtons.OK);
                    return false;
                }
                foreach (FinancialSecurity fs in Program.FinancialSecurities)
                {
                    if (fs != selectedSecurity && fs is InsurancePolicy ip && ip.getPolicyNumber() == textBox_policyNumber.Text)
                    {
                        MessageBox.Show("קיימת כבר פוליסה עם מספר זה", "שגיאה", MessageBoxButtons.OK);
                        return false;
                    }
                }
            }
            return true;
        }

        private void clearForm()
        {
            selectedSecurity = null;
            textBox_financialSecurityId.Text = "";
            comboBox_type.SelectedIndex = 0;
            comboBox_project.SelectedIndex = -1;
            comboBox_project.Text = "";
            textBox_amount.Text = "";
            textBox_issueDate.Text = "";
            textBox_expiryDate.Text = "";
            comboBox_status.SelectedIndex = 0;
            textBox_bankName.Text = "";
            textBox_guaranteeNumber.Text = "";
            textBox_insurerName.Text = "";
            textBox_policyNumber.Text = "";
            textBox_coverageType.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = FinancialSecurity.getNextFinancialSecurityId();
            decimal amount = decimal.Parse(textBox_amount.Text);
            DateTime issueDate = DateTime.Parse(textBox_issueDate.Text);
            DateTime expiryDate = DateTime.Parse(textBox_expiryDate.Text);
            SecurityStatus status = (SecurityStatus)Enum.Parse(typeof(SecurityStatus), comboBox_status.Text);

            Project project = resolveSelectedProject();
            FinancialSecurity fs;
            if (comboBox_type.Text == TypeGuarantee)
                fs = new BankGuarantee(id, project, amount, issueDate, expiryDate, status, textBox_bankName.Text, textBox_guaranteeNumber.Text, true);
            else
                fs = new InsurancePolicy(id, project, amount, issueDate, expiryDate, status, textBox_insurerName.Text, textBox_policyNumber.Text, textBox_coverageType.Text, true);

            if (!Program.FinancialSecurities.Contains(fs)) return;

            MessageBox.Show("הערבות/הביטוח נשמרו בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSecurities();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedSecurity == null)
            {
                MessageBox.Show("יש לבחור רשומה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedSecurity.setProject(resolveSelectedProject());
            selectedSecurity.setAmount(decimal.Parse(textBox_amount.Text));
            selectedSecurity.setIssueDate(DateTime.Parse(textBox_issueDate.Text));
            selectedSecurity.setExpiryDate(DateTime.Parse(textBox_expiryDate.Text));
            selectedSecurity.setStatus((SecurityStatus)Enum.Parse(typeof(SecurityStatus), comboBox_status.Text));

            bool success;
            if (selectedSecurity is BankGuarantee bg)
            {
                bg.setBankName(textBox_bankName.Text);
                bg.setGuaranteeNumber(textBox_guaranteeNumber.Text);
                success = bg.updateBankGuarantee();
            }
            else if (selectedSecurity is InsurancePolicy ip)
            {
                ip.setInsurerName(textBox_insurerName.Text);
                ip.setPolicyNumber(textBox_policyNumber.Text);
                ip.setCoverageType(textBox_coverageType.Text);
                success = ip.updateInsurancePolicy();
            }
            else
            {
                success = false;
            }
            if (!success) return;

            MessageBox.Show("הרשומה עודכנה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadSecurities();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedSecurity == null)
            {
                MessageBox.Show("יש לבחור רשומה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את הרשומה?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            bool success = false;
            if (selectedSecurity is BankGuarantee bg)
                success = bg.deleteBankGuarantee();
            else if (selectedSecurity is InsurancePolicy ip)
                success = ip.deleteInsurancePolicy();
            if (!success) return;

            clearForm();
            loadSecurities();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

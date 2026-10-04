using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול עובדים (UC-02) — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// </summary>
    public partial class EmployeePanel : UserControl
    {
        private Employee selectedEmployee;

        public EmployeePanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_employees);

            foreach (EmployeeRole r in Enum.GetValues(typeof(EmployeeRole)))
                comboBox_role.Items.Add(r.ToString());
            foreach (EmployeeStatus s in Enum.GetValues(typeof(EmployeeStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_role.SelectedIndex = 0;
            comboBox_status.SelectedIndex = 0;

            loadEmployees();
        }

        private void loadEmployees()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("employeeId", typeof(int));
            dt.Columns.Add("firstName", typeof(string));
            dt.Columns.Add("lastName", typeof(string));
            dt.Columns.Add("nationalId", typeof(string));
            dt.Columns.Add("role", typeof(string));
            dt.Columns.Add("dailyRate", typeof(decimal));
            dt.Columns.Add("certificationNo", typeof(string));
            dt.Columns.Add("status", typeof(string));

            foreach (Employee emp in Program.Employees)
            {
                dt.Rows.Add(emp.getEmployeeId(), emp.getFirstName(), emp.getLastName(), emp.getNationalId(),
                    emp.getRole().ToString(), emp.getDailyRate(), emp.getCertificationNo(), emp.getStatus().ToString());
            }

            dataGridView_employees.DataSource = dt;
            // A freshly-created (in-memory) dailyRate and one re-parsed from the DB's
            // DECIMAL(10,2) can carry a different internal scale (e.g. 900 vs 900.00),
            // so the display format is fixed explicitly rather than left to decimal.ToString().
            dataGridView_employees.Columns["dailyRate"].DefaultCellStyle.Format = "N2";

            dataGridView_employees.Columns["employeeId"].HeaderText = "מס' עובד";
            dataGridView_employees.Columns["firstName"].HeaderText = "שם פרטי";
            dataGridView_employees.Columns["lastName"].HeaderText = "שם משפחה";
            dataGridView_employees.Columns["nationalId"].HeaderText = "ת\"ז";
            dataGridView_employees.Columns["role"].HeaderText = "תפקיד";
            dataGridView_employees.Columns["dailyRate"].HeaderText = "תעריף יומי";
            dataGridView_employees.Columns["certificationNo"].HeaderText = "מס' הסמכה";
            dataGridView_employees.Columns["status"].HeaderText = "סטטוס";
        }

        // לחיצה על שורה בטבלה — טעינת העובד לטופס לעריכה/מחיקה
        private void dataGridView_employees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_employees.Rows[e.RowIndex].Cells["employeeId"].Value.ToString());
            selectedEmployee = Employee.seekEmployee(id);
            if (selectedEmployee == null) return;

            textBox_employeeId.Text = selectedEmployee.getEmployeeId().ToString();
            textBox_firstName.Text = selectedEmployee.getFirstName();
            textBox_lastName.Text = selectedEmployee.getLastName();
            textBox_nationalId.Text = selectedEmployee.getNationalId();
            comboBox_role.Text = selectedEmployee.getRole().ToString();
            textBox_dailyRate.Text = selectedEmployee.getDailyRate().ToString();
            textBox_certificationNo.Text = selectedEmployee.getCertificationNo();
            comboBox_status.Text = selectedEmployee.getStatus().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_firstName.Text))
            {
                MessageBox.Show("יש להזין שם פרטי", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_lastName.Text))
            {
                MessageBox.Show("יש להזין שם משפחה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_nationalId.Text))
            {
                MessageBox.Show("יש להזין תעודת זהות", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_dailyRate.Text, out _))
            {
                MessageBox.Show("יש להזין תעריף יומי תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_certificationNo.Text))
            {
                MessageBox.Show("יש להזין מספר הסמכה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox_nationalId.Text, @"^\d{9}$"))
            {
                MessageBox.Show("תעודת זהות חייבת להיות בת 9 ספרות", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            foreach (Employee emp in Program.Employees)
            {
                if (emp != selectedEmployee && emp.getNationalId() == textBox_nationalId.Text)
                {
                    MessageBox.Show("קיים כבר עובד עם תעודת זהות זו", "שגיאה", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }

        private void clearForm()
        {
            selectedEmployee = null;
            textBox_employeeId.Text = "";
            textBox_firstName.Text = "";
            textBox_lastName.Text = "";
            textBox_nationalId.Text = "";
            comboBox_role.SelectedIndex = 0;
            textBox_dailyRate.Text = "";
            textBox_certificationNo.Text = "";
            comboBox_status.SelectedIndex = 0;
        }

        // שמירה = יצירת עובד חדש
        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = Employee.getNextEmployeeId();
            EmployeeRole role = (EmployeeRole)Enum.Parse(typeof(EmployeeRole), comboBox_role.Text);
            EmployeeStatus status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), comboBox_status.Text);
            decimal dailyRate = decimal.Parse(textBox_dailyRate.Text);

            Employee emp = new Employee(id, textBox_firstName.Text, textBox_lastName.Text, textBox_nationalId.Text,
                role, dailyRate, textBox_certificationNo.Text, status, true);

            if (!Program.Employees.Contains(emp)) return;

            MessageBox.Show("העובד נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadEmployees();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show("יש לבחור עובד מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedEmployee.setFirstName(textBox_firstName.Text);
            selectedEmployee.setLastName(textBox_lastName.Text);
            selectedEmployee.setNationalId(textBox_nationalId.Text);
            selectedEmployee.setRole((EmployeeRole)Enum.Parse(typeof(EmployeeRole), comboBox_role.Text));
            selectedEmployee.setDailyRate(decimal.Parse(textBox_dailyRate.Text));
            selectedEmployee.setCertificationNo(textBox_certificationNo.Text);
            selectedEmployee.setStatus((EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), comboBox_status.Text));
            if (!selectedEmployee.updateEmployee()) return;

            MessageBox.Show("העובד עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadEmployees();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show("יש לבחור עובד מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את העובד?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedEmployee.deleteEmployee()) return;
            clearForm();
            loadEmployees();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

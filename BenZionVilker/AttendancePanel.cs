using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול נוכחות עובדים — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// Attendance היא מחלקת קישור בין Employee ל-DailyWorkLog (many-to-many); ראו UC-04's MSS
    /// שלב 3 ("logs attendance and hours worked for each worker"). אין UC מפורט לישות זו עצמה.
    /// </summary>
    public partial class AttendancePanel : UserControl
    {
        private Attendance selectedAttendance;

        public AttendancePanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_attendances);

            foreach (Employee emp in Program.Employees)
                comboBox_employee.Items.Add(emp.getEmployeeId() + " - " + emp.getFirstName() + " " + emp.getLastName());
            foreach (DailyWorkLog log in Program.DailyWorkLogs)
                comboBox_dailyWorkLog.Items.Add(log.getDailyWorkLogId() + " - " + log.getLogDate().ToString("yyyy-MM-dd"));

            loadAttendances();
        }

        private void loadAttendances()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("attendanceId", typeof(int));
            dt.Columns.Add("employee", typeof(string));
            dt.Columns.Add("dailyWorkLog", typeof(string));
            dt.Columns.Add("hoursWorked", typeof(double));
            dt.Columns.Add("taskDescription", typeof(string));

            foreach (Attendance a in Program.Attendances)
            {
                dt.Rows.Add(a.getAttendanceId(), a.getEmployee().getFirstName() + " " + a.getEmployee().getLastName(),
                    a.getDailyWorkLog().getLogDate().ToString("yyyy-MM-dd"), a.hoursWorked(), a.getTaskDescription());
            }

            dataGridView_attendances.DataSource = dt;
            dataGridView_attendances.Columns["attendanceId"].HeaderText = "מס'";
            dataGridView_attendances.Columns["employee"].HeaderText = "עובד";
            dataGridView_attendances.Columns["dailyWorkLog"].HeaderText = "יומן עבודה";
            dataGridView_attendances.Columns["hoursWorked"].HeaderText = "שעות עבודה";
            dataGridView_attendances.Columns["taskDescription"].HeaderText = "תיאור משימה";
        }

        private void dataGridView_attendances_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_attendances.Rows[e.RowIndex].Cells["attendanceId"].Value.ToString());
            selectedAttendance = Attendance.seekAttendance(id);
            if (selectedAttendance == null) return;

            textBox_attendanceId.Text = selectedAttendance.getAttendanceId().ToString();
            comboBox_employee.Text = selectedAttendance.getEmployee().getEmployeeId() + " - " + selectedAttendance.getEmployee().getFirstName() + " " + selectedAttendance.getEmployee().getLastName();
            comboBox_dailyWorkLog.Text = selectedAttendance.getDailyWorkLog().getDailyWorkLogId() + " - " + selectedAttendance.getDailyWorkLog().getLogDate().ToString("yyyy-MM-dd");
            textBox_startTime.Text = selectedAttendance.getStartTime().ToString(@"hh\:mm");
            textBox_endTime.Text = selectedAttendance.getEndTime().ToString(@"hh\:mm");
            textBox_taskDescription.Text = selectedAttendance.getTaskDescription();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_employee.Text))
            {
                MessageBox.Show("יש לבחור עובד", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_dailyWorkLog.Text))
            {
                MessageBox.Show("יש לבחור יומן עבודה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!TimeSpan.TryParse(textBox_startTime.Text, out _))
            {
                MessageBox.Show("יש להזין שעת התחלה תקינה (HH:mm)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!TimeSpan.TryParse(textBox_endTime.Text, out _))
            {
                MessageBox.Show("יש להזין שעת סיום תקינה (HH:mm)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (TimeSpan.Parse(textBox_endTime.Text) <= TimeSpan.Parse(textBox_startTime.Text))
            {
                MessageBox.Show("שעת הסיום חייבת להיות אחרי שעת ההתחלה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_taskDescription.Text))
            {
                MessageBox.Show("יש להזין תיאור משימה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Employee resolveSelectedEmployee()
        {
            int id = int.Parse(comboBox_employee.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Employee.seekEmployee(id);
        }

        private DailyWorkLog resolveSelectedDailyWorkLog()
        {
            int id = int.Parse(comboBox_dailyWorkLog.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return DailyWorkLog.seekDailyWorkLog(id);
        }

        private void clearForm()
        {
            selectedAttendance = null;
            textBox_attendanceId.Text = "";
            comboBox_employee.SelectedIndex = -1;
            comboBox_employee.Text = "";
            comboBox_dailyWorkLog.SelectedIndex = -1;
            comboBox_dailyWorkLog.Text = "";
            textBox_startTime.Text = "";
            textBox_endTime.Text = "";
            textBox_taskDescription.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = Attendance.getNextAttendanceId();
            Attendance attendance = new Attendance(id, resolveSelectedEmployee(), resolveSelectedDailyWorkLog(),
                TimeSpan.Parse(textBox_startTime.Text), TimeSpan.Parse(textBox_endTime.Text), textBox_taskDescription.Text, true);

            if (!Program.Attendances.Contains(attendance)) return;

            MessageBox.Show("הנוכחות נשמרה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadAttendances();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedAttendance == null)
            {
                MessageBox.Show("יש לבחור רשומת נוכחות מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedAttendance.setEmployee(resolveSelectedEmployee());
            selectedAttendance.setDailyWorkLog(resolveSelectedDailyWorkLog());
            selectedAttendance.setStartTime(TimeSpan.Parse(textBox_startTime.Text));
            selectedAttendance.setEndTime(TimeSpan.Parse(textBox_endTime.Text));
            selectedAttendance.setTaskDescription(textBox_taskDescription.Text);
            if (!selectedAttendance.updateAttendance()) return;

            MessageBox.Show("הנוכחות עודכנה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadAttendances();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedAttendance == null)
            {
                MessageBox.Show("יש לבחור רשומת נוכחות מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את רשומת הנוכחות?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedAttendance.deleteAttendance()) return;
            clearForm();
            loadAttendances();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

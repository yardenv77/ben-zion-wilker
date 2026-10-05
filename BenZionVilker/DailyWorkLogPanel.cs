using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך יומן עבודה יומי (UC-04) — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// UC-04's MSS כולל גם רישום נוכחות עובדים ושעות ציוד (Attendance/EquipmentUsage) והשוואה
    /// אוטומטית לתכנון (UC-04.Extend) -- זהו פישוט CRUD בסיסי על DailyWorkLog עצמו, בדומה
    /// לפישוט ה-Skills ב-EmployeePanel; נוכחות ושעות ציוד מנוהלות במסכים נפרדים
    /// (AttendancePanel, EquipmentUsagePanel) וה-Extend UC אינו ממומש.
    /// subcontractor אופציונלי (DailyWorkLog.cs -- יומן יכול להיות כולו צוות פנימי); status הוא
    /// מחרוזת חופשית ולא Enum, לפי הנחת המודל המפורשת ב-design/class-diagram.md Section 5.
    /// </summary>
    public partial class DailyWorkLogPanel : UserControl
    {
        private DailyWorkLog selectedLog;
        private const string NoSubcontractor = "-- ללא קבלן משנה --";

        public DailyWorkLogPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_logs);

            comboBox_subcontractor.Items.Add(NoSubcontractor);
            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (bp is Subcontractor sub)
                    comboBox_subcontractor.Items.Add(sub.getBusinessPartnerId() + " - " + sub.getName());
            }
            comboBox_subcontractor.SelectedIndex = 0;

            foreach (Employee emp in Program.Employees)
                comboBox_submittedBy.Items.Add(emp.getEmployeeId() + " - " + emp.getFullName());

            foreach (WorkLogStatus s in Enum.GetValues(typeof(WorkLogStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadLogs();
        }

        private void loadLogs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dailyWorkLogId", typeof(int));
            dt.Columns.Add("subcontractor", typeof(string));
            dt.Columns.Add("logDate", typeof(DateTime));
            dt.Columns.Add("plannedQuantity", typeof(double));
            dt.Columns.Add("completedQuantity", typeof(double));
            dt.Columns.Add("status", typeof(string));

            foreach (DailyWorkLog log in Program.DailyWorkLogs)
            {
                dt.Rows.Add(log.getDailyWorkLogId(), log.getSubcontractor() == null ? "" : log.getSubcontractor().getName(),
                    log.getLogDate(), log.getPlannedQuantity(), log.getCompletedQuantity(), log.getStatus().ToString());
            }

            dataGridView_logs.DataSource = dt;
            dataGridView_logs.Columns["logDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_logs.Columns["dailyWorkLogId"].HeaderText = "מס'";
            dataGridView_logs.Columns["subcontractor"].HeaderText = "קבלן משנה";
            dataGridView_logs.Columns["logDate"].HeaderText = "תאריך";
            dataGridView_logs.Columns["plannedQuantity"].HeaderText = "כמות מתוכננת";
            dataGridView_logs.Columns["completedQuantity"].HeaderText = "כמות שבוצעה";
            dataGridView_logs.Columns["status"].HeaderText = "סטטוס";
        }

        private void dataGridView_logs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_logs.Rows[e.RowIndex].Cells["dailyWorkLogId"].Value.ToString());
            selectedLog = DailyWorkLog.seekDailyWorkLog(id);
            if (selectedLog == null) return;

            textBox_dailyWorkLogId.Text = selectedLog.getDailyWorkLogId().ToString();
            comboBox_subcontractor.Text = selectedLog.getSubcontractor() == null
                ? NoSubcontractor
                : selectedLog.getSubcontractor().getBusinessPartnerId() + " - " + selectedLog.getSubcontractor().getName();
            comboBox_submittedBy.Text = selectedLog.getSubmittedBy().getEmployeeId() + " - " + selectedLog.getSubmittedBy().getFullName();
            textBox_logDate.Text = selectedLog.getLogDate().ToString("yyyy-MM-dd");
            textBox_plannedQuantity.Text = selectedLog.getPlannedQuantity().ToString();
            textBox_completedQuantity.Text = selectedLog.getCompletedQuantity().ToString();
            comboBox_status.Text = selectedLog.getStatus().ToString();
        }

        private bool validateFields()
        {
            if (!DateTime.TryParse(textBox_logDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך יומן תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!double.TryParse(textBox_plannedQuantity.Text, out double plannedQuantity) || plannedQuantity < 0)
            {
                MessageBox.Show("יש להזין כמות מתוכננת תקינה (לא שלילית)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!double.TryParse(textBox_completedQuantity.Text, out double completedQuantity) || completedQuantity < 0)
            {
                MessageBox.Show("יש להזין כמות שבוצעה תקינה (לא שלילית)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (comboBox_submittedBy.SelectedIndex < 0 && string.IsNullOrWhiteSpace(comboBox_submittedBy.Text))
            {
                MessageBox.Show("יש לבחור מדווח", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Subcontractor resolveSelectedSubcontractor()
        {
            if (comboBox_subcontractor.Text == NoSubcontractor) return null;
            int id = int.Parse(comboBox_subcontractor.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return BusinessPartner.seekBusinessPartner(id) as Subcontractor;
        }

        private Employee resolveSelectedSubmittedBy()
        {
            int id = int.Parse(comboBox_submittedBy.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Employee.seekEmployee(id);
        }

        private void clearForm()
        {
            selectedLog = null;
            textBox_dailyWorkLogId.Text = "";
            comboBox_subcontractor.SelectedIndex = 0;
            comboBox_submittedBy.SelectedIndex = -1;
            comboBox_submittedBy.Text = "";
            textBox_logDate.Text = "";
            textBox_plannedQuantity.Text = "";
            textBox_completedQuantity.Text = "";
            comboBox_status.SelectedIndex = 0;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = DailyWorkLog.getNextDailyWorkLogId();
            WorkLogStatus status = (WorkLogStatus)Enum.Parse(typeof(WorkLogStatus), comboBox_status.Text);
            DailyWorkLog log = new DailyWorkLog(id, resolveSelectedSubcontractor(), resolveSelectedSubmittedBy(), DateTime.Parse(textBox_logDate.Text),
                double.Parse(textBox_plannedQuantity.Text), double.Parse(textBox_completedQuantity.Text), status, true);

            if (!Program.DailyWorkLogs.Contains(log)) return;

            MessageBox.Show("יומן העבודה נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadLogs();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedLog == null)
            {
                MessageBox.Show("יש לבחור יומן מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedLog.setSubcontractor(resolveSelectedSubcontractor());
            selectedLog.setSubmittedBy(resolveSelectedSubmittedBy());
            selectedLog.setLogDate(DateTime.Parse(textBox_logDate.Text));
            selectedLog.setPlannedQuantity(double.Parse(textBox_plannedQuantity.Text));
            selectedLog.setCompletedQuantity(double.Parse(textBox_completedQuantity.Text));
            selectedLog.setStatus((WorkLogStatus)Enum.Parse(typeof(WorkLogStatus), comboBox_status.Text));
            if (!selectedLog.updateDailyWorkLog()) return;

            MessageBox.Show("יומן העבודה עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadLogs();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedLog == null)
            {
                MessageBox.Show("יש לבחור יומן מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את יומן העבודה?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedLog.deleteDailyWorkLog()) return;
            clearForm();
            loadLogs();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

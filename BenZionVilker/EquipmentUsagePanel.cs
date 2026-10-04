using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול שימוש בציוד — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// EquipmentUsage היא מחלקת קישור בין Equipment ל-DailyWorkLog (many-to-many); ראו UC-04's
    /// MSS שלב 4 ("enters operating hours for heavy machinery"). אין UC מפורט לישות זו עצמה.
    /// </summary>
    public partial class EquipmentUsagePanel : UserControl
    {
        private EquipmentUsage selectedUsage;

        public EquipmentUsagePanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_usages);

            foreach (Equipment eq in Program.Equipments)
                comboBox_equipment.Items.Add(eq.getEquipmentId() + " - " + eq.getEquipmentType());
            foreach (DailyWorkLog log in Program.DailyWorkLogs)
                comboBox_dailyWorkLog.Items.Add(log.getDailyWorkLogId() + " - " + log.getLogDate().ToString("yyyy-MM-dd"));

            loadUsages();
        }

        private void loadUsages()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("equipmentUsageId", typeof(int));
            dt.Columns.Add("equipment", typeof(string));
            dt.Columns.Add("dailyWorkLog", typeof(string));
            dt.Columns.Add("hoursOperated", typeof(double));

            foreach (EquipmentUsage eu in Program.EquipmentUsages)
            {
                dt.Rows.Add(eu.getEquipmentUsageId(), eu.getEquipment().getEquipmentType(),
                    eu.getDailyWorkLog().getLogDate().ToString("yyyy-MM-dd"), eu.getHoursOperated());
            }

            dataGridView_usages.DataSource = dt;
            dataGridView_usages.Columns["equipmentUsageId"].HeaderText = "מס'";
            dataGridView_usages.Columns["equipment"].HeaderText = "ציוד";
            dataGridView_usages.Columns["dailyWorkLog"].HeaderText = "יומן עבודה";
            dataGridView_usages.Columns["hoursOperated"].HeaderText = "שעות הפעלה";
        }

        private void dataGridView_usages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_usages.Rows[e.RowIndex].Cells["equipmentUsageId"].Value.ToString());
            selectedUsage = EquipmentUsage.seekEquipmentUsage(id);
            if (selectedUsage == null) return;

            textBox_equipmentUsageId.Text = selectedUsage.getEquipmentUsageId().ToString();
            comboBox_equipment.Text = selectedUsage.getEquipment().getEquipmentId() + " - " + selectedUsage.getEquipment().getEquipmentType();
            comboBox_dailyWorkLog.Text = selectedUsage.getDailyWorkLog().getDailyWorkLogId() + " - " + selectedUsage.getDailyWorkLog().getLogDate().ToString("yyyy-MM-dd");
            textBox_hoursOperated.Text = selectedUsage.getHoursOperated().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_equipment.Text))
            {
                MessageBox.Show("יש לבחור ציוד", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_dailyWorkLog.Text))
            {
                MessageBox.Show("יש לבחור יומן עבודה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!double.TryParse(textBox_hoursOperated.Text, out _))
            {
                MessageBox.Show("יש להזין שעות הפעלה תקינות", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Equipment resolveSelectedEquipment()
        {
            int id = int.Parse(comboBox_equipment.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Equipment.seekEquipment(id);
        }

        private DailyWorkLog resolveSelectedDailyWorkLog()
        {
            int id = int.Parse(comboBox_dailyWorkLog.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return DailyWorkLog.seekDailyWorkLog(id);
        }

        private void clearForm()
        {
            selectedUsage = null;
            textBox_equipmentUsageId.Text = "";
            comboBox_equipment.SelectedIndex = -1;
            comboBox_equipment.Text = "";
            comboBox_dailyWorkLog.SelectedIndex = -1;
            comboBox_dailyWorkLog.Text = "";
            textBox_hoursOperated.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = EquipmentUsage.getNextEquipmentUsageId();
            EquipmentUsage usage = new EquipmentUsage(id, resolveSelectedEquipment(), resolveSelectedDailyWorkLog(), double.Parse(textBox_hoursOperated.Text), true);

            if (!Program.EquipmentUsages.Contains(usage)) return;

            MessageBox.Show("השימוש בציוד נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadUsages();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedUsage == null)
            {
                MessageBox.Show("יש לבחור רשומה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedUsage.setEquipment(resolveSelectedEquipment());
            selectedUsage.setDailyWorkLog(resolveSelectedDailyWorkLog());
            selectedUsage.setHoursOperated(double.Parse(textBox_hoursOperated.Text));
            if (!selectedUsage.updateEquipmentUsage()) return;

            MessageBox.Show("השימוש בציוד עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadUsages();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedUsage == null)
            {
                MessageBox.Show("יש לבחור רשומה מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את רשומת השימוש בציוד?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedUsage.deleteEquipmentUsage()) return;
            clearForm();
            loadUsages();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול שיבוץ ציוד לפרויקטים — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// EquipmentAssignment היא מחלקת קישור עבור צירוף Project o-- Equipment; ראו EquipmentAssignment.cs.
    /// אין UC מפורט לישות זו ב-00e-use-cases.md. endDate אופציונלי -- שיבוץ פעיל שטרם הסתיים.
    /// </summary>
    public partial class EquipmentAssignmentPanel : UserControl
    {
        private EquipmentAssignment selectedAssignment;

        public EquipmentAssignmentPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_assignments);

            foreach (Project p in Program.Projects)
                comboBox_project.Items.Add(p.getProjectId() + " - " + p.getName());
            foreach (Equipment eq in Program.Equipments)
                comboBox_equipment.Items.Add(eq.getEquipmentId() + " - " + eq.getEquipmentType());

            loadAssignments();
        }

        private void loadAssignments()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("equipmentAssignmentId", typeof(int));
            dt.Columns.Add("project", typeof(string));
            dt.Columns.Add("equipment", typeof(string));
            dt.Columns.Add("startDate", typeof(DateTime));
            dt.Columns.Add("endDate", typeof(string));

            foreach (EquipmentAssignment ea in Program.EquipmentAssignments)
            {
                dt.Rows.Add(ea.getEquipmentAssignmentId(), ea.getProject().getName(), ea.getEquipment().getEquipmentType(),
                    ea.getStartDate(), ea.getEndDate().HasValue ? ea.getEndDate().Value.ToString("yyyy-MM-dd") : "");
            }

            dataGridView_assignments.DataSource = dt;
            dataGridView_assignments.Columns["startDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_assignments.Columns["equipmentAssignmentId"].HeaderText = "מס'";
            dataGridView_assignments.Columns["project"].HeaderText = "פרויקט";
            dataGridView_assignments.Columns["equipment"].HeaderText = "ציוד";
            dataGridView_assignments.Columns["startDate"].HeaderText = "תאריך התחלה";
            dataGridView_assignments.Columns["endDate"].HeaderText = "תאריך סיום";
        }

        private void dataGridView_assignments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_assignments.Rows[e.RowIndex].Cells["equipmentAssignmentId"].Value.ToString());
            selectedAssignment = EquipmentAssignment.seekEquipmentAssignment(id);
            if (selectedAssignment == null) return;

            textBox_equipmentAssignmentId.Text = selectedAssignment.getEquipmentAssignmentId().ToString();
            comboBox_project.Text = selectedAssignment.getProject().getProjectId() + " - " + selectedAssignment.getProject().getName();
            comboBox_equipment.Text = selectedAssignment.getEquipment().getEquipmentId() + " - " + selectedAssignment.getEquipment().getEquipmentType();
            textBox_startDate.Text = selectedAssignment.getStartDate().ToString("yyyy-MM-dd");
            textBox_endDate.Text = selectedAssignment.getEndDate().HasValue ? selectedAssignment.getEndDate().Value.ToString("yyyy-MM-dd") : "";
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_project.Text))
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_equipment.Text))
            {
                MessageBox.Show("יש לבחור ציוד", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_startDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך התחלה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox_endDate.Text) && !DateTime.TryParse(textBox_endDate.Text, out _))
            {
                MessageBox.Show("תאריך סיום אינו תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Project resolveSelectedProject()
        {
            int id = int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Project.seekProject(id);
        }

        private Equipment resolveSelectedEquipment()
        {
            int id = int.Parse(comboBox_equipment.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Equipment.seekEquipment(id);
        }

        private void clearForm()
        {
            selectedAssignment = null;
            textBox_equipmentAssignmentId.Text = "";
            comboBox_project.SelectedIndex = -1;
            comboBox_project.Text = "";
            comboBox_equipment.SelectedIndex = -1;
            comboBox_equipment.Text = "";
            textBox_startDate.Text = "";
            textBox_endDate.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = EquipmentAssignment.getNextEquipmentAssignmentId();
            DateTime? endDate = string.IsNullOrWhiteSpace(textBox_endDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_endDate.Text);

            new EquipmentAssignment(id, resolveSelectedProject(), resolveSelectedEquipment(), DateTime.Parse(textBox_startDate.Text), endDate, true);

            MessageBox.Show("השיבוץ נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadAssignments();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedAssignment == null)
            {
                MessageBox.Show("יש לבחור שיבוץ מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedAssignment.setProject(resolveSelectedProject());
            selectedAssignment.setEquipment(resolveSelectedEquipment());
            selectedAssignment.setStartDate(DateTime.Parse(textBox_startDate.Text));
            selectedAssignment.setEndDate(string.IsNullOrWhiteSpace(textBox_endDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_endDate.Text));
            selectedAssignment.updateEquipmentAssignment();

            MessageBox.Show("השיבוץ עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadAssignments();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedAssignment == null)
            {
                MessageBox.Show("יש לבחור שיבוץ מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את השיבוץ?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedAssignment.deleteEquipmentAssignment();
            clearForm();
            loadAssignments();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

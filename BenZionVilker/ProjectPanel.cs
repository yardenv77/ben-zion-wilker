using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול פרויקטים — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-Project ב-00e-use-cases.md; נבנה לפי אותה תבנית כמו EmployeePanel.
    /// actualStartDate/actualEndDate אופציונליים (Project.cs) -- שדה ריק = DBNull.
    /// </summary>
    public partial class ProjectPanel : UserControl
    {
        private Project selectedProject;

        public ProjectPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_projects);

            foreach (Tender t in Program.Tenders)
                comboBox_tender.Items.Add(t.getTenderId() + " - " + t.getTitle());
            foreach (Employee emp in Program.Employees)
                comboBox_projectManager.Items.Add(emp.getEmployeeId() + " - " + emp.getFullName());
            foreach (ProjectStatus s in Enum.GetValues(typeof(ProjectStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadProjects();
        }

        private void loadProjects()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("projectId", typeof(int));
            dt.Columns.Add("tender", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("address", typeof(string));
            dt.Columns.Add("plannedStartDate", typeof(DateTime));
            dt.Columns.Add("plannedEndDate", typeof(DateTime));
            dt.Columns.Add("actualStartDate", typeof(string));
            dt.Columns.Add("actualEndDate", typeof(string));
            dt.Columns.Add("status", typeof(string));

            foreach (Project p in Program.Projects)
            {
                dt.Rows.Add(p.getProjectId(), p.getTender().getTitle(), p.getName(), p.getAddress(),
                    p.getPlannedStartDate(), p.getPlannedEndDate(),
                    p.getActualStartDate().HasValue ? p.getActualStartDate().Value.ToString("yyyy-MM-dd") : "",
                    p.getActualEndDate().HasValue ? p.getActualEndDate().Value.ToString("yyyy-MM-dd") : "",
                    p.getStatus().ToString());
            }

            dataGridView_projects.DataSource = dt;
            dataGridView_projects.Columns["plannedStartDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView_projects.Columns["plannedEndDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_projects.Columns["projectId"].HeaderText = "מס'";
            dataGridView_projects.Columns["tender"].HeaderText = "מכרז";
            dataGridView_projects.Columns["name"].HeaderText = "שם פרויקט";
            dataGridView_projects.Columns["address"].HeaderText = "כתובת";
            dataGridView_projects.Columns["plannedStartDate"].HeaderText = "התחלה מתוכננת";
            dataGridView_projects.Columns["plannedEndDate"].HeaderText = "סיום מתוכנן";
            dataGridView_projects.Columns["actualStartDate"].HeaderText = "התחלה בפועל";
            dataGridView_projects.Columns["actualEndDate"].HeaderText = "סיום בפועל";
            dataGridView_projects.Columns["status"].HeaderText = "סטטוס";
        }

        private void dataGridView_projects_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_projects.Rows[e.RowIndex].Cells["projectId"].Value.ToString());
            selectedProject = Project.seekProject(id);
            if (selectedProject == null) return;

            textBox_projectId.Text = selectedProject.getProjectId().ToString();
            comboBox_tender.Text = selectedProject.getTender().getTenderId() + " - " + selectedProject.getTender().getTitle();
            comboBox_projectManager.Text = selectedProject.getProjectManager().getEmployeeId() + " - " + selectedProject.getProjectManager().getFullName();
            textBox_name.Text = selectedProject.getName();
            textBox_address.Text = selectedProject.getAddress();
            textBox_plannedStartDate.Text = selectedProject.getPlannedStartDate().ToString("yyyy-MM-dd");
            textBox_plannedEndDate.Text = selectedProject.getPlannedEndDate().ToString("yyyy-MM-dd");
            textBox_actualStartDate.Text = selectedProject.getActualStartDate().HasValue ? selectedProject.getActualStartDate().Value.ToString("yyyy-MM-dd") : "";
            textBox_actualEndDate.Text = selectedProject.getActualEndDate().HasValue ? selectedProject.getActualEndDate().Value.ToString("yyyy-MM-dd") : "";
            comboBox_status.Text = selectedProject.getStatus().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_tender.Text))
            {
                MessageBox.Show("יש לבחור מכרז", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (comboBox_projectManager.SelectedIndex < 0 && string.IsNullOrWhiteSpace(comboBox_projectManager.Text))
            {
                MessageBox.Show("יש לבחור מנהל פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_name.Text))
            {
                MessageBox.Show("יש להזין שם פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_address.Text))
            {
                MessageBox.Show("יש להזין כתובת", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_plannedStartDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך התחלה מתוכנן תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_plannedEndDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך סיום מתוכנן תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox_actualStartDate.Text) && !DateTime.TryParse(textBox_actualStartDate.Text, out _))
            {
                MessageBox.Show("תאריך התחלה בפועל אינו תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox_actualEndDate.Text) && !DateTime.TryParse(textBox_actualEndDate.Text, out _))
            {
                MessageBox.Show("תאריך סיום בפועל אינו תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (DateTime.Parse(textBox_plannedStartDate.Text) > DateTime.Parse(textBox_plannedEndDate.Text))
            {
                MessageBox.Show("תאריך סיום מתוכנן לא יכול להיות לפני תאריך התחלה מתוכנן", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(textBox_actualStartDate.Text) && !string.IsNullOrWhiteSpace(textBox_actualEndDate.Text)
                && DateTime.Parse(textBox_actualStartDate.Text) > DateTime.Parse(textBox_actualEndDate.Text))
            {
                MessageBox.Show("תאריך סיום בפועל לא יכול להיות לפני תאריך התחלה בפועל", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Tender resolveSelectedTender()
        {
            int id = int.Parse(comboBox_tender.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Tender.seekTender(id);
        }

        private Employee resolveSelectedProjectManager()
        {
            int id = int.Parse(comboBox_projectManager.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Employee.seekEmployee(id);
        }

        private void clearForm()
        {
            selectedProject = null;
            textBox_projectId.Text = "";
            comboBox_tender.SelectedIndex = -1;
            comboBox_tender.Text = "";
            comboBox_projectManager.SelectedIndex = -1;
            comboBox_projectManager.Text = "";
            textBox_name.Text = "";
            textBox_address.Text = "";
            textBox_plannedStartDate.Text = "";
            textBox_plannedEndDate.Text = "";
            textBox_actualStartDate.Text = "";
            textBox_actualEndDate.Text = "";
            comboBox_status.SelectedIndex = 0;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = Project.getNextProjectId();
            ProjectStatus status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), comboBox_status.Text);
            DateTime? actualStart = string.IsNullOrWhiteSpace(textBox_actualStartDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_actualStartDate.Text);
            DateTime? actualEnd = string.IsNullOrWhiteSpace(textBox_actualEndDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_actualEndDate.Text);

            Project project = new Project(id, resolveSelectedTender(), resolveSelectedProjectManager(), textBox_name.Text, textBox_address.Text,
                DateTime.Parse(textBox_plannedStartDate.Text), DateTime.Parse(textBox_plannedEndDate.Text),
                actualStart, actualEnd, status, true);

            if (!Program.Projects.Contains(project)) return;

            MessageBox.Show("הפרויקט נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadProjects();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedProject == null)
            {
                MessageBox.Show("יש לבחור פרויקט מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedProject.setTender(resolveSelectedTender());
            selectedProject.setProjectManager(resolveSelectedProjectManager());
            selectedProject.setName(textBox_name.Text);
            selectedProject.setAddress(textBox_address.Text);
            selectedProject.setPlannedStartDate(DateTime.Parse(textBox_plannedStartDate.Text));
            selectedProject.setPlannedEndDate(DateTime.Parse(textBox_plannedEndDate.Text));
            selectedProject.setActualStartDate(string.IsNullOrWhiteSpace(textBox_actualStartDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_actualStartDate.Text));
            selectedProject.setActualEndDate(string.IsNullOrWhiteSpace(textBox_actualEndDate.Text) ? (DateTime?)null : DateTime.Parse(textBox_actualEndDate.Text));
            selectedProject.setStatus((ProjectStatus)Enum.Parse(typeof(ProjectStatus), comboBox_status.Text));
            if (!selectedProject.updateProject()) return;

            MessageBox.Show("הפרויקט עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadProjects();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedProject == null)
            {
                MessageBox.Show("יש לבחור פרויקט מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את הפרויקט?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedProject.deleteProject()) return;
            clearForm();
            loadProjects();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

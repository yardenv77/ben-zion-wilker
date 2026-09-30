using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול שורות תקציב — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-BudgetLine ב-00e-use-cases.md; נבנה לפי אותה תבנית כמו EmployeePanel.
    /// </summary>
    public partial class BudgetLinePanel : UserControl
    {
        private BudgetLine selectedBudgetLine;

        public BudgetLinePanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_budgetLines);

            foreach (Project p in Program.Projects)
                comboBox_project.Items.Add(p.getProjectId() + " - " + p.getName());

            loadBudgetLines();
        }

        private void loadBudgetLines()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("budgetLineId", typeof(int));
            dt.Columns.Add("project", typeof(string));
            dt.Columns.Add("category", typeof(string));
            dt.Columns.Add("plannedAmount", typeof(decimal));
            dt.Columns.Add("actualAmount", typeof(decimal));

            foreach (BudgetLine bl in Program.BudgetLines)
                dt.Rows.Add(bl.getBudgetLineId(), bl.getProject().getName(), bl.getCategory(), bl.getPlannedAmount(), bl.getActualAmount());

            dataGridView_budgetLines.DataSource = dt;
            dataGridView_budgetLines.Columns["plannedAmount"].DefaultCellStyle.Format = "N2";
            dataGridView_budgetLines.Columns["actualAmount"].DefaultCellStyle.Format = "N2";

            dataGridView_budgetLines.Columns["budgetLineId"].HeaderText = "מס'";
            dataGridView_budgetLines.Columns["project"].HeaderText = "פרויקט";
            dataGridView_budgetLines.Columns["category"].HeaderText = "קטגוריה";
            dataGridView_budgetLines.Columns["plannedAmount"].HeaderText = "סכום מתוכנן";
            dataGridView_budgetLines.Columns["actualAmount"].HeaderText = "סכום בפועל";
        }

        private void dataGridView_budgetLines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_budgetLines.Rows[e.RowIndex].Cells["budgetLineId"].Value.ToString());
            selectedBudgetLine = BudgetLine.seekBudgetLine(id);
            if (selectedBudgetLine == null) return;

            textBox_budgetLineId.Text = selectedBudgetLine.getBudgetLineId().ToString();
            comboBox_project.Text = selectedBudgetLine.getProject().getProjectId() + " - " + selectedBudgetLine.getProject().getName();
            textBox_category.Text = selectedBudgetLine.getCategory();
            textBox_plannedAmount.Text = selectedBudgetLine.getPlannedAmount().ToString();
            textBox_actualAmount.Text = selectedBudgetLine.getActualAmount().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_project.Text))
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_category.Text))
            {
                MessageBox.Show("יש להזין קטגוריה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_plannedAmount.Text, out _))
            {
                MessageBox.Show("יש להזין סכום מתוכנן תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_actualAmount.Text, out _))
            {
                MessageBox.Show("יש להזין סכום בפועל תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Project resolveSelectedProject()
        {
            int id = int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Project.seekProject(id);
        }

        private void clearForm()
        {
            selectedBudgetLine = null;
            textBox_budgetLineId.Text = "";
            comboBox_project.SelectedIndex = -1;
            comboBox_project.Text = "";
            textBox_category.Text = "";
            textBox_plannedAmount.Text = "";
            textBox_actualAmount.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = BudgetLine.getNextBudgetLineId();
            new BudgetLine(id, resolveSelectedProject(), textBox_category.Text,
                decimal.Parse(textBox_plannedAmount.Text), decimal.Parse(textBox_actualAmount.Text), true);

            MessageBox.Show("שורת התקציב נשמרה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadBudgetLines();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedBudgetLine == null)
            {
                MessageBox.Show("יש לבחור שורת תקציב מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedBudgetLine.setProject(resolveSelectedProject());
            selectedBudgetLine.setCategory(textBox_category.Text);
            selectedBudgetLine.setPlannedAmount(decimal.Parse(textBox_plannedAmount.Text));
            selectedBudgetLine.setActualAmount(decimal.Parse(textBox_actualAmount.Text));
            selectedBudgetLine.updateBudgetLine();

            MessageBox.Show("שורת התקציב עודכנה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadBudgetLines();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedBudgetLine == null)
            {
                MessageBox.Show("יש לבחור שורת תקציב מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את שורת התקציב?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedBudgetLine.deleteBudgetLine();
            clearForm();
            loadBudgetLines();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

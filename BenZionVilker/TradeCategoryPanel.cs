using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול תחומי עיסוק — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-TradeCategory ב-00e-use-cases.md; המסך נבנה לפי אותו תבנית כמו EmployeePanel.
    /// </summary>
    public partial class TradeCategoryPanel : UserControl
    {
        private TradeCategory selectedTradeCategory;

        public TradeCategoryPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_tradeCategories);
            loadTradeCategories();
        }

        private void loadTradeCategories()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tradeCategoryId", typeof(int));
            dt.Columns.Add("categoryName", typeof(string));

            foreach (TradeCategory tc in Program.TradeCategories)
                dt.Rows.Add(tc.getTradeCategoryId(), tc.getCategoryName());

            dataGridView_tradeCategories.DataSource = dt;
            dataGridView_tradeCategories.Columns["tradeCategoryId"].HeaderText = "מס'";
            dataGridView_tradeCategories.Columns["categoryName"].HeaderText = "שם תחום";
        }

        private void dataGridView_tradeCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_tradeCategories.Rows[e.RowIndex].Cells["tradeCategoryId"].Value.ToString());
            selectedTradeCategory = TradeCategory.seekTradeCategory(id);
            if (selectedTradeCategory == null) return;

            textBox_tradeCategoryId.Text = selectedTradeCategory.getTradeCategoryId().ToString();
            textBox_categoryName.Text = selectedTradeCategory.getCategoryName();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_categoryName.Text))
            {
                MessageBox.Show("יש להזין שם תחום", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void clearForm()
        {
            selectedTradeCategory = null;
            textBox_tradeCategoryId.Text = "";
            textBox_categoryName.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = TradeCategory.getNextTradeCategoryId();
            new TradeCategory(id, textBox_categoryName.Text, true);

            MessageBox.Show("תחום העיסוק נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadTradeCategories();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedTradeCategory == null)
            {
                MessageBox.Show("יש לבחור תחום מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedTradeCategory.setCategoryName(textBox_categoryName.Text);
            selectedTradeCategory.updateTradeCategory();

            MessageBox.Show("תחום העיסוק עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadTradeCategories();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedTradeCategory == null)
            {
                MessageBox.Show("יש לבחור תחום מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את תחום העיסוק?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedTradeCategory.deleteTradeCategory();
            clearForm();
            loadTradeCategories();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

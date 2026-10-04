using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול הצעות מחיר מספקים — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// SupplierPriceQuote היא מחלקת קישור טרנרית (Supplier + Tender + TradeCategory), ראו
    /// CLAUDE.md "Ternary association class". אין UC מפורט לישות זו ב-00e-use-cases.md.
    /// </summary>
    public partial class SupplierPriceQuotePanel : UserControl
    {
        private SupplierPriceQuote selectedQuote;

        public SupplierPriceQuotePanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_quotes);

            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (bp is Supplier sup)
                    comboBox_supplier.Items.Add(sup.getBusinessPartnerId() + " - " + sup.getName());
            }
            foreach (Tender t in Program.Tenders)
                comboBox_tender.Items.Add(t.getTenderId() + " - " + t.getTitle());
            foreach (TradeCategory tc in Program.TradeCategories)
                comboBox_tradeCategory.Items.Add(tc.getTradeCategoryId() + " - " + tc.getCategoryName());

            loadQuotes();
        }

        private void loadQuotes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("supplierPriceQuoteId", typeof(int));
            dt.Columns.Add("supplier", typeof(string));
            dt.Columns.Add("tender", typeof(string));
            dt.Columns.Add("tradeCategory", typeof(string));
            dt.Columns.Add("amount", typeof(decimal));
            dt.Columns.Add("dateIssued", typeof(DateTime));
            dt.Columns.Add("validUntil", typeof(DateTime));
            dt.Columns.Add("isSelected", typeof(bool));

            foreach (SupplierPriceQuote q in Program.SupplierPriceQuotes)
            {
                dt.Rows.Add(q.getSupplierPriceQuoteId(), q.getSupplier().getName(), q.getTender().getTitle(),
                    q.getTradeCategory().getCategoryName(), q.getAmount(), q.getDateIssued(), q.getValidUntil(), q.getIsSelected());
            }

            dataGridView_quotes.DataSource = dt;
            dataGridView_quotes.Columns["amount"].DefaultCellStyle.Format = "N2";
            dataGridView_quotes.Columns["dateIssued"].DefaultCellStyle.Format = "yyyy-MM-dd";
            dataGridView_quotes.Columns["validUntil"].DefaultCellStyle.Format = "yyyy-MM-dd";

            dataGridView_quotes.Columns["supplierPriceQuoteId"].HeaderText = "מס'";
            dataGridView_quotes.Columns["supplier"].HeaderText = "ספק";
            dataGridView_quotes.Columns["tender"].HeaderText = "מכרז";
            dataGridView_quotes.Columns["tradeCategory"].HeaderText = "תחום עיסוק";
            dataGridView_quotes.Columns["amount"].HeaderText = "סכום";
            dataGridView_quotes.Columns["dateIssued"].HeaderText = "תאריך הצעה";
            dataGridView_quotes.Columns["validUntil"].HeaderText = "בתוקף עד";
            dataGridView_quotes.Columns["isSelected"].HeaderText = "נבחרה";
        }

        private void dataGridView_quotes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_quotes.Rows[e.RowIndex].Cells["supplierPriceQuoteId"].Value.ToString());
            selectedQuote = SupplierPriceQuote.seekSupplierPriceQuote(id);
            if (selectedQuote == null) return;

            textBox_supplierPriceQuoteId.Text = selectedQuote.getSupplierPriceQuoteId().ToString();
            comboBox_supplier.Text = selectedQuote.getSupplier().getBusinessPartnerId() + " - " + selectedQuote.getSupplier().getName();
            comboBox_tender.Text = selectedQuote.getTender().getTenderId() + " - " + selectedQuote.getTender().getTitle();
            comboBox_tradeCategory.Text = selectedQuote.getTradeCategory().getTradeCategoryId() + " - " + selectedQuote.getTradeCategory().getCategoryName();
            textBox_amount.Text = selectedQuote.getAmount().ToString();
            textBox_dateIssued.Text = selectedQuote.getDateIssued().ToString("yyyy-MM-dd");
            textBox_validUntil.Text = selectedQuote.getValidUntil().ToString("yyyy-MM-dd");
            checkBox_isSelected.Checked = selectedQuote.getIsSelected();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(comboBox_supplier.Text))
            {
                MessageBox.Show("יש לבחור ספק", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_tender.Text))
            {
                MessageBox.Show("יש לבחור מכרז", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_tradeCategory.Text))
            {
                MessageBox.Show("יש לבחור תחום עיסוק", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_amount.Text, out _))
            {
                MessageBox.Show("יש להזין סכום תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_dateIssued.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך הצעה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_validUntil.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך תוקף תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Supplier resolveSelectedSupplier()
        {
            int id = int.Parse(comboBox_supplier.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return BusinessPartner.seekBusinessPartner(id) as Supplier;
        }

        private Tender resolveSelectedTender()
        {
            int id = int.Parse(comboBox_tender.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Tender.seekTender(id);
        }

        private TradeCategory resolveSelectedTradeCategory()
        {
            int id = int.Parse(comboBox_tradeCategory.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return TradeCategory.seekTradeCategory(id);
        }

        private void clearForm()
        {
            selectedQuote = null;
            textBox_supplierPriceQuoteId.Text = "";
            comboBox_supplier.SelectedIndex = -1;
            comboBox_supplier.Text = "";
            comboBox_tender.SelectedIndex = -1;
            comboBox_tender.Text = "";
            comboBox_tradeCategory.SelectedIndex = -1;
            comboBox_tradeCategory.Text = "";
            textBox_amount.Text = "";
            textBox_dateIssued.Text = "";
            textBox_validUntil.Text = "";
            checkBox_isSelected.Checked = false;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = SupplierPriceQuote.getNextSupplierPriceQuoteId();
            SupplierPriceQuote quote = new SupplierPriceQuote(id, resolveSelectedSupplier(), resolveSelectedTender(), resolveSelectedTradeCategory(),
                decimal.Parse(textBox_amount.Text), DateTime.Parse(textBox_dateIssued.Text), DateTime.Parse(textBox_validUntil.Text),
                checkBox_isSelected.Checked, true);

            if (!Program.SupplierPriceQuotes.Contains(quote)) return;

            MessageBox.Show("הצעת המחיר נשמרה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadQuotes();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedQuote == null)
            {
                MessageBox.Show("יש לבחור הצעת מחיר מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedQuote.setSupplier(resolveSelectedSupplier());
            selectedQuote.setTender(resolveSelectedTender());
            selectedQuote.setTradeCategory(resolveSelectedTradeCategory());
            selectedQuote.setAmount(decimal.Parse(textBox_amount.Text));
            selectedQuote.setDateIssued(DateTime.Parse(textBox_dateIssued.Text));
            selectedQuote.setValidUntil(DateTime.Parse(textBox_validUntil.Text));
            selectedQuote.setIsSelected(checkBox_isSelected.Checked);
            if (!selectedQuote.updateSupplierPriceQuote()) return;

            MessageBox.Show("הצעת המחיר עודכנה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadQuotes();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedQuote == null)
            {
                MessageBox.Show("יש לבחור הצעת מחיר מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את הצעת המחיר?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedQuote.deleteSupplierPriceQuote()) return;
            clearForm();
            loadQuotes();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

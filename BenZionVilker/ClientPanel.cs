using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול לקוחות — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-Client ב-00e-use-cases.md; המסך נבנה לפי אותו תבנית כמו EmployeePanel.
    /// </summary>
    public partial class ClientPanel : UserControl
    {
        private Client selectedClient;

        public ClientPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_clients);
            loadClients();
        }

        private void loadClients()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("clientId", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("contactPerson", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("sector", typeof(string));

            foreach (Client c in Program.Clients)
                dt.Rows.Add(c.getClientId(), c.getName(), c.getContactPerson(), c.getPhone(), c.getEmail(), c.getSector());

            dataGridView_clients.DataSource = dt;
            dataGridView_clients.Columns["clientId"].HeaderText = "מס' לקוח";
            dataGridView_clients.Columns["name"].HeaderText = "שם";
            dataGridView_clients.Columns["contactPerson"].HeaderText = "איש קשר";
            dataGridView_clients.Columns["phone"].HeaderText = "טלפון";
            dataGridView_clients.Columns["email"].HeaderText = "דוא\"ל";
            dataGridView_clients.Columns["sector"].HeaderText = "תחום עיסוק";
        }

        private void dataGridView_clients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_clients.Rows[e.RowIndex].Cells["clientId"].Value.ToString());
            selectedClient = Client.seekClient(id);
            if (selectedClient == null) return;

            textBox_clientId.Text = selectedClient.getClientId().ToString();
            textBox_name.Text = selectedClient.getName();
            textBox_contactPerson.Text = selectedClient.getContactPerson();
            textBox_phone.Text = selectedClient.getPhone();
            textBox_email.Text = selectedClient.getEmail();
            textBox_sector.Text = selectedClient.getSector();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_name.Text))
            {
                MessageBox.Show("יש להזין שם לקוח", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_contactPerson.Text))
            {
                MessageBox.Show("יש להזין איש קשר", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_phone.Text))
            {
                MessageBox.Show("יש להזין טלפון", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_email.Text))
            {
                MessageBox.Show("יש להזין דוא\"ל", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_sector.Text))
            {
                MessageBox.Show("יש להזין תחום עיסוק", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void clearForm()
        {
            selectedClient = null;
            textBox_clientId.Text = "";
            textBox_name.Text = "";
            textBox_contactPerson.Text = "";
            textBox_phone.Text = "";
            textBox_email.Text = "";
            textBox_sector.Text = "";
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = Client.getNextClientId();
            new Client(id, textBox_name.Text, textBox_contactPerson.Text, textBox_phone.Text, textBox_email.Text, textBox_sector.Text, true);

            MessageBox.Show("הלקוח נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadClients();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedClient == null)
            {
                MessageBox.Show("יש לבחור לקוח מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedClient.setName(textBox_name.Text);
            selectedClient.setContactPerson(textBox_contactPerson.Text);
            selectedClient.setPhone(textBox_phone.Text);
            selectedClient.setEmail(textBox_email.Text);
            selectedClient.setSector(textBox_sector.Text);
            selectedClient.updateClient();

            MessageBox.Show("הלקוח עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadClients();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedClient == null)
            {
                MessageBox.Show("יש לבחור לקוח מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את הלקוח?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            selectedClient.deleteClient();
            clearForm();
            loadClients();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

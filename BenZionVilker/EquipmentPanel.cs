using System;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך ניהול ציוד — רשימה + טופס יצירה/עדכון/מחיקה במסך אחד.
    /// אין UC מפורט ל-Equipment ב-00e-use-cases.md; המסך נבנה לפי אותו תבנית כמו EmployeePanel.
    /// </summary>
    public partial class EquipmentPanel : UserControl
    {
        private Equipment selectedEquipment;

        public EquipmentPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.WrapInCard(dataGridView_equipments);

            foreach (EquipmentStatus s in Enum.GetValues(typeof(EquipmentStatus)))
                comboBox_status.Items.Add(s.ToString());
            comboBox_status.SelectedIndex = 0;

            loadEquipments();
        }

        private void loadEquipments()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("equipmentId", typeof(int));
            dt.Columns.Add("licenseNumber", typeof(string));
            dt.Columns.Add("equipmentType", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("dailyCost", typeof(decimal));
            dt.Columns.Add("status", typeof(string));

            foreach (Equipment eq in Program.Equipments)
                dt.Rows.Add(eq.getEquipmentId(), eq.getLicenseNumber(), eq.getEquipmentType(), eq.getDescription(), eq.getDailyCost(), eq.getStatus().ToString());

            dataGridView_equipments.DataSource = dt;
            dataGridView_equipments.Columns["dailyCost"].DefaultCellStyle.Format = "N2";

            dataGridView_equipments.Columns["equipmentId"].HeaderText = "מס'";
            dataGridView_equipments.Columns["licenseNumber"].HeaderText = "מס' רישוי";
            dataGridView_equipments.Columns["equipmentType"].HeaderText = "סוג ציוד";
            dataGridView_equipments.Columns["description"].HeaderText = "תיאור";
            dataGridView_equipments.Columns["dailyCost"].HeaderText = "עלות יומית";
            dataGridView_equipments.Columns["status"].HeaderText = "סטטוס";
        }

        private void dataGridView_equipments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_equipments.Rows[e.RowIndex].Cells["equipmentId"].Value.ToString());
            selectedEquipment = Equipment.seekEquipment(id);
            if (selectedEquipment == null) return;

            textBox_equipmentId.Text = selectedEquipment.getEquipmentId().ToString();
            textBox_licenseNumber.Text = selectedEquipment.getLicenseNumber();
            textBox_equipmentType.Text = selectedEquipment.getEquipmentType();
            textBox_description.Text = selectedEquipment.getDescription();
            textBox_dailyCost.Text = selectedEquipment.getDailyCost().ToString();
            comboBox_status.Text = selectedEquipment.getStatus().ToString();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_licenseNumber.Text))
            {
                MessageBox.Show("יש להזין מספר רישוי", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_equipmentType.Text))
            {
                MessageBox.Show("יש להזין סוג ציוד", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBox_description.Text))
            {
                MessageBox.Show("יש להזין תיאור", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_dailyCost.Text, out _))
            {
                MessageBox.Show("יש להזין עלות יומית תקינה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void clearForm()
        {
            selectedEquipment = null;
            textBox_equipmentId.Text = "";
            textBox_licenseNumber.Text = "";
            textBox_equipmentType.Text = "";
            textBox_description.Text = "";
            textBox_dailyCost.Text = "";
            comboBox_status.SelectedIndex = 0;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = Equipment.getNextEquipmentId();
            EquipmentStatus status = (EquipmentStatus)Enum.Parse(typeof(EquipmentStatus), comboBox_status.Text);
            Equipment eq = new Equipment(id, textBox_licenseNumber.Text, textBox_equipmentType.Text, textBox_description.Text, decimal.Parse(textBox_dailyCost.Text), status, true);

            if (!Program.Equipments.Contains(eq)) return;

            MessageBox.Show("הציוד נשמר בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadEquipments();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (selectedEquipment == null)
            {
                MessageBox.Show("יש לבחור ציוד מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            selectedEquipment.setLicenseNumber(textBox_licenseNumber.Text);
            selectedEquipment.setEquipmentType(textBox_equipmentType.Text);
            selectedEquipment.setDescription(textBox_description.Text);
            selectedEquipment.setDailyCost(decimal.Parse(textBox_dailyCost.Text));
            selectedEquipment.setStatus((EquipmentStatus)Enum.Parse(typeof(EquipmentStatus), comboBox_status.Text));
            if (!selectedEquipment.updateEquipment()) return;

            MessageBox.Show("הציוד עודכן בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadEquipments();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedEquipment == null)
            {
                MessageBox.Show("יש לבחור ציוד מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את הציוד?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedEquipment.deleteEquipment()) return;
            clearForm();
            loadEquipments();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

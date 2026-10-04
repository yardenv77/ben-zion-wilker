using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class Equipment
    {
        private int equipmentId;
        private string licenseNumber;
        private string equipmentType;
        private string description;
        private decimal dailyCost;
        private EquipmentStatus status;

        public Equipment(int equipmentId, string licenseNumber, string equipmentType, string description, decimal dailyCost, EquipmentStatus status, bool is_new)
        {
            this.equipmentId = equipmentId;
            this.licenseNumber = licenseNumber;
            this.equipmentType = equipmentType;
            this.description = description;
            this.dailyCost = dailyCost;
            this.status = status;
            if (is_new)
            {
                if (this.createEquipment())
                    Program.Equipments.Add(this);
            }
        }

        public int getEquipmentId() { return this.equipmentId; }
        public string getLicenseNumber() { return this.licenseNumber; }
        public string getEquipmentType() { return this.equipmentType; }
        public string getDescription() { return this.description; }
        public decimal getDailyCost() { return this.dailyCost; }
        public EquipmentStatus getStatus() { return this.status; }

        public void setLicenseNumber(string licenseNumber) { this.licenseNumber = licenseNumber; }
        public void setEquipmentType(string equipmentType) { this.equipmentType = equipmentType; }
        public void setDescription(string description) { this.description = description; }
        public void setDailyCost(decimal dailyCost) { this.dailyCost = dailyCost; }
        public void setStatus(EquipmentStatus status) { this.status = status; }

        public bool createEquipment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_create @equipment_id, @licenseNumber, @equipmentType, @description, @dailyCost, @status";
            cmd.Parameters.AddWithValue("@equipment_id", this.equipmentId);
            cmd.Parameters.AddWithValue("@licenseNumber", this.licenseNumber);
            cmd.Parameters.AddWithValue("@equipmentType", this.equipmentType);
            cmd.Parameters.AddWithValue("@description", this.description);
            cmd.Parameters.AddWithValue("@dailyCost", this.dailyCost);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool updateEquipment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_update @equipment_id, @licenseNumber, @equipmentType, @description, @dailyCost, @status";
            cmd.Parameters.AddWithValue("@equipment_id", this.equipmentId);
            cmd.Parameters.AddWithValue("@licenseNumber", this.licenseNumber);
            cmd.Parameters.AddWithValue("@equipmentType", this.equipmentType);
            cmd.Parameters.AddWithValue("@description", this.description);
            cmd.Parameters.AddWithValue("@dailyCost", this.dailyCost);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deleteEquipment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_delete @equipment_id";
            cmd.Parameters.AddWithValue("@equipment_id", this.equipmentId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.Equipments.Remove(this);
            return success;
        }

        public static void initEquipments()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Equipments = new List<Equipment>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string licenseNumber = rdr.GetValue(1).ToString();
                string equipmentType = rdr.GetValue(2).ToString();
                string description = rdr.GetValue(3).ToString();
                decimal dailyCost = decimal.Parse(rdr.GetValue(4).ToString());
                EquipmentStatus status = (EquipmentStatus)Enum.Parse(typeof(EquipmentStatus), rdr.GetValue(5).ToString());

                Equipment eq = new Equipment(id, licenseNumber, equipmentType, description, dailyCost, status, false);
                Program.Equipments.Add(eq);
            }
        }

        public static Equipment seekEquipment(int id)
        {
            foreach (Equipment eq in Program.Equipments)
            {
                if (eq.getEquipmentId() == id)
                    return eq;
            }
            return null;
        }

        public static int getNextEquipmentId()
        {
            int maxId = 0;
            foreach (Equipment eq in Program.Equipments)
            {
                if (eq.getEquipmentId() > maxId)
                    maxId = eq.getEquipmentId();
            }
            return maxId + 1;
        }
    }
}

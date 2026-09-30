using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Association class linking Equipment <-> DailyWorkLog (many-to-many).
    public class EquipmentUsage
    {
        private int equipmentUsageId;
        private Equipment equipment;
        private DailyWorkLog dailyWorkLog;
        private double hoursOperated;

        public EquipmentUsage(int equipmentUsageId, Equipment equipment, DailyWorkLog dailyWorkLog, double hoursOperated, bool is_new)
        {
            this.equipmentUsageId = equipmentUsageId;
            this.equipment = equipment;
            this.dailyWorkLog = dailyWorkLog;
            this.hoursOperated = hoursOperated;
            if (is_new)
            {
                this.createEquipmentUsage();
                Program.EquipmentUsages.Add(this);
            }
        }

        public int getEquipmentUsageId() { return this.equipmentUsageId; }
        public Equipment getEquipment() { return this.equipment; }
        public DailyWorkLog getDailyWorkLog() { return this.dailyWorkLog; }
        public double getHoursOperated() { return this.hoursOperated; }

        public void setEquipment(Equipment equipment) { this.equipment = equipment; }
        public void setDailyWorkLog(DailyWorkLog dailyWorkLog) { this.dailyWorkLog = dailyWorkLog; }
        public void setHoursOperated(double hoursOperated) { this.hoursOperated = hoursOperated; }

        public void createEquipmentUsage()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_usage_create @equipment_usage_id, @equipment_id, @daily_work_log_id, @hoursOperated";
            cmd.Parameters.AddWithValue("@equipment_usage_id", this.equipmentUsageId);
            cmd.Parameters.AddWithValue("@equipment_id", this.equipment.getEquipmentId());
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLog.getDailyWorkLogId());
            cmd.Parameters.AddWithValue("@hoursOperated", this.hoursOperated);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateEquipmentUsage()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_usage_update @equipment_usage_id, @equipment_id, @daily_work_log_id, @hoursOperated";
            cmd.Parameters.AddWithValue("@equipment_usage_id", this.equipmentUsageId);
            cmd.Parameters.AddWithValue("@equipment_id", this.equipment.getEquipmentId());
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLog.getDailyWorkLogId());
            cmd.Parameters.AddWithValue("@hoursOperated", this.hoursOperated);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteEquipmentUsage()
        {
            Program.EquipmentUsages.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_usage_delete @equipment_usage_id";
            cmd.Parameters.AddWithValue("@equipment_usage_id", this.equipmentUsageId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initEquipmentUsages()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_usage_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.EquipmentUsages = new List<EquipmentUsage>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Equipment equipment = Equipment.seekEquipment(int.Parse(rdr.GetValue(1).ToString()));
                DailyWorkLog dailyWorkLog = DailyWorkLog.seekDailyWorkLog(int.Parse(rdr.GetValue(2).ToString()));
                double hoursOperated = double.Parse(rdr.GetValue(3).ToString());

                EquipmentUsage eu = new EquipmentUsage(id, equipment, dailyWorkLog, hoursOperated, false);
                Program.EquipmentUsages.Add(eu);
            }
        }

        public static EquipmentUsage seekEquipmentUsage(int id)
        {
            foreach (EquipmentUsage eu in Program.EquipmentUsages)
            {
                if (eu.getEquipmentUsageId() == id)
                    return eu;
            }
            return null;
        }

        public static int getNextEquipmentUsageId()
        {
            int maxId = 0;
            foreach (EquipmentUsage eu in Program.EquipmentUsages)
            {
                if (eu.getEquipmentUsageId() > maxId)
                    maxId = eu.getEquipmentUsageId();
            }
            return maxId + 1;
        }
    }
}

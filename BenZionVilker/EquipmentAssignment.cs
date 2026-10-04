using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Link class for the Project o-- Equipment aggregation.
    public class EquipmentAssignment
    {
        private int equipmentAssignmentId;
        private Project project;
        private Equipment equipment;
        private DateTime startDate;
        private DateTime? endDate; // nullable: an active/ongoing assignment has no end date yet

        public EquipmentAssignment(int equipmentAssignmentId, Project project, Equipment equipment,
            DateTime startDate, DateTime? endDate, bool is_new)
        {
            this.equipmentAssignmentId = equipmentAssignmentId;
            this.project = project;
            this.equipment = equipment;
            this.startDate = startDate;
            this.endDate = endDate;
            if (is_new)
            {
                if (this.createEquipmentAssignment())
                    Program.EquipmentAssignments.Add(this);
            }
        }

        public int getEquipmentAssignmentId() { return this.equipmentAssignmentId; }
        public Project getProject() { return this.project; }
        public Equipment getEquipment() { return this.equipment; }
        public DateTime getStartDate() { return this.startDate; }
        public DateTime? getEndDate() { return this.endDate; }

        public void setProject(Project project) { this.project = project; }
        public void setEquipment(Equipment equipment) { this.equipment = equipment; }
        public void setStartDate(DateTime startDate) { this.startDate = startDate; }
        public void setEndDate(DateTime? endDate) { this.endDate = endDate; }

        public bool createEquipmentAssignment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_assignment_create @equipment_assignment_id, @project_id, @equipment_id, @startDate, @endDate";
            cmd.Parameters.AddWithValue("@equipment_assignment_id", this.equipmentAssignmentId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@equipment_id", this.equipment.getEquipmentId());
            cmd.Parameters.AddWithValue("@startDate", this.startDate);
            cmd.Parameters.AddWithValue("@endDate", (object)this.endDate ?? DBNull.Value);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool updateEquipmentAssignment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_assignment_update @equipment_assignment_id, @project_id, @equipment_id, @startDate, @endDate";
            cmd.Parameters.AddWithValue("@equipment_assignment_id", this.equipmentAssignmentId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@equipment_id", this.equipment.getEquipmentId());
            cmd.Parameters.AddWithValue("@startDate", this.startDate);
            cmd.Parameters.AddWithValue("@endDate", (object)this.endDate ?? DBNull.Value);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deleteEquipmentAssignment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_assignment_delete @equipment_assignment_id";
            cmd.Parameters.AddWithValue("@equipment_assignment_id", this.equipmentAssignmentId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.EquipmentAssignments.Remove(this);
            return success;
        }

        public static void initEquipmentAssignments()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_equipment_assignment_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.EquipmentAssignments = new List<EquipmentAssignment>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Project project = Project.seekProject(int.Parse(rdr.GetValue(1).ToString()));
                Equipment equipment = Equipment.seekEquipment(int.Parse(rdr.GetValue(2).ToString()));
                DateTime startDate = DateTime.Parse(rdr.GetValue(3).ToString());
                DateTime? endDate = rdr.GetValue(4) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(4).ToString());

                EquipmentAssignment ea = new EquipmentAssignment(id, project, equipment, startDate, endDate, false);
                Program.EquipmentAssignments.Add(ea);
            }
        }

        public static EquipmentAssignment seekEquipmentAssignment(int id)
        {
            foreach (EquipmentAssignment ea in Program.EquipmentAssignments)
            {
                if (ea.getEquipmentAssignmentId() == id)
                    return ea;
            }
            return null;
        }

        public static int getNextEquipmentAssignmentId()
        {
            int maxId = 0;
            foreach (EquipmentAssignment ea in Program.EquipmentAssignments)
            {
                if (ea.getEquipmentAssignmentId() > maxId)
                    maxId = ea.getEquipmentAssignmentId();
            }
            return maxId + 1;
        }
    }
}

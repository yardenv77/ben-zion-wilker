using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class Project
    {
        private int projectId;
        private Tender tender; // relationship #2: "Tender 1 -- 0..1 Project" (becomes) -- at most one Project per Tender
        private Employee projectManager; // role "projectManager"
        private string name;
        private string address;
        private DateTime plannedStartDate;
        private DateTime plannedEndDate;
        private DateTime? actualStartDate; // nullable: unknown until the project actually starts
        private DateTime? actualEndDate;   // nullable: unknown until the project actually finishes
        private ProjectStatus status;

        public Project(int projectId, Tender tender, Employee projectManager, string name, string address,
            DateTime plannedStartDate, DateTime plannedEndDate, DateTime? actualStartDate, DateTime? actualEndDate,
            ProjectStatus status, bool is_new)
        {
            this.projectId = projectId;
            this.tender = tender;
            this.projectManager = projectManager;
            this.name = name;
            this.address = address;
            this.plannedStartDate = plannedStartDate;
            this.plannedEndDate = plannedEndDate;
            this.actualStartDate = actualStartDate;
            this.actualEndDate = actualEndDate;
            this.status = status;
            if (is_new)
            {
                this.createProject();
                Program.Projects.Add(this);
            }
        }

        public int getProjectId() { return this.projectId; }
        public Tender getTender() { return this.tender; }
        public Employee getProjectManager() { return this.projectManager; }
        public string getName() { return this.name; }
        public string getAddress() { return this.address; }
        public DateTime getPlannedStartDate() { return this.plannedStartDate; }
        public DateTime getPlannedEndDate() { return this.plannedEndDate; }
        public DateTime? getActualStartDate() { return this.actualStartDate; }
        public DateTime? getActualEndDate() { return this.actualEndDate; }
        public ProjectStatus getStatus() { return this.status; }

        public void setTender(Tender tender) { this.tender = tender; }
        public void setProjectManager(Employee projectManager) { this.projectManager = projectManager; }
        public void setName(string name) { this.name = name; }
        public void setAddress(string address) { this.address = address; }
        public void setPlannedStartDate(DateTime plannedStartDate) { this.plannedStartDate = plannedStartDate; }
        public void setPlannedEndDate(DateTime plannedEndDate) { this.plannedEndDate = plannedEndDate; }
        public void setActualStartDate(DateTime? actualStartDate) { this.actualStartDate = actualStartDate; }
        public void setActualEndDate(DateTime? actualEndDate) { this.actualEndDate = actualEndDate; }
        public void setStatus(ProjectStatus status) { this.status = status; }

        public void createProject()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_project_create @project_id, @tender_id, @project_manager_employee_id, @name, @address, @plannedStartDate, @plannedEndDate, @actualStartDate, @actualEndDate, @status";
            cmd.Parameters.AddWithValue("@project_id", this.projectId);
            cmd.Parameters.AddWithValue("@tender_id", this.tender.getTenderId());
            cmd.Parameters.AddWithValue("@project_manager_employee_id", this.projectManager.getEmployeeId());
            cmd.Parameters.AddWithValue("@name", this.name);
            cmd.Parameters.AddWithValue("@address", this.address);
            cmd.Parameters.AddWithValue("@plannedStartDate", this.plannedStartDate);
            cmd.Parameters.AddWithValue("@plannedEndDate", this.plannedEndDate);
            cmd.Parameters.AddWithValue("@actualStartDate", (object)this.actualStartDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@actualEndDate", (object)this.actualEndDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateProject()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_project_update @project_id, @tender_id, @project_manager_employee_id, @name, @address, @plannedStartDate, @plannedEndDate, @actualStartDate, @actualEndDate, @status";
            cmd.Parameters.AddWithValue("@project_id", this.projectId);
            cmd.Parameters.AddWithValue("@tender_id", this.tender.getTenderId());
            cmd.Parameters.AddWithValue("@project_manager_employee_id", this.projectManager.getEmployeeId());
            cmd.Parameters.AddWithValue("@name", this.name);
            cmd.Parameters.AddWithValue("@address", this.address);
            cmd.Parameters.AddWithValue("@plannedStartDate", this.plannedStartDate);
            cmd.Parameters.AddWithValue("@plannedEndDate", this.plannedEndDate);
            cmd.Parameters.AddWithValue("@actualStartDate", (object)this.actualStartDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@actualEndDate", (object)this.actualEndDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteProject()
        {
            Program.Projects.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_project_delete @project_id";
            cmd.Parameters.AddWithValue("@project_id", this.projectId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initProjects()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_project_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Projects = new List<Project>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Tender tender = Tender.seekTender(int.Parse(rdr.GetValue(1).ToString()));
                Employee projectManager = Employee.seekEmployee(int.Parse(rdr.GetValue(2).ToString()));
                string name = rdr.GetValue(3).ToString();
                string address = rdr.GetValue(4).ToString();
                DateTime plannedStartDate = DateTime.Parse(rdr.GetValue(5).ToString());
                DateTime plannedEndDate = DateTime.Parse(rdr.GetValue(6).ToString());
                DateTime? actualStartDate = rdr.GetValue(7) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(7).ToString());
                DateTime? actualEndDate = rdr.GetValue(8) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(8).ToString());
                ProjectStatus status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), rdr.GetValue(9).ToString());

                Project p = new Project(id, tender, projectManager, name, address, plannedStartDate, plannedEndDate, actualStartDate, actualEndDate, status, false);
                Program.Projects.Add(p);
            }
        }

        public static Project seekProject(int id)
        {
            foreach (Project p in Program.Projects)
            {
                if (p.getProjectId() == id)
                    return p;
            }
            return null;
        }

        public static int getNextProjectId()
        {
            int maxId = 0;
            foreach (Project p in Program.Projects)
            {
                if (p.getProjectId() > maxId)
                    maxId = p.getProjectId();
            }
            return maxId + 1;
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class DailyWorkLog
    {
        private int dailyWorkLogId;
        private Subcontractor subcontractor; // nullable: relationship is optional (0..1) -- a log can be entirely internal crew
        private Employee submittedBy; // role "submittedBy": the site supervisor who filed the log
        private DateTime logDate;
        private double plannedQuantity;
        private double completedQuantity;
        private WorkLogStatus status;

        public DailyWorkLog(int dailyWorkLogId, Subcontractor subcontractor, Employee submittedBy, DateTime logDate,
            double plannedQuantity, double completedQuantity, WorkLogStatus status, bool is_new)
        {
            this.dailyWorkLogId = dailyWorkLogId;
            this.subcontractor = subcontractor;
            this.submittedBy = submittedBy;
            this.logDate = logDate;
            this.plannedQuantity = plannedQuantity;
            this.completedQuantity = completedQuantity;
            this.status = status;
            if (is_new)
            {
                if (this.createDailyWorkLog())
                    Program.DailyWorkLogs.Add(this);
            }
        }

        public int getDailyWorkLogId() { return this.dailyWorkLogId; }
        public Subcontractor getSubcontractor() { return this.subcontractor; }
        public Employee getSubmittedBy() { return this.submittedBy; }
        public DateTime getLogDate() { return this.logDate; }
        public double getPlannedQuantity() { return this.plannedQuantity; }
        public double getCompletedQuantity() { return this.completedQuantity; }
        public WorkLogStatus getStatus() { return this.status; }

        public void setSubcontractor(Subcontractor subcontractor) { this.subcontractor = subcontractor; }
        public void setSubmittedBy(Employee submittedBy) { this.submittedBy = submittedBy; }
        public void setLogDate(DateTime logDate) { this.logDate = logDate; }
        public void setPlannedQuantity(double plannedQuantity) { this.plannedQuantity = plannedQuantity; }
        public void setCompletedQuantity(double completedQuantity) { this.completedQuantity = completedQuantity; }
        public void setStatus(WorkLogStatus status) { this.status = status; }

        public bool createDailyWorkLog()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_daily_work_log_create @daily_work_log_id, @subcontractor_id, @submitted_by_employee_id, @logDate, @plannedQuantity, @completedQuantity, @status";
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLogId);
            cmd.Parameters.AddWithValue("@subcontractor_id", (object)this.subcontractor?.getBusinessPartnerId() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@submitted_by_employee_id", this.submittedBy.getEmployeeId());
            cmd.Parameters.AddWithValue("@logDate", this.logDate);
            cmd.Parameters.AddWithValue("@plannedQuantity", this.plannedQuantity);
            cmd.Parameters.AddWithValue("@completedQuantity", this.completedQuantity);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool updateDailyWorkLog()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_daily_work_log_update @daily_work_log_id, @subcontractor_id, @submitted_by_employee_id, @logDate, @plannedQuantity, @completedQuantity, @status";
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLogId);
            cmd.Parameters.AddWithValue("@subcontractor_id", (object)this.subcontractor?.getBusinessPartnerId() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@submitted_by_employee_id", this.submittedBy.getEmployeeId());
            cmd.Parameters.AddWithValue("@logDate", this.logDate);
            cmd.Parameters.AddWithValue("@plannedQuantity", this.plannedQuantity);
            cmd.Parameters.AddWithValue("@completedQuantity", this.completedQuantity);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deleteDailyWorkLog()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_daily_work_log_delete @daily_work_log_id";
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLogId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.DailyWorkLogs.Remove(this);
            return success;
        }

        public static void initDailyWorkLogs()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_daily_work_log_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.DailyWorkLogs = new List<DailyWorkLog>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Subcontractor subcontractor = rdr.GetValue(1) == DBNull.Value
                    ? null
                    : (Subcontractor)BusinessPartner.seekBusinessPartner(int.Parse(rdr.GetValue(1).ToString()));
                Employee submittedBy = Employee.seekEmployee(int.Parse(rdr.GetValue(2).ToString()));
                DateTime logDate = DateTime.Parse(rdr.GetValue(3).ToString());
                double plannedQuantity = double.Parse(rdr.GetValue(4).ToString());
                double completedQuantity = double.Parse(rdr.GetValue(5).ToString());
                WorkLogStatus status = (WorkLogStatus)Enum.Parse(typeof(WorkLogStatus), rdr.GetValue(6).ToString());

                DailyWorkLog log = new DailyWorkLog(id, subcontractor, submittedBy, logDate, plannedQuantity, completedQuantity, status, false);
                Program.DailyWorkLogs.Add(log);
            }
        }

        public static DailyWorkLog seekDailyWorkLog(int id)
        {
            foreach (DailyWorkLog log in Program.DailyWorkLogs)
            {
                if (log.getDailyWorkLogId() == id)
                    return log;
            }
            return null;
        }

        public static int getNextDailyWorkLogId()
        {
            int maxId = 0;
            foreach (DailyWorkLog log in Program.DailyWorkLogs)
            {
                if (log.getDailyWorkLogId() > maxId)
                    maxId = log.getDailyWorkLogId();
            }
            return maxId + 1;
        }
    }
}

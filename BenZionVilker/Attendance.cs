using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Association class linking Employee <-> DailyWorkLog (many-to-many).
    public class Attendance
    {
        private int attendanceId;
        private Employee employee;
        private DailyWorkLog dailyWorkLog;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private string taskDescription;

        public Attendance(int attendanceId, Employee employee, DailyWorkLog dailyWorkLog,
            TimeSpan startTime, TimeSpan endTime, string taskDescription, bool is_new)
        {
            this.attendanceId = attendanceId;
            this.employee = employee;
            this.dailyWorkLog = dailyWorkLog;
            this.startTime = startTime;
            this.endTime = endTime;
            this.taskDescription = taskDescription;
            if (is_new)
            {
                if (this.createAttendance())
                    Program.Attendances.Add(this);
            }
        }

        public int getAttendanceId() { return this.attendanceId; }
        public Employee getEmployee() { return this.employee; }
        public DailyWorkLog getDailyWorkLog() { return this.dailyWorkLog; }
        public TimeSpan getStartTime() { return this.startTime; }
        public TimeSpan getEndTime() { return this.endTime; }
        public string getTaskDescription() { return this.taskDescription; }
        public double hoursWorked() { return (this.endTime - this.startTime).TotalHours; } // derived, per class-diagram.md

        public void setEmployee(Employee employee) { this.employee = employee; }
        public void setDailyWorkLog(DailyWorkLog dailyWorkLog) { this.dailyWorkLog = dailyWorkLog; }
        public void setStartTime(TimeSpan startTime) { this.startTime = startTime; }
        public void setEndTime(TimeSpan endTime) { this.endTime = endTime; }
        public void setTaskDescription(string taskDescription) { this.taskDescription = taskDescription; }

        public bool createAttendance()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_attendance_create @attendance_id, @employee_id, @daily_work_log_id, @startTime, @endTime, @taskDescription";
            cmd.Parameters.AddWithValue("@attendance_id", this.attendanceId);
            cmd.Parameters.AddWithValue("@employee_id", this.employee.getEmployeeId());
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLog.getDailyWorkLogId());
            cmd.Parameters.AddWithValue("@startTime", this.startTime);
            cmd.Parameters.AddWithValue("@endTime", this.endTime);
            cmd.Parameters.AddWithValue("@taskDescription", this.taskDescription);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool updateAttendance()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_attendance_update @attendance_id, @employee_id, @daily_work_log_id, @startTime, @endTime, @taskDescription";
            cmd.Parameters.AddWithValue("@attendance_id", this.attendanceId);
            cmd.Parameters.AddWithValue("@employee_id", this.employee.getEmployeeId());
            cmd.Parameters.AddWithValue("@daily_work_log_id", this.dailyWorkLog.getDailyWorkLogId());
            cmd.Parameters.AddWithValue("@startTime", this.startTime);
            cmd.Parameters.AddWithValue("@endTime", this.endTime);
            cmd.Parameters.AddWithValue("@taskDescription", this.taskDescription);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deleteAttendance()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_attendance_delete @attendance_id";
            cmd.Parameters.AddWithValue("@attendance_id", this.attendanceId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.Attendances.Remove(this);
            return success;
        }

        public static void initAttendances()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_attendance_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Attendances = new List<Attendance>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Employee employee = Employee.seekEmployee(int.Parse(rdr.GetValue(1).ToString()));
                DailyWorkLog dailyWorkLog = DailyWorkLog.seekDailyWorkLog(int.Parse(rdr.GetValue(2).ToString()));
                TimeSpan startTime = TimeSpan.Parse(rdr.GetValue(3).ToString());
                TimeSpan endTime = TimeSpan.Parse(rdr.GetValue(4).ToString());
                string taskDescription = rdr.GetValue(5).ToString();

                Attendance a = new Attendance(id, employee, dailyWorkLog, startTime, endTime, taskDescription, false);
                Program.Attendances.Add(a);
            }
        }

        public static Attendance seekAttendance(int id)
        {
            foreach (Attendance a in Program.Attendances)
            {
                if (a.getAttendanceId() == id)
                    return a;
            }
            return null;
        }

        public static int getNextAttendanceId()
        {
            int maxId = 0;
            foreach (Attendance a in Program.Attendances)
            {
                if (a.getAttendanceId() > maxId)
                    maxId = a.getAttendanceId();
            }
            return maxId + 1;
        }
    }
}

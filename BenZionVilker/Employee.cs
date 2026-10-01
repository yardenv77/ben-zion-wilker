using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class Employee
    {
        private int employeeId;
        private string firstName;
        private string lastName;
        private string nationalId;
        private EmployeeRole role;
        private decimal dailyRate;
        private string certificationNo;
        private EmployeeStatus status;

        public Employee(int employeeId, string firstName, string lastName, string nationalId,
            EmployeeRole role, decimal dailyRate, string certificationNo, EmployeeStatus status, bool is_new)
        {
            this.employeeId = employeeId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.nationalId = nationalId;
            this.role = role;
            this.dailyRate = dailyRate;
            this.certificationNo = certificationNo;
            this.status = status;
            if (is_new)
            {
                this.createEmployee();
                Program.Employees.Add(this);
            }
        }

        public int getEmployeeId() { return this.employeeId; }
        public string getFirstName() { return this.firstName; }
        public string getLastName() { return this.lastName; }
        public string getFullName() { return this.firstName + " " + this.lastName; }
        public string getNationalId() { return this.nationalId; }
        public EmployeeRole getRole() { return this.role; }
        public decimal getDailyRate() { return this.dailyRate; }
        public string getCertificationNo() { return this.certificationNo; }
        public EmployeeStatus getStatus() { return this.status; }

        public void setFirstName(string firstName) { this.firstName = firstName; }
        public void setLastName(string lastName) { this.lastName = lastName; }
        public void setNationalId(string nationalId) { this.nationalId = nationalId; }
        public void setRole(EmployeeRole role) { this.role = role; }
        public void setDailyRate(decimal dailyRate) { this.dailyRate = dailyRate; }
        public void setCertificationNo(string certificationNo) { this.certificationNo = certificationNo; }
        public void setStatus(EmployeeStatus status) { this.status = status; }

        public void createEmployee()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_employee_create @employee_id, @firstName, @lastName, @nationalId, @role, @dailyRate, @certificationNo, @status";
            cmd.Parameters.AddWithValue("@employee_id", this.employeeId);
            cmd.Parameters.AddWithValue("@firstName", this.firstName);
            cmd.Parameters.AddWithValue("@lastName", this.lastName);
            cmd.Parameters.AddWithValue("@nationalId", this.nationalId);
            cmd.Parameters.AddWithValue("@role", this.role.ToString());
            cmd.Parameters.AddWithValue("@dailyRate", this.dailyRate);
            cmd.Parameters.AddWithValue("@certificationNo", this.certificationNo);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateEmployee()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_employee_update @employee_id, @firstName, @lastName, @nationalId, @role, @dailyRate, @certificationNo, @status";
            cmd.Parameters.AddWithValue("@employee_id", this.employeeId);
            cmd.Parameters.AddWithValue("@firstName", this.firstName);
            cmd.Parameters.AddWithValue("@lastName", this.lastName);
            cmd.Parameters.AddWithValue("@nationalId", this.nationalId);
            cmd.Parameters.AddWithValue("@role", this.role.ToString());
            cmd.Parameters.AddWithValue("@dailyRate", this.dailyRate);
            cmd.Parameters.AddWithValue("@certificationNo", this.certificationNo);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteEmployee()
        {
            Program.Employees.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_employee_delete @employee_id";
            cmd.Parameters.AddWithValue("@employee_id", this.employeeId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initEmployees()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_employee_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Employees = new List<Employee>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string firstName = rdr.GetValue(1).ToString();
                string lastName = rdr.GetValue(2).ToString();
                string nationalId = rdr.GetValue(3).ToString();
                EmployeeRole role = (EmployeeRole)Enum.Parse(typeof(EmployeeRole), rdr.GetValue(4).ToString());
                decimal dailyRate = decimal.Parse(rdr.GetValue(5).ToString());
                string certificationNo = rdr.GetValue(6).ToString();
                EmployeeStatus status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), rdr.GetValue(7).ToString());

                Employee e = new Employee(id, firstName, lastName, nationalId, role, dailyRate, certificationNo, status, false);
                Program.Employees.Add(e);
            }
        }

        public static Employee seekEmployee(int id)
        {
            foreach (Employee e in Program.Employees)
            {
                if (e.getEmployeeId() == id)
                    return e;
            }
            return null;
        }

        public static int getNextEmployeeId()
        {
            int maxId = 0;
            foreach (Employee e in Program.Employees)
            {
                if (e.getEmployeeId() > maxId)
                    maxId = e.getEmployeeId();
            }
            return maxId + 1;
        }

        // ====================================================================
        // UC-03 Create Purchase Order (docs/00e-use-cases.md, MSS steps 1-9).
        // "this" Employee is the Accountant performing the action -- the
        // originating entity, same role UserProfile.registerForClass(slotId)
        // plays in the course's class-registration example.
        //
        // Every guard (supplier active, project exists, project has a
        // BudgetLine, at least one line, valid line amounts) is enforced by
        // sp_purchase_order_create_flow itself, not duplicated here -- this
        // method only assigns PKs before the call (per the Primary Key
        // Strategy) and, on success, syncs Program.PurchaseOrders /
        // Program.PurchaseOrderLines to match what the procedure actually
        // committed. On failure it translates the procedure's guard message
        // into the Hebrew string PurchaseOrderPanel shows via MessageBox --
        // matching how PurchaseOrder.cs's state-transition methods already
        // throw with Hebrew text.
        //
        // Unlike the class-registration example, there is no third affected
        // entity whose running counter needs to move here: BudgetLine.actualAmount
        // is written later (when the order is actually invoiced/received), not
        // at PendingPMApproval/PendingBudgetOverride creation time -- BudgetLine
        // is read (for the remaining-budget check) but not updated by this flow.
        // ====================================================================
        public PurchaseOrder createPurchaseOrder(string poNumber, Supplier supplier, Project project,
            DateTime orderDate, List<PurchaseOrderLineInput> lines)
        {
            int purchaseOrderId = PurchaseOrder.getNextPurchaseOrderId();

            DataTable linesTable = new DataTable();
            linesTable.Columns.Add("purchase_order_line_id", typeof(int));
            linesTable.Columns.Add("description", typeof(string));
            linesTable.Columns.Add("unitOfMeasure", typeof(string));
            linesTable.Columns.Add("quantity", typeof(double));
            linesTable.Columns.Add("unitPrice", typeof(decimal));

            int nextLineId = PurchaseOrderLine.getNextPurchaseOrderLineId();
            List<int> lineIds = new List<int>();
            foreach (PurchaseOrderLineInput line in lines)
            {
                linesTable.Rows.Add(nextLineId, line.Description, line.UnitOfMeasure, line.Quantity, line.UnitPrice);
                lineIds.Add(nextLineId);
                nextLineId++;
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_create_flow @purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @orderDate, @lines, @result_status OUTPUT, @result_total_amount OUTPUT, @result_vat_amount OUTPUT";
            cmd.Parameters.AddWithValue("@purchase_order_id", purchaseOrderId);
            cmd.Parameters.AddWithValue("@poNumber", poNumber);
            cmd.Parameters.AddWithValue("@supplier_id", supplier.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@project_id", project.getProjectId());
            cmd.Parameters.AddWithValue("@created_by_employee_id", this.employeeId);
            cmd.Parameters.AddWithValue("@orderDate", orderDate);

            SqlParameter linesParam = cmd.Parameters.AddWithValue("@lines", linesTable);
            linesParam.SqlDbType = SqlDbType.Structured;
            linesParam.TypeName = "dbo.PurchaseOrderLineTableType";

            SqlParameter statusParam = cmd.Parameters.Add("@result_status", SqlDbType.NVarChar, 30);
            statusParam.Direction = ParameterDirection.Output;
            SqlParameter totalParam = cmd.Parameters.Add("@result_total_amount", SqlDbType.Decimal);
            totalParam.Precision = 18; totalParam.Scale = 2;
            totalParam.Direction = ParameterDirection.Output;
            SqlParameter vatParam = cmd.Parameters.Add("@result_vat_amount", SqlDbType.Decimal);
            vatParam.Precision = 18; vatParam.Scale = 2;
            vatParam.Direction = ParameterDirection.Output;

            SQL_CON SC = new SQL_CON();
            try
            {
                SC.execute_non_query_throwing(cmd);
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(translateCreatePurchaseOrderError(ex.Message));
            }

            POStatus resultStatus = (POStatus)Enum.Parse(typeof(POStatus), (string)statusParam.Value);
            decimal resultTotal = (decimal)totalParam.Value;
            decimal resultVat = (decimal)vatParam.Value;

            // is_new: false everywhere below -- the procedure already inserted both
            // tables; constructing with is_new: true would call createPurchaseOrder()/
            // createPurchaseOrderLine() again and try to INSERT the same rows twice.
            PurchaseOrder po = new PurchaseOrder(purchaseOrderId, poNumber, supplier, project, this, null, null,
                orderDate, resultTotal, resultVat, resultStatus, null, null, null, null, false);
            Program.PurchaseOrders.Add(po);

            for (int i = 0; i < lines.Count; i++)
            {
                PurchaseOrderLineInput input = lines[i];
                PurchaseOrderLine line = new PurchaseOrderLine(lineIds[i], po, input.Description, input.UnitOfMeasure,
                    input.Quantity, input.UnitPrice, 0, false);
                Program.PurchaseOrderLines.Add(line);
            }

            return po;
        }

        // Maps sp_purchase_order_create_flow's (English, developer-facing) RAISERROR
        // text to the Hebrew message PurchaseOrderPanel shows the Accountant. Falls
        // back to a generic message for anything unrecognized (e.g. a real
        // connectivity/SQL error, not a guard failure).
        private static string translateCreatePurchaseOrderError(string sqlMessage)
        {
            if (sqlMessage.Contains("is not Active"))
                return "לא ניתן ליצור הזמנת רכש: הספק שנבחר אינו פעיל";
            if (sqlMessage.Contains("supplier") && sqlMessage.Contains("does not exist"))
                return "לא ניתן ליצור הזמנת רכש: הספק שנבחר אינו קיים במערכת";
            if (sqlMessage.Contains("project") && sqlMessage.Contains("does not exist"))
                return "לא ניתן ליצור הזמנת רכש: הפרויקט שנבחר אינו קיים במערכת";
            if (sqlMessage.Contains("has no BudgetLine"))
                return "לא ניתן ליצור הזמנת רכש: לפרויקט שנבחר אין שורת תקציב מוגדרת";
            if (sqlMessage.Contains("no line items"))
                return "לא ניתן ליצור הזמנת רכש: יש להוסיף לפחות שורת פריט אחת";
            if (sqlMessage.Contains("quantity > 0"))
                return "לא ניתן ליצור הזמנת רכש: בכל שורה הכמות חייבת להיות חיובית והמחיר לא יכול להיות שלילי";
            return "לא ניתן ליצור הזמנת רכש: " + sqlMessage;
        }
    }
}

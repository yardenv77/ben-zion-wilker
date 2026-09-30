using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Abstract base for BankGuarantee / InsurancePolicy (table-per-subclass).
    // Never instantiated directly -- always through a BankGuarantee or InsurancePolicy constructor.
    public abstract class FinancialSecurity
    {
        protected int financialSecurityId;
        protected Project project;
        protected decimal amount;
        protected DateTime issueDate;
        protected DateTime expiryDate;
        protected SecurityStatus status;

        protected FinancialSecurity(int financialSecurityId, Project project, decimal amount, DateTime issueDate, DateTime expiryDate, SecurityStatus status)
        {
            this.financialSecurityId = financialSecurityId;
            this.project = project;
            this.amount = amount;
            this.issueDate = issueDate;
            this.expiryDate = expiryDate;
            this.status = status;
        }

        public int getFinancialSecurityId() { return this.financialSecurityId; }
        public Project getProject() { return this.project; }
        public decimal getAmount() { return this.amount; }
        public DateTime getIssueDate() { return this.issueDate; }
        public DateTime getExpiryDate() { return this.expiryDate; }
        public SecurityStatus getStatus() { return this.status; }

        public void setProject(Project project) { this.project = project; }
        public void setAmount(decimal amount) { this.amount = amount; }
        public void setIssueDate(DateTime issueDate) { this.issueDate = issueDate; }
        public void setExpiryDate(DateTime expiryDate) { this.expiryDate = expiryDate; }
        public void setStatus(SecurityStatus status) { this.status = status; }

        // Writes only the FinancialSecurity (parent-table) row. BankGuarantee/InsurancePolicy
        // call this plus their own subclass-table SP when overriding create/update/delete.
        protected void createFinancialSecurity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_financial_security_create @financial_security_id, @project_id, @amount, @issueDate, @expiryDate, @status";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@issueDate", this.issueDate);
            cmd.Parameters.AddWithValue("@expiryDate", this.expiryDate);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        protected void updateFinancialSecurity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_financial_security_update @financial_security_id, @project_id, @amount, @issueDate, @expiryDate, @status";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@issueDate", this.issueDate);
            cmd.Parameters.AddWithValue("@expiryDate", this.expiryDate);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        protected void deleteFinancialSecurity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_financial_security_delete @financial_security_id";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        // Loads FinancialSecurity + BankGuarantee + InsurancePolicy via three basic CRUD
        // calls (no combined SP exists) and instantiates the correct concrete subtype
        // per row, per CLAUDE.md's table-per-subclass pattern.
        public static void initFinancialSecurities()
        {
            SqlCommand baseCmd = new SqlCommand();
            baseCmd.CommandText = "EXECUTE sp_financial_security_get_all";
            SQL_CON baseSC = new SQL_CON();
            SqlDataReader baseRdr = baseSC.execute_query(baseCmd);

            var baseRows = new List<(int id, Project project, decimal amount, DateTime issueDate, DateTime expiryDate, SecurityStatus status)>();
            while (baseRdr.Read())
            {
                baseRows.Add((
                    int.Parse(baseRdr.GetValue(0).ToString()),
                    Project.seekProject(int.Parse(baseRdr.GetValue(1).ToString())),
                    decimal.Parse(baseRdr.GetValue(2).ToString()),
                    DateTime.Parse(baseRdr.GetValue(3).ToString()),
                    DateTime.Parse(baseRdr.GetValue(4).ToString()),
                    (SecurityStatus)Enum.Parse(typeof(SecurityStatus), baseRdr.GetValue(5).ToString())
                ));
            }

            SqlCommand bgCmd = new SqlCommand();
            bgCmd.CommandText = "EXECUTE sp_bank_guarantee_get_all";
            SQL_CON bgSC = new SQL_CON();
            SqlDataReader bgRdr = bgSC.execute_query(bgCmd);
            var bankGuaranteeRows = new Dictionary<int, (string bankName, string guaranteeNumber)>();
            while (bgRdr.Read())
            {
                int id = int.Parse(bgRdr.GetValue(0).ToString());
                bankGuaranteeRows[id] = (bgRdr.GetValue(1).ToString(), bgRdr.GetValue(2).ToString());
            }

            SqlCommand ipCmd = new SqlCommand();
            ipCmd.CommandText = "EXECUTE sp_insurance_policy_get_all";
            SQL_CON ipSC = new SQL_CON();
            SqlDataReader ipRdr = ipSC.execute_query(ipCmd);
            var insurancePolicyRows = new Dictionary<int, (string insurerName, string policyNumber, string coverageType)>();
            while (ipRdr.Read())
            {
                int id = int.Parse(ipRdr.GetValue(0).ToString());
                insurancePolicyRows[id] = (ipRdr.GetValue(1).ToString(), ipRdr.GetValue(2).ToString(), ipRdr.GetValue(3).ToString());
            }

            Program.FinancialSecurities = new List<FinancialSecurity>();

            foreach (var row in baseRows)
            {
                FinancialSecurity fs = null;
                if (bankGuaranteeRows.ContainsKey(row.id))
                {
                    var bg = bankGuaranteeRows[row.id];
                    fs = new BankGuarantee(row.id, row.project, row.amount, row.issueDate, row.expiryDate, row.status,
                        bg.bankName, bg.guaranteeNumber, false);
                }
                else if (insurancePolicyRows.ContainsKey(row.id))
                {
                    var ip = insurancePolicyRows[row.id];
                    fs = new InsurancePolicy(row.id, row.project, row.amount, row.issueDate, row.expiryDate, row.status,
                        ip.insurerName, ip.policyNumber, ip.coverageType, false);
                }

                if (fs != null)
                    Program.FinancialSecurities.Add(fs);
            }
        }

        public static FinancialSecurity seekFinancialSecurity(int id)
        {
            foreach (FinancialSecurity fs in Program.FinancialSecurities)
            {
                if (fs.getFinancialSecurityId() == id)
                    return fs;
            }
            return null;
        }

        public static int getNextFinancialSecurityId()
        {
            int maxId = 0;
            foreach (FinancialSecurity fs in Program.FinancialSecurities)
            {
                if (fs.getFinancialSecurityId() > maxId)
                    maxId = fs.getFinancialSecurityId();
            }
            return maxId + 1;
        }
    }
}

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

        // UC-06 (docs/00e-use-cases.md) + Part 1 problem #3: the 30-day pre-expiry alert the
        // business doesn't have today. getRemainingDays()/isExpiringSoon() match the methods
        // already named on FinancialSecurity in design/class-diagram.md -- declared here, on
        // the shared base, since both BankGuarantee and InsurancePolicy need identical logic.
        public int getRemainingDays() { return (int)(this.expiryDate.Date - DateTime.Now.Date).TotalDays; }

        // Deliberately checks the DATE, not `status == Active` -- nothing in this system
        // auto-flips status to Expired as time passes (no scheduled job), so a stale "Active"
        // record whose real-world expiryDate has already passed is exactly the silent-lapse
        // scenario this alert exists to catch (Part 1 problem #3). Released is the one status
        // trusted here, because it's always a deliberate Finance Officer action (closing the
        // security out), not something that silently goes stale like Expired does.
        public bool isExpiringSoon()
        {
            if (this.status == SecurityStatus.Released) return false;
            int remaining = getRemainingDays();
            return remaining >= 0 && remaining <= 30;
        }

        public void setProject(Project project) { this.project = project; }
        public void setAmount(decimal amount) { this.amount = amount; }
        public void setIssueDate(DateTime issueDate) { this.issueDate = issueDate; }
        public void setExpiryDate(DateTime expiryDate) { this.expiryDate = expiryDate; }
        public void setStatus(SecurityStatus status) { this.status = status; }

        // Builds (without executing) the FinancialSecurity (parent-table) command.
        // BankGuarantee/InsurancePolicy combine this with their own subclass-table command
        // and run both in one transaction via SQL_CON.execute_non_query_transactional.
        protected SqlCommand createFinancialSecurity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_financial_security_create @financial_security_id, @project_id, @amount, @issueDate, @expiryDate, @status";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@issueDate", this.issueDate);
            cmd.Parameters.AddWithValue("@expiryDate", this.expiryDate);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            return cmd;
        }

        protected SqlCommand updateFinancialSecurity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_financial_security_update @financial_security_id, @project_id, @amount, @issueDate, @expiryDate, @status";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@issueDate", this.issueDate);
            cmd.Parameters.AddWithValue("@expiryDate", this.expiryDate);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            return cmd;
        }

        protected SqlCommand deleteFinancialSecurity()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_financial_security_delete @financial_security_id";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            return cmd;
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

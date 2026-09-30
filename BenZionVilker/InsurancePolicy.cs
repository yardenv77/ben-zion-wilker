using System;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class InsurancePolicy : FinancialSecurity
    {
        private string insurerName;
        private string policyNumber;
        private string coverageType;

        public InsurancePolicy(int financialSecurityId, Project project, decimal amount, DateTime issueDate, DateTime expiryDate, SecurityStatus status,
            string insurerName, string policyNumber, string coverageType, bool is_new)
            : base(financialSecurityId, project, amount, issueDate, expiryDate, status)
        {
            this.insurerName = insurerName;
            this.policyNumber = policyNumber;
            this.coverageType = coverageType;
            if (is_new)
            {
                this.createInsurancePolicy();
                Program.FinancialSecurities.Add(this);
            }
        }

        public string getInsurerName() { return this.insurerName; }
        public string getPolicyNumber() { return this.policyNumber; }
        public string getCoverageType() { return this.coverageType; }

        public void setInsurerName(string insurerName) { this.insurerName = insurerName; }
        public void setPolicyNumber(string policyNumber) { this.policyNumber = policyNumber; }
        public void setCoverageType(string coverageType) { this.coverageType = coverageType; }

        public void createInsurancePolicy()
        {
            this.createFinancialSecurity();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_insurance_policy_create @financial_security_id, @insurerName, @policyNumber, @coverageType";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@insurerName", this.insurerName);
            cmd.Parameters.AddWithValue("@policyNumber", this.policyNumber);
            cmd.Parameters.AddWithValue("@coverageType", this.coverageType);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateInsurancePolicy()
        {
            this.updateFinancialSecurity();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_insurance_policy_update @financial_security_id, @insurerName, @policyNumber, @coverageType";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@insurerName", this.insurerName);
            cmd.Parameters.AddWithValue("@policyNumber", this.policyNumber);
            cmd.Parameters.AddWithValue("@coverageType", this.coverageType);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        // Child row must be deleted before the parent row (FK has no cascade).
        public void deleteInsurancePolicy()
        {
            Program.FinancialSecurities.Remove(this);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_insurance_policy_delete @financial_security_id";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.deleteFinancialSecurity();
        }
    }
}

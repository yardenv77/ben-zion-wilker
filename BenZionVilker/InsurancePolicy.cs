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
                if (this.createInsurancePolicy())
                    Program.FinancialSecurities.Add(this);
            }
        }

        public string getInsurerName() { return this.insurerName; }
        public string getPolicyNumber() { return this.policyNumber; }
        public string getCoverageType() { return this.coverageType; }

        public void setInsurerName(string insurerName) { this.insurerName = insurerName; }
        public void setPolicyNumber(string policyNumber) { this.policyNumber = policyNumber; }
        public void setCoverageType(string coverageType) { this.coverageType = coverageType; }

        public bool createInsurancePolicy()
        {
            SqlCommand parentCmd = this.createFinancialSecurity();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_insurance_policy_create @financial_security_id, @insurerName, @policyNumber, @coverageType";
            childCmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            childCmd.Parameters.AddWithValue("@insurerName", this.insurerName);
            childCmd.Parameters.AddWithValue("@policyNumber", this.policyNumber);
            childCmd.Parameters.AddWithValue("@coverageType", this.coverageType);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        public bool updateInsurancePolicy()
        {
            SqlCommand parentCmd = this.updateFinancialSecurity();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_insurance_policy_update @financial_security_id, @insurerName, @policyNumber, @coverageType";
            childCmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            childCmd.Parameters.AddWithValue("@insurerName", this.insurerName);
            childCmd.Parameters.AddWithValue("@policyNumber", this.policyNumber);
            childCmd.Parameters.AddWithValue("@coverageType", this.coverageType);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        // Child row deleted before the parent row (FK has no cascade), both in one transaction.
        public bool deleteInsurancePolicy()
        {
            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_insurance_policy_delete @financial_security_id";
            childCmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);

            SqlCommand parentCmd = this.deleteFinancialSecurity();

            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query_transactional(childCmd, parentCmd);
            if (success)
                Program.FinancialSecurities.Remove(this);
            return success;
        }
    }
}

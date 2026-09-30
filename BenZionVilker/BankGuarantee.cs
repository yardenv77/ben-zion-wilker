using System;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class BankGuarantee : FinancialSecurity
    {
        private string bankName;
        private string guaranteeNumber;

        public BankGuarantee(int financialSecurityId, Project project, decimal amount, DateTime issueDate, DateTime expiryDate, SecurityStatus status,
            string bankName, string guaranteeNumber, bool is_new)
            : base(financialSecurityId, project, amount, issueDate, expiryDate, status)
        {
            this.bankName = bankName;
            this.guaranteeNumber = guaranteeNumber;
            if (is_new)
            {
                this.createBankGuarantee();
                Program.FinancialSecurities.Add(this);
            }
        }

        public string getBankName() { return this.bankName; }
        public string getGuaranteeNumber() { return this.guaranteeNumber; }

        public void setBankName(string bankName) { this.bankName = bankName; }
        public void setGuaranteeNumber(string guaranteeNumber) { this.guaranteeNumber = guaranteeNumber; }

        public void createBankGuarantee()
        {
            this.createFinancialSecurity();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_bank_guarantee_create @financial_security_id, @bankName, @guaranteeNumber";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@bankName", this.bankName);
            cmd.Parameters.AddWithValue("@guaranteeNumber", this.guaranteeNumber);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateBankGuarantee()
        {
            this.updateFinancialSecurity();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_bank_guarantee_update @financial_security_id, @bankName, @guaranteeNumber";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            cmd.Parameters.AddWithValue("@bankName", this.bankName);
            cmd.Parameters.AddWithValue("@guaranteeNumber", this.guaranteeNumber);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        // Child row must be deleted before the parent row (FK has no cascade).
        public void deleteBankGuarantee()
        {
            Program.FinancialSecurities.Remove(this);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_bank_guarantee_delete @financial_security_id";
            cmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.deleteFinancialSecurity();
        }
    }
}

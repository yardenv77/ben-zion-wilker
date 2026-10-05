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
                if (this.createBankGuarantee())
                    Program.FinancialSecurities.Add(this);
            }
        }

        public string getBankName() { return this.bankName; }
        public string getGuaranteeNumber() { return this.guaranteeNumber; }

        // The client can still call (forfeit) the guarantee while it is active and not yet expired
        public bool isCallable() { return this.getStatus() == SecurityStatus.Active && this.getRemainingDays() >= 0; }

        // Extends the guarantee by one year from its current expiry date. A released guarantee cannot be renewed.
        public bool renew()
        {
            if (this.getStatus() == SecurityStatus.Released) return false;
            DateTime oldExpiry = this.getExpiryDate();
            SecurityStatus oldStatus = this.getStatus();
            this.setExpiryDate(oldExpiry.AddYears(1));
            this.setStatus(SecurityStatus.Active);
            if (this.updateBankGuarantee()) return true;
            this.setExpiryDate(oldExpiry);
            this.setStatus(oldStatus);
            return false;
        }

        public void setBankName(string bankName) { this.bankName = bankName; }
        public void setGuaranteeNumber(string guaranteeNumber) { this.guaranteeNumber = guaranteeNumber; }

        public bool createBankGuarantee()
        {
            SqlCommand parentCmd = this.createFinancialSecurity();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_bank_guarantee_create @financial_security_id, @bankName, @guaranteeNumber";
            childCmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            childCmd.Parameters.AddWithValue("@bankName", this.bankName);
            childCmd.Parameters.AddWithValue("@guaranteeNumber", this.guaranteeNumber);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        public bool updateBankGuarantee()
        {
            SqlCommand parentCmd = this.updateFinancialSecurity();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_bank_guarantee_update @financial_security_id, @bankName, @guaranteeNumber";
            childCmd.Parameters.AddWithValue("@financial_security_id", this.financialSecurityId);
            childCmd.Parameters.AddWithValue("@bankName", this.bankName);
            childCmd.Parameters.AddWithValue("@guaranteeNumber", this.guaranteeNumber);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        // Child row deleted before the parent row (FK has no cascade), both in one transaction.
        public bool deleteBankGuarantee()
        {
            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_bank_guarantee_delete @financial_security_id";
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

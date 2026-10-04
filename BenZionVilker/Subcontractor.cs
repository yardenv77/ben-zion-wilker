using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class Subcontractor : BusinessPartner
    {
        private string tradeSpecialty;
        private decimal dailyRate;

        public Subcontractor(int businessPartnerId, string name, string companyRegistrationNo, string contactPerson,
            string phone, string email, double rating, PartnerStatus status,
            string tradeSpecialty, decimal dailyRate, bool is_new)
            : base(businessPartnerId, name, companyRegistrationNo, contactPerson, phone, email, rating, status)
        {
            this.tradeSpecialty = tradeSpecialty;
            this.dailyRate = dailyRate;
            if (is_new)
            {
                if (this.createSubcontractor())
                    Program.BusinessPartners.Add(this);
            }
        }

        public string getTradeSpecialty() { return this.tradeSpecialty; }
        public decimal getDailyRate() { return this.dailyRate; }

        public void setTradeSpecialty(string tradeSpecialty) { this.tradeSpecialty = tradeSpecialty; }
        public void setDailyRate(decimal dailyRate) { this.dailyRate = dailyRate; }

        // Writes both the BusinessPartner (parent) row and the Subcontractor (child) row,
        // in one transaction -- either both are written, or neither is (no orphaned parent row).
        public bool createSubcontractor()
        {
            SqlCommand parentCmd = this.createBusinessPartner();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_subcontractor_create @business_partner_id, @tradeSpecialty, @dailyRate";
            childCmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            childCmd.Parameters.AddWithValue("@tradeSpecialty", this.tradeSpecialty);
            childCmd.Parameters.AddWithValue("@dailyRate", this.dailyRate);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        public bool updateSubcontractor()
        {
            SqlCommand parentCmd = this.updateBusinessPartner();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_subcontractor_update @business_partner_id, @tradeSpecialty, @dailyRate";
            childCmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            childCmd.Parameters.AddWithValue("@tradeSpecialty", this.tradeSpecialty);
            childCmd.Parameters.AddWithValue("@dailyRate", this.dailyRate);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        // Child row deleted before the parent row (FK has no cascade), both in one transaction.
        public bool deleteSubcontractor()
        {
            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_subcontractor_delete @business_partner_id";
            childCmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);

            SqlCommand parentCmd = this.deleteBusinessPartner();

            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query_transactional(childCmd, parentCmd);
            if (success)
                Program.BusinessPartners.Remove(this);
            return success;
        }
    }
}

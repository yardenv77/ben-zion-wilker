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
                this.createSubcontractor();
                Program.BusinessPartners.Add(this);
            }
        }

        public string getTradeSpecialty() { return this.tradeSpecialty; }
        public decimal getDailyRate() { return this.dailyRate; }

        public void setTradeSpecialty(string tradeSpecialty) { this.tradeSpecialty = tradeSpecialty; }
        public void setDailyRate(decimal dailyRate) { this.dailyRate = dailyRate; }

        // Writes both the BusinessPartner (parent) row and the Subcontractor (child) row.
        public void createSubcontractor()
        {
            this.createBusinessPartner();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_subcontractor_create @business_partner_id, @tradeSpecialty, @dailyRate";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            cmd.Parameters.AddWithValue("@tradeSpecialty", this.tradeSpecialty);
            cmd.Parameters.AddWithValue("@dailyRate", this.dailyRate);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSubcontractor()
        {
            this.updateBusinessPartner();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_subcontractor_update @business_partner_id, @tradeSpecialty, @dailyRate";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            cmd.Parameters.AddWithValue("@tradeSpecialty", this.tradeSpecialty);
            cmd.Parameters.AddWithValue("@dailyRate", this.dailyRate);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        // Child row must be deleted before the parent row (FK has no cascade).
        public void deleteSubcontractor()
        {
            Program.BusinessPartners.Remove(this);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_subcontractor_delete @business_partner_id";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.deleteBusinessPartner();
        }
    }
}

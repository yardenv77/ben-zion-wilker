using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Supplier extends BusinessPartner -- no attributes of its own in the class diagram.
    public class Supplier : BusinessPartner
    {
        public Supplier(int businessPartnerId, string name, string companyRegistrationNo, string contactPerson,
            string phone, string email, double rating, PartnerStatus status, bool is_new)
            : base(businessPartnerId, name, companyRegistrationNo, contactPerson, phone, email, rating, status)
        {
            if (is_new)
            {
                this.createSupplier();
                Program.BusinessPartners.Add(this);
            }
        }

        // Writes both the BusinessPartner (parent) row and the Supplier (child) row.
        public void createSupplier()
        {
            this.createBusinessPartner();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_create @business_partner_id";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSupplier()
        {
            this.updateBusinessPartner();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_update @business_partner_id";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        // Child row must be deleted before the parent row (FK has no cascade).
        public void deleteSupplier()
        {
            Program.BusinessPartners.Remove(this);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_delete @business_partner_id";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.deleteBusinessPartner();
        }
    }
}

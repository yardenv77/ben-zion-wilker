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
                if (this.createSupplier())
                    Program.BusinessPartners.Add(this);
            }
        }

        // Writes both the BusinessPartner (parent) row and the Supplier (child) row,
        // in one transaction -- either both are written, or neither is (no orphaned parent row).
        public bool createSupplier()
        {
            SqlCommand parentCmd = this.createBusinessPartner();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_supplier_create @business_partner_id";
            childCmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        public bool updateSupplier()
        {
            SqlCommand parentCmd = this.updateBusinessPartner();

            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_supplier_update @business_partner_id";
            childCmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);

            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query_transactional(parentCmd, childCmd);
        }

        // Child row deleted before the parent row (FK has no cascade), both in one transaction.
        public bool deleteSupplier()
        {
            SqlCommand childCmd = new SqlCommand();
            childCmd.CommandText = "EXECUTE sp_supplier_delete @business_partner_id";
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

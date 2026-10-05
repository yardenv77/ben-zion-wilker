using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Abstract base for Supplier / Subcontractor (table-per-subclass).
    // Never instantiated directly -- always through a Supplier or Subcontractor constructor.
    public abstract class BusinessPartner
    {
        protected int businessPartnerId;
        protected string name;
        protected string companyRegistrationNo;
        protected string contactPerson;
        protected string phone;
        protected string email;
        protected double rating;
        protected PartnerStatus status;

        protected BusinessPartner(int businessPartnerId, string name, string companyRegistrationNo,
            string contactPerson, string phone, string email, double rating, PartnerStatus status)
        {
            this.businessPartnerId = businessPartnerId;
            this.name = name;
            this.companyRegistrationNo = companyRegistrationNo;
            this.contactPerson = contactPerson;
            this.phone = phone;
            this.email = email;
            this.rating = rating;
            this.status = status;
        }

        public int getBusinessPartnerId() { return this.businessPartnerId; }
        public string getName() { return this.name; }
        public string getCompanyRegistrationNo() { return this.companyRegistrationNo; }
        public string getContactPerson() { return this.contactPerson; }
        public string getPhone() { return this.phone; }
        public string getEmail() { return this.email; }
        public double getRating() { return this.rating; }
        public PartnerStatus getStatus() { return this.status; }

        public bool isActive() { return this.status == PartnerStatus.Active; }

        // Payments to this partner (relationship #29 is on BusinessPartner, so it covers
        // suppliers and subcontractors alike), oldest due date first
        public List<SupplierPayment> getEngagementHistory()
        {
            List<SupplierPayment> history = new List<SupplierPayment>();
            foreach (SupplierPayment sp in Program.SupplierPayments)
                if (sp.getBusinessPartner() == this)
                    history.Add(sp);
            history.Sort((a, b) => a.getDueDate().CompareTo(b.getDueDate()));
            return history;
        }

        public void setName(string name) { this.name = name; }
        public void setCompanyRegistrationNo(string companyRegistrationNo) { this.companyRegistrationNo = companyRegistrationNo; }
        public void setContactPerson(string contactPerson) { this.contactPerson = contactPerson; }
        public void setPhone(string phone) { this.phone = phone; }
        public void setEmail(string email) { this.email = email; }
        public void setRating(double rating) { this.rating = rating; }
        public void setStatus(PartnerStatus status) { this.status = status; }

        // Builds (without executing) the BusinessPartner (parent-table) command. Supplier/
        // Subcontractor combine this with their own subclass-table command and run both
        // in one transaction via SQL_CON.execute_non_query_transactional -- see Supplier.cs.
        protected SqlCommand createBusinessPartner()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_business_partner_create @business_partner_id, @name, @companyRegistrationNo, @contactPerson, @phone, @email, @rating, @status";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            cmd.Parameters.AddWithValue("@name", this.name);
            cmd.Parameters.AddWithValue("@companyRegistrationNo", this.companyRegistrationNo);
            cmd.Parameters.AddWithValue("@contactPerson", this.contactPerson);
            cmd.Parameters.AddWithValue("@phone", this.phone);
            cmd.Parameters.AddWithValue("@email", this.email);
            cmd.Parameters.AddWithValue("@rating", this.rating);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            return cmd;
        }

        protected SqlCommand updateBusinessPartner()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_business_partner_update @business_partner_id, @name, @companyRegistrationNo, @contactPerson, @phone, @email, @rating, @status";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            cmd.Parameters.AddWithValue("@name", this.name);
            cmd.Parameters.AddWithValue("@companyRegistrationNo", this.companyRegistrationNo);
            cmd.Parameters.AddWithValue("@contactPerson", this.contactPerson);
            cmd.Parameters.AddWithValue("@phone", this.phone);
            cmd.Parameters.AddWithValue("@email", this.email);
            cmd.Parameters.AddWithValue("@rating", this.rating);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            return cmd;
        }

        protected SqlCommand deleteBusinessPartner()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_business_partner_delete @business_partner_id";
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartnerId);
            return cmd;
        }

        // Loads BusinessPartner + Supplier + Subcontractor via three basic CRUD calls
        // (no combined SP exists -- see stored_procedures.sql header) and instantiates
        // the correct concrete subtype per row, per CLAUDE.md's table-per-subclass pattern.
        public static void initBusinessPartners()
        {
            SqlCommand baseCmd = new SqlCommand();
            baseCmd.CommandText = "EXECUTE sp_business_partner_get_all";
            SQL_CON baseSC = new SQL_CON();
            SqlDataReader baseRdr = baseSC.execute_query(baseCmd);

            var baseRows = new List<(int id, string name, string companyRegistrationNo, string contactPerson,
                string phone, string email, double rating, PartnerStatus status)>();
            while (baseRdr.Read())
            {
                baseRows.Add((
                    int.Parse(baseRdr.GetValue(0).ToString()),
                    baseRdr.GetValue(1).ToString(),
                    baseRdr.GetValue(2).ToString(),
                    baseRdr.GetValue(3).ToString(),
                    baseRdr.GetValue(4).ToString(),
                    baseRdr.GetValue(5).ToString(),
                    double.Parse(baseRdr.GetValue(6).ToString()),
                    (PartnerStatus)Enum.Parse(typeof(PartnerStatus), baseRdr.GetValue(7).ToString())
                ));
            }

            SqlCommand supCmd = new SqlCommand();
            supCmd.CommandText = "EXECUTE sp_supplier_get_all";
            SQL_CON supSC = new SQL_CON();
            SqlDataReader supRdr = supSC.execute_query(supCmd);
            HashSet<int> supplierIds = new HashSet<int>();
            while (supRdr.Read())
                supplierIds.Add(int.Parse(supRdr.GetValue(0).ToString()));

            SqlCommand subCmd = new SqlCommand();
            subCmd.CommandText = "EXECUTE sp_subcontractor_get_all";
            SQL_CON subSC = new SQL_CON();
            SqlDataReader subRdr = subSC.execute_query(subCmd);
            var subcontractorRows = new Dictionary<int, (string tradeSpecialty, decimal dailyRate)>();
            while (subRdr.Read())
            {
                int id = int.Parse(subRdr.GetValue(0).ToString());
                subcontractorRows[id] = (subRdr.GetValue(1).ToString(), decimal.Parse(subRdr.GetValue(2).ToString()));
            }

            Program.BusinessPartners = new List<BusinessPartner>();

            foreach (var row in baseRows)
            {
                BusinessPartner bp = null;
                if (supplierIds.Contains(row.id))
                {
                    bp = new Supplier(row.id, row.name, row.companyRegistrationNo, row.contactPerson,
                        row.phone, row.email, row.rating, row.status, false);
                }
                else if (subcontractorRows.ContainsKey(row.id))
                {
                    var sub = subcontractorRows[row.id];
                    bp = new Subcontractor(row.id, row.name, row.companyRegistrationNo, row.contactPerson,
                        row.phone, row.email, row.rating, row.status, sub.tradeSpecialty, sub.dailyRate, false);
                }

                if (bp != null)
                    Program.BusinessPartners.Add(bp);
            }
        }

        public static BusinessPartner seekBusinessPartner(int id)
        {
            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (bp.getBusinessPartnerId() == id)
                    return bp;
            }
            return null;
        }

        public static int getNextBusinessPartnerId()
        {
            int maxId = 0;
            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (bp.getBusinessPartnerId() > maxId)
                    maxId = bp.getBusinessPartnerId();
            }
            return maxId + 1;
        }
    }
}

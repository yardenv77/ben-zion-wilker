using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class SupplierPayment
    {
        private int supplierPaymentId;
        private string invoiceNumber;
        // References the superclass BusinessPartner, not Supplier/Subcontractor directly
        // (design/class-diagram.md Section 5 model assumption).
        private BusinessPartner businessPartner;
        private decimal amount;
        private DateTime dueDate;
        private DateTime? paidDate; // nullable: not yet set while status is Pending/Overdue
        private SupplierPaymentStatus status;

        public SupplierPayment(int supplierPaymentId, string invoiceNumber, BusinessPartner businessPartner, decimal amount,
            DateTime dueDate, DateTime? paidDate, SupplierPaymentStatus status, bool is_new)
        {
            this.supplierPaymentId = supplierPaymentId;
            this.invoiceNumber = invoiceNumber;
            this.businessPartner = businessPartner;
            this.amount = amount;
            this.dueDate = dueDate;
            this.paidDate = paidDate;
            this.status = status;
            if (is_new)
            {
                this.createSupplierPayment();
                Program.SupplierPayments.Add(this);
            }
        }

        public int getSupplierPaymentId() { return this.supplierPaymentId; }
        public string getInvoiceNumber() { return this.invoiceNumber; }
        public BusinessPartner getBusinessPartner() { return this.businessPartner; }
        public decimal getAmount() { return this.amount; }
        public DateTime getDueDate() { return this.dueDate; }
        public DateTime? getPaidDate() { return this.paidDate; }
        public SupplierPaymentStatus getStatus() { return this.status; }

        public void setInvoiceNumber(string invoiceNumber) { this.invoiceNumber = invoiceNumber; }
        public void setBusinessPartner(BusinessPartner businessPartner) { this.businessPartner = businessPartner; }
        public void setAmount(decimal amount) { this.amount = amount; }
        public void setDueDate(DateTime dueDate) { this.dueDate = dueDate; }
        public void setPaidDate(DateTime? paidDate) { this.paidDate = paidDate; }
        public void setStatus(SupplierPaymentStatus status) { this.status = status; }

        public void createSupplierPayment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_payment_create @supplier_payment_id, @invoiceNumber, @business_partner_id, @amount, @dueDate, @paidDate, @status";
            cmd.Parameters.AddWithValue("@supplier_payment_id", this.supplierPaymentId);
            cmd.Parameters.AddWithValue("@invoiceNumber", this.invoiceNumber);
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartner.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@dueDate", this.dueDate);
            cmd.Parameters.AddWithValue("@paidDate", (object)this.paidDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSupplierPayment()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_payment_update @supplier_payment_id, @invoiceNumber, @business_partner_id, @amount, @dueDate, @paidDate, @status";
            cmd.Parameters.AddWithValue("@supplier_payment_id", this.supplierPaymentId);
            cmd.Parameters.AddWithValue("@invoiceNumber", this.invoiceNumber);
            cmd.Parameters.AddWithValue("@business_partner_id", this.businessPartner.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@dueDate", this.dueDate);
            cmd.Parameters.AddWithValue("@paidDate", (object)this.paidDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteSupplierPayment()
        {
            Program.SupplierPayments.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_payment_delete @supplier_payment_id";
            cmd.Parameters.AddWithValue("@supplier_payment_id", this.supplierPaymentId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initSupplierPayments()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_payment_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.SupplierPayments = new List<SupplierPayment>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string invoiceNumber = rdr.GetValue(1).ToString();
                BusinessPartner businessPartner = BusinessPartner.seekBusinessPartner(int.Parse(rdr.GetValue(2).ToString()));
                decimal amount = decimal.Parse(rdr.GetValue(3).ToString());
                DateTime dueDate = DateTime.Parse(rdr.GetValue(4).ToString());
                DateTime? paidDate = rdr.GetValue(5) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(5).ToString());
                SupplierPaymentStatus status = (SupplierPaymentStatus)Enum.Parse(typeof(SupplierPaymentStatus), rdr.GetValue(6).ToString());

                SupplierPayment sp = new SupplierPayment(id, invoiceNumber, businessPartner, amount, dueDate, paidDate, status, false);
                Program.SupplierPayments.Add(sp);
            }
        }

        public static SupplierPayment seekSupplierPayment(int id)
        {
            foreach (SupplierPayment sp in Program.SupplierPayments)
            {
                if (sp.getSupplierPaymentId() == id)
                    return sp;
            }
            return null;
        }

        public static int getNextSupplierPaymentId()
        {
            int maxId = 0;
            foreach (SupplierPayment sp in Program.SupplierPayments)
            {
                if (sp.getSupplierPaymentId() > maxId)
                    maxId = sp.getSupplierPaymentId();
            }
            return maxId + 1;
        }
    }
}

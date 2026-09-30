using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    // Ternary association class: connects Supplier + Tender + TradeCategory simultaneously
    // (design/class-diagram.md Section 4e). Same CRUD/load shape as a binary association
    // class, with one extra object-reference field (see PATTERNS.md "Association Class").
    public class SupplierPriceQuote
    {
        private int supplierPriceQuoteId;
        private Supplier supplier;
        private Tender tender;
        private TradeCategory tradeCategory;
        private decimal amount;
        private DateTime dateIssued;
        private DateTime validUntil;
        private bool isSelected;

        public SupplierPriceQuote(int supplierPriceQuoteId, Supplier supplier, Tender tender, TradeCategory tradeCategory,
            decimal amount, DateTime dateIssued, DateTime validUntil, bool isSelected, bool is_new)
        {
            this.supplierPriceQuoteId = supplierPriceQuoteId;
            this.supplier = supplier;
            this.tender = tender;
            this.tradeCategory = tradeCategory;
            this.amount = amount;
            this.dateIssued = dateIssued;
            this.validUntil = validUntil;
            this.isSelected = isSelected;
            if (is_new)
            {
                this.createSupplierPriceQuote();
                Program.SupplierPriceQuotes.Add(this);
            }
        }

        public int getSupplierPriceQuoteId() { return this.supplierPriceQuoteId; }
        public Supplier getSupplier() { return this.supplier; }
        public Tender getTender() { return this.tender; }
        public TradeCategory getTradeCategory() { return this.tradeCategory; }
        public decimal getAmount() { return this.amount; }
        public DateTime getDateIssued() { return this.dateIssued; }
        public DateTime getValidUntil() { return this.validUntil; }
        public bool getIsSelected() { return this.isSelected; }

        public void setSupplier(Supplier supplier) { this.supplier = supplier; }
        public void setTender(Tender tender) { this.tender = tender; }
        public void setTradeCategory(TradeCategory tradeCategory) { this.tradeCategory = tradeCategory; }
        public void setAmount(decimal amount) { this.amount = amount; }
        public void setDateIssued(DateTime dateIssued) { this.dateIssued = dateIssued; }
        public void setValidUntil(DateTime validUntil) { this.validUntil = validUntil; }
        public void setIsSelected(bool isSelected) { this.isSelected = isSelected; }

        public void createSupplierPriceQuote()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_price_quote_create @supplier_price_quote_id, @supplier_id, @tender_id, @trade_category_id, @amount, @dateIssued, @validUntil, @isSelected";
            cmd.Parameters.AddWithValue("@supplier_price_quote_id", this.supplierPriceQuoteId);
            cmd.Parameters.AddWithValue("@supplier_id", this.supplier.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@tender_id", this.tender.getTenderId());
            cmd.Parameters.AddWithValue("@trade_category_id", this.tradeCategory.getTradeCategoryId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@dateIssued", this.dateIssued);
            cmd.Parameters.AddWithValue("@validUntil", this.validUntil);
            cmd.Parameters.AddWithValue("@isSelected", this.isSelected);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSupplierPriceQuote()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_price_quote_update @supplier_price_quote_id, @supplier_id, @tender_id, @trade_category_id, @amount, @dateIssued, @validUntil, @isSelected";
            cmd.Parameters.AddWithValue("@supplier_price_quote_id", this.supplierPriceQuoteId);
            cmd.Parameters.AddWithValue("@supplier_id", this.supplier.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@tender_id", this.tender.getTenderId());
            cmd.Parameters.AddWithValue("@trade_category_id", this.tradeCategory.getTradeCategoryId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@dateIssued", this.dateIssued);
            cmd.Parameters.AddWithValue("@validUntil", this.validUntil);
            cmd.Parameters.AddWithValue("@isSelected", this.isSelected);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteSupplierPriceQuote()
        {
            Program.SupplierPriceQuotes.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_price_quote_delete @supplier_price_quote_id";
            cmd.Parameters.AddWithValue("@supplier_price_quote_id", this.supplierPriceQuoteId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initSupplierPriceQuotes()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_supplier_price_quote_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.SupplierPriceQuotes = new List<SupplierPriceQuote>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Supplier supplier = (Supplier)BusinessPartner.seekBusinessPartner(int.Parse(rdr.GetValue(1).ToString()));
                Tender tender = Tender.seekTender(int.Parse(rdr.GetValue(2).ToString()));
                TradeCategory tradeCategory = TradeCategory.seekTradeCategory(int.Parse(rdr.GetValue(3).ToString()));
                decimal amount = decimal.Parse(rdr.GetValue(4).ToString());
                DateTime dateIssued = DateTime.Parse(rdr.GetValue(5).ToString());
                DateTime validUntil = DateTime.Parse(rdr.GetValue(6).ToString());
                bool isSelected = Convert.ToBoolean(rdr.GetValue(7));

                SupplierPriceQuote spq = new SupplierPriceQuote(id, supplier, tender, tradeCategory, amount, dateIssued, validUntil, isSelected, false);
                Program.SupplierPriceQuotes.Add(spq);
            }
        }

        public static SupplierPriceQuote seekSupplierPriceQuote(int id)
        {
            foreach (SupplierPriceQuote spq in Program.SupplierPriceQuotes)
            {
                if (spq.getSupplierPriceQuoteId() == id)
                    return spq;
            }
            return null;
        }

        public static int getNextSupplierPriceQuoteId()
        {
            int maxId = 0;
            foreach (SupplierPriceQuote spq in Program.SupplierPriceQuotes)
            {
                if (spq.getSupplierPriceQuoteId() > maxId)
                    maxId = spq.getSupplierPriceQuoteId();
            }
            return maxId + 1;
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class PurchaseOrderLine
    {
        private int purchaseOrderLineId;
        private PurchaseOrder purchaseOrder;
        private string description;
        private string unitOfMeasure;
        private double quantity;
        private decimal unitPrice;
        private double receivedQuantity;

        public PurchaseOrderLine(int purchaseOrderLineId, PurchaseOrder purchaseOrder, string description,
            string unitOfMeasure, double quantity, decimal unitPrice, double receivedQuantity, bool is_new)
        {
            this.purchaseOrderLineId = purchaseOrderLineId;
            this.purchaseOrder = purchaseOrder;
            this.description = description;
            this.unitOfMeasure = unitOfMeasure;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
            this.receivedQuantity = receivedQuantity;
            if (is_new)
            {
                if (this.createPurchaseOrderLine())
                    Program.PurchaseOrderLines.Add(this);
            }
        }

        public int getPurchaseOrderLineId() { return this.purchaseOrderLineId; }
        public PurchaseOrder getPurchaseOrder() { return this.purchaseOrder; }
        public string getDescription() { return this.description; }
        public string getUnitOfMeasure() { return this.unitOfMeasure; }
        public double getQuantity() { return this.quantity; }
        public decimal getUnitPrice() { return this.unitPrice; }
        public double getReceivedQuantity() { return this.receivedQuantity; }
        public double getRemainingQuantity() { return this.quantity - this.receivedQuantity; }

        // quantity * unitPrice, before VAT -- summed by PurchaseOrder.calculateTotal()/calculateVat()
        public decimal getLineTotal() { return (decimal)this.quantity * this.unitPrice; }

        // A line with no price yet (0) still counts toward the order but contributes nothing to its total
        public bool isPriced() { return this.unitPrice > 0; }

        public void setPurchaseOrder(PurchaseOrder purchaseOrder) { this.purchaseOrder = purchaseOrder; }
        public void setDescription(string description) { this.description = description; }
        public void setUnitOfMeasure(string unitOfMeasure) { this.unitOfMeasure = unitOfMeasure; }
        public void setQuantity(double quantity) { this.quantity = quantity; }
        public void setUnitPrice(decimal unitPrice) { this.unitPrice = unitPrice; }
        public void setReceivedQuantity(double receivedQuantity) { this.receivedQuantity = receivedQuantity; }

        public bool createPurchaseOrderLine()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_line_create @purchase_order_line_id, @purchase_order_id, @description, @unitOfMeasure, @quantity, @unitPrice, @receivedQuantity";
            cmd.Parameters.AddWithValue("@purchase_order_line_id", this.purchaseOrderLineId);
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrder.getPurchaseOrderId());
            cmd.Parameters.AddWithValue("@description", this.description);
            cmd.Parameters.AddWithValue("@unitOfMeasure", this.unitOfMeasure);
            cmd.Parameters.AddWithValue("@quantity", this.quantity);
            cmd.Parameters.AddWithValue("@unitPrice", this.unitPrice);
            cmd.Parameters.AddWithValue("@receivedQuantity", this.receivedQuantity);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        // Deliberately does NOT touch receivedQuantity (step 7.4): it's state-machine-owned
        // by PurchaseOrder.receiveDelivery() (BR-5), never by this generic CRUD update.
        public bool updatePurchaseOrderLine()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_line_update @purchase_order_line_id, @purchase_order_id, @description, @unitOfMeasure, @quantity, @unitPrice";
            cmd.Parameters.AddWithValue("@purchase_order_line_id", this.purchaseOrderLineId);
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrder.getPurchaseOrderId());
            cmd.Parameters.AddWithValue("@description", this.description);
            cmd.Parameters.AddWithValue("@unitOfMeasure", this.unitOfMeasure);
            cmd.Parameters.AddWithValue("@quantity", this.quantity);
            cmd.Parameters.AddWithValue("@unitPrice", this.unitPrice);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deletePurchaseOrderLine()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_line_delete @purchase_order_line_id";
            cmd.Parameters.AddWithValue("@purchase_order_line_id", this.purchaseOrderLineId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.PurchaseOrderLines.Remove(this);
            return success;
        }

        public static void initPurchaseOrderLines()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_line_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.PurchaseOrderLines = new List<PurchaseOrderLine>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                PurchaseOrder purchaseOrder = PurchaseOrder.seekPurchaseOrder(int.Parse(rdr.GetValue(1).ToString()));
                string description = rdr.GetValue(2).ToString();
                string unitOfMeasure = rdr.GetValue(3).ToString();
                double quantity = double.Parse(rdr.GetValue(4).ToString());
                decimal unitPrice = decimal.Parse(rdr.GetValue(5).ToString());
                double receivedQuantity = double.Parse(rdr.GetValue(6).ToString());

                PurchaseOrderLine line = new PurchaseOrderLine(id, purchaseOrder, description, unitOfMeasure, quantity, unitPrice, receivedQuantity, false);
                Program.PurchaseOrderLines.Add(line);
            }
        }

        public static PurchaseOrderLine seekPurchaseOrderLine(int id)
        {
            foreach (PurchaseOrderLine line in Program.PurchaseOrderLines)
            {
                if (line.getPurchaseOrderLineId() == id)
                    return line;
            }
            return null;
        }

        public static int getNextPurchaseOrderLineId()
        {
            int maxId = 0;
            foreach (PurchaseOrderLine line in Program.PurchaseOrderLines)
            {
                if (line.getPurchaseOrderLineId() > maxId)
                    maxId = line.getPurchaseOrderLineId();
            }
            return maxId + 1;
        }
    }
}

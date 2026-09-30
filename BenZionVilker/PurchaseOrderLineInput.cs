namespace BenZionVilker
{
    // Not a domain entity -- no PK, no table, no DB methods of its own. It only
    // shapes the line items the Accountant is about to submit (UC-03 MSS step 5),
    // before a PurchaseOrder exists for them to reference and before they have
    // purchase_order_line_id values assigned. Employee.createPurchaseOrder()
    // consumes a list of these and turns each into a real PurchaseOrderLine.
    public class PurchaseOrderLineInput
    {
        public string Description;
        public string UnitOfMeasure;
        public double Quantity;
        public decimal UnitPrice;

        public PurchaseOrderLineInput(string description, string unitOfMeasure, double quantity, decimal unitPrice)
        {
            this.Description = description;
            this.UnitOfMeasure = unitOfMeasure;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
        }
    }
}

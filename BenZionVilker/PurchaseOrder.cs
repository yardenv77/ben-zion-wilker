using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class PurchaseOrder
    {
        private int purchaseOrderId;
        private string poNumber;
        private Supplier supplier;
        private Project project;
        private Employee createdBy;
        private Employee approvedBy; // nullable: not yet set until PM approval
        private Employee overrideApprovedBy; // nullable: only set when BR-2 required a CEO budget override
        private DateTime orderDate;
        private decimal totalAmount;
        private decimal vatAmount;
        private POStatus status;
        private string rejectionReason; // nullable: only set when status is/was Rejected
        private ClosureReason? closureReason; // nullable: only set on entry to Received/Cancelled
        private DateTime? rejectedAt; // nullable: BR-3 guard support, set on entry to Rejected, cleared by revise()
        private DateTime? archivedAt; // nullable: 7-year retention guard support, set on entry to Archived

        public PurchaseOrder(int purchaseOrderId, string poNumber, Supplier supplier, Project project,
            Employee createdBy, Employee approvedBy, Employee overrideApprovedBy, DateTime orderDate,
            decimal totalAmount, decimal vatAmount, POStatus status, string rejectionReason, ClosureReason? closureReason,
            DateTime? rejectedAt, DateTime? archivedAt, bool is_new)
        {
            this.purchaseOrderId = purchaseOrderId;
            this.poNumber = poNumber;
            this.supplier = supplier;
            this.project = project;
            this.createdBy = createdBy;
            this.approvedBy = approvedBy;
            this.overrideApprovedBy = overrideApprovedBy;
            this.orderDate = orderDate;
            this.totalAmount = totalAmount;
            this.vatAmount = vatAmount;
            this.status = status;
            this.rejectionReason = rejectionReason;
            this.closureReason = closureReason;
            this.rejectedAt = rejectedAt;
            this.archivedAt = archivedAt;
            if (is_new)
            {
                this.createPurchaseOrder();
                Program.PurchaseOrders.Add(this);
            }
        }

        public int getPurchaseOrderId() { return this.purchaseOrderId; }
        public string getPoNumber() { return this.poNumber; }
        public Supplier getSupplier() { return this.supplier; }
        public Project getProject() { return this.project; }
        public Employee getCreatedBy() { return this.createdBy; }
        public Employee getApprovedBy() { return this.approvedBy; }
        public Employee getOverrideApprovedBy() { return this.overrideApprovedBy; }
        public DateTime getOrderDate() { return this.orderDate; }
        public decimal getTotalAmount() { return this.totalAmount; }
        public decimal getVatAmount() { return this.vatAmount; }
        public POStatus getStatus() { return this.status; }
        public string getRejectionReason() { return this.rejectionReason; }
        public ClosureReason? getClosureReason() { return this.closureReason; }

        public void setPoNumber(string poNumber) { this.poNumber = poNumber; }
        public void setSupplier(Supplier supplier) { this.supplier = supplier; }
        public void setProject(Project project) { this.project = project; }
        public void setCreatedBy(Employee createdBy) { this.createdBy = createdBy; }
        // approvedBy/overrideApprovedBy are private setters (step 7.4): they record WHO
        // performed a specific transition (approve()/approveBudgetOverride()), so they may
        // only change as a side effect of that transition, never through generic CRUD update.
        private void setApprovedBy(Employee approvedBy) { this.approvedBy = approvedBy; }
        private void setOverrideApprovedBy(Employee overrideApprovedBy) { this.overrideApprovedBy = overrideApprovedBy; }
        public void setOrderDate(DateTime orderDate) { this.orderDate = orderDate; }
        public void setTotalAmount(decimal totalAmount) { this.totalAmount = totalAmount; }
        public void setVatAmount(decimal vatAmount) { this.vatAmount = vatAmount; }
        // status/rejectionReason/closureReason are private setters on purpose (step 7.4):
        // they may ONLY change through the guarded transition methods below, never through
        // updatePurchaseOrder() or any other generic-CRUD path.
        private void setStatus(POStatus status) { this.status = status; }
        private void setRejectionReason(string rejectionReason) { this.rejectionReason = rejectionReason; }
        private void setClosureReason(ClosureReason? closureReason) { this.closureReason = closureReason; }

        public void createPurchaseOrder()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_create @purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @approved_by_employee_id, @override_approved_by_employee_id, @orderDate, @totalAmount, @vatAmount, @status, @rejectionReason, @closureReason, @rejectedAt, @archivedAt";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@poNumber", this.poNumber);
            cmd.Parameters.AddWithValue("@supplier_id", this.supplier.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@created_by_employee_id", this.createdBy.getEmployeeId());
            cmd.Parameters.AddWithValue("@approved_by_employee_id", (object)this.approvedBy?.getEmployeeId() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@override_approved_by_employee_id", (object)this.overrideApprovedBy?.getEmployeeId() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@orderDate", this.orderDate);
            cmd.Parameters.AddWithValue("@totalAmount", this.totalAmount);
            cmd.Parameters.AddWithValue("@vatAmount", this.vatAmount);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            cmd.Parameters.AddWithValue("@rejectionReason", (object)this.rejectionReason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@closureReason", (object)this.closureReason?.ToString() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@rejectedAt", (object)this.rejectedAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@archivedAt", (object)this.archivedAt ?? DBNull.Value);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        // Deliberately does NOT touch status/rejectionReason/closureReason (step 7.4):
        // those three columns are state-machine-owned and change only through the
        // guarded transition methods below, each with its own dedicated procedure.
        public void updatePurchaseOrder()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_update @purchase_order_id, @poNumber, @supplier_id, @project_id, @created_by_employee_id, @orderDate, @totalAmount, @vatAmount";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@poNumber", this.poNumber);
            cmd.Parameters.AddWithValue("@supplier_id", this.supplier.getBusinessPartnerId());
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@created_by_employee_id", this.createdBy.getEmployeeId());
            cmd.Parameters.AddWithValue("@orderDate", this.orderDate);
            cmd.Parameters.AddWithValue("@totalAmount", this.totalAmount);
            cmd.Parameters.AddWithValue("@vatAmount", this.vatAmount);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deletePurchaseOrder()
        {
            Program.PurchaseOrders.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_delete @purchase_order_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initPurchaseOrders()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.PurchaseOrders = new List<PurchaseOrder>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string poNumber = rdr.GetValue(1).ToString();
                Supplier supplier = (Supplier)BusinessPartner.seekBusinessPartner(int.Parse(rdr.GetValue(2).ToString()));
                Project project = Project.seekProject(int.Parse(rdr.GetValue(3).ToString()));
                Employee createdBy = Employee.seekEmployee(int.Parse(rdr.GetValue(4).ToString()));
                Employee approvedBy = rdr.GetValue(5) == DBNull.Value ? null : Employee.seekEmployee(int.Parse(rdr.GetValue(5).ToString()));
                Employee overrideApprovedBy = rdr.GetValue(6) == DBNull.Value ? null : Employee.seekEmployee(int.Parse(rdr.GetValue(6).ToString()));
                DateTime orderDate = DateTime.Parse(rdr.GetValue(7).ToString());
                decimal totalAmount = decimal.Parse(rdr.GetValue(8).ToString());
                decimal vatAmount = decimal.Parse(rdr.GetValue(9).ToString());
                POStatus status = (POStatus)Enum.Parse(typeof(POStatus), rdr.GetValue(10).ToString());
                string rejectionReason = rdr.GetValue(11) == DBNull.Value ? null : rdr.GetValue(11).ToString();
                ClosureReason? closureReason = rdr.GetValue(12) == DBNull.Value ? (ClosureReason?)null : (ClosureReason)Enum.Parse(typeof(ClosureReason), rdr.GetValue(12).ToString());
                DateTime? rejectedAt = rdr.GetValue(13) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(13).ToString());
                DateTime? archivedAt = rdr.GetValue(14) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(14).ToString());

                PurchaseOrder po = new PurchaseOrder(id, poNumber, supplier, project, createdBy, approvedBy, overrideApprovedBy,
                    orderDate, totalAmount, vatAmount, status, rejectionReason, closureReason, rejectedAt, archivedAt, false);
                Program.PurchaseOrders.Add(po);
            }
        }

        public static PurchaseOrder seekPurchaseOrder(int id)
        {
            foreach (PurchaseOrder po in Program.PurchaseOrders)
            {
                if (po.getPurchaseOrderId() == id)
                    return po;
            }
            return null;
        }

        public static int getNextPurchaseOrderId()
        {
            int maxId = 0;
            foreach (PurchaseOrder po in Program.PurchaseOrders)
            {
                if (po.getPurchaseOrderId() > maxId)
                    maxId = po.getPurchaseOrderId();
            }
            return maxId + 1;
        }

        // ====================================================================
        // State transitions (course step 7.3, docs/design/state-diagram.md).
        // Status changes ONLY happen through these methods -- never through
        // updatePurchaseOrder(). Each guard failure throws with a Hebrew
        // message the panel shows via MessageBox (step 7.5).
        // ====================================================================

        // BR-1 guard: has this PO ever left Draft? In-memory only, per session -- unlike
        // rejectedAt/archivedAt above, this one is NOT persisted (flagged, not yet fixed).
        private bool everSubmitted = false;

        public bool exceedsBudget()
        {
            decimal remaining = 0;
            foreach (BudgetLine bl in Program.BudgetLines)
                if (bl.getProject() == this.project)
                    remaining += bl.getPlannedAmount() - bl.getActualAmount();
            return this.totalAmount > remaining;
        }

        public bool isFullyReceived()
        {
            bool hasLines = false;
            foreach (PurchaseOrderLine line in Program.PurchaseOrderLines)
            {
                if (line.getPurchaseOrder() != this) continue;
                hasLines = true;
                if (line.getRemainingQuantity() > 0) return false;
            }
            return hasLines;
        }

        // t2a/t2b: Draft -> PendingBudgetOverride or PendingPMApproval (BR-2)
        public void submit()
        {
            if (this.status != POStatus.Draft)
                throw new InvalidOperationException("ניתן לשלוח לאישור רק הזמנה שנמצאת בטיוטה");

            POStatus newStatus = this.exceedsBudget() ? POStatus.PendingBudgetOverride : POStatus.PendingPMApproval;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_submit @purchase_order_id, @new_status";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@new_status", newStatus.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.everSubmitted = true;
            this.status = newStatus;
        }

        // t3: UnderApproval (either sub-state) -> Draft
        public void withdraw()
        {
            if (this.status != POStatus.PendingPMApproval && this.status != POStatus.PendingBudgetOverride)
                throw new InvalidOperationException("ניתן למשוך רק הזמנה שממתינה לאישור");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_withdraw @purchase_order_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.status = POStatus.Draft;
        }

        // t4: UnderApproval (either sub-state) -> Rejected -- covers both a PM
        // rejection and a CEO rejection of the budget override (border transition)
        public void reject(string reason)
        {
            if (this.status != POStatus.PendingPMApproval && this.status != POStatus.PendingBudgetOverride)
                throw new InvalidOperationException("ניתן לדחות רק הזמנה שממתינה לאישור");
            if (string.IsNullOrWhiteSpace(reason))
                throw new InvalidOperationException("יש להזין סיבת דחייה");

            DateTime now = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_reject @purchase_order_id, @rejectionReason, @rejectedAt";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@rejectionReason", reason);
            cmd.Parameters.AddWithValue("@rejectedAt", now);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.status = POStatus.Rejected;
            this.rejectionReason = reason;
            this.rejectedAt = now;
        }

        // t5: Rejected -> Draft
        public void revise()
        {
            if (this.status != POStatus.Rejected)
                throw new InvalidOperationException("ניתן לתקן רק הזמנה שנדחתה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_revise @purchase_order_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.status = POStatus.Draft;
            this.rejectedAt = null;
        }

        // t6: Rejected -> Cancelled (BR-3). System-triggered -- no UI button (step 7.5).
        public void autoCancel()
        {
            if (this.status != POStatus.Rejected)
                throw new InvalidOperationException("ביטול אוטומטי חל רק על הזמנה שנדחתה");
            if (this.rejectedAt == null || (DateTime.Now - this.rejectedAt.Value).TotalDays < 14)
                throw new InvalidOperationException("טרם חלפו 14 יום מהדחייה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_auto_cancel @purchase_order_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.status = POStatus.Cancelled;
            this.closureReason = ClosureReason.Cancelled;
        }

        // t7/t8: Draft -> deleted (BR-1: never submitted) or Draft -> Cancelled (BR-1: previously submitted)
        public void cancel()
        {
            if (this.status != POStatus.Draft)
                throw new InvalidOperationException("ניתן לבטל במסך זה רק הזמנה שנמצאת בטיוטה");

            if (!this.everSubmitted)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EXECUTE sp_purchase_order_cancel_delete @purchase_order_id";
                cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
                SQL_CON SC = new SQL_CON();
                SC.execute_non_query(cmd);

                Program.PurchaseOrderLines.RemoveAll(l => l.getPurchaseOrder() == this);
                Program.PurchaseOrders.Remove(this);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "EXECUTE sp_purchase_order_cancel @purchase_order_id";
                cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
                SQL_CON SC = new SQL_CON();
                SC.execute_non_query(cmd);

                this.status = POStatus.Cancelled;
                this.closureReason = ClosureReason.Cancelled;
            }
        }

        // t10: PendingBudgetOverride -> PendingPMApproval. overrideApprovedBy {role=CEO}
        // records who approved the budget override -- a side effect of this transition,
        // not a free-form editable field (step 7.4).
        public void approveBudgetOverride(Employee ceo)
        {
            if (this.status != POStatus.PendingBudgetOverride)
                throw new InvalidOperationException("אין חריגת תקציב הממתינה לאישור מנכ\"ל");
            if (ceo == null)
                throw new InvalidOperationException("יש לבחור מי מאשר את חריגת התקציב");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_approve_budget_override @purchase_order_id, @override_approved_by_employee_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@override_approved_by_employee_id", ceo.getEmployeeId());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.overrideApprovedBy = ceo;
            this.status = POStatus.PendingPMApproval;
        }

        // t11: PendingPMApproval -> InFulfillment (enters Sent). approvedBy {role=ProjectManager}
        // records who approved -- a side effect of this transition (step 7.4).
        public void approve(Employee projectManager)
        {
            if (this.status != POStatus.PendingPMApproval)
                throw new InvalidOperationException("ניתן לאשר רק הזמנה שממתינה לאישור מנהל פרויקט");
            if (projectManager == null)
                throw new InvalidOperationException("יש לבחור מי מאשר את ההזמנה");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_approve @purchase_order_id, @approved_by_employee_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@approved_by_employee_id", projectManager.getEmployeeId());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.approvedBy = projectManager;
            this.status = POStatus.Sent;
        }

        // UC-03.Include: "Send Purchase Order Email to Supplier" (docs/00e-use-cases.md MSS
        // step 12, triggered right after approve()). Simulated rather than sent over real
        // SMTP -- the course's oral-exam guide lists a real external email send as optional
        // bonus content, not a baseline requirement -- but assembles the genuine notification
        // content (supplier contact, every line, computed totals) so the include-UC is
        // actually demonstrable rather than a no-op. Pure string-building, no UI: the caller
        // (PurchaseOrderPanel) decides how to show it, per this project's "entity classes
        // don't do UI" convention.
        public string composeSupplierEmail()
        {
            StringBuilder body = new StringBuilder();
            body.AppendLine("אל: " + this.supplier.getName() + " <" + this.supplier.getEmail() + ">");
            body.AppendLine("נושא: הזמנת רכש מס' " + this.poNumber + " אושרה");
            body.AppendLine();
            body.AppendLine("שלום,");
            body.AppendLine("הזמנת הרכש שלהלן אושרה במסגרת פרויקט \"" + this.project.getName() + "\" ונשלחת אליכם לביצוע:");
            body.AppendLine();

            foreach (PurchaseOrderLine line in Program.PurchaseOrderLines)
            {
                if (line.getPurchaseOrder() != this) continue;
                body.AppendLine("- " + line.getDescription() + " | כמות: " + line.getQuantity() + " " + line.getUnitOfMeasure()
                    + " | מחיר יחידה: " + line.getUnitPrice().ToString("N2") + " ש\"ח");
            }

            body.AppendLine();
            body.AppendLine("סכום לפני מע\"מ: " + (this.totalAmount - this.vatAmount).ToString("N2") + " ש\"ח");
            body.AppendLine("מע\"מ: " + this.vatAmount.ToString("N2") + " ש\"ח");
            body.AppendLine("סה\"כ לתשלום: " + this.totalAmount.ToString("N2") + " ש\"ח");
            body.AppendLine();
            body.AppendLine("בברכה,");
            body.AppendLine(this.createdBy.getFullName());

            return body.ToString();
        }

        // t14/t15/t16: Sent/PartiallyReceived -> PartiallyReceived or Received (BR-5)
        public void receiveDelivery(PurchaseOrderLine line, double qty)
        {
            if (this.status != POStatus.Sent && this.status != POStatus.PartiallyReceived)
                throw new InvalidOperationException("ניתן לרשום קבלה רק להזמנה שנשלחה");
            if (line == null || line.getPurchaseOrder() != this)
                throw new InvalidOperationException("השורה אינה שייכת להזמנה זו");
            if (qty <= 0 || qty > line.getRemainingQuantity())
                throw new InvalidOperationException("כמות הקבלה חייבת להיות חיובית ולא לחרוג מהכמות שנותרה בשורה");

            double newReceivedQuantity = line.getReceivedQuantity() + qty;
            bool willBeFullyReceived = true;
            foreach (PurchaseOrderLine l in Program.PurchaseOrderLines)
            {
                if (l.getPurchaseOrder() != this) continue;
                double effectiveReceived = (l == line) ? newReceivedQuantity : l.getReceivedQuantity();
                if (effectiveReceived < l.getQuantity()) { willBeFullyReceived = false; break; }
            }
            POStatus newStatus = willBeFullyReceived ? POStatus.Received : POStatus.PartiallyReceived;
            ClosureReason? newClosureReason = willBeFullyReceived ? ClosureReason.Received : (ClosureReason?)null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_receive_delivery @purchase_order_id, @purchase_order_line_id, @receivedQuantity, @new_status, @closureReason";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@purchase_order_line_id", line.getPurchaseOrderLineId());
            cmd.Parameters.AddWithValue("@receivedQuantity", newReceivedQuantity);
            cmd.Parameters.AddWithValue("@new_status", newStatus.ToString());
            cmd.Parameters.AddWithValue("@closureReason", (object)newClosureReason?.ToString() ?? DBNull.Value);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            line.setReceivedQuantity(newReceivedQuantity);
            this.status = newStatus;
            if (newClosureReason.HasValue) this.closureReason = newClosureReason;
        }

        // t17: InFulfillment (either sub-state) -> Cancelled (BR-4)
        public void cancelRemaining(bool supplierAgreed)
        {
            if (this.status != POStatus.Sent && this.status != POStatus.PartiallyReceived)
                throw new InvalidOperationException("ניתן לבטל יתרה רק להזמנה שנשלחה");
            if (!supplierAgreed)
                throw new InvalidOperationException("ביטול היתרה מחייב הסכמת הספק");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_cancel_remaining @purchase_order_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.status = POStatus.Cancelled;
            this.closureReason = ClosureReason.Cancelled;
        }

        // t18/t19: Received or Cancelled -> Archived
        public void archive()
        {
            if (this.status != POStatus.Received && this.status != POStatus.Cancelled)
                throw new InvalidOperationException("ניתן להעביר לארכיון רק הזמנה שהתקבלה או בוטלה");

            DateTime now = DateTime.Now;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_archive @purchase_order_id, @archivedAt";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            cmd.Parameters.AddWithValue("@archivedAt", now);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            this.status = POStatus.Archived;
            this.archivedAt = now;
        }

        // t20: Archived -> deleted. 7-year retention guard.
        public void purge()
        {
            if (this.status != POStatus.Archived)
                throw new InvalidOperationException("ניתן לגרוס רק הזמנה שבארכיון");
            if (this.archivedAt == null || (DateTime.Now - this.archivedAt.Value).TotalDays < 7 * 365)
                throw new InvalidOperationException("טרם חלפו 7 שנות השמירה הנדרשות");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_purchase_order_purge @purchase_order_id";
            cmd.Parameters.AddWithValue("@purchase_order_id", this.purchaseOrderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);

            Program.PurchaseOrderLines.RemoveAll(l => l.getPurchaseOrder() == this);
            Program.PurchaseOrders.Remove(this);
        }
    }
}

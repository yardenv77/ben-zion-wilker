using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך יצירת הזמנת רכש (UC-03) — רשימה + טופס יצירה/עדכון לשדות ה-CRUD (פרטי ההזמנה
    /// עצמה), וכפתורי פעולה אחד לכל מעבר-מצב שמופעל על ידי משתמש (שלב 7.5), במקום כפתור
    /// Update גנרי: submit/withdraw/reject/revise/cancel/approveBudgetOverride/approve/
    /// cancelRemaining/archive. autoCancel()/purge() מופעלים על ידי המערכת (BR-3, שמירת 7
    /// שנים) ולכן אין להם כפתור. comboBox_status/comboBox_closureReason נשארים תצוגה בלבד
    /// (Enabled=false) -- הם נגזרים מהמעברים, לא נבחרים ישירות. שורות הפריטים מנוהלות
    /// במסך נפרד (PurchaseOrderLinePanel).
    /// </summary>
    public partial class PurchaseOrderPanel : UserControl
    {
        private PurchaseOrder selectedPurchaseOrder;
        private const string NoneOption = "-- אין --";

        // Lines queued for the quick-create-with-lines flow below (UC-03, atomic
        // alternative). Not PurchaseOrderLine objects -- they don't exist yet, since
        // there is no PurchaseOrder for them to reference until the flow commits.
        private readonly List<PurchaseOrderLineInput> pendingQuickCreateLines = new List<PurchaseOrderLineInput>();

        public PurchaseOrderPanel()
        {
            InitializeComponent();
            applyTheme();

            foreach (BusinessPartner bp in Program.BusinessPartners)
            {
                if (bp is Supplier sup)
                    comboBox_supplier.Items.Add(sup.getBusinessPartnerId() + " - " + sup.getName());
            }
            foreach (Project proj in Program.Projects)
                comboBox_project.Items.Add(proj.getProjectId() + " - " + proj.getName());
            foreach (Employee emp in Program.Employees)
                comboBox_createdBy.Items.Add(emp.getEmployeeId() + " - " + emp.getFullName());

            comboBox_approvedBy.Items.Add(NoneOption);
            comboBox_overrideApprovedBy.Items.Add(NoneOption);
            foreach (Employee emp in Program.Employees)
            {
                comboBox_approvedBy.Items.Add(emp.getEmployeeId() + " - " + emp.getFullName());
                comboBox_overrideApprovedBy.Items.Add(emp.getEmployeeId() + " - " + emp.getFullName());
            }
            comboBox_approvedBy.SelectedIndex = 0;
            comboBox_overrideApprovedBy.SelectedIndex = 0;

            comboBox_closureReason.Items.Add(NoneOption);
            foreach (ClosureReason cr in Enum.GetValues(typeof(ClosureReason)))
                comboBox_closureReason.Items.Add(cr.ToString());

            // comboBox_status/comboBox_closureReason stay display-only (step 7.4) -- they're
            // derived from transitions, never chosen directly. comboBox_approvedBy/
            // comboBox_overrideApprovedBy/textBox_rejectionReason are now INPUTS for the
            // approve()/approveBudgetOverride()/reject() verb buttons below (step 7.5).
            comboBox_status.Enabled = false;
            comboBox_closureReason.Enabled = false;
            comboBox_closureReason.SelectedIndex = 0;

            foreach (POStatus s in Enum.GetValues(typeof(POStatus)))
                comboBox_status.Items.Add(EnumDisplay.Hebrew(s));
            comboBox_status.SelectedIndex = 0;

            loadPurchaseOrders();
            refreshPendingLinesGrid();
            refreshActionButtons();
        }

        // Course step 10.2 ("professional, not cluttered, easy for users less comfortable
        // with new technology") -- a PO's status allows only 1-3 of these 9 verb buttons at
        // any given moment (docs/design/state-diagram.md), so showing all 9 as equally
        // clickable forces the user to learn which ones actually work by trial and error.
        // Disabled rather than hidden: the full set of possible actions stays visible (so
        // nothing seems to vanish) and the layout never has to reflow -- only the buttons
        // valid for the selected order's current status stay enabled and colored.
        private void refreshActionButtons()
        {
            POStatus? status = selectedPurchaseOrder?.getStatus();

            button_updateDetails.Enabled = status == POStatus.Draft || status == POStatus.Rejected;
            button_delete.Enabled = status == POStatus.Draft;
            button_submit.Enabled = status == POStatus.Draft;
            button_cancel.Enabled = status == POStatus.Draft;
            button_withdraw.Enabled = status == POStatus.PendingPMApproval || status == POStatus.PendingBudgetOverride;
            button_reject.Enabled = status == POStatus.PendingPMApproval || status == POStatus.PendingBudgetOverride;
            button_approveBudgetOverride.Enabled = status == POStatus.PendingBudgetOverride;
            button_approve.Enabled = status == POStatus.PendingPMApproval;
            button_revise.Enabled = status == POStatus.Rejected;
            button_cancelRemaining.Enabled = status == POStatus.Sent || status == POStatus.PartiallyReceived;
            button_archive.Enabled = status == POStatus.Received || status == POStatus.Cancelled;
        }

        // Course step 10 ("polish") -- palette/typography from Theme.cs, applied once here
        // rather than duplicated per control in the Designer.cs. Buttons are grouped by
        // semantic role: primary = the two create actions, danger = reject/cancel/delete,
        // everything else secondary.
        private void applyTheme()
        {
            Theme.ApplyPanelBackground(this);
            Theme.ApplyTitle(label_title);
            Theme.CenterHorizontally(label_title, this.Width);
            Theme.ApplySectionLabel(label_quickCreateSection);

            foreach (Control c in this.Controls)
            {
                if (c is Label lbl && lbl != label_title && lbl != label_quickCreateSection)
                    Theme.ApplyFieldLabel(lbl, this);
                else if (c is TextBox tb)
                    Theme.ApplyTextBox(tb);
                else if (c is ComboBox cb)
                    Theme.ApplyComboBox(cb);
            }

            Theme.ApplyPrimaryButton(button_save);
            Theme.ApplyPrimaryButton(button_quickCreate);
            Theme.ApplyDangerButton(button_reject);
            Theme.ApplyDangerButton(button_cancel);
            Theme.ApplyDangerButton(button_delete);
            Theme.ApplySecondaryButton(button_updateDetails);
            Theme.ApplySecondaryButton(button_submit);
            Theme.ApplySecondaryButton(button_withdraw);
            Theme.ApplySecondaryButton(button_revise);
            Theme.ApplySecondaryButton(button_approveBudgetOverride);
            Theme.ApplySecondaryButton(button_approve);
            Theme.ApplySecondaryButton(button_cancelRemaining);
            Theme.ApplySecondaryButton(button_archive);
            Theme.ApplySecondaryButton(button_addLineToQueue);
            Theme.ApplySecondaryButton(button_removeSelectedLine);
            Theme.ApplySecondaryButton(button_back);

            // Fill (not the new AllCells default): this grid's column widths were already
            // hand-tuned via FillWeight below in loadPurchaseOrders() and verified visually.
            Theme.ApplyDataGridView(dataGridView_purchaseOrders, DataGridViewAutoSizeColumnsMode.Fill);
            Theme.ApplyDataGridView(dataGridView_newLines);
            Theme.ApplyStatusBadgeColumn(dataGridView_purchaseOrders, "status",
                raw => EnumDisplay.Hebrew((POStatus)Enum.Parse(typeof(POStatus), raw)));
            Theme.WrapInCard(dataGridView_purchaseOrders);
            Theme.WrapInCard(dataGridView_newLines);
        }

        private void loadPurchaseOrders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("purchaseOrderId", typeof(int));
            dt.Columns.Add("poNumber", typeof(string));
            dt.Columns.Add("supplier", typeof(string));
            dt.Columns.Add("project", typeof(string));
            dt.Columns.Add("orderDate", typeof(DateTime));
            dt.Columns.Add("totalAmount", typeof(decimal));
            dt.Columns.Add("vatAmount", typeof(decimal));
            dt.Columns.Add("status", typeof(string));

            foreach (PurchaseOrder po in Program.PurchaseOrders)
            {
                dt.Rows.Add(po.getPurchaseOrderId(), po.getPoNumber(), po.getSupplier().getName(), po.getProject().getName(), po.getOrderDate(),
                    po.getTotalAmount(), po.getVatAmount(), po.getStatus().ToString());
            }

            dataGridView_purchaseOrders.DataSource = dt;

            // purchaseOrderId is the internal surrogate PK -- meaningless to the Accountant.
            // Kept in the DataTable (dataGridView_purchaseOrders_CellClick still reads it by
            // name to look up the selected PurchaseOrder) but hidden from view; poNumber is
            // the real business identifier and takes its place as the rightmost (first-read,
            // RTL) column.
            dataGridView_purchaseOrders.Columns["purchaseOrderId"].Visible = false;

            dataGridView_purchaseOrders.Columns["poNumber"].HeaderText = "מס' הזמנה";
            dataGridView_purchaseOrders.Columns["supplier"].HeaderText = "ספק";
            dataGridView_purchaseOrders.Columns["project"].HeaderText = "פרויקט";
            dataGridView_purchaseOrders.Columns["orderDate"].HeaderText = "תאריך";
            dataGridView_purchaseOrders.Columns["totalAmount"].HeaderText = "סכום כולל";
            dataGridView_purchaseOrders.Columns["vatAmount"].HeaderText = "מע\"מ";
            dataGridView_purchaseOrders.Columns["status"].HeaderText = "סטטוס";

            dataGridView_purchaseOrders.Columns["totalAmount"].DefaultCellStyle.Format = "N2";
            dataGridView_purchaseOrders.Columns["vatAmount"].DefaultCellStyle.Format = "N2";
            dataGridView_purchaseOrders.Columns["orderDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Relative widths under AutoSizeColumnsMode.Fill -- supplier/project get the most
            // room (free-text names), status gets enough for the longest Hebrew label
            // ("ממתין לאישור מנכ"ל") without the badge clipping.
            dataGridView_purchaseOrders.Columns["poNumber"].FillWeight = 90;
            dataGridView_purchaseOrders.Columns["supplier"].FillWeight = 150;
            dataGridView_purchaseOrders.Columns["project"].FillWeight = 160;
            dataGridView_purchaseOrders.Columns["orderDate"].FillWeight = 75;
            dataGridView_purchaseOrders.Columns["totalAmount"].FillWeight = 95;
            dataGridView_purchaseOrders.Columns["vatAmount"].FillWeight = 80;
            dataGridView_purchaseOrders.Columns["status"].FillWeight = 150;
        }

        private void dataGridView_purchaseOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = int.Parse(dataGridView_purchaseOrders.Rows[e.RowIndex].Cells["purchaseOrderId"].Value.ToString());
            selectedPurchaseOrder = PurchaseOrder.seekPurchaseOrder(id);
            if (selectedPurchaseOrder == null) return;

            textBox_purchaseOrderId.Text = selectedPurchaseOrder.getPurchaseOrderId().ToString();
            textBox_poNumber.Text = selectedPurchaseOrder.getPoNumber();
            comboBox_supplier.Text = selectedPurchaseOrder.getSupplier().getBusinessPartnerId() + " - " + selectedPurchaseOrder.getSupplier().getName();
            comboBox_project.Text = selectedPurchaseOrder.getProject().getProjectId() + " - " + selectedPurchaseOrder.getProject().getName();
            comboBox_createdBy.Text = selectedPurchaseOrder.getCreatedBy().getEmployeeId() + " - " + selectedPurchaseOrder.getCreatedBy().getFullName();
            comboBox_approvedBy.Text = selectedPurchaseOrder.getApprovedBy() == null ? NoneOption : selectedPurchaseOrder.getApprovedBy().getEmployeeId() + " - " + selectedPurchaseOrder.getApprovedBy().getFullName();
            comboBox_overrideApprovedBy.Text = selectedPurchaseOrder.getOverrideApprovedBy() == null ? NoneOption : selectedPurchaseOrder.getOverrideApprovedBy().getEmployeeId() + " - " + selectedPurchaseOrder.getOverrideApprovedBy().getFullName();
            textBox_orderDate.Text = selectedPurchaseOrder.getOrderDate().ToString("yyyy-MM-dd");
            textBox_totalAmount.Text = selectedPurchaseOrder.getTotalAmount().ToString();
            textBox_vatAmount.Text = selectedPurchaseOrder.getVatAmount().ToString();
            comboBox_status.Text = EnumDisplay.Hebrew(selectedPurchaseOrder.getStatus());
            textBox_rejectionReason.Text = selectedPurchaseOrder.getRejectionReason();
            comboBox_closureReason.Text = selectedPurchaseOrder.getClosureReason().HasValue ? selectedPurchaseOrder.getClosureReason().Value.ToString() : NoneOption;

            refreshActionButtons();
        }

        private bool validateFields()
        {
            if (string.IsNullOrWhiteSpace(textBox_poNumber.Text))
            {
                MessageBox.Show("יש להזין מספר הזמנה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_supplier.Text))
            {
                MessageBox.Show("יש לבחור ספק", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_project.Text))
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (comboBox_createdBy.SelectedIndex < 0 && string.IsNullOrWhiteSpace(comboBox_createdBy.Text))
            {
                MessageBox.Show("יש לבחור מי יצר את ההזמנה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!DateTime.TryParse(textBox_orderDate.Text, out _))
            {
                MessageBox.Show("יש להזין תאריך הזמנה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_totalAmount.Text, out _))
            {
                MessageBox.Show("יש להזין סכום כולל תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            if (!decimal.TryParse(textBox_vatAmount.Text, out _))
            {
                MessageBox.Show("יש להזין סכום מע\"מ תקין", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private Supplier resolveSelectedSupplier()
        {
            int id = int.Parse(comboBox_supplier.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return BusinessPartner.seekBusinessPartner(id) as Supplier;
        }

        private Project resolveSelectedProject()
        {
            int id = int.Parse(comboBox_project.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Project.seekProject(id);
        }

        private Employee resolveSelectedCreatedBy()
        {
            int id = int.Parse(comboBox_createdBy.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Employee.seekEmployee(id);
        }

        private Employee resolveSelectedEmployeeOrNull(ComboBox box)
        {
            if (box.Text == NoneOption || string.IsNullOrWhiteSpace(box.Text)) return null;
            int id = int.Parse(box.Text.Split(new[] { " - " }, StringSplitOptions.None)[0]);
            return Employee.seekEmployee(id);
        }

        private ClosureReason? resolveSelectedClosureReason()
        {
            if (comboBox_closureReason.Text == NoneOption || string.IsNullOrWhiteSpace(comboBox_closureReason.Text)) return null;
            return (ClosureReason)Enum.Parse(typeof(ClosureReason), comboBox_closureReason.Text);
        }

        private void clearForm()
        {
            selectedPurchaseOrder = null;
            textBox_purchaseOrderId.Text = "";
            textBox_poNumber.Text = "";
            comboBox_supplier.SelectedIndex = -1;
            comboBox_supplier.Text = "";
            comboBox_project.SelectedIndex = -1;
            comboBox_project.Text = "";
            comboBox_createdBy.SelectedIndex = -1;
            comboBox_createdBy.Text = "";
            comboBox_approvedBy.SelectedIndex = 0;
            comboBox_overrideApprovedBy.SelectedIndex = 0;
            textBox_orderDate.Text = "";
            textBox_totalAmount.Text = "";
            textBox_vatAmount.Text = "";
            comboBox_status.SelectedIndex = 0;
            textBox_rejectionReason.Text = "";
            comboBox_closureReason.SelectedIndex = 0;

            refreshActionButtons();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!validateFields()) return;

            int id = PurchaseOrder.getNextPurchaseOrderId();
            // A new order always starts life in Draft (t0 in the state diagram), never
            // approved/rejected/closed by fiat -- status/rejectionReason/closureReason
            // change only through the transition methods, per step 7.4.
            PurchaseOrder po = new PurchaseOrder(id, textBox_poNumber.Text, resolveSelectedSupplier(), resolveSelectedProject(),
                resolveSelectedCreatedBy(), null, null,
                DateTime.Parse(textBox_orderDate.Text), decimal.Parse(textBox_totalAmount.Text), decimal.Parse(textBox_vatAmount.Text),
                POStatus.Draft, null, null, null, null, false, null, true);

            if (!Program.PurchaseOrders.Contains(po)) return;

            MessageBox.Show("הזמנת הרכש נשמרה בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadPurchaseOrders();
        }

        private void button_updateDetails_Click(object sender, EventArgs e)
        {
            if (selectedPurchaseOrder == null)
            {
                MessageBox.Show("יש לבחור הזמנת רכש מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!validateFields()) return;

            // status/rejectionReason/closureReason/approvedBy/overrideApprovedBy are
            // intentionally NOT touched here (step 7.4) -- they change only through the
            // guarded transition methods below (step 7.5).
            selectedPurchaseOrder.setPoNumber(textBox_poNumber.Text);
            selectedPurchaseOrder.setSupplier(resolveSelectedSupplier());
            selectedPurchaseOrder.setProject(resolveSelectedProject());
            selectedPurchaseOrder.setCreatedBy(resolveSelectedCreatedBy());
            selectedPurchaseOrder.setOrderDate(DateTime.Parse(textBox_orderDate.Text));
            selectedPurchaseOrder.setTotalAmount(decimal.Parse(textBox_totalAmount.Text));
            selectedPurchaseOrder.setVatAmount(decimal.Parse(textBox_vatAmount.Text));
            if (!selectedPurchaseOrder.updatePurchaseOrder()) return;

            MessageBox.Show("פרטי ההזמנה עודכנו בהצלחה", "הודעה", MessageBoxButtons.OK);
            clearForm();
            loadPurchaseOrders();
        }

        // ====================================================================
        // Verb buttons (step 7.5) -- one per user-triggered transition in
        // docs/design/state-diagram.md. Each calls the matching transition
        // method on PurchaseOrder, shows the Hebrew guard-failure message on
        // exception, and refreshes the list on success.
        // ====================================================================

        private bool requireSelection()
        {
            if (selectedPurchaseOrder == null)
            {
                MessageBox.Show("יש לבחור הזמנת רכש מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void button_submit_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.submit();
                MessageBox.Show("ההזמנה נשלחה לאישור", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_withdraw_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.withdraw();
                MessageBox.Show("ההזמנה נמשכה לטיוטה", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_reject_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                // Who's rejecting depends on which sub-state the order is currently in: the
                // CEO rejects from PendingBudgetOverride (comboBox_overrideApprovedBy), the PM
                // rejects from PendingPMApproval (comboBox_approvedBy) -- same two fields the
                // approve-path buttons already read from, just read here before the rejection
                // instead of after an approval.
                ComboBox rejecterBox = selectedPurchaseOrder.getStatus() == POStatus.PendingBudgetOverride
                    ? comboBox_overrideApprovedBy : comboBox_approvedBy;
                selectedPurchaseOrder.reject(textBox_rejectionReason.Text, resolveSelectedEmployeeOrNull(rejecterBox));
                MessageBox.Show("ההזמנה נדחתה", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_revise_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.revise();
                MessageBox.Show("ההזמנה הוחזרה לטיוטה לתיקון", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.cancel();
                MessageBox.Show("ההזמנה בוטלה", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_approveBudgetOverride_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.approveBudgetOverride(resolveSelectedEmployeeOrNull(comboBox_overrideApprovedBy));
                MessageBox.Show("חריגת התקציב אושרה", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_approve_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.approve(resolveSelectedEmployeeOrNull(comboBox_approvedBy));
                // UC-03.Include: "Send Purchase Order Email to Supplier" -- simulated (no real
                // SMTP), but shows the actual composed notification so the include-UC is
                // genuinely demonstrable, not just a status flip.
                // Explicit RTL options: MessageBox.Show doesn't infer reading direction from
                // the text's content, only from parameters given here -- the plain overload
                // used by every other MessageBox.Show in this app renders Hebrew LTR-aligned.
                // That's only visible on a short one-liner if you look closely, but this is a
                // multi-line composed message where it's clearly wrong, so it's fixed here.
                MessageBox.Show(selectedPurchaseOrder.composeSupplierEmail(), "📧 מייל נשלח לספק",
                    MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_cancelRemaining_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;

            DialogResult confirm = MessageBox.Show("ביטול היתרה מחייב הסכמת הספק. האם הספק הסכים?", "אישור", MessageBoxButtons.YesNo);
            try
            {
                selectedPurchaseOrder.cancelRemaining(confirm == DialogResult.Yes);
                MessageBox.Show("יתרת ההזמנה בוטלה", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_archive_Click(object sender, EventArgs e)
        {
            if (!requireSelection()) return;
            try
            {
                selectedPurchaseOrder.archive();
                MessageBox.Show("ההזמנה הועברה לארכיון", "הודעה", MessageBoxButtons.OK);
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן לבצע פעולה", MessageBoxButtons.OK);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedPurchaseOrder == null)
            {
                MessageBox.Show("יש לבחור הזמנת רכש מהרשימה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            DialogResult result = MessageBox.Show("האם למחוק את הזמנת הרכש?", "אישור מחיקה", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            if (!selectedPurchaseOrder.deletePurchaseOrder()) return;
            clearForm();
            loadPurchaseOrders();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            mainForm.showPanel(new MainMenuPanel());
        }

        // ====================================================================
        // Quick-create-with-lines (docs/00e-use-cases.md UC-03 MSS 1-9) --
        // atomic alternative to button_save + PurchaseOrderLinePanel +
        // button_submit above. Every guard (supplier active, project has a
        // budget, at least one line, valid amounts) is enforced by
        // sp_purchase_order_create_flow itself via Employee.createPurchaseOrder();
        // this code only queues lines client-side and reports the outcome.
        // ====================================================================

        private void refreshPendingLinesGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("unitOfMeasure", typeof(string));
            dt.Columns.Add("quantity", typeof(double));
            dt.Columns.Add("unitPrice", typeof(decimal));

            foreach (PurchaseOrderLineInput line in pendingQuickCreateLines)
                dt.Rows.Add(line.Description, line.UnitOfMeasure, line.Quantity, line.UnitPrice);

            dataGridView_newLines.DataSource = dt;
            // NotSortable: button_removeSelectedLine_Click maps CurrentRow.Index straight
            // into pendingQuickCreateLines -- if a column-header click were allowed to
            // reorder the displayed rows, that index would point at the wrong line.
            foreach (DataGridViewColumn col in dataGridView_newLines.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (dataGridView_newLines.Columns["unitPrice"] != null)
                dataGridView_newLines.Columns["unitPrice"].DefaultCellStyle.Format = "N2";
        }

        private void button_addLineToQueue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_newLineDescription.Text))
            {
                MessageBox.Show("יש להזין תיאור פריט", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox_newLineUnitOfMeasure.Text))
            {
                MessageBox.Show("יש להזין יחידת מידה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!double.TryParse(textBox_newLineQuantity.Text, out double quantity) || quantity <= 0)
            {
                MessageBox.Show("יש להזין כמות חיובית", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!decimal.TryParse(textBox_newLineUnitPrice.Text, out decimal unitPrice) || unitPrice < 0)
            {
                MessageBox.Show("יש להזין מחיר יחידה תקין (לא שלילי)", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            pendingQuickCreateLines.Add(new PurchaseOrderLineInput(textBox_newLineDescription.Text, textBox_newLineUnitOfMeasure.Text, quantity, unitPrice));
            refreshPendingLinesGrid();

            textBox_newLineDescription.Text = "";
            textBox_newLineUnitOfMeasure.Text = "";
            textBox_newLineQuantity.Text = "";
            textBox_newLineUnitPrice.Text = "";
        }

        private void button_removeSelectedLine_Click(object sender, EventArgs e)
        {
            if (dataGridView_newLines.CurrentRow == null || dataGridView_newLines.CurrentRow.Index >= pendingQuickCreateLines.Count)
            {
                MessageBox.Show("יש לבחור שורה מהרשימה להסרה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            pendingQuickCreateLines.RemoveAt(dataGridView_newLines.CurrentRow.Index);
            refreshPendingLinesGrid();
        }

        // Reuses textBox_poNumber/comboBox_supplier/comboBox_project/comboBox_createdBy/
        // textBox_orderDate as header inputs -- this is a second way to reach UC-03's
        // outcome, not a second set of header fields. totalAmount/vatAmount are
        // deliberately NOT read from textBox_totalAmount/textBox_vatAmount: the atomic
        // flow computes them server-side from pendingQuickCreateLines.
        private void button_quickCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_poNumber.Text))
            {
                MessageBox.Show("יש להזין מספר הזמנה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBox_supplier.Text))
            {
                MessageBox.Show("יש לבחור ספק", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBox_project.Text))
            {
                MessageBox.Show("יש לבחור פרויקט", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(comboBox_createdBy.Text))
            {
                MessageBox.Show("יש לבחור מי יוצר את ההזמנה", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (!DateTime.TryParse(textBox_orderDate.Text, out DateTime orderDate))
            {
                MessageBox.Show("יש להזין תאריך הזמנה תקין (yyyy-MM-dd)", "שגיאה", MessageBoxButtons.OK);
                return;
            }
            if (pendingQuickCreateLines.Count == 0)
            {
                MessageBox.Show("יש להוסיף לפחות שורת פריט אחת לרשימה לפני היצירה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            Employee createdBy = resolveSelectedCreatedBy();
            try
            {
                PurchaseOrder po = createdBy.createPurchaseOrder(textBox_poNumber.Text, resolveSelectedSupplier(),
                    resolveSelectedProject(), orderDate, pendingQuickCreateLines);

                MessageBox.Show("הזמנת הרכש נוצרה בהצלחה בסטטוס \"" + EnumDisplay.Hebrew(po.getStatus()) + "\" עם " +
                    pendingQuickCreateLines.Count + " שורות פריט", "הודעה", MessageBoxButtons.OK);

                pendingQuickCreateLines.Clear();
                refreshPendingLinesGrid();
                clearForm();
                loadPurchaseOrders();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "לא ניתן ליצור הזמנת רכש", MessageBoxButtons.OK);
            }
        }
    }
}

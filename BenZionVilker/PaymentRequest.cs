using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class PaymentRequest
    {
        private int paymentRequestId;
        private Project project;
        private decimal amount;
        private DateTime submissionDate;
        private DateTime? approvalDate; // nullable: not yet set while status is Submitted/UnderReview/Rejected
        private PaymentRequestStatus status;

        public PaymentRequest(int paymentRequestId, Project project, decimal amount, DateTime submissionDate,
            DateTime? approvalDate, PaymentRequestStatus status, bool is_new)
        {
            this.paymentRequestId = paymentRequestId;
            this.project = project;
            this.amount = amount;
            this.submissionDate = submissionDate;
            this.approvalDate = approvalDate;
            this.status = status;
            if (is_new)
            {
                this.createPaymentRequest();
                Program.PaymentRequests.Add(this);
            }
        }

        public int getPaymentRequestId() { return this.paymentRequestId; }
        public Project getProject() { return this.project; }
        public decimal getAmount() { return this.amount; }
        public DateTime getSubmissionDate() { return this.submissionDate; }
        public DateTime? getApprovalDate() { return this.approvalDate; }
        public PaymentRequestStatus getStatus() { return this.status; }

        // Derived from SubmittedDocument (composition child), replacing the former
        // free-text missingDocuments column -- see design/class-diagram.md Section 5.
        public List<DocumentType> getMissingDocuments()
        {
            var present = new HashSet<DocumentType>();
            foreach (SubmittedDocument doc in Program.SubmittedDocuments)
            {
                if (doc.getPaymentRequest() == this)
                    present.Add(doc.getType());
            }
            var missing = new List<DocumentType>();
            foreach (DocumentType dt in Enum.GetValues(typeof(DocumentType)))
            {
                if (!present.Contains(dt))
                    missing.Add(dt);
            }
            return missing;
        }

        public void setProject(Project project) { this.project = project; }
        public void setAmount(decimal amount) { this.amount = amount; }
        public void setSubmissionDate(DateTime submissionDate) { this.submissionDate = submissionDate; }
        public void setApprovalDate(DateTime? approvalDate) { this.approvalDate = approvalDate; }
        public void setStatus(PaymentRequestStatus status) { this.status = status; }

        public void createPaymentRequest()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_payment_request_create @payment_request_id, @project_id, @amount, @submissionDate, @approvalDate, @status";
            cmd.Parameters.AddWithValue("@payment_request_id", this.paymentRequestId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@submissionDate", this.submissionDate);
            cmd.Parameters.AddWithValue("@approvalDate", (object)this.approvalDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updatePaymentRequest()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_payment_request_update @payment_request_id, @project_id, @amount, @submissionDate, @approvalDate, @status";
            cmd.Parameters.AddWithValue("@payment_request_id", this.paymentRequestId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@amount", this.amount);
            cmd.Parameters.AddWithValue("@submissionDate", this.submissionDate);
            cmd.Parameters.AddWithValue("@approvalDate", (object)this.approvalDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deletePaymentRequest()
        {
            Program.PaymentRequests.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_payment_request_delete @payment_request_id";
            cmd.Parameters.AddWithValue("@payment_request_id", this.paymentRequestId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initPaymentRequests()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_payment_request_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.PaymentRequests = new List<PaymentRequest>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Project project = Project.seekProject(int.Parse(rdr.GetValue(1).ToString()));
                decimal amount = decimal.Parse(rdr.GetValue(2).ToString());
                DateTime submissionDate = DateTime.Parse(rdr.GetValue(3).ToString());
                DateTime? approvalDate = rdr.GetValue(4) == DBNull.Value ? (DateTime?)null : DateTime.Parse(rdr.GetValue(4).ToString());
                PaymentRequestStatus status = (PaymentRequestStatus)Enum.Parse(typeof(PaymentRequestStatus), rdr.GetValue(5).ToString());

                PaymentRequest pr = new PaymentRequest(id, project, amount, submissionDate, approvalDate, status, false);
                Program.PaymentRequests.Add(pr);
            }
        }

        public static PaymentRequest seekPaymentRequest(int id)
        {
            foreach (PaymentRequest pr in Program.PaymentRequests)
            {
                if (pr.getPaymentRequestId() == id)
                    return pr;
            }
            return null;
        }

        public static int getNextPaymentRequestId()
        {
            int maxId = 0;
            foreach (PaymentRequest pr in Program.PaymentRequests)
            {
                if (pr.getPaymentRequestId() > maxId)
                    maxId = pr.getPaymentRequestId();
            }
            return maxId + 1;
        }
    }
}

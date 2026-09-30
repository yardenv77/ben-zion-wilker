using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class SubmittedDocument
    {
        private int submittedDocumentId;
        private PaymentRequest paymentRequest;
        private DocumentType type;
        private DateTime receivedOn;

        public SubmittedDocument(int submittedDocumentId, PaymentRequest paymentRequest, DocumentType type, DateTime receivedOn, bool is_new)
        {
            this.submittedDocumentId = submittedDocumentId;
            this.paymentRequest = paymentRequest;
            this.type = type;
            this.receivedOn = receivedOn;
            if (is_new)
            {
                this.createSubmittedDocument();
                Program.SubmittedDocuments.Add(this);
            }
        }

        public int getSubmittedDocumentId() { return this.submittedDocumentId; }
        public PaymentRequest getPaymentRequest() { return this.paymentRequest; }
        public DocumentType getType() { return this.type; }
        public DateTime getReceivedOn() { return this.receivedOn; }

        public void setPaymentRequest(PaymentRequest paymentRequest) { this.paymentRequest = paymentRequest; }
        public void setType(DocumentType type) { this.type = type; }
        public void setReceivedOn(DateTime receivedOn) { this.receivedOn = receivedOn; }

        public void createSubmittedDocument()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_submitted_document_create @submitted_document_id, @payment_request_id, @type, @receivedOn";
            cmd.Parameters.AddWithValue("@submitted_document_id", this.submittedDocumentId);
            cmd.Parameters.AddWithValue("@payment_request_id", this.paymentRequest.getPaymentRequestId());
            cmd.Parameters.AddWithValue("@type", this.type.ToString());
            cmd.Parameters.AddWithValue("@receivedOn", this.receivedOn);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateSubmittedDocument()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_submitted_document_update @submitted_document_id, @payment_request_id, @type, @receivedOn";
            cmd.Parameters.AddWithValue("@submitted_document_id", this.submittedDocumentId);
            cmd.Parameters.AddWithValue("@payment_request_id", this.paymentRequest.getPaymentRequestId());
            cmd.Parameters.AddWithValue("@type", this.type.ToString());
            cmd.Parameters.AddWithValue("@receivedOn", this.receivedOn);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteSubmittedDocument()
        {
            Program.SubmittedDocuments.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_submitted_document_delete @submitted_document_id";
            cmd.Parameters.AddWithValue("@submitted_document_id", this.submittedDocumentId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initSubmittedDocuments()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_submitted_document_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.SubmittedDocuments = new List<SubmittedDocument>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                PaymentRequest paymentRequest = PaymentRequest.seekPaymentRequest(int.Parse(rdr.GetValue(1).ToString()));
                DocumentType type = (DocumentType)Enum.Parse(typeof(DocumentType), rdr.GetValue(2).ToString());
                DateTime receivedOn = DateTime.Parse(rdr.GetValue(3).ToString());

                SubmittedDocument sd = new SubmittedDocument(id, paymentRequest, type, receivedOn, false);
                Program.SubmittedDocuments.Add(sd);
            }
        }

        public static SubmittedDocument seekSubmittedDocument(int id)
        {
            foreach (SubmittedDocument sd in Program.SubmittedDocuments)
            {
                if (sd.getSubmittedDocumentId() == id)
                    return sd;
            }
            return null;
        }

        public static int getNextSubmittedDocumentId()
        {
            int maxId = 0;
            foreach (SubmittedDocument sd in Program.SubmittedDocuments)
            {
                if (sd.getSubmittedDocumentId() > maxId)
                    maxId = sd.getSubmittedDocumentId();
            }
            return maxId + 1;
        }
    }
}

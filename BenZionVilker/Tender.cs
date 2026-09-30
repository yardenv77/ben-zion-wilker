using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class Tender
    {
        private int tenderId;
        private string tenderNumber;
        private Client client;
        private string title;
        private decimal estimatedValue;
        private DateTime submissionDeadline;
        private DateTime publishedDate;
        private TenderStatus status;

        public Tender(int tenderId, string tenderNumber, Client client, string title, decimal estimatedValue,
            DateTime submissionDeadline, DateTime publishedDate, TenderStatus status, bool is_new)
        {
            this.tenderId = tenderId;
            this.tenderNumber = tenderNumber;
            this.client = client;
            this.title = title;
            this.estimatedValue = estimatedValue;
            this.submissionDeadline = submissionDeadline;
            this.publishedDate = publishedDate;
            this.status = status;
            if (is_new)
            {
                this.createTender();
                Program.Tenders.Add(this);
            }
        }

        public int getTenderId() { return this.tenderId; }
        public string getTenderNumber() { return this.tenderNumber; }
        public Client getClient() { return this.client; }
        public string getTitle() { return this.title; }
        public decimal getEstimatedValue() { return this.estimatedValue; }
        public DateTime getSubmissionDeadline() { return this.submissionDeadline; }
        public DateTime getPublishedDate() { return this.publishedDate; }
        public TenderStatus getStatus() { return this.status; }

        public void setTenderNumber(string tenderNumber) { this.tenderNumber = tenderNumber; }
        public void setClient(Client client) { this.client = client; }
        public void setTitle(string title) { this.title = title; }
        public void setEstimatedValue(decimal estimatedValue) { this.estimatedValue = estimatedValue; }
        public void setSubmissionDeadline(DateTime submissionDeadline) { this.submissionDeadline = submissionDeadline; }
        public void setPublishedDate(DateTime publishedDate) { this.publishedDate = publishedDate; }
        public void setStatus(TenderStatus status) { this.status = status; }

        public void createTender()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_tender_create @tender_id, @tenderNumber, @client_id, @title, @estimatedValue, @submissionDeadline, @publishedDate, @status";
            cmd.Parameters.AddWithValue("@tender_id", this.tenderId);
            cmd.Parameters.AddWithValue("@tenderNumber", this.tenderNumber);
            cmd.Parameters.AddWithValue("@client_id", this.client.getClientId());
            cmd.Parameters.AddWithValue("@title", this.title);
            cmd.Parameters.AddWithValue("@estimatedValue", this.estimatedValue);
            cmd.Parameters.AddWithValue("@submissionDeadline", this.submissionDeadline);
            cmd.Parameters.AddWithValue("@publishedDate", this.publishedDate);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateTender()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_tender_update @tender_id, @tenderNumber, @client_id, @title, @estimatedValue, @submissionDeadline, @publishedDate, @status";
            cmd.Parameters.AddWithValue("@tender_id", this.tenderId);
            cmd.Parameters.AddWithValue("@tenderNumber", this.tenderNumber);
            cmd.Parameters.AddWithValue("@client_id", this.client.getClientId());
            cmd.Parameters.AddWithValue("@title", this.title);
            cmd.Parameters.AddWithValue("@estimatedValue", this.estimatedValue);
            cmd.Parameters.AddWithValue("@submissionDeadline", this.submissionDeadline);
            cmd.Parameters.AddWithValue("@publishedDate", this.publishedDate);
            cmd.Parameters.AddWithValue("@status", this.status.ToString());
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteTender()
        {
            Program.Tenders.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_tender_delete @tender_id";
            cmd.Parameters.AddWithValue("@tender_id", this.tenderId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initTenders()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_tender_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Tenders = new List<Tender>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string tenderNumber = rdr.GetValue(1).ToString();
                Client client = Client.seekClient(int.Parse(rdr.GetValue(2).ToString()));
                string title = rdr.GetValue(3).ToString();
                decimal estimatedValue = decimal.Parse(rdr.GetValue(4).ToString());
                DateTime submissionDeadline = DateTime.Parse(rdr.GetValue(5).ToString());
                DateTime publishedDate = DateTime.Parse(rdr.GetValue(6).ToString());
                TenderStatus status = (TenderStatus)Enum.Parse(typeof(TenderStatus), rdr.GetValue(7).ToString());

                Tender t = new Tender(id, tenderNumber, client, title, estimatedValue, submissionDeadline, publishedDate, status, false);
                Program.Tenders.Add(t);
            }
        }

        public static Tender seekTender(int id)
        {
            foreach (Tender t in Program.Tenders)
            {
                if (t.getTenderId() == id)
                    return t;
            }
            return null;
        }

        public static int getNextTenderId()
        {
            int maxId = 0;
            foreach (Tender t in Program.Tenders)
            {
                if (t.getTenderId() > maxId)
                    maxId = t.getTenderId();
            }
            return maxId + 1;
        }
    }
}

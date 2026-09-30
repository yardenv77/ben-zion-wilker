using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class Client
    {
        private int clientId;
        private string name;
        private string contactPerson;
        private string phone;
        private string email;
        private string sector;

        public Client(int clientId, string name, string contactPerson, string phone, string email, string sector, bool is_new)
        {
            this.clientId = clientId;
            this.name = name;
            this.contactPerson = contactPerson;
            this.phone = phone;
            this.email = email;
            this.sector = sector;
            if (is_new)
            {
                this.createClient();
                Program.Clients.Add(this);
            }
        }

        public int getClientId() { return this.clientId; }
        public string getName() { return this.name; }
        public string getContactPerson() { return this.contactPerson; }
        public string getPhone() { return this.phone; }
        public string getEmail() { return this.email; }
        public string getSector() { return this.sector; }

        public void setName(string name) { this.name = name; }
        public void setContactPerson(string contactPerson) { this.contactPerson = contactPerson; }
        public void setPhone(string phone) { this.phone = phone; }
        public void setEmail(string email) { this.email = email; }
        public void setSector(string sector) { this.sector = sector; }

        public void createClient()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_client_create @client_id, @name, @contactPerson, @phone, @email, @sector";
            cmd.Parameters.AddWithValue("@client_id", this.clientId);
            cmd.Parameters.AddWithValue("@name", this.name);
            cmd.Parameters.AddWithValue("@contactPerson", this.contactPerson);
            cmd.Parameters.AddWithValue("@phone", this.phone);
            cmd.Parameters.AddWithValue("@email", this.email);
            cmd.Parameters.AddWithValue("@sector", this.sector);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void updateClient()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_client_update @client_id, @name, @contactPerson, @phone, @email, @sector";
            cmd.Parameters.AddWithValue("@client_id", this.clientId);
            cmd.Parameters.AddWithValue("@name", this.name);
            cmd.Parameters.AddWithValue("@contactPerson", this.contactPerson);
            cmd.Parameters.AddWithValue("@phone", this.phone);
            cmd.Parameters.AddWithValue("@email", this.email);
            cmd.Parameters.AddWithValue("@sector", this.sector);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public void deleteClient()
        {
            Program.Clients.Remove(this);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_client_delete @client_id";
            cmd.Parameters.AddWithValue("@client_id", this.clientId);
            SQL_CON SC = new SQL_CON();
            SC.execute_non_query(cmd);
        }

        public static void initClients()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_client_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.Clients = new List<Client>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string name = rdr.GetValue(1).ToString();
                string contactPerson = rdr.GetValue(2).ToString();
                string phone = rdr.GetValue(3).ToString();
                string email = rdr.GetValue(4).ToString();
                string sector = rdr.GetValue(5).ToString();

                Client c = new Client(id, name, contactPerson, phone, email, sector, false);
                Program.Clients.Add(c);
            }
        }

        public static Client seekClient(int id)
        {
            foreach (Client c in Program.Clients)
            {
                if (c.getClientId() == id)
                    return c;
            }
            return null;
        }

        public static int getNextClientId()
        {
            int maxId = 0;
            foreach (Client c in Program.Clients)
            {
                if (c.getClientId() > maxId)
                    maxId = c.getClientId();
            }
            return maxId + 1;
        }
    }
}

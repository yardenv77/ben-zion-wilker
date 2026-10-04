using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class TradeCategory
    {
        private int tradeCategoryId;
        private string categoryName;

        public TradeCategory(int tradeCategoryId, string categoryName, bool is_new)
        {
            this.tradeCategoryId = tradeCategoryId;
            this.categoryName = categoryName;
            if (is_new)
            {
                if (this.createTradeCategory())
                    Program.TradeCategories.Add(this);
            }
        }

        public int getTradeCategoryId() { return this.tradeCategoryId; }
        public string getCategoryName() { return this.categoryName; }

        public void setCategoryName(string categoryName) { this.categoryName = categoryName; }

        public bool createTradeCategory()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_trade_category_create @trade_category_id, @categoryName";
            cmd.Parameters.AddWithValue("@trade_category_id", this.tradeCategoryId);
            cmd.Parameters.AddWithValue("@categoryName", this.categoryName);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool updateTradeCategory()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_trade_category_update @trade_category_id, @categoryName";
            cmd.Parameters.AddWithValue("@trade_category_id", this.tradeCategoryId);
            cmd.Parameters.AddWithValue("@categoryName", this.categoryName);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deleteTradeCategory()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_trade_category_delete @trade_category_id";
            cmd.Parameters.AddWithValue("@trade_category_id", this.tradeCategoryId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.TradeCategories.Remove(this);
            return success;
        }

        public static void initTradeCategories()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_trade_category_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.TradeCategories = new List<TradeCategory>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                string categoryName = rdr.GetValue(1).ToString();

                TradeCategory tc = new TradeCategory(id, categoryName, false);
                Program.TradeCategories.Add(tc);
            }
        }

        public static TradeCategory seekTradeCategory(int id)
        {
            foreach (TradeCategory tc in Program.TradeCategories)
            {
                if (tc.getTradeCategoryId() == id)
                    return tc;
            }
            return null;
        }

        public static int getNextTradeCategoryId()
        {
            int maxId = 0;
            foreach (TradeCategory tc in Program.TradeCategories)
            {
                if (tc.getTradeCategoryId() > maxId)
                    maxId = tc.getTradeCategoryId();
            }
            return maxId + 1;
        }
    }
}

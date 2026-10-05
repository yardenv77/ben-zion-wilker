using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace BenZionVilker
{
    public class BudgetLine
    {
        private int budgetLineId;
        private Project project;
        private string category;
        private decimal plannedAmount;
        private decimal actualAmount;

        public BudgetLine(int budgetLineId, Project project, string category, decimal plannedAmount, decimal actualAmount, bool is_new)
        {
            this.budgetLineId = budgetLineId;
            this.project = project;
            this.category = category;
            this.plannedAmount = plannedAmount;
            this.actualAmount = actualAmount;
            if (is_new)
            {
                if (this.createBudgetLine())
                    Program.BudgetLines.Add(this);
            }
        }

        public int getBudgetLineId() { return this.budgetLineId; }
        public Project getProject() { return this.project; }
        public string getCategory() { return this.category; }
        public decimal getPlannedAmount() { return this.plannedAmount; }
        public decimal getActualAmount() { return this.actualAmount; }

        // Planned minus actual: what is left on this line (negative = overrun)
        public decimal getVariance() { return this.plannedAmount - this.actualAmount; }

        // Has actual spending reached the given percentage of the planned amount?
        public bool isOverThreshold(double percent)
        {
            return this.actualAmount >= this.plannedAmount * (decimal)percent / 100;
        }

        public void setProject(Project project) { this.project = project; }
        public void setCategory(string category) { this.category = category; }
        public void setPlannedAmount(decimal plannedAmount) { this.plannedAmount = plannedAmount; }
        public void setActualAmount(decimal actualAmount) { this.actualAmount = actualAmount; }

        public bool createBudgetLine()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_budget_line_create @budget_line_id, @project_id, @category, @plannedAmount, @actualAmount";
            cmd.Parameters.AddWithValue("@budget_line_id", this.budgetLineId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@category", this.category);
            cmd.Parameters.AddWithValue("@plannedAmount", this.plannedAmount);
            cmd.Parameters.AddWithValue("@actualAmount", this.actualAmount);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool updateBudgetLine()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_budget_line_update @budget_line_id, @project_id, @category, @plannedAmount, @actualAmount";
            cmd.Parameters.AddWithValue("@budget_line_id", this.budgetLineId);
            cmd.Parameters.AddWithValue("@project_id", this.project.getProjectId());
            cmd.Parameters.AddWithValue("@category", this.category);
            cmd.Parameters.AddWithValue("@plannedAmount", this.plannedAmount);
            cmd.Parameters.AddWithValue("@actualAmount", this.actualAmount);
            SQL_CON SC = new SQL_CON();
            return SC.execute_non_query(cmd);
        }

        public bool deleteBudgetLine()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_budget_line_delete @budget_line_id";
            cmd.Parameters.AddWithValue("@budget_line_id", this.budgetLineId);
            SQL_CON SC = new SQL_CON();
            bool success = SC.execute_non_query(cmd);
            if (success)
                Program.BudgetLines.Remove(this);
            return success;
        }

        public static void initBudgetLines()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EXECUTE sp_budget_line_get_all";
            SQL_CON SC = new SQL_CON();
            SqlDataReader rdr = SC.execute_query(cmd);

            Program.BudgetLines = new List<BudgetLine>();

            while (rdr.Read())
            {
                int id = int.Parse(rdr.GetValue(0).ToString());
                Project project = Project.seekProject(int.Parse(rdr.GetValue(1).ToString()));
                string category = rdr.GetValue(2).ToString();
                decimal plannedAmount = decimal.Parse(rdr.GetValue(3).ToString());
                decimal actualAmount = decimal.Parse(rdr.GetValue(4).ToString());

                BudgetLine bl = new BudgetLine(id, project, category, plannedAmount, actualAmount, false);
                Program.BudgetLines.Add(bl);
            }
        }

        public static BudgetLine seekBudgetLine(int id)
        {
            foreach (BudgetLine bl in Program.BudgetLines)
            {
                if (bl.getBudgetLineId() == id)
                    return bl;
            }
            return null;
        }

        public static int getNextBudgetLineId()
        {
            int maxId = 0;
            foreach (BudgetLine bl in Program.BudgetLines)
            {
                if (bl.getBudgetLineId() > maxId)
                    maxId = bl.getBudgetLineId();
            }
            return maxId + 1;
        }
    }
}

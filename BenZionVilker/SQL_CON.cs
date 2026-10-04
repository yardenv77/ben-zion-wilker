using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Configuration;

namespace BenZionVilker
{
    /// <summary>
    /// מחלקה שאחראית על החיבור לבסיס הנתונים וביצוע שאילתות.
    /// כל פעולה מול בסיס הנתונים עוברת דרך מחלקה זו.
    ///
    /// שתי סוגי פעולות:
    /// 1. execute_non_query - פעולות שמשנות נתונים (INSERT, UPDATE, DELETE)
    /// 2. execute_query    - פעולות שמחזירות נתונים (SELECT)
    ///
    /// מחרוזת החיבור נקראת מ-app.config (connectionStrings/SadDb) ולא מקודדת בקוד,
    /// כדי שסיסמת ה-Azure SQL לא תיכנס ל-git.
    /// </summary>
    class SQL_CON
    {
        SqlConnection conn;

        public SQL_CON()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SadDb"].ConnectionString;
            conn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// ביצוע פעולה שמשנה נתונים בבסיס הנתונים (INSERT, UPDATE, DELETE).
        /// מחזירה bool: true אם הפעולה הצליחה, false אם נכשלה -- כך שהקוד הקורא (הישות
        /// ואז המסך) יודע בוודאות אם ההצלחה "התממשה" בפועל, ולא רק הניח שכך.
        /// לא מציגה הודעת הצלחה גנרית -- כל מסך מציג הודעה משלו, רק כשההצלחה אכן קרתה
        /// (ראו button_save/update/delete_Click בכל *Panel.cs).
        ///
        /// הזרימה:
        /// 1. פתיחת חיבור לבסיס הנתונים
        /// 2. קישור הפקודה לחיבור
        /// 3. ביצוע הפקודה
        /// 4. סגירת החיבור (תמיד! גם אם הייתה שגיאה)
        /// </summary>
        /// <param name="cmd">פקודת SQL מוכנה עם פרמטרים</param>
        /// <returns>true אם הפעולה הצליחה, false אם נכשלה</returns>
        public bool execute_non_query(SqlCommand cmd)
        {
            try
            {
                conn.Open();              // שלב 1: פתיחת חיבור
                cmd.Connection = conn;    // שלב 2: קישור הפקודה לחיבור
                cmd.ExecuteNonQuery();     // שלב 3: ביצוע (INSERT/UPDATE/DELETE)
                return true;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                // 547 = הפרת מפתח זר/CHECK ב-SQL Server (למשל: מחיקת ספק שיש לו הזמנות רכש פתוחות)
                MessageBox.Show("לא ניתן לבצע את הפעולה — קיימות רשומות אחרות במערכת המקושרות לרשומה זו.", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בביצוע הפעולה: " + ex.Message, "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            finally
            {
                // שלב 4: סגירת החיבור - חייבת לקרות תמיד!
                // finally מתבצע גם אם הייתה שגיאה וגם אם לא
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// כמו execute_non_query, אבל מריצה כמה פקודות (בד"כ שורת-הורה + שורת-תת-מחלקה,
        /// table-per-subclass -- ראו BusinessPartner/FinancialSecurity) בטרנזקציה אחת:
        /// או שכולן נכתבות, או שאף אחת לא. בלי זה, כישלון בפקודה השנייה היה משאיר שורת-הורה
        /// "יתומה" בבסיס הנתונים בלי שורת-הבת המתאימה לה.
        /// </summary>
        /// <param name="cmds">פקודות SQL מוכנות עם פרמטרים, בסדר הרצה (למשל: הורה לפני בת ביצירה, בת לפני הורה במחיקה)</param>
        /// <returns>true אם כל הפקודות הצליחו (ו-COMMIT בוצע), false אם אחת מהן נכשלה (ו-ROLLBACK בוצע)</returns>
        public bool execute_non_query_transactional(params SqlCommand[] cmds)
        {
            SqlTransaction tran = null;
            try
            {
                conn.Open();
                tran = conn.BeginTransaction();
                foreach (SqlCommand cmd in cmds)
                {
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                return true;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                tran?.Rollback();
                MessageBox.Show("לא ניתן לבצע את הפעולה — קיימות רשומות אחרות במערכת המקושרות לרשומה זו.", "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            catch (Exception ex)
            {
                tran?.Rollback();
                MessageBox.Show("שגיאה בביצוע הפעולה: " + ex.Message, "שגיאה", MessageBoxButtons.OK);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// כמו execute_non_query, אבל לא בולעת שגיאות ולא מציגה הודעה משלה.
        /// מיועדת לפרוצדורות עם guards בתוך ה-SP עצמה (כמו sp_purchase_order_create_flow),
        /// שבהן הקוד הקורא (למשל Employee.createPurchaseOrder) צריך לתפוס את השגיאה
        /// בעצמו ולתרגם אותה להודעה בעברית ספציפית -- ולא לקבל MessageBox גנרי
        /// שמונע ממנו לדעת שהפעולה בכלל נכשלה.
        /// </summary>
        public void execute_non_query_throwing(SqlCommand cmd)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// ביצוע שאילתה שמחזירה נתונים מבסיס הנתונים (SELECT).
        /// מחזירה SqlDataReader - אובייקט שמאפשר לקרוא את התוצאות שורה אחרי שורה.
        ///
        /// לא בולעת שגיאות: אם החיבור/השאילתה נכשלים, הפעולה זורקת את החריגה הלאה במקום
        /// להציג MessageBox ולהחזיר null. הסיבה: 23 מתודות initXxx() ברחבי הפרויקט קוראות
        /// ל-execute_query וממשיכות מיד ל-while(rdr.Read()) בלי לבדוק אם rdr הוא null --
        /// כשהתנהגות ה-null הקודמת נתקלה בתקלת רשת חולפת באתחול, זה גרם ל-
        /// NullReferenceException לא-מטופל וקריסה מוחלטת של האפליקציה. עכשיו הקריאה נזרקת
        /// עד ל-Program.Main(), שתופס אותה ברמת האפליקציה עם הודעה ואפשרות ניסיון חוזר --
        /// ראו Program.cs. הקורא היחיד מחוץ ל-initXxx() (ProjectProfitabilityReportPanel)
        /// תופס את זה בעצמו מקומית.
        /// </summary>
        /// <param name="cmd">פקודת SQL מוכנה עם פרמטרים</param>
        /// <returns>SqlDataReader לקריאת התוצאות</returns>
        public SqlDataReader execute_query(SqlCommand cmd)
        {
            conn.Open();
            cmd.Connection = conn;
            try
            {
                return cmd.ExecuteReader();
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
    }
}

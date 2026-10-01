using System;
using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// מסך התחברות — לא UC (ראו PATTERNS.md: "Login is not a UC. Authentication is an NFR
    /// precondition. A LoginPanel is a technical artifact.") ולא מופיע בדיאגרמת ה-UC או
    /// במפרטים. נוסף כדי לספק את הדרישה הטכנית ל"מסך התחברות" בהגנה בעל-פה
    /// (cloned/docs/12-oral-exam-guide.html), לא כחלק מהתחום עצמו.
    ///
    /// אין ישות עם שדה סיסמה בדיאגרמת המחלקות (ראו CLAUDE.md, "Entry Flow") -- אין ממה
    /// לגזור אימות אמיתי. בדיוק כמו LoginPanel.cs של פרויקט הדוגמה של הקורס
    /// (cloned/example_project/), זה מסך טכני שמאתר Employee קיים לפי מספרו, ובודק סיסמה
    /// קבועה בקוד (לא ישות/שדה אמיתיים, לא אבטחה אמיתית) -- שקוף ומתועד ככזה, לא מוצג
    /// כמנגנון אבטחה של ממש.
    /// </summary>
    public partial class LoginPanel : UserControl
    {
        private const string DemoPassword = "1987"; // שנת הקמת החברה -- לא סיסמה אמיתית, לא נשמרת בשום מקום

        public LoginPanel()
        {
            InitializeComponent();
            Theme.ApplyStandardPanelTheme(this);
            Theme.CenterHorizontally(label_title, this.Width);
            Theme.CenterHorizontally(label_subtitle, this.Width);
            Theme.ApplyPrimaryButton(button_login); // "login" לא תואם את דפוסי השם ב-ApplyStandardPanelTheme, אז דורס ידנית ל-primary
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_employeeId.Text, out int id))
            {
                MessageBox.Show("יש להזין מספר עובד תקין", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            Employee emp = Employee.seekEmployee(id);
            if (emp == null)
            {
                MessageBox.Show("עובד לא נמצא", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            if (textBox_password.Text != DemoPassword)
            {
                MessageBox.Show("סיסמה שגויה", "שגיאה", MessageBoxButtons.OK);
                return;
            }

            mainForm.showPanel(new MainMenuPanel());
        }
    }
}

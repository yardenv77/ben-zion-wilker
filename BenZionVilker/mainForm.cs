using System.Windows.Forms;

namespace BenZionVilker
{
    /// <summary>
    /// הטופס הראשי — חלון יחיד שמחליף תוכן בתוכו.
    /// כל המסכים במערכת הם UserControl שנטענים לתוך panelMain.
    /// </summary>
    public partial class mainForm : Form
    {
        // הפניה סטטית לטופס הראשי — כדי שכל מסך יוכל לנווט
        private static mainForm instance;

        public mainForm()
        {
            InitializeComponent();
            instance = this;
            // אין מסך Login: אין ישות עם שדה סיסמה במודל התחום (ראו CLAUDE.md, "Entry Flow").
            // מציגים ישירות את תפריט הבית.
            showPanel(new MainMenuPanel());
        }

        /// <summary>
        /// החלפת המסך הנוכחי במסך חדש.
        /// זו הדרך היחידה לנווט בין מסכים במערכת.
        ///
        /// שימוש: mainForm.showPanel(new MyPanel());
        /// </summary>
        public static void showPanel(UserControl panel)
        {
            instance.panelMain.Controls.Clear();
            // Dock.Top (not Fill): width still matches panelMain, but height stays whatever
            // the panel's own Designer.cs authored -- a panel taller than the visible window
            // (e.g. PurchaseOrderPanel, 1040px) gets panelMain's AutoScroll instead of being
            // silently clipped with no way to reach what's below (including its own back
            // button). A panel that already fits just sits without a scrollbar appearing.
            panel.Dock = DockStyle.Top;
            instance.panelMain.Controls.Add(panel);
        }
    }
}

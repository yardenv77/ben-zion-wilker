# בן ציון וילקר (1987) בע"מ — מערכת ניהול פרויקטים

פרויקט בקורס **ניתוח ועיצוב מערכות מידע**, הנדסת תעשייה וניהול, אוניברסיטת בן-גוריון — קבוצה 5.

מערכת WinForms לניהול פרויקטים עבור חברת קבלנות ותשתיות (שיפוצים, עבודות עפר, בנייה ופיתוח), שעובדת בעיקר מול לקוחות ציבוריים (משרד הביטחון, רפאל וגופי ממשלה נוספים). הפרויקט בנוי על בסיס ניתוח ארגוני אמיתי של החברה — ראו `org-analysis/` לפירוט.

## חברי הקבוצה

לפי עמוד השער של ההגשה (`Part2_Group5.pdf`):

| ת"ז |
|---|
| 206625881 |
| 322988692 |
| 206665465 |
| 207000696 |

## טכנולוגיה

- **C# / .NET 8**, WinForms (`net8.0-windows`)
- **Azure SQL** (`sad-groupname-sql-yarden.database.windows.net`, בסיס נתונים `BenZionVilker`) — משותף לכל חברי הקבוצה, מתחברים עם אותו login
- גישה לבסיס הנתונים דרך Stored Procedures בלבד — ללא SQL גולמי בקוד
- פיתוח מלא דרך Claude Code

## מבנה הפרויקט

```
BenZionVilker.sln              ← פותחים את זה ב-Visual Studio
BenZionVilker/                 ← קוד ה-C# (ישויות, מסכים, Theme)
scripts/                       ← create_database.sql, stored_procedures.sql, seed_data.sql, empty_database.sql, drop_database.sql, migrate_*.sql
design/                        ← תרשים מחלקות, תרשים מצבים ו-ERD (.md + .html אינטראקטיבי)
org-analysis/                  ← ניתוח ארגוני, ראיונות, בעיות, תהליכים עסקיים (עברית)
00-requirements.md             ← דרישות פונקציונליות ולא-פונקציונליות
00e-use-cases.md               ← מפרטי Use Case דו-שכבתיים (UC-01 עד UC-06)
CLAUDE.md                      ← קובץ ההקשר של הפרויקט עבור Claude Code
Part1_Group5.pdf / Part2_Group5.pdf  ← ההגשות המקוריות
```

## הרצה

**דרישות קדם:** Visual Studio (2022/2025) עם .NET 8 SDK + Windows Desktop Runtime, Git.

1. משכו את הריפו:
   ```
   git clone https://github.com/yardenv77/ben-zion-wilker.git
   ```
2. צרו `BenZionVilker/app.config` (לא נשמר ב-git כי מכיל סיסמה) עם מחרוזת חיבור בשם `SadDb`:
   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <configuration>
     <connectionStrings>
       <add name="SadDb"
            connectionString="Server=tcp:sad-groupname-sql-yarden.database.windows.net,1433;Initial Catalog=BenZionVilker;User ID=<משתמש>;Password=<סיסמה>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            providerName="Microsoft.Data.SqlClient" />
     </connectionStrings>
   </configuration>
   ```
   את פרטי ההתחברות מקבלים מחברת קבוצה ישירות (לא דרך git).
3. פתחו את `BenZionVilker.sln` ב-Visual Studio ← Build ← F5. (או משורת הפקודה: `dotnet build BenZionVilker.sln`, ואז להריץ את `BenZionVilker/bin/Debug/net8.0-windows/BenZionVilker.exe`.)
4. **התחברות:** מזהה עובד קיים (למשל `11` — רותם סגל, חשבת) וסיסמת הדמו `1987`. אין במערכת ישות עם סיסמאות אמיתיות, ולכן מסך ההתחברות בודק סיסמה קבועה (ראו `LoginPanel.cs`).

כולם מתחברים לאותו בסיס נתונים אמיתי בענן — שינוי שנעשה אצל חברת קבוצה אחת גלוי לכולם מיד.

### בניית בסיס הנתונים מאפס

להרצה על בסיס נתונים ריק, בסדר הזה: `create_database.sql` ← `stored_procedures.sql` ← `seed_data.sql`.
לאיפוס: `empty_database.sql` (מוחק את הנתונים) ואז שוב `seed_data.sql`; או `drop_database.sql` (מוחק את הטבלאות).
קבצי `migrate_*.sql` נועדו רק לעדכון בסיס נתונים **קיים** שנבנה לפני השינוי — בבסיס נתונים חדש לא מריצים אותם.

## Use Cases במימוש

**שלושת ה-UCs הנבחרים לחלק התכנותי + ה-CRUD המלא:**

| UC | שם | שחקן ראשי | מסך | מה כולל |
|---|---|---|---|---|
| UC-03 | יצירת הזמנת רכש | Accountant | `PurchaseOrderPanel` | מחזור החיים המלא לפי תרשים המצבים (11 מצבים): הגשה, אישור חריגת תקציב, אישור/דחייה, קבלת אספקה, ארכיון; «include» שליחת מייל לספק |
| UC-05 | דוח רווחיות ותזרים מזומנים | CEO | `ProjectProfitabilityReportPanel` | רווחיות לפי פרויקט + תזרים מזומנים חודשי (טבלאות וגרפים); «extend» ייצוא ל-PDF |
| UC-06 | ניהול ערבויות וביטוחים | Finance Officer | `FinancialSecurityPanel` | ערבויות בנקאיות ופוליסות ביטוח (הורשה), התראה 30 יום לפני פקיעה |
| CRUD | ניהול עובדים (UC-02) | Site Supervisor | `EmployeePanel` | יצירה / צפייה / עדכון / מחיקה |

גם ל-UC-01 (ניהול ספקים) ול-UC-04 (יומן עבודה) יש מסכים עובדים, וכן מסכי תחזוקה לכל שאר ישויות התחום (לקוחות, תחומי עיסוק, ציוד, קבלני משנה, מכרזים, פרויקטים, שורות תקציב, בקשות תשלום, שורות הזמנת רכש, תשלומי ספקים, הצעות מחיר, נוכחות, שימוש בציוד, שיבוץ ציוד) — ראו `MainMenuPanel.cs`.

**הרחבות מעבר להיקף הבסיסי (בונוס):** שליחת מייל מדומה לספק עם אישור הזמנת רכש והתראות למנכ"ל / מנהל פרויקט / חשבת בכל מעבר מצב (UC-03); גרפים מצוירים ידנית ויצוא PDF ללא ספרייה חיצונית (UC-05); התראה אוטומטית 30 יום לפני פקיעת ערבות/ביטוח (UC-06); עיצוב ממשק אחיד (`Theme.cs`) עם תגיות סטטוס צבעוניות.

## תיעוד נוסף

- `CLAUDE.md` — תיעוד מלא של מוסכמות הארכיטקטורה, סדר טעינת הישויות, אנומרטורים והחלטות עיצוב של הפרויקט.
- `design/class-diagram.html`, `design/state-diagram.html` ו-`design/erd.html` — דיאגרמות אינטראקטיביות (לפתוח בדפדפן).

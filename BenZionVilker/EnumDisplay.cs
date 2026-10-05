namespace BenZionVilker
{
    // UI-only Hebrew display labels (course step 10 "polish" -- docs/CLAUDE.md's Language
    // Conventions requires Hebrew UI text, which the grid/combo boxes were showing raw
    // English enum names instead of). NOT used for DB communication: PurchaseOrder.cs still
    // sends status.ToString() (the English enum name matching the CHECK constraint) to the
    // stored procedures exactly as before -- this only changes what the user sees rendered.
    public static class EnumDisplay
    {
        public static string Hebrew(POStatus status)
        {
            switch (status)
            {
                case POStatus.Draft: return "טיוטה";
                case POStatus.UnderApproval: return "בבדיקה";
                case POStatus.PendingPMApproval: return "ממתין לאישור מנהל";
                case POStatus.PendingBudgetOverride: return "ממתין לאישור מנכ\"ל";
                case POStatus.Rejected: return "נדחה";
                case POStatus.InFulfillment: return "בביצוע";
                case POStatus.Sent: return "נשלח לספק";
                case POStatus.PartiallyReceived: return "התקבל חלקית";
                case POStatus.Received: return "התקבל";
                case POStatus.Cancelled: return "בוטל";
                case POStatus.Archived: return "בארכיון";
                default: return status.ToString();
            }
        }

        public static string Hebrew(DocumentType type)
        {
            switch (type)
            {
                case DocumentType.Invoice: return "חשבונית";
                case DocumentType.QuantityStatement: return "דוח כמויות";
                case DocumentType.SupervisorApproval: return "אישור מפקח";
                case DocumentType.SiteDiaryExtract: return "תמצית יומן אתר";
                case DocumentType.InsuranceCertificate: return "אישור ביטוח";
                default: return type.ToString();
            }
        }

        // "אישור מפקח, אישור ביטוח" -- or "אין" when nothing is missing, rather than an empty cell
        public static string Hebrew(System.Collections.Generic.List<DocumentType> types)
        {
            if (types.Count == 0) return "אין";
            return string.Join(", ", types.ConvertAll(Hebrew));
        }
    }
}

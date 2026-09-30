namespace BenZionVilker
{
    // Enum member names match the DB CHECK-constraint string values exactly
    // (docs/scripts/create_database.sql) -- no spaces, so no ToDisplayString
    // helper is needed (see PATTERNS.md "Enumerations").

    public enum TenderStatus
    {
        Searching, InPreparation, Submitted, Won, Lost
    }

    public enum ProjectStatus
    {
        InProgress, Completed, OnHold, Cancelled
    }

    public enum PartnerStatus
    {
        Active, Inactive
    }

    public enum POStatus
    {
        Draft, UnderApproval, PendingPMApproval, PendingBudgetOverride, Rejected,
        InFulfillment, Sent, PartiallyReceived, Received, Cancelled, Archived
    }

    public enum ClosureReason
    {
        Received, Cancelled
    }

    public enum EmployeeRole
    {
        SiteSupervisor, EquipmentManager, SafetyManager,
        ProjectManager, Accountant, FinanceOfficer, CEO, TenderCoordinator
    }

    public enum EmployeeStatus
    {
        Active, OnVacation, Suspended, Terminated
    }

    public enum EquipmentStatus
    {
        Available, InUse, UnderRepair
    }

    public enum SecurityStatus
    {
        Active, Expired, Released
    }

    public enum PaymentRequestStatus
    {
        Submitted, UnderReview, Approved, Rejected, Paid
    }

    public enum SupplierPaymentStatus
    {
        Pending, Paid, Overdue
    }

    public enum WorkLogStatus
    {
        Submitted, UnderReview
    }

    public enum DocumentType
    {
        Invoice, QuantityStatement, SupervisorApproval, SiteDiaryExtract, InsuranceCertificate
    }
}

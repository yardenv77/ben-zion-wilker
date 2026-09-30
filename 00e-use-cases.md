# Use Case Specifications

## 8.1 System Actors

| Actor (English name) | Role description and importance to the system/organization |
|---|---|
| Project Manager | The central user - tracking projects, approving orders, budget, execution and payments |
| Tender Coordinator | Locating tenders, tracking submission dates and managing status |
| Purchasing Manager | Managing suppliers/subcontractors, price comparison and purchase orders |
| Site Supervisor | Manages work in the field - daily work log, reporting on employees and equipment |
| Accountant | Issuing official purchase orders, tracking supplier payments |
| Finance Officer | Guarantees, insurance and project financial compliance |
| CEO | Profitability and cash-flow reports, approving permissions for financial information |
| Quality Inspector | Documenting execution defects and tracking their correction |
| Equipment Manager | Managing field equipment and mechanical tools |
| Safety Manager | Safety briefings and documentation of safety incidents |
| Subcontractor (secondary) | Receives digital work orders and confirms reading them |
| Supplier (secondary) | Receives purchase orders and automatic email notifications |
| MOD Portal (secondary, external system) | The Ministry of Defense portal - receives automatically synced data |

## 8.2 UC Diagrams - Note on the Generalization Relationship

> The following note is transcribed from the source document (Part 1, §8.2), explaining a modeling choice used on the UC diagrams themselves. The three UC diagrams referenced there (First UC diagram: Financial Management & Control; Second UC diagram: Procurement, Suppliers & Subcontractors; Third UC diagram) are images in the source PDF; this file transcribes the use case specifications, not the diagrams.

**Explanation of an element not covered in the course material:** the Generalization relationship between Use Cases is not covered in the course material. The course material teaches this relationship only between actors. Per the official UML specification, Generalization can also be used between Use Cases. The relationship expresses that the child UC inherits the base behavior of the parent UC and adds its own specific details. In our case, **Manage Bank Guarantee** and **Manage Insurance Policy** are two specific realizations of the general process of **Manage Guarantee & Insurance** (see UC-06 below).
Source: https://circle.visual-paradigm.com/generalization-use-case/

## 8.3 Selected Use Case Specifications

> Two-layer format: each use case has (1) a **Behavioral Spec** - analysis-level, technology-neutral, describing what the system does without naming classes, stored procedures, or database fields, followed by (2) an **Implementation Notes** section - design-level, mapping behavioral steps to specific classes, methods, and stored procedures. The source documents (Part 1 and Part 2) do not yet contain implementation notes for any use case - each Implementation Notes section below is left as a TODO placeholder rather than inventing content, per project convention.

---

### UC-01: Manage Supplier

#### Behavioral Spec

**Use Case Number:** UC-01
**Use Case Name:** Manage Supplier

**Pre-conditions:**
1. The Purchasing Manager is authenticated and logged into the system.
2. The system database is active and accessible.

**Post-conditions:**
The Supplier registry is updated, and changes are committed to the system database.

**Permissions:**
- Purchasing Manager: Full CRUD permissions (Create, Read, Update, Soft Delete).
- Accountant: Read-only permissions.

**Supplier Entry Form Fields (for Create/Update):**
- Supplier ID (System-generated, Read-Only)
- Supplier Name (Text, Required, Unique)
- Company Registration No. (9-digit Number, Required, Unique)
- Category/Trade (Dropdown Selection, Required)
- Contact Person (Text, Required)
- Phone Number (Numeric, Format: 05X-XXXXXXX, Required)
- Email (Email Format, Required)
- Initial Supplier Rating (Decimal, Default: 5.0)
- Supplier Status (Active / Inactive, Default: Active)

**Main Success Scenario (MSS) - Read & Search:**
1. The Purchasing Manager selects the "Manage Suppliers" option from the main navigation menu.
2. The System retrieves and displays the active list of all suppliers.
3. The Purchasing Manager inputs search or filter criteria.
4. The System filters the list in real-time and displays matching supplier records.

**Extensions (Exceptions):**
- 3a. No Matching Supplier Found:
  1. The System displays a warning message: "No suppliers match the search criteria".
  2. The System prompts the user to clear filters or add a new supplier.
  3. The flow returns to step 3.

**Scenarios:**

*Scenario A: Create Supplier (Create)*
1. The Purchasing Manager clicks on "Add New Supplier".
2. The System displays an empty Supplier Entry Form.
3. The Purchasing Manager fills in the required fields and clicks "Save".
4. The System validates the fields (unique Name and Registration Number, required fields filled).
5. The System saves the new Supplier record and displays a success message.
- 4a. Validation Fails: The System highlights invalid fields and prevents saving.

*Scenario B: Update Supplier (Update)*
1. The Purchasing Manager selects a supplier and clicks "Edit".
2. The System displays the form pre-populated with the supplier's data.
3. The Purchasing Manager updates fields and clicks "Save".
4. The System validates and updates the record.

*Scenario C: Soft Delete Supplier (Delete)*
1. The Purchasing Manager selects a supplier and clicks "Delete".
2. The System displays a confirmation prompt.
3. The Purchasing Manager confirms.
4. The System updates the supplier status to "Inactive" (soft delete).

#### Implementation Notes

> TODO - not yet specified in the source documents.

---

### UC-02: Manage Employee

#### Behavioral Spec

**Use Case Number:** UC-02
**Use Case Name:** Manage Employee

**Pre-conditions:**
1. The Site Supervisor is logged into the system with appropriate access credentials.

**Post-conditions:**
The employee registry and skill sets are updated in the central database.

**Permissions:**
- Site Supervisor: Full CRUD permissions (Create, Read, Update, Delete).
- Project Manager: Read-only permissions.

**Employee Data Fields (for Create/Update):**
- Employee ID (System-generated, Read-Only)
- First Name & Last Name (Text, Required)
- National ID / ID Number (9-digit Number, Required, Unique)
- Role (Dropdown: Site supervisor / Equipment Manager / Safety Manager, Required)
- Skills (Multi-select list: Excavation, Welding, Safety Inspection, Concrete)
- Daily Rate (Currency, Required)
- Certification/License No. (Alphanumeric, Optional)
- Status (Dropdown: Active / On Vacation / Suspended / Terminated)

**Main Success Scenario (MSS) - Read & Search:**
1. The Site Supervisor navigates to the "Manage Employees" portal.
2. The System retrieves and displays a comprehensive list of all active construction workers and engineers.
3. The Site Supervisor filters the list by "Location/Project" or "Skill Category".
4. The System displays the matching employee profiles with their current status.

**Extensions (Exceptions):**
- 3a. Empty Search Results:
  1. The System displays a message: "No employees match the selected criteria".
  2. The flow returns to step 3.

**Scenarios:**

*Scenario A: Add New Employee (Create)*
1. The Site Supervisor clicks on "Add Employee".
2. The System displays the Employee Form with all the fields listed above.
3. The Site Supervisor inputs the new employee's details and clicks "Save".
4. The System validates that the ID number is unique and required fields are filled.
5. The System commits the new employee record in "Active" status.

*Scenario B: Update Employee Profile & Skills (Update)*
1. The Site Supervisor selects an employee and clicks "Edit Profile".
2. The System displays the current employee profile.
3. The Site Supervisor updates daily rate, status, or adds a skill.
4. The Site Supervisor clicks "Save Changes" and the System updates the database.

*Scenario C: Deactivate Employee (Delete)*
1. The Site Supervisor selects an employee who has left and clicks "Deactivate".
2. The System checks if the employee is currently scheduled or active.
3. If free of assignments, the System changes status to "Terminated" (soft delete) and blocks login access.

#### Implementation Notes

> TODO - not yet specified in the source documents.

---

### UC-03: Create Purchase Order

#### Behavioral Spec

**Domain:** Procurement — **Primary Actor:** Accountant | **Secondary Actors:** Project Manager (approver), Supplier (recipient)
**Related Business Process:** 2.2 Purchasing Management and Contractor Finalization (Part 1)
**Related Functional Requirements:** (see docs/00-requirements.md)

**Pre-conditions:**
1. The Accountant is logged in.
2. The target Supplier exists in the system database and is "Active".
3. An approved Project and its corresponding Budget Line exist.

**Post-conditions:**
- A Purchase Order (PO) is saved in the database under status "Pending Approval".
- Include Trigger: The included Use Case "Send Purchase Order Email to Supplier" (UC-03.Include) is successfully executed.

**Relationship:** «include» Send Purchase Order Email to Supplier (UC-03.Include)

**Main Success Scenario (MSS):**
1. The Accountant clicks on "Create New Purchase Order".
2. The System displays the Purchase Order Generation Screen.
3. The Accountant selects an active Project and a Supplier.
4. The System auto-populates Supplier contact info and validates remaining budget.
5. The Accountant adds item lines to the Purchase Order (description, unit of measure, quantity, unit price).
6. The System calculates line totals, VAT, and total PO amount in real-time.
7. The Accountant submits the Purchase Order.
8. The System checks if the PO total exceeds the predefined budget limit.
9. The System saves the PO in "Pending PM Approval" status and alerts the Project Manager.
10. The Project Manager reviews and clicks "Approve".
11. The System updates the PO status to "Approved & Active".
12. Execute Include: The System triggers UC-03.Include: Send Purchase Order Email to Supplier.

**Extensions (Exceptions):**
- 4a. Selected Supplier is Inactive:
  1. The System blocks PO creation and displays an error message.
  2. The flow returns to step 3.
- 8a. Purchase Order Exceeds Remaining Budget Limit:
  1. The System flags the transaction and displays a warning.
  2. The System prompts the Accountant to modify quantities or request a Budget Override from the CEO.
  3. If override requested: routes authorization task to CEO, halts flow in "Pending CEO Budget Override" status.
- 10a. Project Manager Rejects the Purchase Order:
  1. The Project Manager inputs a rejection reason and clicks "Reject".
  2. The System updates PO status to "Draft - Rejected".
  3. The System notifies the Accountant.
  4. The flow terminates.

**Purpose (rationale, from Part 2):** Today the company discovers a budget overrun only after the fact, once an invoice is received from the supplier, because of data being scattered across separate Excel files per project and the lack of a consistent, real-time basis for comparison (business problem #1, Part 1). A digital purchase order that checks the remaining budget before the order is sent to the supplier - not after the invoice is received - moves the control point earlier in time, and prevents budget overruns that today are only discovered after the fact.

#### Implementation Notes

> TODO - not yet specified in the source documents.

---

### UC-04: Record Daily Work Log

#### Behavioral Spec

**Domain:** Field Execution — **Primary Actor:** Site Supervisor
**Related Functional Requirements:** FR10, FR20

**Pre-conditions:**
1. The Site Supervisor is logged into the mobile application.
2. The supervisor is assigned to an active construction site.

**Post-conditions:**
- The daily work log (manpower, equipment hours, material quantities) is stored in the database.
- Extend Trigger: If the reported quantity falls below the planned threshold, the Use Case "Track Subcontractor Progress vs Plan" is executed.

**Relationship:** «extend» Track Subcontractor Progress vs Plan (UC-04.Extend), when the condition [reported quantity falls below planned threshold] holds.

**Main Success Scenario (MSS):**
1. The Site Supervisor selects "New Daily Log".
2. The System displays the daily input form, auto-populated with date, site, and scheduled employees.
3. The Site Supervisor logs attendance and hours worked for each worker.
4. The Site Supervisor enters operating hours for heavy machinery.
5. The Site Supervisor inputs completed quantities of work for the day.
6. The Site Supervisor submits the daily log.
7. The System validates entries and registers the log under "Submitted" status.
8. The System compares reported completed quantity against the planned schedule.
9. The System determines that progress is within acceptable parameters.
10. The System displays a success confirmation message.

**Extensions (Exceptions):**
- 7a. Missing Critical Fields:
  1. The System highlights missing fields and prevents submission.
  2. The flow returns to step 5.
- 8a. Reported quantity falls below planned threshold (Extend Point):
  1. The System detects that completed quantities are significantly below the planned threshold.
  2. Execute Extend: If the condition [reported quantity falls below planned threshold] is met, the system triggers the extending Use Case: UC-04.Extend: Track Subcontractor Progress vs Plan.
  3. The flow resumes at step 10.

**Purpose (rationale, from Part 2):** Today the Project Manager discovers a delay in execution only in the evening, when the Site Supervisor phones in a report of lower-than-planned progress - after a work day has already been lost. A digital work log, accessible from the field, with automatic comparison against the plan, gives a true, real-time picture instead of after-the-fact discovery, and allows the manager to respond to a delay on the same day rather than the next day.

#### Implementation Notes

> TODO - not yet specified in the source documents.

---

### UC-05: Generate Profitability & Cash Flow Report

#### Behavioral Spec

**Domain:** Executive Finance — **Primary Actor:** CEO
**Related Functional Requirements:** FR07

**Pre-conditions:**
1. The CEO is logged into the executive dashboard with financial access privileges.
2. Actual cost data and revenue data are populated in the database.

**Post-conditions:**
The dynamic Profitability & Cash Flow Report is calculated, compiled, and rendered on the dashboard.

**Relationship:** «extend» Export Report to PDF (UC-05.Extend), when the condition [user requests export] holds.

**Main Success Scenario (MSS):**
1. The CEO selects "Generate Profitability & Cash Flow Report" from the financial menu.
2. The System displays the Report Parameters Screen.
3. The CEO selects the target Project(s) and specifies the fiscal date range.
4. The CEO clicks on "Generate".
5. The System retrieves all recorded project revenue from approved payment requests.
6. The System aggregates all actual costs associated with the project.
7. The System calculates the net profit, percentage margin, and monthly cash flow trends.
8. The System renders the interactive report dashboard with tables and charts.
9. The CEO views the report.

**Extensions (Exceptions):**
- 5a. No Financial Data Exists for the Selected Range:
  1. The System displays an information message.
  2. The System prompts the CEO to change the filters.
  3. The flow returns to step 3.
- 8a. CEO Requests Document Export (Extend Point):
  1. The CEO clicks on the "Export to PDF" button on the dashboard.
  2. Execute Extend: Since the condition [user requests export] is met, the system triggers the extending Use Case: UC-05.Extend: Export Report to PDF.
  3. The flow terminates.

**Purpose (rationale, from Part 2):** Today the CEO has no up-to-date picture of profitability and cash flow in real time - the information is scattered between external client portals and separate Excel files per project, and every report requires manual collection. A consolidated, dynamic report makes it possible to identify unprofitable projects or budget overruns early, and to plan the need for external financing in advance instead of discovering it after the fact.

#### Implementation Notes

> TODO - not yet specified in the source documents.

---

### UC-06: Manage Guarantee & Insurance

> This use case is specified only in Part 2 (as rationale/context for the class diagram exercise), at a lighter level of detail than UC-01 through UC-05 above - it has a description and purpose, but the source does not give it formal Pre-conditions, Post-conditions, Permissions, a full Main Success Scenario, or Extensions. Those fields are left as TODO rather than invented. See also §8.2 above (the Generalization note) - this is the parent UC that "Manage Bank Guarantee" and "Manage Insurance Policy" specialize.

#### Behavioral Spec

**Domain:** Risk & Compliance — **Primary Actor:** Finance Officer
**Related Functional Requirements:** FR06
**Relationship:** Generalization → Manage Bank Guarantee, Manage Insurance Policy (two child UCs inheriting the base behavior and adding their own specific details).

**Pre-conditions:** TODO - not specified in the source.

**Post-conditions:** TODO - not specified in the source.

**Permissions:** TODO - not specified in the source.

**Description:** The Finance Officer manages the full lifecycle of financial securities (bank guarantees and insurance policies) required for each project. This includes creating a new security record (amount, issue date, expiry date, and status), viewing and searching all active or expired securities across projects, updating their status as they progress, and marking them released or expired. Because bank guarantees and insurance policies share the same lifecycle and alerting behavior but differ in their type-specific fields (guarantee number and issuing bank vs. policy number, insurer, and coverage type), the general use case "Manage Guarantee & Insurance" is specialized into two child use cases - "Manage Bank Guarantee" and "Manage Insurance Policy" - each inheriting the shared flow and adding its own specific fields. In all cases, the system automatically alerts the Finance Officer 30 days before a security's expiry date.

**Main Success Scenario (MSS):** TODO - not specified in the source (only the narrative description above is given).

**Extensions (Exceptions):** TODO - not specified in the source.

**Purpose (rationale, from Part 2):** The company currently manages guarantees and insurance policies in separate files with no centralized expiry tracking (business problem #9, Part 1) - creating a real risk of a guarantee or insurance policy lapsing without notice, which could lead to a breach of contract terms with the Ministry of Defense. Centralized management with automatic alerts removes this risk, and demonstrates full consistency with the Generalization relationship mirrored in the class diagram between `FinancialSecurity` and `BankGuarantee`/`InsurancePolicy`.

#### Implementation Notes

> TODO - not yet specified in the source documents.

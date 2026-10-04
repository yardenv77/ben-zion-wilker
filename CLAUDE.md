# CLAUDE.md — Ben Tzion Wilker (1987) Ltd. Project Management System

## What This Project Is

This is a university course project for **Software Analysis and Design (SAD)**, Industrial Engineering and Management, Ben-Gurion University. The team is building a **C# WinForms order/project-management system** for **Ben Tzion Wilker (1987) Ltd.**, a mid-sized construction and infrastructure company in Tel Aviv (renovations, earthworks, construction and development), operating at roughly ₪300M/year, working mostly for public-sector clients (Ministry of Defense, Rafael, other government bodies).

The organization today has **no internal information system at all**. Day-to-day work runs entirely through the external portals of whichever client issued the work (Ministry of Defense portal, Rafael's systems, Israel Railways' StoreNext), supplemented by disconnected per-project Excel files, WhatsApp, phone calls and paper forms. Because income data (orders/tenders from client portals) and expense data (supplier invoices, labor, materials, tracked in separate spreadsheets) are never synchronized, budget overruns and profitability erosion are only discovered after the fact. There is no centralized view of project status, tender deadlines, or guarantee/insurance expiry across the company. Full detail: `docs/org-analysis/01-organization.md` through `04-business-processes.md`.

The system being built both **upgrades existing processes** (tender tracking with automatic deadline reminders, automatic comparison of incoming client orders against the winning bid, a digital field work log replacing paper forms, structured quantity-report export) and **introduces processes that don't exist today** (continuous per-project budget control with automatic overrun alerts, formal guarantee/insurance expiry tracking, a consolidated multi-project management dashboard).

This is a teaching project, not a production system — it demonstrates the full analysis → design → development pipeline (organizational analysis → requirements → use cases → class diagram → code) and where AI assistance fits versus where human analysis judgment is required.

---

## Architecture Conventions (inherited from the course's shared `PATTERNS.md`)

> The following is inlined verbatim from the SAD course's shared `cloned/PATTERNS.md`, which documents the conventions every SAD student project follows. This project's own domain-specific decisions are in the sections after this one.

### The Human vs. AI Distinction — Course-Wide Principle

This distinction is central to how you should approach work in this course. Do not blur it.

**Humans must do this** (AI cannot substitute, even though AI will later use the output):
- Organizational context: who the business is, what problem it has, why it matters
- Stakeholder identification and needs elicitation
- Problem statement and project scope
- Initial domain modeling: what entities exist, what relationships make sense
- Deciding which use cases exist and which don't (e.g., deciding Login is an NFR, not a UC)
- Prioritization and tradeoffs

**AI can accelerate this** (once the human thinking is done):
- Structuring requirements into user story format
- Writing VP18-style UC specs from a brief description
- Generating the UC diagram HTML from the spec data
- Generating entity classes, stored procedures, and panels from UC specs + implementation notes
- Checking consistency between artifacts (traceability)

Project artifacts are produced in this order: organization/problem context → requirements → UC specs → code. Each is input to the next.

### Architecture — Key Patterns

#### Entity Pattern
Every entity class is self-contained. Each one owns:
- Private fields + getters/setters
- Constructor with `bool is_new` — if `true`, calls `getNextXYZId()` to assign a new PK, then calls `createXYZ()` and only adds to `Program.list` if it returned `true`; if `false`, just sets fields (used during loading)
- `createXYZ()`, `updateXYZ()`, `deleteXYZ()` — each builds a `SqlCommand` with a stored procedure and returns `bool` (success/failure, propagated from `SQL_CON.execute_non_query`). `SQL_CON` shows a MessageBox itself only on failure (translated Hebrew for FK/CHECK violations, generic Hebrew otherwise) — it never shows a success message, so callers use the returned `bool` to gate their own success messaging and in-memory list updates (a failed `deleteXYZ()` must not remove the entity from `Program.list`). Table-per-subclass entities (`Supplier`/`Subcontractor`, `BankGuarantee`/`InsurancePolicy`) run their parent-row + child-row commands together via `SQL_CON.execute_non_query_transactional(...)`, so a failure on either row rolls back both instead of leaving an orphaned parent-only row.
- `static initXYZs()` — loads all records from DB into `Program.XYZs`, always calls constructor with `is_new = false`
- `static seekXYZ(id)` — searches `Program.XYZs` by ID
- `static getNextXYZId()` — returns `max(id) + 1` over `Program.XYZs` (or `1` if the list is empty). See "Primary Key Strategy" below.

#### Primary Key Strategy
**Primary keys are assigned in C#, not by the database.**

- DDL: every PK is `INT NOT NULL PRIMARY KEY`. Do **not** use `IDENTITY(1,1)`.
- The entity class's `static getNextXYZId()` returns `max(id) + 1` over the in-memory list.
- The `is_new` constructor calls `getNextXYZId()` before `createXYZ()` to assign the new row's PK.
- Create stored procedures take the PK as the first parameter (`@<entity>_id`). They do **not** use `SCOPE_IDENTITY()` and do **not** return the new ID.

This is deliberate: students can read the full lifecycle of an ID in one place (the C# constructor), and DB writes are deterministic from the entity's state. Concurrency is not a concern in the single-user teaching context.

#### Enumerations — CHECK Constraint, Never a Lookup Table
An attribute with a fixed set of values (status, type, role, category) is an **enumeration**, not a related entity.

- **DB:** store it as text on the entity's own column (`NVARCHAR(20)`) with a `CHECK (<column> IN (N'…', N'…'))` constraint listing every legal value. **Do not create a lookup/reference table, and do not make it a foreign key.**
- **C#:** declare a matching `enum`. Where the values contain spaces, use underscores in the enum and a small `XyzHelper` with `ToDisplayString()` / `FromDisplayString()` to convert.
- **UI:** populate combo boxes from `Enum.GetValues(typeof(Xyz))` — never from a DB query.
- The `CHECK` list and the `enum` must stay identical; adding a value means editing both and re-running the schema script.

A separate table is for data users create and change. A fixed value list is already declared in code — a second copy in a table adds a join, a load step and a way for the two to drift apart.

#### In-Memory Lists
All data lives in `Program.*` static lists after startup. No DB calls during normal use except writes.

`initLists()` load order is strict: **base entities first, then entities with FK references, then association classes last.** This project's concrete load order is given below in "Domain Entities and Load Order".

#### DB Operations
All DB access goes through stored procedures. **No ad-hoc SQL strings in application code.** This is an NFR.

#### Panel Navigation
Single-window model. All screens are `UserControl` panels. Navigation: `mainForm.showPanel(new XYZPanel())`. Every panel has a Back button. **No additional Forms or dialogs during normal operation.**

#### Inheritance — Table-per-Subclass
When an entity has subtypes, use table-per-subclass: a base table for the parent + one table per subclass holding only the subclass's unique fields + a FK to the base table. Load with a LEFT JOIN and check for `DBNull.Value` to determine subtype.

This project has two such hierarchies: `BusinessPartner` → `Supplier` / `Subcontractor`, and `FinancialSecurity` → `BankGuarantee` / `InsurancePolicy`.

#### Association Class
When a many-to-many relationship has its own attributes, model it as an association class linking the two sides. In the C# class, both sides are stored as **object references, not IDs**.

`SupplierPriceQuote` was originally modeled as a **ternary** association class (linking `Supplier`, `Tender` and `TradeCategory` simultaneously). The updated class diagram reframes it as a **mediating class** instead: three separate binary associations (`Supplier`-`SupplierPriceQuote`, `TradeCategory`-`SupplierPriceQuote`, `Tender`-`SupplierPriceQuote`), each `1 -- 0..*`. This is a UML notation change only — the underlying table (`supplier_id`, `tender_id`, `trade_category_id`, plus its own PK) is identical either way, so nothing in `scripts/create_database.sql` or the C# entity needs to change because of it.

#### No Service Layer
Entity classes own their own DB methods. One file per entity. This is intentional for teaching — students see the full lifecycle of an entity in one place.

### UC Diagram — Conventions

The diagram is generated from inline JavaScript data and rendered by an external shared script. Rules:

- All data globals must use `var` (not `const`/`let`) so they become `window` properties
- Wireframes are embedded as `useCaseDocs[id].wireframe` HTML strings — **not** as separate files
- All wireframe visible text must be in Hebrew; all form fields use `disabled`; no `<script>` tags inside wireframes
- The `[hidden] { display: none !important; }` style override is required in `<head>` for tab switching to work
- **Login/authentication must never appear as a UC.** Note it only in the `assumptions` array.

#### Two-Layer UC Spec Format
Each detailed UC has two sections:
1. **Formal spec** (analysis level) — behavioral, technology-neutral. No class names, SP names, or field names.
2. **Implementation Notes** (design level, clearly labelled) — maps behavioral steps to specific classes, methods, and stored procedures.

This separation is intentional and pedagogically important. Do not merge them.

### Language Conventions

| Context | Language |
|---|---|
| C# code (classes, methods, variables) | English |
| UI labels, button text, MessageBox text | Hebrew |
| DB text fields | Hebrew — use `NVARCHAR`, never `VARCHAR` |
| Student guide docs (`docs/*.md`) | Hebrew |
| Requirements and UC spec documents | English |
| UC diagram text (actor labels, UC names, flow steps) | Hebrew |

Note: this project's own organizational-analysis docs (`org-analysis/01`–`04`) are in Hebrew (raw interview/analysis material); `00-requirements.md` and `00e-use-cases.md` are in English (translated formal specs), matching the "Requirements and UC spec documents | English" row above.

#### RTL Layout for Hebrew UI

Every `Form` and `UserControl` with Hebrew visible text **must** be set up for right-to-left rendering:

- `RightToLeft = Yes` on the form/panel (mirrors text direction, button alignment, scrollbar position).
- `RightToLeftLayout = true` on the root form (mirrors the entire layout, including TabControl direction and DataGridView column order).
- Set these on the parent — children inherit unless overridden.

Generate panels with these properties set from the start. Retrofitting RTL onto LTR-built panels is painful — labels overlap, alignment breaks, the designer file fights you.

### Git

The repository is the group's shared state; the database is not. Anything a teammate needs in order to reproduce the project must be committed.

- **Commit at the phase boundaries** listed in the course's `LESSON_STEPS`. `git pull` before starting work, push when the phase is done.
- **Never commit credentials or build output:** `.mcp.json`, `app.config`, `cloned/`, `bin/`, `obj/`. The `.gitignore` already covers these — do not override it. If credentials are pushed by mistake, rotate the password: deleting the file does not remove it from history.
- **Schema changes go into `scripts/*.sql` and get committed.** Never apply a change by hand to the database alone — the database is a derived artifact, and a teammate who pulls and re-runs the scripts would silently lose the change.
- **Claude runs the git commands.** Students should not run git in the terminal: it is often not on the terminal's PATH on student machines, which produces a confusing "command not found".

### Database

The project database is **`BenZionVilker`** on Azure SQL (**`sad-groupname-sql-yarden.database.windows.net`**). The `mssql` MCP server connects directly to this database, so **no `USE` statement is needed** at the start of a batch — and none should be added: **Azure SQL does not support `USE`** to switch databases within a connection.

### Decisions Already Made — Do Not Revisit Without Discussion

These apply across all SAD projects:

- **Login is not a UC.** Authentication is an NFR precondition. A `LoginPanel` is a technical artifact. Do not add Login to UC diagrams or UC specs.
- **Wireframes belong inside the UC diagram modal**, not in separate files.
- **No ad-hoc SQL.** All DB operations use stored procedures.
- **No service layer.** Entity classes own their own DB methods.
- **Single window, panel navigation.** No additional forms or dialogs during normal operation.

---

## Document Map

| File | Purpose |
|---|---|
| `org-analysis/01-organization.md` | Organization description and current (non-)state of information systems (Hebrew) |
| `org-analysis/02-interviews.md` | Interview transcripts (CEO, administration manager) + document/screenshot technique + survey findings (Hebrew) |
| `org-analysis/03-problems.md` | 10 documented business problems, each with cause, impact, stakeholder, desired solution (Hebrew) |
| `org-analysis/04-business-processes.md` | 3 BPMN business processes (tendering; order intake & field planning; execution day & final accounting) + description of upgraded/new processes (Hebrew) |
| `00-requirements.md` | 27 Functional Requirements + 7 NFRs + traceability matrix (English) |
| `00e-use-cases.md` | Two-layer VP18-style UC specs for UC-01 through UC-06 (English) |
| `design/class-diagram.md` / `design/class-diagram.html` | Full domain class diagram: 23 classes, 13 enumerations, 1 external system, all relationships (English; `.html` is the interactive source) |
| `design/state-diagram.md` / `design/state-diagram.html` | `PurchaseOrder` lifecycle state diagram: 11 states, all transitions with triggers/guards/side-effects, business rules (English; `.html` is the interactive source) |
| `design/erd.md` / `design/erd.html` | Entity-relationship diagram of the actual database schema per `scripts/create_database.sql`: 23 real tables, real columns/types, real FKs, real CHECK constraints — the physical counterpart to `design/class-diagram.md`'s UML domain model (English; `.html` is the interactive source) |
| `Part1_Group5.pdf`, `Part2_Group5.pdf` | Original submitted documents (backup — prefer the markdown above; consult these only when the markdown is unclear or a diagram image is needed) |

---

## Domain Entities and Load Order

Per `PATTERNS.md`, `initLists()` load order is strict: base entities first, then entities with FK references, then association/link classes last. Based on the relationships in `design/class-diagram.md`, this project's load order is:

**Phase 1 — Base entities (no FK to another domain entity):**
```
Client → TradeCategory → Employee → Equipment → Supplier/Subcontractor (BusinessPartner subclasses) → DailyWorkLog (FK: Subcontractor, Employee)
```

**Phase 2 — Entities with FK references:**
```
Tender (FK: Client)
  → Project (FK: Tender, Employee[projectManager])
    → BankGuarantee/InsurancePolicy (FinancialSecurity subclasses; FK: Project)
    → BudgetLine (FK: Project)
    → PaymentRequest (FK: Project)
      → SubmittedDocument (FK: PaymentRequest)
    → PurchaseOrder (FK: Supplier, Project, Employee[createdBy/approvedBy/overrideApprovedBy])
      → PurchaseOrderLine (FK: PurchaseOrder)
SupplierPayment (FK: BusinessPartner)
```

**Phase 3 — Association / link classes (loaded last):**
```
SupplierPriceQuote (Supplier + Tender + TradeCategory, three binary associations)
Attendance (Employee + DailyWorkLog)
EquipmentUsage (Equipment + DailyWorkLog)
EquipmentAssignment (Project + Equipment)
```

`Supplier`/`Subcontractor` and `BankGuarantee`/`InsurancePolicy` are each loaded as a single table-per-subclass step (LEFT JOIN + `DBNull.Value` check), per the Inheritance pattern above. `FinancialSecurity` (and its subclasses) moved from Phase 1 to Phase 2 once it gained a required FK to `Project` (see below) — it now depends on `Project`, so it can no longer load before it.

The two gaps previously flagged here (`Project`↔`PurchaseOrder`, `Project`↔`FinancialSecurity`) are resolved: the updated `design/class-diagram.md` now has both as direct relationships (§3, #9 and #24), and both FKs are in `scripts/create_database.sql`.

### Enumerations

| Enumeration | Values |
|---|---|
| TenderStatus | Searching, InPreparation, Submitted, Won, Lost |
| ProjectStatus | InProgress, Completed, OnHold, Cancelled |
| PartnerStatus | Active, Inactive |
| POStatus | Draft, UnderApproval, PendingPMApproval, PendingBudgetOverride, Rejected, InFulfillment, Sent, PartiallyReceived, Received, Cancelled, Archived |
| ClosureReason | Received, Cancelled |
| EmployeeRole | SiteSupervisor, EquipmentManager, SafetyManager, ProjectManager, Accountant, FinanceOfficer, CEO, TenderCoordinator |
| EmployeeStatus | Active, OnVacation, Suspended, Terminated |
| WorkLogStatus | Submitted, UnderReview |
| EquipmentStatus | Available, InUse, UnderRepair |
| SecurityStatus | Active, Expired, Released |
| PaymentRequestStatus | Submitted, UnderReview, Approved, Rejected, Paid |
| DocumentType | Invoice, QuantityStatement, SupervisorApproval, SiteDiaryExtract, InsuranceCertificate |
| SupplierPaymentStatus | Pending, Paid, Overdue |

`DailyWorkLog.status` was originally a deliberate free `String` (per an earlier model assumption), but the updated class diagram converts it to the `WorkLogStatus` enumeration above — a confirmed decision, not a leftover inconsistency.

`POStatus` was revised to its current 11 values (from an original 5) by `design/state-diagram.md`'s `PurchaseOrder` lifecycle diagram — see `design/class-diagram.md` §5 for the full rationale (`Approved` removed, `PendingApproval` renamed to `PendingPMApproval`, `Draft`/`UnderApproval`/`InFulfillment`/`PartiallyReceived`/`Received`/`Cancelled`/`Archived` added, plus the new `rejectionReason`/`closureReason`/`ClosureReason`/`receivedQuantity` fields). This is the expected way a state diagram in this course is allowed to correct its class diagram, not an inconsistency to avoid. `00e-use-cases.md`'s UC-03 MSS text still needs to be aligned to the confirmed approval order (CEO budget override happens *before* PM approval, per BR-2) — flagged here, not rewritten, since editing a UC spec's behavioral text is a team decision. Also flagged (not rewritten): the same UC-03 text uses status labels (`"Pending Approval"`, `"Approved & Active"`, `"Pending CEO Budget Override"`, `"Draft - Rejected"`) that predate this enum, and `"Approved & Active"` in particular no longer corresponds to any state (approval now leads straight to `Sent`).

`EmployeeRole` was widened from 3 field-workforce values to all 8 system-actor roles shown above, merging what was previously a separate, unenforced permissions list (`00e-use-cases.md` §8.1, still described below in "Use Cases to Implement" for UC-permission purposes) directly onto the `Employee` entity — a confirmed decision, needed so `Project`'s `projectManager` role and `PurchaseOrder`'s `createdBy`/`approvedBy`/`overrideApprovedBy` roles (see `design/class-diagram.md` §4e) can reference real `Employee` rows.

### Type Mapping (class diagram → C# / SQL)

| Class diagram type | C# type | SQL type |
|---|---|---|
| String | `string` | `NVARCHAR(n)` |
| Integer | `int` | `INT` |
| Real | `double` | `FLOAT` |
| Boolean | `bool` | `BIT` |
| Date | `DateTime` | `DATE` or `DATETIME` |
| Money | `decimal` | `DECIMAL(18,2)` |
| List (e.g. `getActiveProjects():List`) | method return only, not a stored field — computed from `Program.*` lists | — |

### External System

`MODPortal` (Ministry of Defense portal) is an external system, not a domain class — do not create an entity, table, or `Program.*` list for it. It's referenced only as the target of the (currently out-of-scope) MOD-sync functionality (FR17).

---

## Use Cases to Implement

The five actors and roles most relevant to system permissions: Project Manager, Tender Coordinator, Purchasing Manager, Site Supervisor, Accountant, Finance Officer, CEO, Quality Inspector, Equipment Manager, Safety Manager (full actor table: `00e-use-cases.md` §8.1). `Subcontractor`, `Supplier` and `MODPortal` are secondary/external actors, not system users.

`00e-use-cases.md` fully specifies six use cases (two-layer format: behavioral spec + Implementation Notes, currently `TODO` for all six pending design work):

| UC | Name | Primary Actor | Relationship |
|---|---|---|---|
| UC-01 | Manage Supplier | Purchasing Manager | — |
| UC-02 | Manage Employee | Site Supervisor | — |
| UC-03 | Create Purchase Order | Accountant | «include» Send Purchase Order Email to Supplier (UC-03.Include) |
| UC-04 | Record Daily Work Log | Site Supervisor | «extend» Track Subcontractor Progress vs Plan (UC-04.Extend), when reported quantity falls below planned threshold |
| UC-05 | Generate Profitability & Cash Flow Report | CEO | «extend» Export Report to PDF (UC-05.Extend), when export requested |
| UC-06 | Manage Guarantee & Insurance | Finance Officer | Generalization parent of `FinancialSecurity`'s two subclasses |

UC-06 is documented and implemented as a **single use case**: the source specifies only UC-06 itself (with `BankGuarantee` and `InsurancePolicy` handled as the two `FinancialSecurity` subclasses within it, per the Inheritance pattern above), not two separately-specified child use cases "Manage Bank Guarantee" / "Manage Insurance Policy" — those exist only as a class-level generalization note in `00e-use-cases.md` §8.2, without their own MSS/steps. Do not invent MSS/Extensions content for them; if the team wants them as separate UCs, that's a use-case-identification decision for the team, not something to add unilaterally.

`00-requirements.md` §9 traces all 27 FRs to a fuller set of ~26 use case names (e.g. "Manage Tender", "Sync Data with MOD Portal", "Manage Equipment") beyond the six detailed above — those exist at the requirements-traceability level but are not yet specced or in scope for implementation until the team writes their formal specs.

Every UC's Implementation Notes section is currently `TODO` in `00e-use-cases.md` — per the source documents' own convention, this was left unspecified rather than invented. Implementation Notes (mapping MSS steps to specific classes/methods/stored procedures) should be filled in per-UC as each is actually implemented, not guessed in advance.

---

## Project-Specific Decisions

Beyond the cross-project decisions in the Architecture Conventions section above:

- **Full class diagram is the target domain model.** All 23 classes and their load order (above) are documented now, even though only six use cases currently have full behavioral specs — code should be built incrementally against the full model rather than a UC-reduced subset, matching how `design/class-diagram.md` was already designed as one complete diagram. Note: the current `class-diagram.html` source itself annotates a smaller "programming part" subset (green outline, ~13 classes covering 4 UCs) — this project has explicitly decided to keep the full-model approach instead, since entity/panel code already exists for effectively every class (see `design/class-diagram.md` §5's "Scope of the programming part" note).
- **`SupplierPriceQuote` as a mediating class.** Modeled as three binary associations (`Supplier`, `Tender`, `TradeCategory`, each `1--0..*`) rather than a single ternary association class. Same underlying table shape either way — see the "Association Class" pattern note above.
- **`PurchaseOrder`↔`Project` and `FinancialSecurity`↔`Project`** relationships, previously flagged as a gap, are now resolved: both are direct associations in `design/class-diagram.md` §3 (#9 and #24) and both FKs exist in `scripts/create_database.sql`.
- **`EmployeeRole` merge and `DailyWorkLog.status` → `WorkLogStatus`** are both confirmed decisions (see "Enumerations" above), not oversights — reversing two earlier model assumptions after the team reviewed the updated class diagram.

---

## Entry Flow

**`LoginPanel` exists, but it is a technical artifact, not a domain feature.** The original reasoning below (no entry point derivable from the domain model) is still correct and still the reason there is no *real* authentication anywhere in this system — it's preserved here because it's the honest answer to give at the oral exam if asked why login isn't backed by a real credential entity. `LoginPanel` was added afterward, specifically to satisfy `cloned/docs/12-oral-exam-guide.html`'s explicit grading requirement for a login screen (~50% "עמידה בדרישות" component lists "מסך התחברות (login)" alongside the UCs and CRUD screens). Per PATTERNS.md ("Login is not a UC. Authentication is an NFR precondition. A LoginPanel is a technical artifact.") this still never appears in the UC diagram or in `00e-use-cases.md` — that rule is unaffected by whether a LoginPanel physically exists.

- **No entity in `design/class-diagram.md` holds credential-like fields.** `Client` and `BusinessPartner` each have an `email` attribute, but neither they nor any of the other 23 classes have a password (or any other authentication) field. There is no credential-holding entity to check a login against.
- **`LoginPanel` therefore does not check real credentials**, mirroring the course's own sample project (`cloned/example_project/LoginPanel.cs`, which looks up a `Worker` by ID and checks a hardcoded password, not a real stored one): it looks up an existing `Employee` by `employeeId` and checks a fixed, hardcoded demo password (`LoginPanel.cs`'s `DemoPassword` constant — not a field on any entity, not persisted anywhere). This is a deliberate, documented simplification, not an oversight — say so plainly if asked.
- The 10 human actors (`00e-use-cases.md` §8.1: Project Manager, Tender Coordinator, Purchasing Manager, Site Supervisor, Accountant, Finance Officer, CEO, Quality Inspector, Equipment Manager, Safety Manager) are still not distinguished by the UI beyond this gate — after a successful login, every actor lands on the same flat `MainMenuPanel` (per the flat-menu-vs-login decision rule: "flat menu, only when single actor or no credentials anywhere" — the *domain* still has no real credentials, so per-role home panels still don't apply once past the technical login gate). Permissions/roles noted in each UC's "Permissions" section (e.g. UC-01: Purchasing Manager has full CRUD, Accountant is read-only) are still not enforced anywhere.

**Navigation:** `mainForm` hosts a single `panelMain` container and exposes `static void showPanel(UserControl panel)`, exactly like the sample project. `mainForm`'s constructor calls `showPanel(new LoginPanel())` on startup; a successful login calls `showPanel(new MainMenuPanel())`.

**`MainMenuPanel` button → UC → panel map** (restricted to UC-01–UC-06, the only use cases with full specs in `00e-use-cases.md`; the ~26 other names traced in `00-requirements.md` §9 are traceability-only and not yet spec'd):

| Button (Hebrew) | UC | Primary Actor | Panel |
|---|---|---|---|
| ניהול ספקים | UC-01 Manage Supplier | Purchasing Manager | `SupplierPanel` |
| ניהול עובדים | UC-02 Manage Employee | Site Supervisor | `EmployeePanel` |
| יצירת הזמנת רכש | UC-03 Create Purchase Order | Accountant | `PurchaseOrderPanel` |
| יומן עבודה יומי | UC-04 Record Daily Work Log | Site Supervisor | `DailyWorkLogPanel` |
| דוח רווחיות ותזרים מזומנים | UC-05 Generate Profitability & Cash Flow Report | CEO | `ProjectProfitabilityReportPanel` |
| ניהול ערבויות וביטוחים | UC-06 Manage Guarantee & Insurance | Finance Officer | `FinancialSecurityPanel` |

All six are implemented; none are TODO placeholders anymore. `MainMenuPanel` also exposes CRUD panels for every other domain entity (Client, TradeCategory, Equipment, Subcontractor, Tender, Project, BudgetLine, PaymentRequest, PurchaseOrderLine, SupplierPayment, SupplierPriceQuote, Attendance, EquipmentUsage, EquipmentAssignment) — see `MainMenuPanel.cs`'s own doc comment for the full button list.

**`EmployeePanel`** (first CRUD panel, UC-02): list (`DataGridView`) + view/edit form + שמירה (create) / עדכון (update) / מחיקה (delete) / חזרה (back, returns to `MainMenuPanel`) in one panel, over `Employee`'s `createEmployee`/`updateEmployee`/`deleteEmployee`. Fields match `Employee`'s DB columns exactly (`employeeId`, `firstName`, `lastName`, `nationalId`, `role`, `dailyRate`, `certificationNo`, `status`); UC-02's "Skills" field is not implemented since it isn't a column on `Employee` in `create_database.sql`.

---

## Visual Design

Course step 10 ("polish the interface"). The source of truth is `BenZionVilker/Theme.cs` — this section documents it in prose per step 10.5; if the two ever disagree, `Theme.cs` is correct and this section is stale and should be updated to match, not the other way around. Every panel applies it at runtime from its constructor (`Theme.ApplyStandardPanelTheme(this)` for a generic panel, or an explicit per-control call list for `MainMenuPanel`/`PurchaseOrderPanel`, which need bespoke button-role or color-coding logic) — colors and fonts are never hand-set in a `Designer.cs` file.

**Color palette:**

| Token | Hex | Usage |
|---|---|---|
| Brand primary | `#1B3A5C` | Screen titles, the one primary action button per screen, section headers |
| Brand primary (dark) | `#14283F` | Primary-button hover/press state |
| Ground | `#F5F5F3` | Panel background |
| Surface | `#FFFFFF` | Cards, form fields, grid rows |
| Border | `#D8D8D4` | Grid lines, outlined-button default border |
| Text primary | `#1F2933` | Body text, grid cell text |
| Text secondary | `#5A6472` | Field labels, grid column headers |

Status-badge colors (used for any status-like enum value shown in a grid, via `Theme.ApplyStatusBadgeColumn`, or in a colored info block):

| Category | Background | Text | Example values |
|---|---|---|---|
| Success | `#E1F0E2` | `#1B5E20` | Received, Approved, Active, Paid, Completed |
| Warning | `#FBE7CC` | `#8A4B04` | PendingPMApproval, PendingBudgetOverride, UnderApproval, Submitted, OnHold, Overdue |
| Danger | `#FAE1E1` | `#8E1C1C` | Rejected, Cancelled, Expired, Inactive, Suspended, Terminated |
| Info (default) | `#E4EAF0` | `#14283F` | everything else — Draft, Sent, InFulfillment, InProgress, ... |

`MainMenuPanel`'s three columns are color-coded by domain area instead of the status palette (so navigation color-coding never collides with status meaning): Procurement reuses brand primary (`#1B3A5C` / `#E4EAF0` bg), Field execution is teal (`#206A5D` / `#DCEEEA` bg), Finance & Projects is plum (`#5B4E8A` / `#E8E4F2` bg).

**Fonts:** Segoe UI everywhere (not "David" — Segoe UI is Windows' own UI font, has solid Hebrew coverage, and is guaranteed present on any Windows machine, so what's designed here renders identically on any lab PC). Scale: title 18pt bold, section header 12pt bold, body 10pt regular, body bold 10pt (button/header text), badge text 8.5pt bold, caption 8.5pt regular.

**Buttons** are flat-style (`FlatStyle.Flat`), colored by semantic role rather than by position — `Theme.ApplyStandardPanelTheme` infers the role from the control's own name (the project's `button_save`/`button_delete`/`button_manageX` naming convention makes this reliable):
- **Primary** (filled brand blue, white text): the one create/save action on a screen — `button_save`, or a name containing "create"/"quickcreate".
- **Danger** (white, red outline and text, red-tint hover): destructive actions — a name containing "delete", "reject", or "cancel".
- **Secondary** (white, blue outline and text, blue-tint hover): everything else.
- **Menu tile** (`MainMenuPanel` only): same outline pattern, colored per column category instead of brand blue.

Hover is real, not a static mock: `FlatAppearance.MouseOverBackColor`/`MouseDownBackColor` on every button (native WinForms property, no manual `MouseEnter`/`MouseLeave` needed for buttons). A `DataGridView` row, which WinForms has no native hover state for, gets one via `CellMouseEnter`/`CellMouseLeave` manually swapping `DefaultCellStyle.BackColor`.

**Spacing / layout rules:**
- Panel background is always `Ground`; every control that sits on it (form fields, buttons) is `Surface` white, so the two-tone contrast itself reads as "content area" vs. "page background" without a drawn border.
- `label_title` is centered against the panel's own authored width, not the Designer.cs's original fixed X (which was computed for "David"'s now-replaced text metrics) — see `Theme.CenterHorizontally`.
- `DataGridView` columns default to `AutoSizeColumnsMode.AllCells` (each column sized to its widest header/content, horizontal scrollbar appears if the total doesn't fit) — not `Fill`, which was silently truncating Hebrew headers and long values across every grid. `PurchaseOrderPanel`'s main list is the one deliberate exception: it passes `Fill` explicitly with hand-tuned `FillWeight` per column, verified visually.
- Status values are shown as rounded pill badges in a grid (`Theme.ApplyStatusBadgeColumn`, via `CellPainting` — WinForms has no built-in badge cell), not as plain colored text.
- A `mainForm.showPanel` panel docks `Top` (not `Fill`): width matches the window, height stays whatever the panel's own `Designer.cs` authored. `panelMain.AutoScroll = true` is the resulting fallback for a panel taller than the visible window, rather than the window silently clipping the bottom of a tall panel (including, on `PurchaseOrderPanel`, its own back button).

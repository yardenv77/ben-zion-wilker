# Requirements Specification

## 7.1 Functional Requirements

*Top priority = core component for immediate delivery; Medium priority = secondary phase*

| ID | Description | User Story | Priority | Source (AI/Org) |
|---|---|---|---|---|
| FR01 | The system shall allow create, view, update and delete of budget parameters for each project (planned revenue from the bid, planned expenses by category, currency). | As a Project Manager, I want to define and update the project's budget framework, so that there is a fixed baseline for comparison against actual execution. | Top | Org |
| FR02 | The system shall allow create, view, update and delete of tender records (including submission dates, status, portal link). The system shall send automatic alerts 3 and 7 days before the submission date. | As a Tender Coordinator, I want to receive an automatic reminder 7 days before every tender, so that I don't miss an important submission date. | Top | Org |
| FR03 | The system shall allow create, view, update and delete of suppliers and subcontractors, including saving the price-quote history for each supplier by trade type, and displaying a side-by-side comparison table of all existing price quotes for a selected trade type, marking the cheapest quote. | As a Purchasing Manager, I want to compare all price quotes for a given trade type in one table, so that I can quickly choose the most cost-effective offer. | Medium | Org |
| FR04 | The system shall allow create, view, update and delete of employee records, including saving skills, real-time availability, and their assignment to projects. | As a Site Supervisor, I want to immediately see who is assigned to the site today and who is available, so that I can quickly fix manpower shortages. | Top | AI |
| FR05 | The system shall allow creation of purchase requests (work orders to suppliers and subcontractors) and tracking the status of every order (pending/in progress/completed) and payment status. The system shall display a list of orders whose payment is overdue, the number of overdue days and the total amount, with an alert when the delay exceeds 30 days. | As an Accountant, I want to see which purchase orders are overdue on payment and for how long, so that I can update suppliers and plan payments. | Top | AI |
| FR06 | The system shall allow create, view, update and delete of bank guarantee and insurance records for each project, including saving the issue date, expiry, amount and link to the relevant project. The system shall display an automatic alert as the expiry date approaches (30 days before). | As a Finance Officer, I want to see a list of all active guarantees and insurance policies and get an alert before expiry, so that I don't miss a critical validity date. | Top | Org |
| FR07 | A report showing profitability per project: planned/actual revenue, actual expenses, net profit, budget-overrun flag, sortable and filterable by client/date/status. In addition, the report shall present a consolidated monthly cash-flow forecast for all projects (expected payments to suppliers versus expected income from clients), and shall flag months in which expenses are expected to exceed income. | As the company's CEO, I want to see up-to-date profitability for every project and a consolidated monthly cash-flow forecast, so that I know which projects are profitable and when we will need external financing. | Top | AI |
| FR08 | A report showing execution tracking and statuses of all projects and orders in real time: planned vs. actual completion date, actual percent complete, status of every task (on schedule/delayed/ahead), number of days delayed or ahead. The report displays critical issues in red (severe delay), sorted by priority, with an interface that allows filtering by client or project manager. | As a Project Manager, I want to see an up-to-date report of all delayed projects, so that I can immediately identify execution problems and take corrective action. | Top | AI |
| FR09 | The system shall send an automatic alert (email + in-app notification) when actual expenses on a task or project exceed 10%, 20% or 50% of the planned budget. | As a Project Manager, I want to get an immediate alert when the budget is exceeded, so that I can stop delays immediately. | Top | AI |
| FR10 | A mobile application allowing the Site Supervisor to enter a daily work log from the field (date, hours worked, employees who participated, tasks completed, issues/notes). The data is transferred automatically to the central system in real time. | As a Site Supervisor, I want to enter a work log on mobile from the field, so that I don't have to wait until the evening to send data to the office. | Medium | AI |
| FR11 | An option to export any report (profitability, execution, expenses) to a PDF file with professional formatting, ready to be sent to clients or archived. | As a Project Manager, I want to export a report to PDF, so that I can send it to the client or to the CEO. | Medium | Org |
| FR12 | Every list (projects, suppliers, employees) shall support dynamic filtering by: status, client, date range, amount range, name, etc. | As a Purchasing Manager, I want to filter suppliers by trade type, so that I don't have to browse the entire old supplier list. | Medium | Org |
| FR13 | Definition of permission levels: only the Purchasing Manager sees invoices, only the CEO sees profitability reports, only the Site Supervisor sees work logs, etc. | As the CEO, I want a field worker not to see financial information, so that sensitive information isn't exposed. | Top | Org |
| FR14 | For every tender - add a status field (pending/under evaluation/awaiting result/won/lost). Automatic ordering by status in the report. | As a Tender Coordinator, I want to see the status of every tender, so that I can track it. | Medium | Org |
| FR15 | When a purchase order is created, the system automatically sends an email to the supplier with the order details (number, amount, completion date). | As a Purchasing Manager, I want a supplier to receive an automatic email with the order details within seconds, so that I don't have to write a manual email. | Medium | Org |
| FR16 | A digital safety log - daily safety briefings, incident/injury reports, safety-equipment inspections (helmets, harnesses, etc.), flagging of issues. | As a Safety Manager, I want to document every safety incident, so that I have records for government inspections. | Top | Org |
| FR17 | Automatic synchronization of project data (status, quantity report, payment requests) with the MOD portal, without the need for manual copying. | As a Project Manager, I want the report data to be transferred automatically to the MOD portal, so that I don't have to manually copy it every time. | Top | AI |
| FR18 | The system shall allow create, view, update and delete of reports on execution defects (inaccurate work, defective materials, deviations). Saving of correction status and resolution date. | As a Quality Inspector, I want to document a defect in execution and track it until it's fixed, so that the client sees that we took responsibility. | Medium | AI |
| FR19 | The system shall allow create, view, update and delete of equipment and tools in the field (bulldozer, excavator, truck). Saving of rental dates, daily cost, site of use, and status (available/in use/under repair). | As an Equipment Manager, I want to see in real time which equipment is available and on which dates, so that I can plan equipment assignments efficiently without duplication. | Medium | AI |
| FR20 | For every work day, the system shall compare the quantity planned in the schedule against the quantity actually reported in the digital work log (FR10) for each subcontractor, and shall automatically flag a material delay (below a predefined completion percentage) for an immediate alert to the Project Manager. | As a Project Manager, I want to know immediately when a subcontractor isn't keeping the planned pace, so that I don't discover the delay only in the evening after a work day has already been lost. | Top | AI |
| FR21 | *(Solution to problem #6)* The system shall automatically compare the work-order data received from the client's portal (specification, budget, schedule) against the data of the original winning bid, and shall display an alert for every mismatch (in amount, scope of work, or schedule) requiring manual approval by the Project Manager before the project is opened in the system. | As a Project Manager, I want the system to automatically compare the received order to the winning bid and alert on mismatches, so that I catch mistakes before they harm the project's budget or schedule. | Top | Org |
| FR22 | For every payment request submitted to the client (MOD / Rafael), the system shall display the current status (submitted / under review / approved / rejected / missing documents), the submission date, and shall allow attaching missing documents directly from the system. | As a Project Manager, I want to know exactly what the status of my payment request is and what's missing, so that I don't have to call and ask, and can plan cash flow with certainty. | Top | AI |
| FR23 | The system shall allow creating an official, detailed work order for subcontractors (including task description, quantities, technical specification, location and target date, with the option to attach a photo/sketch), and shall send it digitally to the contractor for a read-acknowledgement before work begins. | As a Project Manager, I want to issue a digital, detailed work order to a subcontractor instead of a phone order, so that I reduce the misunderstandings that today cause 20-30% of orders to end in faulty execution and lost work days. | Medium | AI |
| FR24 | Project history - saving all projects completed in the past with information: planned/actual budget, planned/actual time, client, trade type. Comparison between similar projects for better forecasting. | As a Project Manager, I want to see how similar projects were carried out in the past, so that I can make a more accurate estimate for new projects. | Medium | AI |
| FR25 | The system shall allow exporting the quantity report to an Excel file in a fixed template matching the MOD's requirement (project name and number, date, task/planned-quantity/completed-quantity/%complete/expected-date table, and a summary row), without manual editing of the structure. | As a Project Manager, I want to export a quantity report directly in the format the MOD requires, so that I don't waste 30-45 minutes on manual editing with every update. | Medium | AI |
| FR26 | Create, view, update and delete of projects (name, client, address, planned start/end dates, status: tender/production & execution/completed). | As a Project Manager, I want to manage the project's core details, so that there is one reliable record that all other data (budget, guarantees, logs) connects to. | Top | Org |
| FR27 | The system shall allow create, view and update of unexpected engineering-challenge/delay records in the field (type, description, impact on schedule), including documentation of the solution given and its resolution date. Updating the record shall automatically update the expected completion date of the relevant task/project. | As a Project Manager, I want to document an unexpected engineering challenge or delay in the field and the solution provided, so that changes are properly recorded and the schedule updates accordingly instead of being discovered late. | Top | Org |

## 7.2 Non-Functional Requirements

| ID | Description | User Story / Free-form Statement | Priority | Source (AI/Org) |
|---|---|---|---|---|
| NFR01 | **System availability:** the system shall be available 99.5% of the time (maximum 3.6 downtime hours per month). There shall be an automatic backup of all data every 6 hours. | As a Project Manager, I want the system to be almost always available, so that I can rely on it every day. | Top | Org |
| NFR02 | **Performance:** the response time of every report/list shall not exceed 2 seconds, even with 1000+ records. Search must complete within 1 second. | As a field user, I want the application to respond quickly, so that I don't wait a long time. | Top | Org |
| NFR03 | **Security:** all communication between the application/browser and the system shall be encrypted (SSL/TLS). There shall be two-factor authentication (2FA) for managers. Activity monitoring (audit log) of every change in the system. | As the CEO, I want our financial data to be protected from unauthorized reading. | Top | Org |
| NFR04 | **Backup and disaster recovery:** a full backup of the database shall run every day at 23:00. The last 7 days of backups shall be kept in a separate copy. In the event of a severe failure, recovery time shall be up to 4 hours. | As a Risk Manager, I want project data to be protected against loss due to faults. | Top | Org |
| NFR05 | **Mobile compatibility:** the application shall work on iOS (version 14 and above) and Android (version 11 and above). The interface shall fit screens sized 4 to 6.5 inches. | As a field Site Supervisor, I want the application to work on my phone. | Medium | Org |
| NFR06 | **Usability:** the interface shall be Hebrew-friendly (RTL). Every major function shall be reachable within 3 mouse clicks. A new user's learning time shall not exceed 4 hours. | As a site manager, I want the application to be easy to understand without lengthy training. | Top | AI |
| NFR07 | **Documentation and technical support:** every report shall be documented in Hebrew. Technical support available 5 days a week, 8:00-17:00, maximum response time for a critical issue: 2 hours. | As a user, I want to be able to get help when I have a problem. | Medium | Org |

## 9. Traceability Matrix

> Source: a Visual Paradigm-generated matrix (Requirement × Use Case relationship view), transcribed here as an FR → Use Case list rather than reproducing the full 27×26 grid. FR12 traces to three use cases at once (matching its own wording, which names all three list types: projects, suppliers, employees).

| FR | Linked Use Case(s) |
|---|---|
| FR01 | Manage Project Budget |
| FR02 | Manage Tender |
| FR03 | Manage Supplier |
| FR04 | Manage Employee |
| FR05 | Create Purchase Order; Track Payment Status |
| FR06 | Manage Guarantee & Insurance |
| FR07 | Generate Profitability & Cash Flow Report |
| FR08 | View Multi-Project Execution Status Report |
| FR09 | Send Budget Overrun Alert |
| FR10 | Record Daily Work Log |
| FR11 | Export Report to PDF |
| FR12 | Manage Employee; Manage Project; Manage Supplier |
| FR13 | Manage User Permissions |
| FR14 | Manage Tender |
| FR15 | Send Purchase Order Email to Supplier |
| FR16 | Manage Safety Briefing & Incident Log |
| FR17 | Sync Data with MOD Portal |
| FR18 | Log Quality Defect & Track Correction |
| FR19 | Manage Equipment |
| FR20 | Track Subcontractor Progress vs Plan |
| FR21 | Match Incoming Order vs Winning Bid |
| FR22 | Track Payment Request Status |
| FR23 | Issue Digital Work Order |
| FR24 | View Project History & Comparison |
| FR25 | Export Quantity Report |
| FR26 | Manage Project |
| FR27 | Log Field Issue & Update Schedule |

**Importance of the traceability matrix** (source commentary, translated): the traceability matrix makes it possible to verify that every functional requirement defined at the analysis stage is actually translated into a real capability in the system, and prevents a situation of "orphan requirements" that remain unimplemented. At the same time, it reveals whether there are use cases that are not anchored in any documented requirement. Throughout the project's lifecycle, the matrix serves as a control tool: any change in a requirement or in the design makes it possible to immediately identify which additional parts of the system are affected and require an update.

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BenZionVilker
{
    // Course step 10 ("polish the interface") -- the palette/typography approved in the
    // design-direction artifact, applied at runtime from each panel's constructor (right
    // after InitializeComponent()) rather than hand-edited into every Designer.cs. One
    // place to keep colors/fonts consistent across every panel instead of duplicating
    // property assignments per screen.
    public static class Theme
    {
        public static readonly Color BrandPrimary = ColorTranslator.FromHtml("#1B3A5C");
        public static readonly Color BrandPrimaryDark = ColorTranslator.FromHtml("#14283F");

        public static readonly Color Ground = ColorTranslator.FromHtml("#F5F5F3");
        public static readonly Color Surface = Color.White;
        public static readonly Color Border = ColorTranslator.FromHtml("#D8D8D4");
        public static readonly Color TextPrimary = ColorTranslator.FromHtml("#1F2933");
        public static readonly Color TextSecondary = ColorTranslator.FromHtml("#5A6472");

        public static readonly Color SuccessBg = ColorTranslator.FromHtml("#E1F0E2");
        public static readonly Color SuccessText = ColorTranslator.FromHtml("#1B5E20");
        public static readonly Color WarningBg = ColorTranslator.FromHtml("#FBE7CC");
        public static readonly Color WarningText = ColorTranslator.FromHtml("#8A4B04");
        public static readonly Color DangerBg = ColorTranslator.FromHtml("#FAE1E1");
        public static readonly Color DangerText = ColorTranslator.FromHtml("#8E1C1C");
        public static readonly Color InfoBg = ColorTranslator.FromHtml("#E4EAF0");
        public static readonly Color InfoText = ColorTranslator.FromHtml("#14283F");

        // Category accents for MainMenuPanel's three columns -- distinct from the status-badge
        // colors above (success/warning/danger/info already mean something specific), so
        // color-coding navigation by domain area doesn't visually collide with status meaning.
        public static readonly Color CategoryProcurement = BrandPrimary;              // reuses the brand color -- procurement is the primary domain
        public static readonly Color CategoryProcurementBg = InfoBg;
        public static readonly Color CategoryField = ColorTranslator.FromHtml("#206A5D");     // teal -- field work & equipment
        public static readonly Color CategoryFieldBg = ColorTranslator.FromHtml("#DCEEEA");
        public static readonly Color CategoryFinance = ColorTranslator.FromHtml("#5B4E8A");   // plum -- projects & finance
        public static readonly Color CategoryFinanceBg = ColorTranslator.FromHtml("#E8E4F2");

        public static Font TitleFont => new Font("Segoe UI", 18F, FontStyle.Bold);
        public static Font SectionFont => new Font("Segoe UI", 12F, FontStyle.Bold);
        public static Font BodyFont => new Font("Segoe UI", 10F);
        public static Font BodyBoldFont => new Font("Segoe UI", 10F, FontStyle.Bold);
        // Slightly smaller than BodyFont -- reduces (never fully guarantees) how often a
        // field label's fixed-size box needs AutoEllipsis to avoid overlapping its textbox.
        public static Font FieldLabelFont => new Font("Segoe UI", 9F);
        public static Font CaptionFont => new Font("Segoe UI", 8.5F);

        public static void ApplyPanelBackground(Control panel) { panel.BackColor = Ground; }

        public static void ApplyTitle(Label label)
        {
            label.Font = TitleFont;
            label.ForeColor = BrandPrimaryDark;
        }

        // Designer.cs gives label_title a fixed X computed for the old "David" font's text
        // width; Segoe UI renders at a different width, so that fixed X no longer centers it.
        // Recomputed here from the label's actual (AutoSize) width after the font change,
        // against the panel's own authored width (containerWidth) -- not the live Dock=Top
        // runtime width, which isn't set yet at construction time.
        public static void CenterHorizontally(Control c, int containerWidth)
        {
            c.Left = (containerWidth - c.Width) / 2;
        }

        // Centers within a sub-region (e.g. one menu column) rather than the whole panel.
        public static void CenterHorizontally(Control c, int regionLeft, int regionWidth)
        {
            c.Left = regionLeft + (regionWidth - c.Width) / 2;
        }

        public static void ApplySectionLabel(Label label)
        {
            label.Font = SectionFont;
            label.ForeColor = BrandPrimaryDark;
        }

        public static void ApplySectionLabel(Label label, Color color)
        {
            label.Font = SectionFont;
            label.ForeColor = color;
        }

        // Every previous attempt at this trusted label.Width as a starting point -- wrong.
        // Designer.cs's own InitializeComponent already runs "label.Font = new Font(\"Segoe
        // UI\", 12F)" on an AutoSize=true label before ANY Theme code executes, which
        // immediately regrows the label's Width to fit that (wide) font -- so by the time
        // this method ever sees label.Width, it's already the wrong, too-wide number, not
        // the tight box Designer.cs originally drew for "David". No margin subtracted from a
        // corrupted baseline can be trusted.
        //
        // The fix has to come from outside the label: panel's siblings (TextBox/ComboBox/etc.)
        // are NOT AutoSize, so their Location is still exactly what Designer.cs wrote -- the
        // real available gap is "distance from this label's own X to the nearest sibling
        // positioned to its right on the same row", found geometrically, not assumed.
        public static void ApplyFieldLabel(Label label, Control panel)
        {
            int labelX = label.Location.X;
            int labelY = label.Location.Y;
            int nearestRightX = panel.Width;
            foreach (Control sib in panel.Controls)
            {
                if (sib == label) continue;
                if (Math.Abs(sib.Location.Y - labelY) > 15) continue; // not the same row
                if (sib.Location.X <= labelX) continue;               // not to the right
                if (sib.Location.X < nearestRightX) nearestRightX = sib.Location.X;
            }
            int targetWidth = Math.Max(20, nearestRightX - labelX - 14);

            label.AutoSize = false;
            label.ForeColor = TextSecondary;
            label.TextAlign = ContentAlignment.MiddleRight;

            // Measure the actual text at decreasing font sizes until it provably fits --
            // correct by construction regardless of exactly how wide Segoe UI renders any
            // specific Hebrew string, rather than assuming one fixed point size works.
            float size = 9F;
            while (size > 6.5F)
            {
                using (Font candidate = new Font("Segoe UI", size))
                {
                    if (TextRenderer.MeasureText(label.Text, candidate).Width <= targetWidth)
                        break;
                }
                size -= 0.5F;
            }
            label.Font = new Font("Segoe UI", size);
            label.Width = targetWidth;
        }

        public static void ApplyTextBox(TextBox t)
        {
            t.Font = BodyFont;
            t.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyCheckBox(CheckBox c)
        {
            c.Font = BodyFont;
            c.ForeColor = TextPrimary;
        }

        public static void ApplyDateTimePicker(DateTimePicker d)
        {
            d.Font = BodyFont;
            d.CalendarForeColor = TextPrimary;
            d.CalendarMonthBackground = Surface;
        }

        public static void ApplyComboBox(ComboBox c)
        {
            c.Font = BodyFont;
            // Deliberately NOT FlatStyle.Flat: that combination with RightToLeft=Yes (every
            // panel in this app) is a known WinForms rendering quirk that can paint the whole
            // control as a solid system-highlight-blue rectangle instead of a normal dropdown.
            // The font change alone is enough for visual consistency; not worth a broken control.
        }

        // Filled brand-blue -- reserved for the one primary action on a screen (create/save).
        public static void ApplyPrimaryButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = BrandPrimary;
            b.ForeColor = Color.White;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = BrandPrimaryDark;
            b.FlatAppearance.MouseDownBackColor = BrandPrimaryDark;
            b.Font = BodyBoldFont;
            b.Cursor = Cursors.Hand;
        }

        // Outlined brand-blue -- every other non-destructive action.
        public static void ApplySecondaryButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Surface;
            b.ForeColor = BrandPrimary;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = BrandPrimary;
            b.FlatAppearance.MouseOverBackColor = InfoBg;
            b.FlatAppearance.MouseDownBackColor = InfoBg;
            b.Font = BodyBoldFont;
            b.Cursor = Cursors.Hand;
        }

        // Outlined red -- reject/cancel/delete.
        public static void ApplyDangerButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Surface;
            b.ForeColor = DangerText;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = DangerText;
            b.FlatAppearance.MouseOverBackColor = DangerBg;
            b.FlatAppearance.MouseDownBackColor = DangerBg;
            b.Font = BodyBoldFont;
            b.Cursor = Cursors.Hand;
        }

        // Main-menu navigation tiles, color-coded per domain column (see the Category* fields
        // above) instead of one flat style repeated ~20 times -- the accent border/text/hover
        // both differentiates the three areas and reads as more deliberate than a wall of
        // identical gray-bordered buttons.
        public static void ApplyMenuButton(Button b, Color accent, Color accentBg)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = Surface;
            b.ForeColor = accent;
            b.FlatAppearance.BorderSize = 2;
            b.FlatAppearance.BorderColor = accent;
            b.FlatAppearance.MouseOverBackColor = accentBg;
            b.FlatAppearance.MouseDownBackColor = accentBg;
            b.Font = BodyBoldFont;
            b.Cursor = Cursors.Hand;
            b.TextAlign = ContentAlignment.MiddleCenter;
        }

        public static void ApplyMenuButton(Button b) => ApplyMenuButton(b, BrandPrimary, InfoBg);

        // Wraps an existing control (a list DataGridView) in a RoundedPanel card, entirely at
        // runtime -- no Designer.cs edits needed anywhere. Reparents the control in place
        // (same Location, inset by padding) so every existing Location/Size in each panel's
        // Designer.cs stays correct; the control keeps its identity (same field, same
        // DataSource/CellClick wiring), it just lives inside a new rounded container now.
        // MUST be called AFTER ApplyStandardPanelTheme (or the panel's own equivalent theme
        // calls) on the same control -- that method only styles direct children of the
        // panel, and this reparents the control out of that list.
        public static RoundedPanel WrapInCard(Control content, int padding = 8)
        {
            Control parent = content.Parent;
            Point original = content.Location;
            Size size = content.Size;

            RoundedPanel card = new RoundedPanel
            {
                BackColor = Surface,
                CornerRadius = 14,
                Location = new Point(original.X - padding, original.Y - padding),
                Size = new Size(size.Width + padding * 2, size.Height + padding * 2),
                Anchor = content.Anchor
            };

            parent.Controls.Remove(content);
            content.Location = new Point(padding, padding);
            card.Controls.Add(content);
            parent.Controls.Add(card);
            card.SendToBack();

            // A grid with few/narrow columns under AllCells sizing (the new default) still
            // keeps its full original Designer.cs width -- the columns end up hugging the
            // control's own RTL-internal right edge, leaving the rest of the card as bare
            // empty canvas (visually obvious now that the card's rounded edge outlines
            // exactly where that dead space is). Fixed once here, for every wrapped grid,
            // by shrinking the card+grid to the columns' real total width the moment real
            // data is bound -- capped at the original width (never grows past it, so a
            // many-column grid like PurchaseOrderPanel's list, already sized to fill that
            // width under Fill mode, is untouched). The card's RIGHT edge is what a
            // right-to-left layout anchors content against, so it shrinks from the left,
            // not the right -- the card doesn't drift away from where its neighboring
            // right-aligned controls expect it.
            if (content is DataGridView grid)
            {
                int maxWidth = size.Width;
                int cardRight = card.Location.X + card.Width;
                grid.DataBindingComplete += (s, e) =>
                {
                    if (grid.Columns.Count == 0) return;
                    int contentWidth = 0;
                    foreach (DataGridViewColumn col in grid.Columns)
                        if (col.Visible) contentWidth += col.Width;
                    contentWidth += SystemInformation.VerticalScrollBarWidth + 4;

                    int newGridWidth = Math.Min(maxWidth, Math.Max(contentWidth, 200));
                    if (Math.Abs(grid.Width - newGridWidth) < 4) return;

                    grid.Width = newGridWidth;
                    card.Width = newGridWidth + padding * 2;
                    card.Location = new Point(cardRight - card.Width, card.Location.Y);
                };
            }

            return card;
        }

        // One-line rollout for a panel that doesn't need PurchaseOrderPanel's bespoke
        // role-per-button treatment: colors every Label/TextBox/ComboBox/Button/DataGridView
        // generically, guessing each button's role from its control name (the project's own
        // naming convention -- button_save, button_delete, button_manageX -- makes this
        // reliable). Grid HEADER TEXT and status-badge translation are NOT handled here --
        // those are entity-specific (which columns exist, what a status enum's Hebrew label
        // is) and still need a per-panel look at that panel's loadX() method.
        public static void ApplyStandardPanelTheme(Control panel)
        {
            ApplyPanelBackground(panel);
            int containerWidth = panel.Width;

            foreach (Control c in panel.Controls)
            {
                if (c is Label lbl)
                {
                    if (lbl.Name == "label_title") { ApplyTitle(lbl); CenterHorizontally(lbl, containerWidth); }
                    else ApplyFieldLabel(lbl, panel);
                }
                else if (c is TextBox tb)
                {
                    ApplyTextBox(tb);
                }
                else if (c is ComboBox cb)
                {
                    ApplyComboBox(cb);
                }
                else if (c is CheckBox chk)
                {
                    ApplyCheckBox(chk);
                }
                else if (c is DateTimePicker dtp)
                {
                    ApplyDateTimePicker(dtp);
                }
                else if (c is Button b)
                {
                    string n = b.Name.ToLowerInvariant();
                    if (n.Contains("delete") || n.Contains("reject") || n.Contains("cancel"))
                        ApplyDangerButton(b);
                    else if (n == "button_save" || n.Contains("create") || n.Contains("quickcreate"))
                        ApplyPrimaryButton(b);
                    else
                        ApplySecondaryButton(b);
                }
                else if (c is DataGridView g)
                {
                    ApplyDataGridView(g);
                }
            }
        }

        // autoSize defaults to AllCells (size each column to its widest header/content,
        // horizontal scrollbar appears if the total exceeds the grid's width) instead of the
        // Fill the Designer.cs files set -- Fill forces every column into the visible width no
        // matter how much text it holds, which is what was truncating Hebrew headers and long
        // values across every grid. PurchaseOrderPanel's main list passes Fill explicitly: its
        // FillWeight proportions were already tuned and verified for that specific column set.
        public static Font KpiValueFont => new Font("Segoe UI", 20F, FontStyle.Bold);
        public static Font KpiCaptionFont => new Font("Segoe UI", 9F);

        // A colored summary tile (reference: the "1.52M אומדן פרויקט" gold tile) -- bg/text
        // pick a semantic pair from the status palette above (InfoBg/InfoText for a neutral
        // figure like revenue, SuccessBg/SuccessText or DangerBg/DangerText for a number
        // whose sign matters, like net profit) so a report card and a status badge read as
        // the same visual language rather than two different color systems.
        public static void ApplyKpiCard(RoundedPanel card, Label valueLabel, Label captionLabel, Color bg, Color text)
        {
            card.BackColor = bg;
            card.CornerRadius = 14;
            valueLabel.Font = KpiValueFont;
            valueLabel.ForeColor = text;
            valueLabel.BackColor = Color.Transparent;
            captionLabel.Font = KpiCaptionFont;
            captionLabel.ForeColor = text;
            captionLabel.BackColor = Color.Transparent;
        }

        public static void ApplyDataGridView(DataGridView g, DataGridViewAutoSizeColumnsMode autoSize = DataGridViewAutoSizeColumnsMode.AllCells)
        {
            g.AutoSizeColumnsMode = autoSize;
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Surface;
            g.GridColor = Border;
            g.EnableHeadersVisualStyles = false; // required -- otherwise Windows visual styles override header colors below
            g.ColumnHeadersDefaultCellStyle.BackColor = Ground;
            g.ColumnHeadersDefaultCellStyle.ForeColor = TextSecondary;
            g.ColumnHeadersDefaultCellStyle.Font = BodyBoldFont;
            g.ColumnHeadersHeight = 36;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            g.DefaultCellStyle.BackColor = Surface;
            g.DefaultCellStyle.ForeColor = TextPrimary;
            g.DefaultCellStyle.Font = BodyFont;
            g.DefaultCellStyle.SelectionBackColor = InfoBg;
            g.DefaultCellStyle.SelectionForeColor = TextPrimary;
            g.AlternatingRowsDefaultCellStyle.BackColor = Ground;
            g.RowHeadersVisible = false;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.RowTemplate.Height = 30;

            // WinForms DataGridView has no native hover-row state, so it's tracked manually.
            int hoveredRow = -1;
            g.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex == hoveredRow) return;
                hoveredRow = e.RowIndex;
                g.Rows[e.RowIndex].DefaultCellStyle.BackColor = InfoBg;
            };
            g.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex != hoveredRow) return;
                g.Rows[e.RowIndex].DefaultCellStyle.BackColor = (e.RowIndex % 2 == 1) ? Ground : Surface;
                hoveredRow = -1;
            };
        }

        public static Font BadgeFont => new Font("Segoe UI", 8.5F, FontStyle.Bold);

        // Draws the given column's cell as a colored pill instead of plain text -- WinForms
        // has no built-in badge cell type, so this hooks CellPainting directly.
        //
        // displayText maps the cell's raw value (the English enum token stored in the bound
        // DataTable, e.g. "PendingBudgetOverride") to what's actually drawn (e.g. its Hebrew
        // EnumDisplay label) -- color lookup always uses the raw token, since that's the
        // stable, single-word key StatusBg/StatusText switch on; only the drawn text goes
        // through the mapper. Pass null to draw the raw value as-is.
        public static void ApplyStatusBadgeColumn(DataGridView g, string columnName, System.Func<string, string> displayText = null)
        {
            g.CellPainting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                if (g.Columns[e.ColumnIndex].Name != columnName) return;

                e.PaintBackground(e.ClipBounds, true);

                string rawStatus = e.Value.ToString();
                string label = displayText != null ? displayText(rawStatus) : rawStatus;
                Rectangle pill = new Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 5, e.CellBounds.Width - 12, e.CellBounds.Height - 10);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = RoundedRectPath(pill, pill.Height / 2))
                using (SolidBrush brush = new SolidBrush(StatusBg(rawStatus)))
                {
                    e.Graphics.FillPath(brush, path);
                }
                // EndEllipsis: a safety net, not the primary fix -- the primary fix is giving
                // this column enough FillWeight and using short Hebrew labels in the first
                // place, so this only ever engages for a genuinely unexpected long value.
                TextRenderer.DrawText(e.Graphics, label, BadgeFont, pill, StatusText(rawStatus),
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis);

                e.Handled = true;
            };
        }

        private static GraphicsPath RoundedRectPath(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Maps this project's status-like enum values (POStatus, ProjectStatus, ...) to a
        // badge color by category, not by exact enum. Unrecognized values fall back to "info".
        private static Color StatusBg(string status)
        {
            switch (status)
            {
                case "Received": case "Approved": case "Active": case "Paid": case "Completed":
                    return SuccessBg;
                case "PendingPMApproval": case "PendingBudgetOverride": case "UnderApproval":
                case "UnderReview": case "Submitted": case "OnHold": case "Overdue":
                    return WarningBg;
                case "Rejected": case "Cancelled": case "Expired": case "Inactive": case "Suspended": case "Terminated":
                    return DangerBg;
                default:
                    return InfoBg; // Draft, Sent, InFulfillment, InProgress, PartiallyReceived, Archived, ...
            }
        }

        private static Color StatusText(string status)
        {
            switch (status)
            {
                case "Received": case "Approved": case "Active": case "Paid": case "Completed":
                    return SuccessText;
                case "PendingPMApproval": case "PendingBudgetOverride": case "UnderApproval":
                case "UnderReview": case "Submitted": case "OnHold": case "Overdue":
                    return WarningText;
                case "Rejected": case "Cancelled": case "Expired": case "Inactive": case "Suspended": case "Terminated":
                    return DangerText;
                default:
                    return InfoText;
            }
        }
    }
}

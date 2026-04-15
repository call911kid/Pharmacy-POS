using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrugSearch.Api;
using DrugSearch.Models;
using UI.Theme;

namespace UI.Forms
{
    public class DrugSearchForm : Form
    {
        // ── Search bar ────────────────────────────────────────────────────────
        private TextBox txtSearch = null!;
        private Button btnSearch = null!;
        private Label lblSearchTitle = null!;
        private CheckBox chkLocalSource = null!;
        private CheckBox chkOpenFdaSource = null!;

        // ── Results list ──────────────────────────────────────────────────────
        private ListBox lstResults = null!;
        private Label lblResultsCount = null!;

        // ── Detail panel ──────────────────────────────────────────────────────
        private Panel panelDetail = null!;
        private Label lblDrugName = null!;
        private Label lblManufacturer = null!;
        private Button btnTranslate = null!;
        private ProgressBar progTranslate = null!;
        private Label lblTransStatus = null!;

        // Sections
        private Panel panelSections = null!;
        private readonly List<SectionControl> _sections = new();

        // ── State ─────────────────────────────────────────────────────────────
        private DrugLabel[] _allResults = Array.Empty<DrugLabel>();
        private DrugLabel? _selected;
        private bool _isTranslated = false;

        // Original English text cache per section
        private readonly Dictionary<string, string> _englishCache = new();

        // ═════════════════════════════════════════════════════════════════════
        public DrugSearchForm()
        {
            Text = "Drug Information Search — FDA";
            Size = new Size(1200, 800);
            MinimumSize = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterScreen;
            Font = UiPalette.BodyFont(10f);
            BackColor = UiPalette.Surface;

            this.Load += DrugSearchForm_Load;
        }

        private void DrugSearchForm_Load(object? sender, EventArgs e)
        {
            BuildUI();
        }

        // ═════════════════════════════════════════════════════════════════════
        #region UI Builder

        private void BuildUI()
        {
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = UiPalette.Surface
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            var leftPanel = BuildLeftPanel();
            table.Controls.Add(leftPanel, 0, 0);

            var rightPanel = BuildRightPanel();
            table.Controls.Add(rightPanel, 1, 0);

            var header = BuildHeader();

            var mainContainer = new Panel { Dock = DockStyle.Fill };
            mainContainer.Controls.Add(table);

            this.Controls.Add(mainContainer);
            this.Controls.Add(header);
        }

        private Panel BuildHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = UiPalette.Primary
            };

            var title = new Label
            {
                Text = "💊  Drug Information Search",
                ForeColor = Color.White,
                Font = UiPalette.BodyFont(16f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 18)
            };

            var sub = new Label
            {
                Text = "Powered by Egypt local dataset + OpenFDA",
                ForeColor = UiPalette.PrimaryMuted,
                Font = UiPalette.BodyFont(9f),
                AutoSize = true,
                Location = new Point(26, 50)
            };

            header.Controls.Add(title);
            header.Controls.Add(sub);
            return header;
        }

        private Panel BuildLeftPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UiPalette.RowAlternate,
                Padding = new Padding(0)
            };

            var searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 175,
                BackColor = UiPalette.Primary,
                Padding = new Padding(16, 16, 16, 16)
            };

            lblSearchTitle = new Label
            {
                Text = "🔍 Search Drug Name",
                ForeColor = Color.White,
                Font = UiPalette.BodyFont(11f, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 28
            };

            txtSearch = new TextBox
            {
                Font = UiPalette.BodyFont(12f),
                BorderStyle = BorderStyle.None,
                Height = 45,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                ForeColor = UiPalette.TextPrimary,
                Margin = new Padding(0, 8, 0, 0),
                Padding = new Padding(12, 12, 12, 12)
            };

            var sourcePanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0, 8, 0, 0)
            };
            chkLocalSource = new CheckBox
            {
                Text = "Local",
                Checked = true,
                AutoSize = true,
                Font = UiPalette.BodyFont(9f),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            chkOpenFdaSource = new CheckBox
            {
                Text = "OpenFDA",
                Checked = true,
                AutoSize = true,
                Font = UiPalette.BodyFont(9f),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Margin = new Padding(16, 3, 3, 3)
            };
            sourcePanel.Controls.Add(chkLocalSource);
            sourcePanel.Controls.Add(chkOpenFdaSource);

            btnSearch = new Button
            {
                Text = "SEARCH",
                Height = 45,
                Dock = DockStyle.Bottom,
                BackColor = Color.White,
                ForeColor = UiPalette.Primary,
                FlatStyle = FlatStyle.Flat,
                Font = UiPalette.BodyFont(11f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 12, 0, 0)
            };
            btnSearch.FlatAppearance.BorderSize = 1;
            btnSearch.FlatAppearance.BorderColor = UiPalette.Primary;
            btnSearch.Click += async (s, ev) => await DoSearchAsync();

            txtSearch.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    _ = DoSearchAsync();
                }
            };

            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(sourcePanel);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(lblSearchTitle);

            lblResultsCount = new Label
            {
                Text = "Enter a drug name to search",
                ForeColor = UiPalette.TextMuted,
                Font = UiPalette.BodyFont(10f, FontStyle.Italic),
                Dock = DockStyle.Top,
                Height = 35,
                Padding = new Padding(16, 10, 0, 0),
                BackColor = Color.White
            };

            lstResults = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = UiPalette.BodyFont(11f),
                ItemHeight = 50,
                DrawMode = DrawMode.OwnerDrawFixed,
                BackColor = Color.White,
                SelectionMode = SelectionMode.One,
                IntegralHeight = false
            };
            lstResults.DrawItem += LstResults_DrawItem;
            lstResults.SelectedIndexChanged += LstResults_SelectedIndexChanged;

            panel.Controls.Add(lstResults);
            panel.Controls.Add(lblResultsCount);
            panel.Controls.Add(searchPanel);

            return panel;
        }

        private Panel BuildRightPanel()
        {
            panelDetail = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20, 20, 20, 20)
            };

            var headerStrip = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = UiPalette.PrimaryMuted,
                Padding = new Padding(20, 16, 20, 16)
            };

            var topLine = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = UiPalette.Primary
            };
            headerStrip.Controls.Add(topLine);

            lblDrugName = new Label
            {
                Text = "Select a drug from the list",
                Font = UiPalette.BodyFont(16f, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary,
                Dock = DockStyle.Top,
                Height = 36,
                AutoEllipsis = true
            };

            lblManufacturer = new Label
            {
                Text = "",
                Font = UiPalette.BodyFont(10f),
                ForeColor = UiPalette.TextMuted,
                Dock = DockStyle.Top,
                Height = 26,
                Padding = new Padding(0, 4, 0, 0)
            };

            var btnRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };

            btnTranslate = new Button
            {
                Text = "🌐  Translate to Arabic",
                Width = 220,
                Height = 40,
                BackColor = UiPalette.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = UiPalette.BodyFont(11f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false,
                Margin = new Padding(0, 0, 10, 0)
            };
            btnTranslate.FlatAppearance.BorderSize = 0;
            btnTranslate.Click += async (s, ev) => await DoTranslateAsync();

            progTranslate = new ProgressBar
            {
                Width = 200,
                Height = 8,
                Style = ProgressBarStyle.Marquee,
                Visible = false,
                Margin = new Padding(10, 16, 0, 0)
            };

            lblTransStatus = new Label
            {
                Text = "",
                ForeColor = UiPalette.TextMuted,
                Font = UiPalette.BodyFont(9f, FontStyle.Italic),
                AutoSize = true,
                Margin = new Padding(5, 12, 0, 0)
            };

            btnRow.Controls.Add(btnTranslate);
            btnRow.Controls.Add(progTranslate);
            btnRow.Controls.Add(lblTransStatus);

            headerStrip.Controls.Add(btnRow);
            headerStrip.Controls.Add(lblManufacturer);
            headerStrip.Controls.Add(lblDrugName);

            panelSections = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(0, 16, 0, 0)
            };

            panelDetail.Controls.Add(panelSections);
            panelDetail.Controls.Add(headerStrip);

            return panelDetail;
        }

        #endregion

        #region Custom ListBox drawing

        private void LstResults_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= _allResults.Length) return;

            var drug = _allResults[e.Index];
            bool selected = (e.State & DrawItemState.Selected) != 0;

            Color bgColor;
            if (selected)
                bgColor = UiPalette.Primary;
            else if (e.Index % 2 == 0)
                bgColor = Color.White;
            else
                bgColor = UiPalette.RowAlternate;

            e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);

            using var fontBold = UiPalette.BodyFont(11f, FontStyle.Bold);
            using var fontSmall = UiPalette.BodyFont(9f);

            Color fg1 = selected ? Color.White : UiPalette.TextPrimary;
            Color fg2 = selected ? UiPalette.PrimaryMuted : UiPalette.TextMuted;

            string brandName = drug.OpenFda?.BrandName?[0] ?? "Unknown Drug";
            string genericName = drug.OpenFda?.GenericName?[0] ?? "";

            var rect1 = new RectangleF(e.Bounds.X + 16, e.Bounds.Y + 8, e.Bounds.Width - 32, 22);
            var rect2 = new RectangleF(e.Bounds.X + 16, e.Bounds.Y + 28, e.Bounds.Width - 32, 18);

            e.Graphics.DrawString(brandName, fontBold, new SolidBrush(fg1), rect1);
            e.Graphics.DrawString(genericName, fontSmall, new SolidBrush(fg2), rect2);

            if (!selected)
            {
                using var pen = new Pen(UiPalette.Border, 1);
                e.Graphics.DrawLine(pen, e.Bounds.Left + 16, e.Bounds.Bottom - 1,
                    e.Bounds.Right - 16, e.Bounds.Bottom - 1);
            }

            e.DrawFocusRectangle();
        }

        #endregion

        #region Search

        private async Task DoSearchAsync()
        {
            string query = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(query)) return;

            btnSearch.Enabled = false;
            btnSearch.Text = "Searching...";
            lblResultsCount.Text = "Searching...";
            lblResultsCount.ForeColor = UiPalette.Primary;

            lstResults.Items.Clear();
            panelSections.Controls.Clear();
            _sections.Clear();
            _englishCache.Clear();
            _isTranslated = false;
            btnTranslate.Text = "🌐  Translate to Arabic";
            btnTranslate.Enabled = false;
            lblDrugName.Text = "Searching...";
            lblManufacturer.Text = "";

            try
            {
                DrugLabel[] localResults = Array.Empty<DrugLabel>();
                DrugLabel[] remoteResults = Array.Empty<DrugLabel>();
                bool useLocal = chkLocalSource.Checked;
                bool useOpenFda = chkOpenFdaSource.Checked;

                if (!useLocal && !useOpenFda)
                {
                    lblResultsCount.Text = "Select at least one source.";
                    lblResultsCount.ForeColor = Color.FromArgb(220, 38, 38);
                    return;
                }

                if (useLocal)
                {
                    localResults = await Task.Run(() => LocalEgyptDrugClient.SearchByName(query, 20));
                    await EnrichLocalResultsAsync(localResults);
                }

                if (useOpenFda)
                {
                    try
                    {
                        remoteResults = await FdaClient.SearchByNameAsync(query, 15);
                    }
                    catch
                    {
                        remoteResults = Array.Empty<DrugLabel>();
                    }

                    foreach (var remote in remoteResults)
                    {
                        remote.DataSource = "OpenFDA";
                    }
                }

                _allResults = localResults.Concat(remoteResults).ToArray();

                if (_allResults.Length == 0)
                {
                    lblResultsCount.Text = "No results found.";
                    lblResultsCount.ForeColor = Color.FromArgb(220, 38, 38);
                    lblDrugName.Text = "No results";
                }
                else
                {
                    int localCount = _allResults.Count(x => x.DataSource == "Local");
                    int remoteCount = _allResults.Count(x => x.DataSource == "OpenFDA");
                    lblResultsCount.Text = $"✓ {_allResults.Length} result(s): Local {localCount} | OpenFDA {remoteCount}";
                    lblResultsCount.ForeColor = UiPalette.Primary;

                    for (int i = 0; i < _allResults.Length; i++)
                    {
                        string sourceTag = _allResults[i].DataSource == "Local" ? "✓ Local" : "OpenFDA";
                        string brand = _allResults[i].OpenFda?.BrandName?[0] ?? $"Result {i + 1}";
                        lstResults.Items.Add($"[{sourceTag}] {brand}");
                    }

                    lstResults.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblResultsCount.Text = $"Error: {ex.Message}";
                lblResultsCount.ForeColor = Color.FromArgb(220, 38, 38);
                lblDrugName.Text = "Search failed";
            }
            finally
            {
                btnSearch.Enabled = true;
                btnSearch.Text = "SEARCH";
            }
        }

        #endregion

        #region Selection → show details

        private void LstResults_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int idx = lstResults.SelectedIndex;
            if (idx < 0 || idx >= _allResults.Length) return;

            _selected = _allResults[idx];
            _isTranslated = false;
            _englishCache.Clear();
            btnTranslate.Text = "🌐  Translate to Arabic";
            btnTranslate.Enabled = true;
            lblTransStatus.Text = string.Empty;

            ShowDrugDetails(_selected);
        }

        private void ShowDrugDetails(DrugLabel drug, bool translated = false)
        {
            panelSections.Controls.Clear();
            _sections.Clear();

            lblDrugName.Text = drug.OpenFda?.BrandName?[0] ?? "Unknown";
            lblManufacturer.Text = $"Generic: {Join(drug.OpenFda?.GenericName)}   |   " +
                $"Manufacturer: {Join(drug.OpenFda?.ManufacturerName)}   |   " +
                $"Route: {Join(drug.OpenFda?.Route)}   |   Source: {drug.DataSource}";

            var sections = new (string Title, string Icon, string Content, Color Accent)[]
            {
                ("Active Ingredient(s)", "💊", Join(drug.ActiveIngredient), UiPalette.Primary),
                ("Purpose", "🎯", Join(drug.Purpose), UiPalette.Primary),
                ("Uses / Indications", "📋", Join(drug.IndicationsAndUsage), UiPalette.Primary),
                ("Dosage & Direction", "🕐", Join(drug.DosageAndAdministration), Color.FromArgb(245, 158, 11)),
                ("Warnings", "⚠️", Join(drug.Warnings), Color.FromArgb(220, 38, 38)),
                ("Do NOT Use", "🚫", Join(drug.DoNotUse), Color.FromArgb(220, 38, 38)),
                ("Ask Your Doctor", "🩺", Join(drug.AskDoctor), Color.FromArgb(124, 58, 237)),
                ("When Using", "ℹ️", Join(drug.WhenUsing), Color.FromArgb(6, 148, 162)),
                ("Stop Use If", "🛑", Join(drug.StopUse), Color.FromArgb(220, 38, 38)),
                ("Overdose", "☠️", Join(drug.Overdosage), Color.FromArgb(220, 38, 38)),
                ("Pregnancy / Nursing", "🤰", Join(drug.PregnancyOrBreastFeeding), Color.FromArgb(219, 39, 119)),
                ("Storage", "📦", Join(drug.StorageAndHandling), UiPalette.TextMuted),
            };

            // استخدم Panel عادية بدل FlowLayoutPanel عشان التحكم أفضل
            var container = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                BackColor = Color.White,
                Padding = new Padding(0, 0, 0, 20)
            };

            int yOffset = 0;
            int cardWidth = panelSections.ClientSize.Width - 40;
            if (cardWidth < 200) cardWidth = 500; // قيمة افتراضية لو العرض صغير

            foreach (var (title, icon, content, accent) in sections)
            {
                if (string.IsNullOrWhiteSpace(content)) continue;

                var sc = new SectionControl(title, icon, content, accent)
                {
                    Width = cardWidth,
                    Location = new Point(0, yOffset),
                    Margin = new Padding(0, 0, 0, 12)
                };

                _sections.Add(sc);
                container.Controls.Add(sc);

                yOffset += sc.Height + 12;

                if (!translated)
                    _englishCache[title] = content;
            }

            container.Height = yOffset;
            panelSections.Controls.Add(container);

            // تحديث العرض لما يتغير الحجم
            panelSections.Resize += (s, ev) =>
            {
                if (panelSections.Controls.Count > 0 && panelSections.Controls[0] is Panel p)
                {
                    int newWidth = panelSections.ClientSize.Width - 40;
                    if (newWidth < 200) newWidth = 500;

                    int newYOffset = 0;
                    foreach (Control c in p.Controls)
                    {
                        if (c is SectionControl sc)
                        {
                            c.Width = newWidth;
                            c.Location = new Point(0, newYOffset);
                            newYOffset += c.Height + 12;
                        }
                    }
                    p.Height = newYOffset;
                }
            };
        }

        #endregion

        #region Translation

        private async Task DoTranslateAsync()
        {
            if (_selected == null) return;

            if (_isTranslated)
            {
                RestoreEnglish();
                _isTranslated = false;
                btnTranslate.Text = "🌐  Translate to Arabic";
                lblTransStatus.Text = "";
                return;
            }

            btnTranslate.Enabled = false;
            progTranslate.Visible = true;
            lblTransStatus.Text = "Translating... please wait";
            lblTransStatus.ForeColor = UiPalette.Primary;

            try
            {
                foreach (var sc in _sections)
                {
                    lblTransStatus.Text = $"Translating: {sc.SectionTitle}...";
                    Application.DoEvents();

                    string translated = await TranslationClient.TranslateAsync(sc.CurrentText);
                    sc.SetText(translated, rtl: true);
                }

                _isTranslated = true;
                btnTranslate.Text = "↩  Back to English";
                lblTransStatus.Text = "✓ Translation complete";
                lblTransStatus.ForeColor = UiPalette.Primary;
            }
            catch (Exception ex)
            {
                lblTransStatus.Text = $"✗ {ex.Message}";
                lblTransStatus.ForeColor = Color.FromArgb(220, 38, 38);
            }
            finally
            {
                progTranslate.Visible = false;
                btnTranslate.Enabled = true;
            }
        }

        private void RestoreEnglish()
        {
            foreach (var sc in _sections)
            {
                if (_englishCache.TryGetValue(sc.SectionTitle, out string? eng) && eng != null)
                    sc.SetText(eng, rtl: false);
            }
        }

        #endregion

        #region Helpers

        private static string Join(string[]? arr)
            => arr == null || arr.Length == 0 ? "" : string.Join(" ", arr).Trim();

        private static bool IsMissingDetails(DrugLabel drug)
        {
            return (drug.ActiveIngredient == null || drug.ActiveIngredient.Length == 0)
                && (drug.Purpose == null || drug.Purpose.Length == 0)
                && (drug.IndicationsAndUsage == null || drug.IndicationsAndUsage.Length == 0)
                && (drug.DosageAndAdministration == null || drug.DosageAndAdministration.Length == 0)
                && (drug.Warnings == null || drug.Warnings.Length == 0);
        }

        private static async Task EnrichLocalResultsAsync(IEnumerable<DrugLabel> locals)
        {
            foreach (var local in locals)
            {
                if (!IsMissingDetails(local))
                {
                    continue;
                }

                string brand = local.OpenFda?.BrandName?.FirstOrDefault() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(brand))
                {
                    continue;
                }

                DrugLabel[] fetched;
                try
                {
                    fetched = await FdaClient.SearchByNameAsync(brand, 1);
                }
                catch
                {
                    continue;
                }

                var extra = fetched.FirstOrDefault();
                if (extra is null)
                {
                    continue;
                }

                local.ActiveIngredient ??= extra.ActiveIngredient;
                local.Purpose ??= extra.Purpose;
                local.IndicationsAndUsage ??= extra.IndicationsAndUsage;
                local.DosageAndAdministration ??= extra.DosageAndAdministration;
                local.Warnings ??= extra.Warnings;
                local.DoNotUse ??= extra.DoNotUse;
                local.AskDoctor ??= extra.AskDoctor;
                local.WhenUsing ??= extra.WhenUsing;
                local.StopUse ??= extra.StopUse;
                local.Overdosage ??= extra.Overdosage;
                local.PregnancyOrBreastFeeding ??= extra.PregnancyOrBreastFeeding;
                local.StorageAndHandling ??= extra.StorageAndHandling;
            }
        }

        #endregion
    }

    public class SectionControl : Panel
    {
        public string SectionTitle { get; }
        public string CurrentText { get; private set; }

        private readonly Label _titleLabel;
        private readonly Label _bodyLabel;

        public SectionControl(string title, string icon, string body, Color accent)
        {
            SectionTitle = title;
            CurrentText = body;

            BackColor = UiPalette.Surface;
            Margin = new Padding(0, 0, 0, 12);
            Padding = new Padding(16, 14, 16, 14);

            Height = 100;

            Paint += (s, e) =>
            {
                using var brush = new SolidBrush(accent);
                e.Graphics.FillRectangle(brush, 0, 0, 6, Height);
                using var pen = new Pen(UiPalette.Border, 1);
                e.Graphics.DrawRectangle(pen, 6, 0, Width - 7, Height - 1);
            };

            _titleLabel = new Label
            {
                Text = $"{icon}  {title}",
                Font = UiPalette.BodyFont(10f, FontStyle.Bold),
                ForeColor = accent,
                Location = new Point(16, 14),
                AutoSize = true,
                MaximumSize = new Size(1000, 28)
            };

            _bodyLabel = new Label
            {
                Text = body,
                Font = UiPalette.BodyFont(10f),
                ForeColor = UiPalette.TextPrimary,
                Location = new Point(16, 42),
                AutoSize = true,
                MaximumSize = new Size(1000, 0)
            };

            Controls.Add(_titleLabel);
            Controls.Add(_bodyLabel);

            HandleCreated += (s, e) => RecalcHeight();

            SizeChanged += (s, e) => RecalcHeight();
        }

        public void SetText(string text, bool rtl)
        {
            CurrentText = text;
            _bodyLabel.Text = text;
            _bodyLabel.RightToLeft = rtl ? RightToLeft.Yes : RightToLeft.No;
            RecalcHeight();
        }

        private void RecalcHeight()
        {
            if (Width <= 50) return;

            int availableWidth = Width - Padding.Horizontal - 20;
            if (availableWidth <= 0) availableWidth = 400;

            _titleLabel.MaximumSize = new Size(availableWidth, 0);
            _bodyLabel.MaximumSize = new Size(availableWidth, 0);

            SizeF textSize = TextRenderer.MeasureText(
                _bodyLabel.Text,
                _bodyLabel.Font,
                new Size(availableWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);

            int bodyHeight = (int)textSize.Height + 10;
            _bodyLabel.Height = bodyHeight;

            Height = _titleLabel.Height + bodyHeight + Padding.Vertical + 20;
        }
    }
}
using UI.Controls;
using UI.Theme;

namespace UI.Views
{
    public sealed class ShellModuleView : UserControl
    {
        public string Title { get; }
        public string Subtitle { get; }

        public ShellModuleView(
            string title,
            string subtitle,
            string notesTitle,
            IEnumerable<string> notes,
            IEnumerable<ShellModuleStat> stats,
            ShellModuleLedger ledger)
        {
            Title = title;
            Subtitle = subtitle;

            BackColor = UiPalette.AppBackground;
            Dock = DockStyle.Fill;
            Padding = new Padding(0);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = UiPalette.AppBackground,
                Padding = new Padding(0)
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            root.Controls.Add(BuildStatsPanel(stats), 0, 0);
            root.Controls.Add(BuildWorkspace(notesTitle, notes, ledger), 0, 1);

            Controls.Add(root);
        }

        private Control BuildStatsPanel(IEnumerable<ShellModuleStat> stats)
        {
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 128,
                WrapContents = false,
                AutoSize = false,
                Margin = new Padding(0, 0, 0, 20),
                BackColor = Color.Transparent
            };

            foreach (var stat in stats)
            {
                flow.Controls.Add(BuildStatCard(stat));
            }

            return flow;
        }

        private Control BuildWorkspace(string notesTitle, IEnumerable<string> notes, ShellModuleLedger ledger)
        {
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                FixedPanel = FixedPanel.Panel1,
                SplitterDistance = 340,
                BackColor = UiPalette.AppBackground,
                IsSplitterFixed = false
            };

            split.Panel1.BackColor = UiPalette.AppBackground;
            split.Panel2.BackColor = UiPalette.AppBackground;
            split.Panel1.Padding = new Padding(0, 0, 12, 0);
            split.Panel2.Padding = new Padding(12, 0, 0, 0);

            split.Panel1.Controls.Add(BuildNotesPanel(notesTitle, notes));
            split.Panel2.Controls.Add(BuildLedgerPanel(ledger));

            return split;
        }

        private Control BuildNotesPanel(string title, IEnumerable<string> items)
        {
            var panel = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BorderColor = UiPalette.Border,
                CornerRadius = 12
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Transparent
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var titleLabel = new Label
            {
                AutoSize = true,
                Font = UiPalette.TitleFont(15F),
                ForeColor = UiPalette.TextPrimary,
                Text = title,
                Margin = new Padding(0, 0, 0, 12)
            };

            var list = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            foreach (var item in items)
            {
                list.Controls.Add(new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(270, 0),
                    Font = UiPalette.BodyFont(10.5F),
                    ForeColor = UiPalette.TextMuted,
                    Margin = new Padding(0, 0, 0, 14),
                    Text = $"- {item}"
                });
            }

            layout.Controls.Add(titleLabel, 0, 0);
            layout.Controls.Add(list, 0, 1);
            panel.Controls.Add(layout);
            return panel;
        }

        private Control BuildLedgerPanel(ShellModuleLedger ledger)
        {
            var panel = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BorderColor = UiPalette.Border,
                CornerRadius = 12
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var titleLabel = new Label
            {
                AutoSize = true,
                Font = UiPalette.TitleFont(15F),
                ForeColor = UiPalette.TextPrimary,
                Text = ledger.Title,
                Margin = new Padding(0, 0, 0, 6)
            };

            var subtitleLabel = new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(10F),
                ForeColor = UiPalette.TextMuted,
                Text = ledger.Subtitle,
                Margin = new Padding(0, 0, 0, 18)
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                MultiSelect = false,
                EnableHeadersVisualStyles = false,
                BackgroundColor = UiPalette.Surface,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = UiPalette.Border,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 42,
                RowTemplate = { Height = 38 },
                Font = UiPalette.BodyFont(10F)
            };

            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiPalette.PrimaryMuted,
                ForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F, FontStyle.Bold),
                SelectionBackColor = UiPalette.PrimaryMuted,
                SelectionForeColor = UiPalette.TextPrimary,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiPalette.Surface,
                ForeColor = UiPalette.TextPrimary,
                SelectionBackColor = UiPalette.PrimaryMuted,
                SelectionForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F),
                Padding = new Padding(8, 0, 8, 0)
            };

            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiPalette.RowAlternate,
                ForeColor = UiPalette.TextPrimary,
                SelectionBackColor = UiPalette.PrimaryMuted,
                SelectionForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F),
                Padding = new Padding(8, 0, 8, 0)
            };

            foreach (var column in ledger.Columns)
            {
                var gridColumn = new DataGridViewTextBoxColumn
                {
                    Name = column.Name,
                    HeaderText = column.Header,
                    FillWeight = column.FillWeight
                };

                if (column.AlignRight)
                {
                    gridColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                grid.Columns.Add(gridColumn);
            }

            foreach (var row in ledger.Rows)
            {
                grid.Rows.Add(row);
            }

            layout.Controls.Add(titleLabel, 0, 0);
            layout.Controls.Add(subtitleLabel, 0, 1);
            layout.Controls.Add(grid, 0, 2);

            panel.Controls.Add(layout);
            return panel;
        }

        private Control BuildStatCard(ShellModuleStat stat)
        {
            var card = new RoundedPanel
            {
                Width = 230,
                Height = 108,
                Padding = new Padding(20),
                Margin = new Padding(0, 0, 18, 0),
                BorderColor = UiPalette.Border,
                CornerRadius = 12
            };

            card.Controls.Add(new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(9.5F, FontStyle.Bold),
                ForeColor = UiPalette.TextMuted,
                Text = stat.Label,
                Location = new Point(20, 18)
            });

            card.Controls.Add(new Label
            {
                AutoSize = true,
                Font = UiPalette.TitleFont(16F),
                ForeColor = UiPalette.TextPrimary,
                Text = stat.Value,
                Location = new Point(20, 40)
            });

            card.Controls.Add(new Label
            {
                AutoSize = false,
                Width = 185,
                Height = 36,
                Font = UiPalette.BodyFont(9F),
                ForeColor = UiPalette.TextMuted,
                Text = stat.Description,
                Location = new Point(20, 68)
            });

            return card;
        }
    }

    public sealed record ShellModuleStat(string Label, string Value, string Description);
    public sealed record ShellModuleColumn(string Name, string Header, float FillWeight = 100F, bool AlignRight = false);
    public sealed record ShellModuleLedger(string Title, string Subtitle, IReadOnlyList<ShellModuleColumn> Columns, IReadOnlyList<string[]> Rows);
}

namespace UI.Views
{
    public sealed class ModuleView : UserControl
    {
        public string Title { get; }
        public string Subtitle { get; }

        public ModuleView(
            string title,
            string subtitle,
            string checklistTitle,
            IEnumerable<string> checklistItems,
            IEnumerable<ModuleStat> stats)
        {
            Title = title;
            Subtitle = subtitle;

            BackColor = Color.FromArgb(245, 247, 251);
            Dock = DockStyle.Fill;
            Padding = new Padding(24);

            var root = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            root.Controls.Add(BuildIntroPanel(title, subtitle), 0, 0);
            root.Controls.Add(BuildStatsPanel(stats), 0, 1);
            root.Controls.Add(BuildChecklistPanel(checklistTitle, checklistItems), 0, 2);

            Controls.Add(root);
        }

        private Control BuildIntroPanel(string title, string subtitle)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 122,
                BackColor = Color.White,
                Padding = new Padding(22),
                Margin = new Padding(0, 0, 0, 18)
            };

            var titleLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 32, 56),
                Text = title,
                Location = new Point(0, 0)
            };

            var subtitleLabel = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(86, 96, 118),
                Text = subtitle,
                Location = new Point(0, 42),
                Width = 820,
                Height = 56
            };

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(subtitleLabel);
            return panel;
        }

        private Control BuildStatsPanel(IEnumerable<ModuleStat> stats)
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 164,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, 18),
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };

            foreach (var stat in stats)
            {
                panel.Controls.Add(BuildStatCard(stat));
            }

            return panel;
        }

        private Control BuildChecklistPanel(string title, IEnumerable<string> items)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(22)
            };

            var titleLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 32, 56),
                Text = title
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Height = 220,
                Padding = new Padding(0, 8, 0, 0)
            };

            foreach (var item in items)
            {
                flow.Controls.Add(new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(860, 0),
                    Margin = new Padding(0, 0, 0, 12),
                    Font = new Font("Segoe UI", 10.5F),
                    ForeColor = Color.FromArgb(66, 75, 96),
                    Text = $"• {item}"
                });
            }

            panel.Controls.Add(flow);
            panel.Controls.Add(titleLabel);
            return panel;
        }

        private Control BuildStatCard(ModuleStat stat)
        {
            var card = new Panel
            {
                Width = 240,
                Height = 140,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 16, 0),
                Padding = new Padding(18)
            };

            var label = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 99, 124),
                Text = stat.Label
            };

            var value = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 32, 56),
                Text = stat.Value,
                Location = new Point(0, 28)
            };

            var description = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(86, 96, 118),
                Text = stat.Description,
                Location = new Point(0, 66),
                Width = 200,
                Height = 54
            };

            card.Controls.Add(label);
            card.Controls.Add(value);
            card.Controls.Add(description);
            return card;
        }
    }

    public sealed record ModuleStat(string Label, string Value, string Description);
}

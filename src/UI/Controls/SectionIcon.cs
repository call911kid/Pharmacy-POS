using UI.Theme;

namespace UI.Controls
{
    internal enum SectionIconKind
    {
        Dashboard,
        Register,
        Inventory,
        Supplier,
        Customer,
        Adjustment,
        Scanner
    }

    internal sealed class SectionIcon : Control
    {
        public SectionIconKind Kind { get; set; }
        public Color AccentColor { get; set; } = UiPalette.Primary;

        public SectionIcon()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new Size(18, 18);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(AccentColor, 1.8f)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };
            using var brush = new SolidBrush(AccentColor);

            var rect = new Rectangle(2, 2, Width - 4, Height - 4);

            switch (Kind)
            {
                case SectionIconKind.Dashboard:
                    e.Graphics.DrawRectangle(pen, 3, 3, 5, 5);
                    e.Graphics.DrawRectangle(pen, 10, 3, 5, 5);
                    e.Graphics.DrawRectangle(pen, 3, 10, 5, 5);
                    e.Graphics.DrawRectangle(pen, 10, 10, 5, 5);
                    break;
                case SectionIconKind.Register:
                    e.Graphics.DrawRectangle(pen, 2, 5, 13, 10);
                    e.Graphics.DrawLine(pen, 7, 2, 16, 11);
                    break;
                case SectionIconKind.Inventory:
                    e.Graphics.DrawRectangle(pen, 4, 3, 9, 12);
                    e.Graphics.DrawLine(pen, 6, 3, 6, 1);
                    e.Graphics.DrawLine(pen, 11, 3, 11, 1);
                    e.Graphics.FillEllipse(brush, 7, 6, 3, 3);
                    break;
                case SectionIconKind.Supplier:
                    e.Graphics.DrawRectangle(pen, 2, 8, 10, 6);
                    e.Graphics.DrawLine(pen, 12, 9, 15, 9);
                    e.Graphics.DrawEllipse(pen, 4, 13, 2, 2);
                    e.Graphics.DrawEllipse(pen, 10, 13, 2, 2);
                    break;
                case SectionIconKind.Customer:
                    e.Graphics.DrawEllipse(pen, 6, 2, 6, 6);
                    e.Graphics.DrawArc(pen, 3, 8, 12, 8, 200, 140);
                    break;
                case SectionIconKind.Adjustment:
                    e.Graphics.DrawLine(pen, 3, 5, 15, 5);
                    e.Graphics.DrawLine(pen, 3, 9, 15, 9);
                    e.Graphics.DrawLine(pen, 3, 13, 15, 13);
                    e.Graphics.FillEllipse(brush, 5, 3, 4, 4);
                    e.Graphics.FillEllipse(brush, 9, 7, 4, 4);
                    e.Graphics.FillEllipse(brush, 6, 11, 4, 4);
                    break;
                case SectionIconKind.Scanner:
                    e.Graphics.DrawArc(pen, rect, 0, 360);
                    e.Graphics.DrawLine(pen, 9, 4, 9, 14);
                    e.Graphics.DrawLine(pen, 4, 9, 14, 9);
                    break;
            }
        }
    }
}

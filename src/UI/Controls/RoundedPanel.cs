using System.Drawing.Drawing2D;
using UI.Theme;
using System.ComponentModel;

namespace UI.Controls
{
    public class RoundedPanel : Panel
    {
        private int _cornerRadius = 12;
        private int _borderThickness = 1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = Math.Max(1, value);
                UpdateRegion();
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = UiPalette.Border;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = Math.Max(1, value);
                Invalidate();
            }
        }

        public RoundedPanel()
        {
            BackColor = UiPalette.Surface;
            Resize += (_, _) => UpdateRegion();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = CreatePath(ClientRectangle, CornerRadius);
            using var pen = new Pen(BorderColor, BorderThickness);
            e.Graphics.DrawPath(pen, path);
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            using var path = CreatePath(ClientRectangle, CornerRadius);
            Region = new Region(path);
        }

        public static GraphicsPath CreatePath(Rectangle bounds, int radius)
        {
            var diameter = radius * 2;
            var rectangle = Rectangle.Inflate(bounds, -1, -1);
            var path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(rectangle.X, rectangle.Y, diameter, diameter, 180, 90);
            path.AddArc(rectangle.Right - diameter, rectangle.Y, diameter, diameter, 270, 90);
            path.AddArc(rectangle.Right - diameter, rectangle.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rectangle.X, rectangle.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}

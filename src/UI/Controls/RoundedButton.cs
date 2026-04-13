using System.Drawing.Drawing2D;
using UI.Theme;
using System.ComponentModel;

namespace UI.Controls
{
    public class RoundedButton : Button
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 10;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ButtonColor { get; set; } = UiPalette.Primary;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = UiPalette.Primary;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color TextColor { get; set; } = Color.White;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = TextColor;
            Font = UiPalette.BodyFont(10F, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(Parent?.BackColor ?? UiPalette.AppBackground);

            using var path = RoundedPanel.CreatePath(ClientRectangle, CornerRadius);
            using var brush = new SolidBrush(ButtonColor);
            using var borderPen = new Pen(BorderColor, 1);

            pevent.Graphics.FillPath(brush, path);
            pevent.Graphics.DrawPath(borderPen, path);

            TextRenderer.DrawText(
                pevent.Graphics,
                Text,
                Font,
                ClientRectangle,
                TextColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}

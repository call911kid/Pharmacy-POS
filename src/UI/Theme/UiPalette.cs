namespace UI.Theme
{
    internal static class UiPalette
    {
        public static readonly Color AppBackground = Color.FromArgb(244, 247, 249);
        public static readonly Color Surface = Color.White;
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color Primary = Color.FromArgb(2, 132, 199);
        public static readonly Color PrimaryMuted = Color.FromArgb(224, 242, 254);
        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);
        public static readonly Color RowAlternate = Color.FromArgb(248, 250, 252);

        public static Font TitleFont(float size) => new("Georgia", size, FontStyle.Bold);
        public static Font BodyFont(float size, FontStyle style = FontStyle.Regular) => new("Segoe UI", size, style);
    }
}

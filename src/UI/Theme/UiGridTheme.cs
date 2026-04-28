namespace UI.Theme
{
    internal static class UiGridTheme
    {
        public static void ApplyReadOnly(DataGridView grid)
        {
            ApplyBase(grid);
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public static void ApplyEditable(DataGridView grid)
        {
            ApplyBase(grid);
            grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private static void ApplyBase(DataGridView grid)
        {
            grid.BackgroundColor = UiPalette.Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = UiPalette.Border;
            grid.MultiSelect = false;

            grid.DefaultCellStyle.BackColor = UiPalette.Surface;
            grid.DefaultCellStyle.ForeColor = UiPalette.TextPrimary;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 247);
            grid.DefaultCellStyle.SelectionForeColor = UiPalette.TextPrimary;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.DefaultCellStyle.Font = UiPalette.BodyFont(9f);

            grid.AlternatingRowsDefaultCellStyle.BackColor = UiPalette.RowAlternate;

            grid.ColumnHeadersDefaultCellStyle.BackColor = UiPalette.AppBackground;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = UiPalette.TextPrimary;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiPalette.AppBackground;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiPalette.TextPrimary;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.Font = UiPalette.BodyFont(9.5f);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            grid.RowHeadersVisible = false;
        }
    }
}

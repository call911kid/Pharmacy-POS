using BLL.DTOs.Batch;
using BLL.DTOs.Dashboard;
using BLL.Interfaces;
using UI.Theme;

namespace UI.Views.Dashboard
{
    public partial class DashboardPage : UserControl
    {
        private readonly IDashboardService _dashboardService;
        private readonly BindingSource _recentInvoicesBindingSource = new();
        private readonly BindingSource _recentBatchesBindingSource = new();
        private readonly BindingSource _lowStockBindingSource = new();
        private readonly BindingSource _expiringBindingSource = new();

        public DashboardPage(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            InitializeComponent();
            ApplyPalette();
            ConfigureGrids();
            Load += async (_, _) => await LoadOverviewAsync();
        }

        private void ApplyPalette()
        {
            BackColor = UiPalette.AppBackground;
            rootLayout.BackColor = UiPalette.AppBackground;
            headerLayout.BackColor = UiPalette.AppBackground;
            middleLayout.BackColor = UiPalette.AppBackground;
            alertsLayout.BackColor = UiPalette.AppBackground;

            StyleHeader();

            StyleSection(recentInvoicesLayout, recentInvoicesTitleLbl);
            StyleSection(recentBatchesLayout, recentBatchesTitleLbl);
            StyleSection(lowStockLayout, lowStockTitleLbl);
            StyleSection(expiringLayout, expiringTitleLbl);
        }

        private void StyleHeader()
        {
            
            
        }

        private static void StyleSection(TableLayoutPanel sectionLayout, Label titleLabel)
        {
            sectionLayout.BackColor = UiPalette.Surface;
            sectionLayout.Padding = new Padding(8);
            sectionLayout.Margin = new Padding(6);

            titleLabel.Font = UiPalette.BodyFont(10.5f);
            titleLabel.ForeColor = UiPalette.TextPrimary;
        }

        private void ConfigureGrids()
        {
            recentInvoicesGrid.AutoGenerateColumns = false;
            recentInvoicesGrid.DataSource = _recentInvoicesBindingSource;
            recentBatchesGrid.AutoGenerateColumns = false;
            recentBatchesGrid.DataSource = _recentBatchesBindingSource;
            lowStockGrid.AutoGenerateColumns = false;
            lowStockGrid.DataSource = _lowStockBindingSource;
            expiringGrid.AutoGenerateColumns = false;
            expiringGrid.DataSource = _expiringBindingSource;

            invoiceIdColumn.DataPropertyName = nameof(DashboardInvoiceDto.Id);
            invoiceCustomerIdColumn.DataPropertyName = nameof(DashboardInvoiceDto.CustomerName);
            invoiceDateColumn.DataPropertyName = nameof(DashboardInvoiceDto.InvoiceDate);
            invoiceTotalAmountColumn.DataPropertyName = nameof(DashboardInvoiceDto.TotalAmount);
            invoiceStatusColumn.DataPropertyName = nameof(DashboardInvoiceDto.Status);

            batchIdColumn.DataPropertyName = nameof(BatchSummaryDto.Id);
            supplierNameColumn.DataPropertyName = nameof(BatchSummaryDto.SupplierName);
            purchaseDateColumn.DataPropertyName = nameof(BatchSummaryDto.PurchaseDate);
            itemsCountColumn.DataPropertyName = nameof(BatchSummaryDto.ItemsCount);
            totalQuantityRemainingColumn.DataPropertyName = nameof(BatchSummaryDto.TotalQuantityRemaining);
            lowStockProductColumn.DataPropertyName = nameof(DashboardAlertItemDto.ProductName);
            lowStockQtyColumn.DataPropertyName = nameof(DashboardAlertItemDto.QuantityRemaining);
            lowStockExpiryColumn.DataPropertyName = nameof(DashboardAlertItemDto.ExpirationDate);
            expiringProductColumn.DataPropertyName = nameof(DashboardAlertItemDto.ProductName);
            expiringQtyColumn.DataPropertyName = nameof(DashboardAlertItemDto.QuantityRemaining);
            expiringDateColumn.DataPropertyName = nameof(DashboardAlertItemDto.ExpirationDate);

            invoiceDateColumn.DefaultCellStyle.Format = "dd-MMM-yyyy";
            invoiceTotalAmountColumn.DefaultCellStyle.Format = "0.00 'EGP'";
            lowStockExpiryColumn.DefaultCellStyle.Format = "dd-MMM-yyyy";
            expiringDateColumn.DefaultCellStyle.Format = "dd-MMM-yyyy";

            ConfigureGridTheme(recentInvoicesGrid);
            ConfigureGridTheme(recentBatchesGrid);
            ConfigureGridTheme(lowStockGrid);
            ConfigureGridTheme(expiringGrid);

            lowStockGrid.DataBindingComplete += (_, _) => ApplyLowStockHighlighting();
            expiringGrid.DataBindingComplete += (_, _) => ApplyExpiringHighlighting();
        }

        private static void ConfigureGridTheme(DataGridView grid)
        {
            grid.BackgroundColor = UiPalette.Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = UiPalette.Border;
            grid.DefaultCellStyle.BackColor = UiPalette.Surface;
            grid.DefaultCellStyle.ForeColor = UiPalette.TextPrimary;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 247);
            grid.DefaultCellStyle.SelectionForeColor = UiPalette.TextPrimary;
            grid.AlternatingRowsDefaultCellStyle.BackColor = UiPalette.RowAlternate;
            grid.ColumnHeadersDefaultCellStyle.BackColor = UiPalette.AppBackground;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = UiPalette.TextPrimary;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = UiPalette.AppBackground;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = UiPalette.TextPrimary;
            grid.ColumnHeadersDefaultCellStyle.Font = UiPalette.BodyFont(9.5f);
            grid.DefaultCellStyle.Font = UiPalette.BodyFont(9f);
            grid.MultiSelect = false;
        }

        private void ApplyLowStockHighlighting()
        {
            foreach (DataGridViewRow row in lowStockGrid.Rows)
            {
                if (row.DataBoundItem is not DashboardAlertItemDto item)
                {
                    continue;
                }

                row.DefaultCellStyle.BackColor = item.QuantityRemaining <= 3
                    ? Color.FromArgb(254, 226, 226)
                    : Color.FromArgb(255, 247, 237);
            }
        }

        private void ApplyExpiringHighlighting()
        {
            var today = DateTime.Today;

            foreach (DataGridViewRow row in expiringGrid.Rows)
            {
                if (row.DataBoundItem is not DashboardAlertItemDto item)
                {
                    continue;
                }

                var daysUntilExpiry = (item.ExpirationDate.Date - today).TotalDays;
                row.DefaultCellStyle.BackColor = daysUntilExpiry <= 7
                    ? Color.FromArgb(254, 242, 242)
                    : Color.FromArgb(255, 251, 235);
            }
        }

        private async Task LoadOverviewAsync()
        {
            try
            {
                var overview = await _dashboardService.GetOverviewAsync();
                label2.Text = DateTime.Today.ToString("dddd, dd MMM yyyy");

                _recentInvoicesBindingSource.DataSource = overview.RecentInvoices;
                _recentBatchesBindingSource.DataSource = overview.RecentBatches;
                _lowStockBindingSource.DataSource = overview.LowStockItems;
                _expiringBindingSource.DataSource = overview.ExpiringItems;

                ClearGridSelections();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Dashboard",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearGridSelections()
        {
            recentInvoicesGrid.ClearSelection();
            recentBatchesGrid.ClearSelection();
            lowStockGrid.ClearSelection();
            expiringGrid.ClearSelection();
        }
    }
}

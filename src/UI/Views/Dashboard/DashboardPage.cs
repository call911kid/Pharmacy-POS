using BLL.DTOs.Batch;
using BLL.DTOs.Dashboard;
using BLL.Interfaces;

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
            ConfigureGrids();
            Load += async (_, _) => await LoadOverviewAsync();
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
            invoiceCustomerIdColumn.DataPropertyName = nameof(DashboardInvoiceDto.CustomerId);
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
            invoiceTotalAmountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            lowStockExpiryColumn.DefaultCellStyle.Format = "dd-MMM-yyyy";
            expiringDateColumn.DefaultCellStyle.Format = "dd-MMM-yyyy";
        }

        private async Task LoadOverviewAsync()
        {
            try
            {
                var overview = await _dashboardService.GetOverviewAsync();

                customersValueLbl.Text = overview.CustomersCount.ToString();
                suppliersValueLbl.Text = overview.SuppliersCount.ToString();
                productsValueLbl.Text = overview.ProductsCount.ToString();
                batchesValueLbl.Text = overview.BatchesCount.ToString();

                _recentInvoicesBindingSource.DataSource = overview.RecentInvoices;
                _recentBatchesBindingSource.DataSource = overview.RecentBatches;
                _lowStockBindingSource.DataSource = overview.LowStockItems;
                _expiringBindingSource.DataSource = overview.ExpiringItems;
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
    }
}

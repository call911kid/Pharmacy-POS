using BLL.DTOs.Batch;
using BLL.DTOs.BatchItem;
using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UI.Events;
using UI.Forms.BatchDialog;

namespace UI.Views.Inventory
{
    public partial class InventoryPage : UserControl
    {
        private readonly IBatchService _batchService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly ScannerEventBus _scannerEventBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly BindingSource _batchesBindingSource = new();
        private readonly BindingSource _batchItemsBindingSource = new();
        private IReadOnlyList<BatchSummaryDto> _batches = Array.Empty<BatchSummaryDto>();
        private bool _isLoading;

        public InventoryPage(
            IBatchService batchService,
            ISupplierService supplierService,
            IProductService productService,
            ScannerEventBus scannerEventBus,
            IServiceProvider serviceProvider)
        {
            _batchService = batchService;
            _supplierService = supplierService;
            _productService = productService;
            _scannerEventBus = scannerEventBus;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            ConfigureGrids();
            SubscribeToEvents();
            ClearBatchDetails();
        }

        private void ConfigureGrids()
        {
            batchesGrid.AutoGenerateColumns = false;
            batchesGrid.DataSource = _batchesBindingSource;

            batchItemsGrid.AutoGenerateColumns = false;
            batchItemsGrid.DataSource = _batchItemsBindingSource;

            batchIdColumn.DataPropertyName = nameof(BatchSummaryDto.Id);
            supplierColumn.DataPropertyName = nameof(BatchSummaryDto.SupplierName);
            purchaseDateColumn.DataPropertyName = nameof(BatchSummaryDto.PurchaseDate);
            itemsCountColumn.DataPropertyName = nameof(BatchSummaryDto.ItemsCount);
            totalRemainingColumn.DataPropertyName = nameof(BatchSummaryDto.TotalQuantityRemaining);

            productColumn.DataPropertyName = nameof(BatchItemDto.ProductName);
            quantityReceivedColumn.DataPropertyName = nameof(BatchItemDto.QuantityReceived);
            quantityRemainingColumn.DataPropertyName = nameof(BatchItemDto.QuantityRemaining);
            expirationDateColumn.DataPropertyName = nameof(BatchItemDto.ExpirationDate);
            costPriceColumn.DataPropertyName = nameof(BatchItemDto.CostPrice);
            sellingPriceColumn.DataPropertyName = nameof(BatchItemDto.MandatorySellingPrice);

            costPriceColumn.DefaultCellStyle.Format = "0.00 'EGP'";
            costPriceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            sellingPriceColumn.DefaultCellStyle.Format = "0.00 'EGP'";
            sellingPriceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SubscribeToEvents()
        {
            Load += async (_, _) => await LoadBatchesAsync();
            searchTextBox.TextChanged += (_, _) => ApplyFilter();
            batchesGrid.SelectionChanged += async (_, _) => await LoadSelectedBatchDetailsAsync();
            addBatchBtn.Click += async (_, _) => await OpenBatchDialogAsync();
            editBatchBtn.Click += async (_, _) => await OpenBatchDialogAsync(GetSelectedBatch()?.Id);
            deleteBatchBtn.Click += async (_, _) => await DeleteSelectedBatchAsync();
        }

        private async Task LoadBatchesAsync()
        {
            try
            {
                _isLoading = true;
                ToggleBusyState(true);
                _batches = (await _batchService.GetBatchSummariesAsync(1, 500)).ToList();
                ApplyFilter();
                await LoadSelectedBatchDetailsAsync();
            }
            finally
            {
                ToggleBusyState(false);
                _isLoading = false;
            }
        }

        private void ApplyFilter()
        {
            var searchTerm = searchTextBox.Text.Trim();
            IEnumerable<BatchSummaryDto> filtered = _batches;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filtered = filtered.Where(batch =>
                    batch.Id.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    batch.SupplierName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            _batchesBindingSource.DataSource = filtered.ToList();
        }

        private BatchSummaryDto? GetSelectedBatch()
        {
            return batchesGrid.CurrentRow?.DataBoundItem as BatchSummaryDto;
        }

        private async Task LoadSelectedBatchDetailsAsync()
        {
            var selectedBatch = GetSelectedBatch();

            if (selectedBatch is null)
            {
                _batchItemsBindingSource.DataSource = new List<BatchItemDto>();
                ClearBatchDetails();
                return;
            }

            try
            {
                var batchDetails = await _batchService.GetBatchByIdAsync(selectedBatch.Id);
                _batchItemsBindingSource.DataSource = batchDetails.Items.ToList();
                PopulateBatchDetails(selectedBatch);
            }
            catch
            {
                _batchItemsBindingSource.DataSource = new List<BatchItemDto>();
                ClearBatchDetails();
            }
        }

        private void PopulateBatchDetails(BatchSummaryDto batch)
        {
            batchIdValueLbl.Text = batch.Id.ToString();
            supplierValueLbl.Text = batch.SupplierName;
            purchaseDateValueLbl.Text = batch.PurchaseDate.ToShortDateString();
            itemsCountValueLbl.Text = batch.ItemsCount.ToString();
            totalRemainingValueLbl.Text = batch.TotalQuantityRemaining.ToString();
        }

        private void ClearBatchDetails()
        {
            batchIdValueLbl.Text = "--";
            supplierValueLbl.Text = "--";
            purchaseDateValueLbl.Text = "--";
            itemsCountValueLbl.Text = "--";
            totalRemainingValueLbl.Text = "--";
        }

        private async Task OpenBatchDialogAsync(int? batchId = null)
        {
            using var dialog = batchId is int id
                ? ActivatorUtilities.CreateInstance<BatchDialog>(_serviceProvider, id)
                : _serviceProvider.GetRequiredService<BatchDialog>();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadBatchesAsync();
            }
        }

        private async Task DeleteSelectedBatchAsync()
        {
            var batch = GetSelectedBatch();
            if (batch is null)
            {
                MessageBox.Show(
                    "Select a batch first.",
                    "Inventory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirmation = MessageBox.Show(
                $"Delete batch #{batch.Id}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                ToggleBusyState(true);
                await _batchService.DeleteBatchAsync(batch.Id);
                await LoadBatchesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not delete batch.\n\n{ex.Message}",
                    "Inventory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private void ToggleBusyState(bool isBusy)
        {
            searchTextBox.Enabled = !isBusy;
            addBatchBtn.Enabled = !isBusy;
            editBatchBtn.Enabled = !isBusy && !_isLoading && GetSelectedBatch() is not null;
            deleteBatchBtn.Enabled = !isBusy && !_isLoading && GetSelectedBatch() is not null;
            batchesGrid.Enabled = !isBusy;
            batchItemsGrid.Enabled = !isBusy;
        }
    }
}

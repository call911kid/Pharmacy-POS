using BLL.DTOs.Batch;
using BLL.DTOs.Supplier;
using BLL.Interfaces;
using UI.Forms.SupplierDialog;

namespace UI.Views.Suppliers
{
    public partial class SuppliersPage : UserControl
    {
        private readonly ISupplierService _supplierService;
        private readonly IBatchService _batchService;
        private readonly BindingSource _suppliersBindingSource = new();
        private readonly BindingSource _batchesBindingSource = new();
        private IReadOnlyList<SupplierDto> _suppliers = Array.Empty<SupplierDto>();
        private bool _isLoading;

        public SuppliersPage(ISupplierService supplierService, IBatchService batchService)
        {
            _supplierService = supplierService;
            _batchService = batchService;
            InitializeComponent();
            ConfigureGrids();
            SubscribeToEvents();
        }

        private void ConfigureGrids()
        {
            suppliersGrid.AutoGenerateColumns = false;
            suppliersGrid.DataSource = _suppliersBindingSource;

            supplierBatchesGrid.AutoGenerateColumns = false;
            supplierBatchesGrid.DataSource = _batchesBindingSource;

            batchIdColumn.DataPropertyName = nameof(BatchSummaryDto.Id);
            purchaseDateColumn.DataPropertyName = nameof(BatchSummaryDto.PurchaseDate);
            itemsCountColumn.DataPropertyName = nameof(BatchSummaryDto.ItemsCount);
            totalReceivedColumn.DataPropertyName = nameof(BatchSummaryDto.TotalQuantityReceived);
            totalRemainingColumn.DataPropertyName = nameof(BatchSummaryDto.TotalQuantityRemaining);
        }

        private void SubscribeToEvents()
        {
            Load += async (_, _) => await LoadSuppliersAsync();
            searchTextBox.TextChanged += (_, _) => ApplyFilter();
            suppliersGrid.SelectionChanged += async (_, _) =>
            {
                UpdateSelectionState();
                await LoadSelectedSupplierBatchesAsync();
            };
            addBtn.Click += async (_, _) => await OpenSupplierEditorAsync();
            editBtn.Click += async (_, _) => await OpenSupplierEditorAsync(GetSelectedSupplier());
            deleteBtn.Click += async (_, _) => await DeleteSelectedSupplierAsync();
        }

        private async Task LoadSuppliersAsync()
        {
            try
            {
                _isLoading = true;
                ToggleBusyState(true);
                _suppliers = (await _supplierService.GetAllSuppliersAsync(1, 500)).ToList();
                ApplyFilter();
                UpdateSelectionState();
                await LoadSelectedSupplierBatchesAsync();
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
            IEnumerable<SupplierDto> filtered = _suppliers;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filtered = filtered.Where(supplier =>
                    supplier.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    supplier.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            _suppliersBindingSource.DataSource = filtered.ToList();
        }

        private SupplierDto? GetSelectedSupplier()
        {
            return suppliersGrid.CurrentRow?.DataBoundItem as SupplierDto;
        }

        private async Task LoadSelectedSupplierBatchesAsync()
        {
            var selectedSupplier = GetSelectedSupplier();

            if (selectedSupplier is null)
            {
                _batchesBindingSource.DataSource = new List<BatchSummaryDto>();
                return;
            }

            try
            {
                var batches = await _batchService.GetBatchesBySupplierAsync(selectedSupplier.Id);
                _batchesBindingSource.DataSource = batches.ToList();
            }
            catch
            {
                _batchesBindingSource.DataSource = new List<BatchSummaryDto>();
            }
        }

        private async Task OpenSupplierEditorAsync(SupplierDto? supplier = null)
        {
            using var form = new SupplierDialog(_supplierService, supplier);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                await LoadSuppliersAsync();
            }
        }

        private async Task DeleteSelectedSupplierAsync()
        {
            var supplier = GetSelectedSupplier();

            if (supplier is null)
            {
                MessageBox.Show(
                    "Select a supplier first.",
                    "Suppliers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirmation = MessageBox.Show(
                $"Delete supplier '{supplier.Name}'?",
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
                await _supplierService.DeleteSupplierAsync(supplier.Id);
                await LoadSuppliersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Delete Supplier",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private void ToggleBusyState(bool isBusy)
        {
            searchTextBox.Enabled = !isBusy;
            addBtn.Enabled = !isBusy;
            suppliersGrid.Enabled = !isBusy;
            supplierBatchesGrid.Enabled = !isBusy;
            UpdateSelectionState();
        }

        private void UpdateSelectionState()
        {
            var hasSelection = GetSelectedSupplier() is not null;
            editBtn.Enabled = hasSelection && !_isLoading;
            deleteBtn.Enabled = hasSelection && !_isLoading;
        }
    }
}

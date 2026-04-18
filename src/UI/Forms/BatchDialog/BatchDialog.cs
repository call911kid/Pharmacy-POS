using System.ComponentModel;
using BLL.DTOs.Batch;
using BLL.DTOs.BatchItem;
using BLL.DTOs.Product;
using BLL.DTOs.Supplier;
using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UI.Events;
using UI.Forms;
using SupplierDialogForm = UI.Forms.SupplierDialog.SupplierDialog;

namespace UI.Forms.BatchDialog
{
    public partial class BatchDialog : Form
    {
        private readonly IBatchService _batchService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly ScannerEventBus _scannerEventBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly int? _batchId;
        private readonly BindingList<BatchItemEditorRow> _itemsBinding = new();

        private bool _isBusy;

        public BatchDialog(
            IBatchService batchService,
            ISupplierService supplierService,
            IProductService productService,
            ScannerEventBus scannerEventBus,
            IServiceProvider serviceProvider)
            : this(
                batchService,
                supplierService,
                productService,
                scannerEventBus,
                serviceProvider,
                null)
        {
        }

        public BatchDialog(
            IBatchService batchService,
            ISupplierService supplierService,
            IProductService productService,
            ScannerEventBus scannerEventBus,
            IServiceProvider serviceProvider,
            int batchId)
            : this(
                batchService,
                supplierService,
                productService,
                scannerEventBus,
                serviceProvider,
                (int?)batchId)
        {
        }

        private BatchDialog(
            IBatchService batchService,
            ISupplierService supplierService,
            IProductService productService,
            ScannerEventBus scannerEventBus,
            IServiceProvider serviceProvider,
            int? batchId)
        {
            _batchService = batchService;
            _supplierService = supplierService;
            _productService = productService;
            _scannerEventBus = scannerEventBus;
            _serviceProvider = serviceProvider;
            _batchId = batchId;

            InitializeComponent();
            ConfigureForm();
            ConfigureGrid();
            SubscribeToEvents();
        }

        private void ConfigureForm()
        {
            Text = _batchId is null ? "Add Batch" : "Edit Batch";
            batchItemsLabel.Text = _batchId is null ? "Batch Items" : $"Batch Items for #{_batchId}";
            purchaseDatePicker.Value = DateTime.Today;
            scanStatusLabel.Text = "Select a row and scan a product barcode to attach it quickly.";
            scanStatusLabel.ForeColor = SystemColors.ControlText;
        }

        private void ConfigureGrid()
        {
            itemsGrid.AutoGenerateColumns = false;
            itemsGrid.DataSource = _itemsBinding;

            productIdColumn.DataPropertyName = nameof(BatchItemEditorRow.ProductId);
            barcodeColumn.DataPropertyName = nameof(BatchItemEditorRow.Barcode);
            productColumn.DataPropertyName = nameof(BatchItemEditorRow.ProductName);
            qtyReceivedColumn.DataPropertyName = nameof(BatchItemEditorRow.QuantityReceived);
            qtyRemainingColumn.DataPropertyName = nameof(BatchItemEditorRow.QuantityRemaining);
            expiryColumn.DataPropertyName = nameof(BatchItemEditorRow.ExpirationDate);
            costPriceColumn.DataPropertyName = nameof(BatchItemEditorRow.CostPrice);
            sellingPriceColumn.DataPropertyName = nameof(BatchItemEditorRow.MandatorySellingPrice);

            expiryColumn.DefaultCellStyle.Format = "d";
            costPriceColumn.DefaultCellStyle.Format = "0.00";
            sellingPriceColumn.DefaultCellStyle.Format = "0.00";
        }

        private void SubscribeToEvents()
        {
            Load += async (_, _) => await LoadDataAsync();
            barcodeTextBox.KeyDown += async (_, e) =>
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }

                e.SuppressKeyPress = true;
                await AssignProductByBarcodeAsync(barcodeTextBox.Text.Trim(), true);
            };

            useBarcodeBtn.Click += async (_, _) => await AssignProductByBarcodeAsync(barcodeTextBox.Text.Trim(), true);
            addProductBtn.Click += async (_, _) => await OpenAddProductAsync();
            addSupplierBtn.Click += async (_, _) => await OpenAddSupplierAsync();
            addItemBtn.Click += (_, _) => AddNewItem();
            removeItemBtn.Click += (_, _) => RemoveSelectedItem();
            saveBtn.Click += async (_, _) => await SaveAsync();
            clearBtn.Click += (_, _) => ClearForm();
            cancelBtn.Click += (_, _) => DialogResult = DialogResult.Cancel;
            _scannerEventBus.BarcodeScanned += OnBarcodeScanned;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                ToggleBusyState(true);
                await LoadSuppliersAsync();

                if (_batchId is not null)
                {
                    await PopulateBatchAsync(_batchId.Value);
                }

                if (_itemsBinding.Count == 0)
                {
                    AddNewItem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Batch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private async Task LoadSuppliersAsync(int? selectedSupplierId = null)
        {
            var suppliers = (await _supplierService.GetAllSuppliersAsync(1, 500)).ToList();
            supplierComboBox.DataSource = suppliers;
            supplierComboBox.DisplayMember = nameof(SupplierDto.Name);
            supplierComboBox.ValueMember = nameof(SupplierDto.Id);

            if (selectedSupplierId is int supplierId)
            {
                supplierComboBox.SelectedValue = supplierId;
            }
            else if (suppliers.Count > 0)
            {
                supplierComboBox.SelectedIndex = 0;
            }
        }

        private async Task PopulateBatchAsync(int batchId)
        {
            var batch = await _batchService.GetBatchByIdAsync(batchId);
            supplierComboBox.SelectedValue = batch.SupplierId;
            purchaseDatePicker.Value = batch.PurchaseDate;
            _itemsBinding.Clear();

            foreach (var item in batch.Items)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                _itemsBinding.Add(new BatchItemEditorRow
                {
                    ProductId = item.ProductId,
                    Barcode = product?.Barcode ?? string.Empty,
                    ProductName = product?.Name ?? item.ProductName,
                    QuantityReceived = item.QuantityReceived,
                    QuantityRemaining = item.QuantityRemaining,
                    ExpirationDate = item.ExpirationDate,
                    CostPrice = item.CostPrice,
                    MandatorySellingPrice = item.MandatorySellingPrice
                });
            }
        }

        private async Task SaveAsync()
        {
            if (supplierComboBox.SelectedValue is not int supplierId)
            {
                MessageBox.Show(
                    "Please select a supplier first.",
                    "Batch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var validationErrors = ValidateRows().ToList();
            if (validationErrors.Count > 0)
            {
                MessageBox.Show(
                    string.Join(Environment.NewLine, validationErrors),
                    "Batch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var dto = new CreateBatchDto
            {
                SupplierId = supplierId,
                PurchaseDate = purchaseDatePicker.Value.Date,
                BatchItems = _itemsBinding.Select(item => new CreateBatchItemDto
                {
                    ProductId = item.ProductId,
                    QuantityReceived = item.QuantityReceived,
                    QuantityRemaining = item.QuantityRemaining,
                    ExpirationDate = item.ExpirationDate.Date,
                    CostPrice = item.CostPrice,
                    MandatorySellingPrice = item.MandatorySellingPrice
                }).ToList()
            };

            try
            {
                ToggleBusyState(true);

                if (_batchId is null)
                {
                    await _batchService.AddBatchAsync(dto);
                }
                else
                {
                    await _batchService.UpdateBatchAsync(_batchId.Value, dto);
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Batch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private IEnumerable<string> ValidateRows()
        {
            if (_itemsBinding.Count == 0)
            {
                yield return "Add at least one batch item.";
                yield break;
            }

            for (var i = 0; i < _itemsBinding.Count; i++)
            {
                var item = _itemsBinding[i];
                var rowNumber = i + 1;

                if (item.ProductId <= 0)
                {
                    yield return $"Row {rowNumber}: attach a valid product.";
                }

                if (item.QuantityReceived <= 0)
                {
                    yield return $"Row {rowNumber}: quantity received must be greater than zero.";
                }

                if (item.QuantityRemaining < 0)
                {
                    yield return $"Row {rowNumber}: remaining quantity cannot be negative.";
                }

                if (item.CostPrice < 0)
                {
                    yield return $"Row {rowNumber}: cost price cannot be negative.";
                }

                if (item.MandatorySellingPrice < 0)
                {
                    yield return $"Row {rowNumber}: sell price cannot be negative.";
                }
            }
        }

        private async Task OpenAddSupplierAsync()
        {
            using var dialog = ActivatorUtilities.CreateInstance<SupplierDialogForm>(_serviceProvider);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadSuppliersAsync();
            }
        }

        private async Task OpenAddProductAsync()
        {
            using var dialog = ActivatorUtilities.CreateInstance<ProductEditorForm>(_serviceProvider, barcodeTextBox.Text.Trim());
            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.CreatedProduct is not null)
            {
                barcodeTextBox.Text = dialog.CreatedProduct.Barcode;
                scanStatusLabel.Text = $"Created product '{dialog.CreatedProduct.Name}'. Scan its barcode to add it as a batch item.";
                scanStatusLabel.ForeColor = SystemColors.ControlText;
            }
        }

        private void AddNewItem()
        {
            var item = CreateNewItemRow();
            _itemsBinding.Add(item);
            SelectRow(item);
        }

        private void RemoveSelectedItem()
        {
            if (itemsGrid.CurrentRow?.DataBoundItem is BatchItemEditorRow row)
            {
                _itemsBinding.Remove(row);
            }

            if (_itemsBinding.Count == 0)
            {
                AddNewItem();
            }
        }

        private void ClearForm()
        {
            if (_batchId is null)
            {
                if (supplierComboBox.Items.Count > 0)
                {
                    supplierComboBox.SelectedIndex = 0;
                }

                purchaseDatePicker.Value = DateTime.Today;
                barcodeTextBox.Clear();
                scanStatusLabel.Text = "Select a row and scan a product barcode to attach it quickly.";
                scanStatusLabel.ForeColor = SystemColors.ControlText;
                _itemsBinding.Clear();
                AddNewItem();
                return;
            }

            _ = LoadDataAsync();
        }

        private void OnBarcodeScanned(object? sender, string barcode)
        {
            if (IsDisposed || string.IsNullOrWhiteSpace(barcode))
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => _ = AssignProductByBarcodeAsync(barcode, true)));
                return;
            }

            _ = AssignProductByBarcodeAsync(barcode, true);
        }

        private async Task AssignProductByBarcodeAsync(string barcode, bool focusGridAfterLookup)
        {
            if (string.IsNullOrWhiteSpace(barcode) || _isBusy)
            {
                return;
            }

            try
            {
                ToggleBusyState(true);
                barcodeTextBox.Text = barcode;

                var product = await _productService.GetProductByBarcodeAsync(barcode);
                if (product is null)
                {
                    scanStatusLabel.Text = $"[!] No product matched barcode '{barcode}'. Use Add Product to create it.";
                    scanStatusLabel.ForeColor = Color.Firebrick;
                    return;
                }

                var targetRow = GetOrCreateRowForScannedProduct(product);
                IncrementScannedQuantity(targetRow);
                SelectRow(targetRow);
                scanStatusLabel.Text = $"Received '{product.Name}'. Quantity is now {targetRow.QuantityReceived}.";
                scanStatusLabel.ForeColor = SystemColors.ControlText;

                if (focusGridAfterLookup && itemsGrid.CurrentRow is not null)
                {
                    itemsGrid.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Product Lookup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private BatchItemEditorRow GetOrCreateRowForScannedProduct(ProductDto product)
        {
            var existingRow = _itemsBinding.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingRow is not null)
            {
                return existingRow;
            }

            if (itemsGrid.CurrentRow?.DataBoundItem is BatchItemEditorRow selectedRow && selectedRow.ProductId <= 0)
            {
                ApplyProductToRow(selectedRow, product);
                selectedRow.QuantityReceived = 0;
                selectedRow.QuantityRemaining = 0;
                return selectedRow;
            }

            var newRow = CreateNewItemRow();
            ApplyProductToRow(newRow, product);
            _itemsBinding.Add(newRow);
            return newRow;
        }

        private void ApplyProductToRow(BatchItemEditorRow row, ProductDto product)
        {
            row.ProductId = product.Id;
            row.Barcode = product.Barcode;
            row.ProductName = product.Name;
        }

        private void IncrementScannedQuantity(BatchItemEditorRow row)
        {
            var nextQuantity = row.QuantityReceived <= 0 ? 1 : row.QuantityReceived + 1;
            row.QuantityReceived = nextQuantity;
            row.QuantityRemaining = nextQuantity;
        }

        private BatchItemEditorRow CreateNewItemRow()
        {
            return new BatchItemEditorRow
            {
                ExpirationDate = DateTime.Today.AddYears(1)
            };
        }

        private void SelectRow(BatchItemEditorRow row)
        {
            var rowIndex = _itemsBinding.IndexOf(row);
            if (rowIndex < 0 || rowIndex >= itemsGrid.Rows.Count)
            {
                return;
            }

            itemsGrid.ClearSelection();
            itemsGrid.Rows[rowIndex].Selected = true;
            itemsGrid.CurrentCell = itemsGrid.Rows[rowIndex].Cells[qtyReceivedColumn.Index];
        }

        private void ToggleBusyState(bool isBusy)
        {
            _isBusy = isBusy;
            supplierComboBox.Enabled = !isBusy;
            addSupplierBtn.Enabled = !isBusy;
            purchaseDatePicker.Enabled = !isBusy;
            barcodeTextBox.Enabled = !isBusy;
            useBarcodeBtn.Enabled = !isBusy;
            addProductBtn.Enabled = !isBusy;
            itemsGrid.Enabled = !isBusy;
            addItemBtn.Enabled = !isBusy;
            removeItemBtn.Enabled = !isBusy;
            saveBtn.Enabled = !isBusy;
            clearBtn.Enabled = !isBusy;
            cancelBtn.Enabled = !isBusy;
        }

        private sealed class BatchItemEditorRow : INotifyPropertyChanged
        {
            private int _productId;
            private string _barcode = string.Empty;
            private string _productName = string.Empty;
            private int _quantityReceived;
            private int _quantityRemaining;
            private DateTime _expirationDate = DateTime.Today.AddYears(1);
            private decimal _costPrice;
            private decimal _mandatorySellingPrice;

            public int ProductId
            {
                get => _productId;
                set => SetField(ref _productId, value, nameof(ProductId));
            }

            public string Barcode
            {
                get => _barcode;
                set => SetField(ref _barcode, value, nameof(Barcode));
            }

            public string ProductName
            {
                get => _productName;
                set => SetField(ref _productName, value, nameof(ProductName));
            }

            public int QuantityReceived
            {
                get => _quantityReceived;
                set
                {
                    if (SetField(ref _quantityReceived, value, nameof(QuantityReceived)) && _quantityRemaining < value)
                    {
                        QuantityRemaining = value;
                    }
                }
            }

            public int QuantityRemaining
            {
                get => _quantityRemaining;
                set => SetField(ref _quantityRemaining, value, nameof(QuantityRemaining));
            }

            public DateTime ExpirationDate
            {
                get => _expirationDate;
                set => SetField(ref _expirationDate, value, nameof(ExpirationDate));
            }

            public decimal CostPrice
            {
                get => _costPrice;
                set => SetField(ref _costPrice, value, nameof(CostPrice));
            }

            public decimal MandatorySellingPrice
            {
                get => _mandatorySellingPrice;
                set => SetField(ref _mandatorySellingPrice, value, nameof(MandatorySellingPrice));
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            private bool SetField<T>(ref T field, T value, string propertyName)
            {
                if (EqualityComparer<T>.Default.Equals(field, value))
                {
                    return false;
                }

                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
        }
    }
}

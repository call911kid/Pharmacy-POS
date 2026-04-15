using System.ComponentModel;
using BLL.DTOs.Batch;
using BLL.DTOs.BatchItem;
using BLL.DTOs.Product;
using BLL.Interfaces;
using UI.Events;
using UI.Theme;

namespace UI.Forms
{
    public sealed class InventoryBatchEditorForm : Form
    {
        private readonly IBatchService _batchService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly ScannerEventBus _eventBus;
        private readonly int? _batchId;

        private readonly ComboBox _supplierCombo;
        private readonly DateTimePicker _purchaseDatePicker;
        private readonly TextBox _txtBarcode;
        private readonly Label _lblScanStatus;
        private readonly DataGridView _itemsGrid;
        private readonly BindingList<BatchItemEditorRow> _itemsBinding = new();
        private readonly Button _btnSave;
        private readonly Button _btnCancel;
        private readonly Button _btnAddSupplier;
        private readonly Button _btnAddProduct;
        private readonly Button _btnFindProduct;
        private readonly Button _btnAddItem;
        private readonly Button _btnRemoveItem;

        private bool _isBusy;

        public InventoryBatchEditorForm(
            IBatchService batchService,
            ISupplierService supplierService,
            IProductService productService,
            ScannerEventBus eventBus,
            int? batchId = null)
        {
            _batchService = batchService;
            _supplierService = supplierService;
            _productService = productService;
            _eventBus = eventBus;
            _batchId = batchId;

            Text = batchId is null ? "Add Batch" : "Edit Batch";
            Width = 1100;
            Height = 680;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            BackColor = UiPalette.AppBackground;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(16),
                BackColor = UiPalette.AppBackground
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var topPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 4,
                AutoSize = true,
                BackColor = UiPalette.Surface,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 0, 12)
            };
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));

            var lblSupplier = new Label
            {
                AutoSize = true,
                Text = "Supplier",
                Font = UiPalette.BodyFont(10F),
                Anchor = AnchorStyles.Left,
                ForeColor = UiPalette.TextPrimary
            };

            _supplierCombo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = UiPalette.BodyFont(10F)
            };

            _btnAddSupplier = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Add Supplier"
            };
            _btnAddSupplier.Click += async (_, _) => await OpenAddSupplierAsync();

            _purchaseDatePicker = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };

            var lblPurchaseDate = new Label
            {
                AutoSize = true,
                Text = "Purchase Date",
                Font = UiPalette.BodyFont(10F),
                Anchor = AnchorStyles.Left,
                ForeColor = UiPalette.TextPrimary
            };

            var barcodeLabel = new Label
            {
                AutoSize = true,
                Text = "Barcode",
                Font = UiPalette.BodyFont(10F),
                Anchor = AnchorStyles.Left,
                ForeColor = UiPalette.TextPrimary
            };

            _txtBarcode = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Scan or type barcode, then press Enter",
                Font = UiPalette.BodyFont(10F)
            };
            _txtBarcode.KeyDown += async (_, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await AssignProductByBarcodeAsync(_txtBarcode.Text.Trim(), true);
                }
            };

            _btnFindProduct = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Use Barcode"
            };
            _btnFindProduct.Click += async (_, _) => await AssignProductByBarcodeAsync(_txtBarcode.Text.Trim(), true);

            _btnAddProduct = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Add Product"
            };
            _btnAddProduct.Click += async (_, _) => await OpenAddProductAsync();

            _lblScanStatus = new Label
            {
                AutoSize = true,
                Text = "Select a row and scan a product barcode to attach it quickly.",
                Font = UiPalette.BodyFont(9.5F),
                ForeColor = UiPalette.TextMuted,
                Margin = new Padding(0, 8, 0, 0)
            };

            topPanel.Controls.Add(lblSupplier, 0, 0);
            topPanel.Controls.Add(_supplierCombo, 1, 0);
            topPanel.Controls.Add(_btnAddSupplier, 2, 0);
            topPanel.Controls.Add(lblPurchaseDate, 0, 1);
            topPanel.Controls.Add(_purchaseDatePicker, 1, 1);
            topPanel.Controls.Add(barcodeLabel, 0, 2);
            topPanel.Controls.Add(_txtBarcode, 1, 2);
            topPanel.Controls.Add(_btnFindProduct, 2, 2);
            topPanel.Controls.Add(_btnAddProduct, 3, 2);
            topPanel.Controls.Add(_lblScanStatus, 1, 3);
            topPanel.SetColumnSpan(_lblScanStatus, 3);

            _itemsGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = UiPalette.Surface,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product ID",
                DataPropertyName = nameof(BatchItemEditorRow.ProductId),
                Width = 90,
                ReadOnly = true
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Barcode",
                DataPropertyName = nameof(BatchItemEditorRow.Barcode),
                Width = 150,
                ReadOnly = true
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product",
                DataPropertyName = nameof(BatchItemEditorRow.ProductName),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qty",
                DataPropertyName = nameof(BatchItemEditorRow.QuantityReceived),
                Width = 85
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Remaining",
                DataPropertyName = nameof(BatchItemEditorRow.QuantityRemaining),
                Width = 95,
                ReadOnly = true
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Expiry",
                DataPropertyName = nameof(BatchItemEditorRow.ExpirationDate),
                Width = 120
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cost",
                DataPropertyName = nameof(BatchItemEditorRow.CostPrice),
                Width = 90
            });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sell Price",
                DataPropertyName = nameof(BatchItemEditorRow.MandatorySellingPrice),
                Width = 95
            });

            _itemsGrid.DataSource = _itemsBinding;

            var bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 12, 0, 0)
            };

            _btnAddItem = new Button { Text = "Add Item", Width = 120, Height = 36 };
            _btnAddItem.Click += (_, _) => AddNewItem();

            _btnRemoveItem = new Button { Text = "Remove Item", Width = 120, Height = 36 };
            _btnRemoveItem.Click += (_, _) => RemoveSelectedItem();

            _btnSave = new Button { Text = "Save", Width = 120, Height = 36 };
            _btnSave.Click += async (_, _) => await SaveAsync();

            _btnCancel = new Button { Text = "Cancel", Width = 120, Height = 36 };
            _btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

            bottomPanel.Controls.Add(_btnAddItem);
            bottomPanel.Controls.Add(_btnRemoveItem);
            bottomPanel.Controls.Add(_btnSave);
            bottomPanel.Controls.Add(_btnCancel);

            root.Controls.Add(topPanel, 0, 0);
            root.Controls.Add(_itemsGrid, 0, 1);
            root.Controls.Add(bottomPanel, 0, 2);
            Controls.Add(root);

            Load += async (_, _) => await LoadDataAsync();
            FormClosed += (_, _) => _eventBus.BarcodeScanned -= OnBarcodeScanned;
            _eventBus.BarcodeScanned += OnBarcodeScanned;
        }

        private async Task LoadDataAsync()
        {
            try
            {
                ToggleBusyState(true);

                var suppliers = (await _supplierService.GetAllSuppliersAsync(1, 500)).ToList();
                _supplierCombo.DataSource = suppliers;
                _supplierCombo.DisplayMember = nameof(BLL.DTOs.Supplier.SupplierDto.Name);
                _supplierCombo.ValueMember = nameof(BLL.DTOs.Supplier.SupplierDto.Id);

                if (_batchId is not null)
                {
                    var batch = await _batchService.GetBatchByIdAsync(_batchId.Value);
                    _supplierCombo.SelectedValue = batch.SupplierId;
                    _purchaseDatePicker.Value = batch.PurchaseDate;
                    _itemsBinding.Clear();

                    foreach (var item in batch.Items)
                    {
                        var product = await _productService.GetProductByIdAsync(item.ProductId);
                        _itemsBinding.Add(new BatchItemEditorRow
                        {
                            ProductId = item.ProductId,
                            Barcode = product?.Barcode ?? string.Empty,
                            ProductName = product?.Name ?? $"Product #{item.ProductId}",
                            QuantityReceived = item.QuantityReceived,
                            QuantityRemaining = item.QuantityRemaining,
                            ExpirationDate = item.ExpirationDate,
                            CostPrice = item.CostPrice,
                            MandatorySellingPrice = item.MandatorySellingPrice
                        });
                    }
                }

                if (_itemsBinding.Count == 0)
                {
                    AddNewItem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private async Task SaveAsync()
        {
            if (_supplierCombo.SelectedValue is not int supplierId)
            {
<<<<<<< HEAD
                MessageBox.Show("Please select a supplier.", "Save Batch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
=======
                MessageBox.Show("Please select a supplier.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var validationErrors = ValidateRows().ToList();
            if (validationErrors.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, validationErrors), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
>>>>>>> 22430c9dcbccc23e0159956594f0190daa650008
                return;
            }

            var create = new CreateBatchDto
            {
                SupplierId = supplierId,
<<<<<<< HEAD
                PurchaseDate = DateTime.Now,
                BatchItems = _itemsGrid.Rows.Cast<DataGridViewRow>().Where(r => r.DataBoundItem is CreateBatchItemDto).Select(r => r.DataBoundItem as CreateBatchItemDto).Where(i => i is not null).Select(i => i!).ToList()
=======
                PurchaseDate = _purchaseDatePicker.Value.Date,
                BatchItems = _itemsBinding.Select(item => new CreateBatchItemDto
                {
                    ProductId = item.ProductId,
                    QuantityReceived = item.QuantityReceived,
                    QuantityRemaining = item.QuantityRemaining,
                    ExpirationDate = item.ExpirationDate.Date,
                    CostPrice = item.CostPrice,
                    MandatorySellingPrice = item.MandatorySellingPrice
                }).ToList()
>>>>>>> 22430c9dcbccc23e0159956594f0190daa650008
            };

            try
            {
                ToggleBusyState(true);
                if (_batchId is null)
                {
                    await _batchService.AddBatchAsync(create);
                }
                else
                {
                    await _batchService.UpdateBatchAsync(_batchId.Value, create);
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                yield return "Please add at least one batch item.";
                yield break;
            }

            for (var i = 0; i < _itemsBinding.Count; i++)
            {
                var item = _itemsBinding[i];
                var rowNumber = i + 1;

                if (item.ProductId <= 0)
                {
                    yield return $"Row {rowNumber}: attach a valid product before saving.";
                }

                if (item.QuantityReceived <= 0)
                {
                    yield return $"Row {rowNumber}: quantity must be greater than zero.";
                }

                if (item.CostPrice < 0)
                {
                    yield return $"Row {rowNumber}: cost price cannot be negative.";
                }

                if (item.MandatorySellingPrice < 0)
                {
                    yield return $"Row {rowNumber}: selling price cannot be negative.";
                }
            }
        }

        private async Task OpenAddSupplierAsync()
        {
            var selectedSupplierId = _supplierCombo.SelectedValue is int supplierId
                ? supplierId
                : (int?)null;

            using var form = new SupplierEditorForm(_supplierService);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                var suppliers = (await _supplierService.GetAllSuppliersAsync(1, 500)).ToList();
                _supplierCombo.DataSource = suppliers;

                if (selectedSupplierId is int existingSupplierId)
                {
                    _supplierCombo.SelectedValue = existingSupplierId;
                }
                else if (suppliers.Count > 0)
                {
                    _supplierCombo.SelectedIndex = suppliers.Count - 1;
                }
            }
        }

        private void AddNewItem()
        {
            var item = CreateNewItemRow();
            item.QuantityReceived = 1;
            item.QuantityRemaining = 1;
            _itemsBinding.Add(item);
            SelectRow(item);
        }

        private void RemoveSelectedItem()
        {
            if (_itemsGrid.CurrentRow?.DataBoundItem is BatchItemEditorRow item)
            {
                _itemsBinding.Remove(item);
            }

            if (_itemsBinding.Count == 0)
            {
                AddNewItem();
            }
        }

        private void OnBarcodeScanned(object? sender, string barcode)
        {
            if (IsDisposed)
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
                _txtBarcode.Text = barcode;

                var product = await _productService.GetProductByBarcodeAsync(barcode);
                if (product is null)
                {
                    _lblScanStatus.Text = $"No product matched barcode '{barcode}'. Use Add Product to create it.";
                    _lblScanStatus.ForeColor = Color.FromArgb(185, 28, 28);
                    return;
                }

                var targetRow = GetOrCreateRowForScannedProduct(product);
                IncrementScannedQuantity(targetRow);
                SelectRow(targetRow);

                _lblScanStatus.Text = $"Received '{product.Name}'. Quantity is now {targetRow.QuantityReceived}.";
                _lblScanStatus.ForeColor = UiPalette.TextPrimary;

                if (focusGridAfterLookup && _itemsGrid.CurrentRow is not null)
                {
                    _itemsGrid.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Product Lookup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private async Task OpenAddProductAsync()
        {
            using var form = new ProductEditorForm(_productService, _eventBus, _txtBarcode.Text.Trim());
            if (form.ShowDialog(this) == DialogResult.OK && form.CreatedProduct is not null)
            {
                _txtBarcode.Text = form.CreatedProduct.Barcode;
                _lblScanStatus.Text = $"Created product '{form.CreatedProduct.Name}'. Scan its barcode to add it as a batch item.";
                _lblScanStatus.ForeColor = UiPalette.TextPrimary;
            }
        }

        private BatchItemEditorRow GetOrCreateRowForScannedProduct(ProductDto product)
        {
            var existingRow = _itemsBinding.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingRow is not null)
            {
                return existingRow;
            }

            if (_itemsGrid.CurrentRow?.DataBoundItem is BatchItemEditorRow selectedRow &&
                selectedRow.ProductId <= 0)
            {
                selectedRow.QuantityReceived = 0;
                selectedRow.QuantityRemaining = 0;
                ApplyProductToRow(selectedRow, product);
                return selectedRow;
            }

            var newRow = CreateNewItemRow();
            _itemsBinding.Add(newRow);
            ApplyProductToRow(newRow, product);
            return newRow;
        }

        private BatchItemEditorRow GetSelectedOrNewRow()
        {
            if (_itemsGrid.CurrentRow?.DataBoundItem is BatchItemEditorRow selectedRow)
            {
                return selectedRow;
            }

            var newRow = CreateNewItemRow();
            _itemsBinding.Add(newRow);
            SelectRow(newRow);
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
            row.QuantityReceived = row.QuantityReceived <= 0
                ? 1
                : row.QuantityReceived + 1;
            row.QuantityRemaining = row.QuantityRemaining <= 0
                ? 1
                : row.QuantityRemaining + 1;
        }

        private BatchItemEditorRow CreateNewItemRow()
        {
            return new BatchItemEditorRow
            {
                QuantityReceived = 0,
                QuantityRemaining = 0,
                ExpirationDate = DateTime.Today.AddYears(1),
                CostPrice = 0m,
                MandatorySellingPrice = 0m
            };
        }

        private void SelectRow(BatchItemEditorRow row)
        {
            var rowIndex = _itemsBinding.IndexOf(row);
            if (rowIndex < 0 || rowIndex >= _itemsGrid.Rows.Count)
            {
                return;
            }

            _itemsGrid.ClearSelection();
            _itemsGrid.Rows[rowIndex].Selected = true;
            _itemsGrid.CurrentCell = _itemsGrid.Rows[rowIndex].Cells[3];
        }

        private void ToggleBusyState(bool isBusy)
        {
            _isBusy = isBusy;
            UseWaitCursor = isBusy;
            _supplierCombo.Enabled = !isBusy;
            _purchaseDatePicker.Enabled = !isBusy;
            _txtBarcode.Enabled = !isBusy;
            _btnFindProduct.Enabled = !isBusy;
            _btnAddProduct.Enabled = !isBusy;
            _btnAddSupplier.Enabled = !isBusy;
            _btnAddItem.Enabled = !isBusy;
            _btnRemoveItem.Enabled = !isBusy;
            _btnSave.Enabled = !isBusy;
            _btnCancel.Enabled = !isBusy;
            _itemsGrid.Enabled = !isBusy;
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
                set => SetField(ref _quantityReceived, value, nameof(QuantityReceived));
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

            private void SetField<T>(ref T field, T value, string propertyName)
            {
                if (EqualityComparer<T>.Default.Equals(field, value))
                {
                    return;
                }

                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using System.ComponentModel;
using BLL.DTOs.Customer;
using BLL.DTOs.Invoice;
using BLL.DTOs.InvoiceItem;
using BLL.Interfaces;
using Common.Enums;
using UI.Events;

namespace UI.Views.POS
{
    public partial class PosPage : UserControl
    {
        private readonly IBatchService _batchService;
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;
        private readonly ScannerEventBus _scannerEventBus;
        private readonly BindingSource _cartBindingSource = new();
        private readonly BindingList<CartItemRow> _cartItems = new();
        private int? _resolvedCustomerId;
        private bool _isLoading;
        private bool _isResolvingCustomer;

        public PosPage(
            IBatchService batchService,
            IInvoiceService invoiceService,
            ICustomerService customerService,
            ScannerEventBus scannerEventBus)
        {
            _batchService = batchService;
            _invoiceService = invoiceService;
            _customerService = customerService;
            _scannerEventBus = scannerEventBus;

            InitializeComponent();
            ConfigureGrid();
            SubscribeToEvents();
            ResetSummary();
        }

        private void ConfigureGrid()
        {
            cartGrid.AutoGenerateColumns = false;
            cartGrid.DataSource = _cartBindingSource;
            _cartBindingSource.DataSource = _cartItems;

            productColumn.DataPropertyName = nameof(CartItemRow.ProductName);
            quantityColumn.DataPropertyName = nameof(CartItemRow.Quantity);
            unitPriceColumn.DataPropertyName = nameof(CartItemRow.UnitPrice);
            lineTotalColumn.DataPropertyName = nameof(CartItemRow.LineTotal);

            unitPriceColumn.DefaultCellStyle.Format = "0.00 'EGP'";
            unitPriceColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            lineTotalColumn.DefaultCellStyle.Format = "0.00 'EGP'";
            lineTotalColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            quantityColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void SubscribeToEvents()
        {
            Load += (_, _) => LoadPageState();
            searchBtn.Click += async (_, _) => await AddByBarcodeAsync(searchTextBox.Text.Trim());
            searchTextBox.KeyDown += async (_, e) =>
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }

                e.SuppressKeyPress = true;
                await AddByBarcodeAsync(searchTextBox.Text.Trim());
            };

            saveInvoiceBtn.Click += async (_, _) => await SaveInvoiceAsync();
            clearCartBtn.Click += (_, _) => ClearCart();
            removeSelectedBtn.Click += (_, _) => RemoveSelectedItem();
            customerPhoneTextBox.TextChanged += async (_, _) => await ResolveCustomerByPhoneAsync();
            _scannerEventBus.BarcodeScanned += OnBarcodeScanned;
            Disposed += (_, _) => _scannerEventBus.BarcodeScanned -= OnBarcodeScanned;
        }

        private void LoadPageState()
        {
            invoiceDateValueLbl.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            customerNameTextBox.Enabled = false;
            customerNameTextBox.Clear();
            _resolvedCustomerId = null;
        }

        private async Task ResolveCustomerByPhoneAsync()
        {
            if (_isResolvingCustomer)
            {
                return;
            }

            var phone = customerPhoneTextBox.Text.Trim();
            var digitsOnly = new string(phone.Where(char.IsDigit).ToArray());
            if (digitsOnly != phone)
            {
                var selectionStart = customerPhoneTextBox.SelectionStart;
                customerPhoneTextBox.Text = digitsOnly;
                customerPhoneTextBox.SelectionStart = Math.Max(
                    0,
                    Math.Min(selectionStart - (phone.Length - digitsOnly.Length), customerPhoneTextBox.TextLength));
                return;
            }

            if (digitsOnly.Length != 11)
            {
                _resolvedCustomerId = null;
                customerNameTextBox.Enabled = false;
                customerNameTextBox.Clear();
                return;
            }

            try
            {
                _isResolvingCustomer = true;
                var requestedPhone = digitsOnly;
                var customer = await _customerService.GetCustomerByPhoneAsync(requestedPhone);

                if (customerPhoneTextBox.Text.Trim() != requestedPhone)
                {
                    return;
                }

                if (customer is not null)
                {
                    _resolvedCustomerId = customer.Id;
                    customerNameTextBox.Text = customer.Name;
                    customerNameTextBox.Enabled = false;
                    return;
                }

                _resolvedCustomerId = null;
                customerNameTextBox.Clear();
                customerNameTextBox.Enabled = true;
                customerNameTextBox.Focus();
            }
            finally
            {
                _isResolvingCustomer = false;
            }
        }

        private async void OnBarcodeScanned(object? sender, string barcode)
        {
            if (IsDisposed || string.IsNullOrWhiteSpace(barcode))
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(new Action(async () => await AddByBarcodeAsync(barcode)));
                return;
            }

            await AddByBarcodeAsync(barcode);
        }

        private async Task AddByBarcodeAsync(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode) || _isLoading)
            {
                return;
            }

            try
            {
                ToggleBusyState(true);
                var batchItem = await _batchService.GetBatchItemByBarcodeAsync(barcode);

                if (batchItem is null)
                {
                    MessageBox.Show(
                        $"Product with barcode '{barcode}' not found or out of stock.",
                        "POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = _cartItems.FirstOrDefault(item => item.BatchItemId == batchItem.Id);
                if (existingItem is not null)
                {
                    existingItem.Quantity += 1;
                    var index = _cartItems.IndexOf(existingItem);
                    _cartItems.ResetItem(index);
                }
                else
                {
                    _cartItems.Add(new CartItemRow
                    {
                        BatchItemId = batchItem.Id,
                        ProductId = batchItem.ProductId,
                        ProductName = batchItem.ProductName,
                        Quantity = 1,
                        UnitPrice = batchItem.MandatorySellingPrice
                    });
                }

                searchTextBox.Clear();
                RecalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "POS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
                searchTextBox.Focus();
            }
        }

        private async Task SaveInvoiceAsync()
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show(
                    "Cart is empty.",
                    "POS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var phone = customerPhoneTextBox.Text.Trim();
            if (phone.Length != 11)
            {
                MessageBox.Show(
                    "Enter a valid 11-digit customer phone number before saving.",
                    "POS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var customerId = _resolvedCustomerId;
            if (customerId is null)
            {
                var name = customerNameTextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show(
                        "Enter the customer name for a new phone number before saving.",
                        "POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var createdCustomer = await _customerService.CreateCustomerAsync(new CreateCustomerByPhoneDto
                {
                    Phone = phone,
                    Name = name
                });

                if (createdCustomer is null)
                {
                    MessageBox.Show(
                        "Could not create customer for this invoice.",
                        "POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                _resolvedCustomerId = createdCustomer.Id;
                customerId = createdCustomer.Id;
                customerNameTextBox.Text = createdCustomer.Name;
                customerNameTextBox.Enabled = false;
            }

            var dto = new CreateInvoiceDto
            {
                CustomerId = customerId.Value,
                InvoiceDate = DateTime.Now,
                Status = InvoiceStatus.Finalized,
                InvoiceItems = _cartItems.Select(item => new CreateInvoiceItemDto
                {
                    ProductId = item.ProductId,
                    BatchItemId = item.BatchItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            try
            {
                ToggleBusyState(true);
                var saved = await _invoiceService.CreateSaleInvoiceAsync(dto);
                if (!saved)
                {
                    MessageBox.Show(
                        "Could not save invoice.",
                        "POS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(
                    "Invoice saved successfully.",
                    "POS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "POS",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
            }
        }

        private void ClearCart()
        {
            _cartItems.Clear();
            ResetSummary();
        }

        private void RemoveSelectedItem()
        {
            if (cartGrid.CurrentRow?.DataBoundItem is not CartItemRow selected)
            {
                return;
            }

            _cartItems.Remove(selected);
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            totalItemsValueLbl.Text = _cartItems.Sum(item => item.Quantity).ToString();
            totalAmountValueLbl.Text = $"{_cartItems.Sum(item => item.LineTotal):0.00} EGP";
        }

        private void ResetSummary()
        {
            totalItemsValueLbl.Text = "0";
            totalAmountValueLbl.Text = "0.00 EGP";
        }

        private void ToggleBusyState(bool isBusy)
        {
            _isLoading = isBusy;
            searchTextBox.Enabled = !isBusy;
            searchBtn.Enabled = !isBusy;
            customerPhoneTextBox.Enabled = !isBusy;
            customerNameTextBox.Enabled = !isBusy && _resolvedCustomerId is null && customerPhoneTextBox.TextLength == 11;
            saveInvoiceBtn.Enabled = !isBusy;
            clearCartBtn.Enabled = !isBusy;
            removeSelectedBtn.Enabled = !isBusy;
            cartGrid.Enabled = !isBusy;
        }

        private sealed class CartItemRow : INotifyPropertyChanged
        {
            private int _quantity;

            public int BatchItemId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal UnitPrice { get; set; }

            public int Quantity
            {
                get => _quantity;
                set
                {
                    if (_quantity == value)
                    {
                        return;
                    }

                    _quantity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LineTotal)));
                }
            }

            public decimal LineTotal => Quantity * UnitPrice;

            public event PropertyChangedEventHandler? PropertyChanged;
        }
    }
}

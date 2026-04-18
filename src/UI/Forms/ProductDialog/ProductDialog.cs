using BLL.DTOs.Product;
using BLL.Exceptions;
using BLL.Interfaces;
using UI.Events;

namespace UI.Forms.ProductDialog
{
    public partial class ProductDialog : Form
    {
        private readonly IProductService _productService;
        private readonly ScannerEventBus _eventBus;
        private readonly string _initialBarcode;

        public ProductDto? CreatedProduct { get; private set; }

        public ProductDialog(IProductService productService, ScannerEventBus eventBus, string? initialBarcode = null)
        {
            _productService = productService;
            _eventBus = eventBus;
            _initialBarcode = initialBarcode?.Trim() ?? string.Empty;

            InitializeComponent();
            ConfigureForm();
            SubscribeToEvents();
        }

        private void ConfigureForm()
        {
            productBarcodeTextBox.Text = _initialBarcode;
            scanStatusLabel.Text = "Type the product details manually or scan a barcode while this window is open.";
            scanStatusLabel.ForeColor = SystemColors.ControlText;
        }

        private void SubscribeToEvents()
        {
            saveBtn.Click += async (_, _) => await SaveAsync();
            clearBtn.Click += (_, _) => ClearForm();
            cancelBtn.Click += (_, _) => DialogResult = DialogResult.Cancel;
            Shown += (_, _) => productNameTextBox.Focus();
            FormClosed += (_, _) => _eventBus.BarcodeScanned -= OnBarcodeScanned;
            _eventBus.BarcodeScanned += OnBarcodeScanned;
        }

        private void OnBarcodeScanned(object? sender, string barcode)
        {
            if (IsDisposed || string.IsNullOrWhiteSpace(barcode) || !CanHandleScannerInput())
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => ApplyScannedBarcode(barcode)));
                return;
            }

            ApplyScannedBarcode(barcode);
        }

        private bool CanHandleScannerInput()
        {
            return Visible
                && TopLevel
                && ReferenceEquals(Form.ActiveForm, this);
        }

        private void ApplyScannedBarcode(string barcode)
        {
            productBarcodeTextBox.Text = barcode;
            productBarcodeTextBox.SelectionStart = productBarcodeTextBox.TextLength;
            scanStatusLabel.Text = "Barcode captured from scanner. Review the product name, then save.";
            scanStatusLabel.ForeColor = SystemColors.ControlText;
        }

        private void ClearForm()
        {
            productNameTextBox.Clear();
            productBarcodeTextBox.Text = _initialBarcode;
            scanStatusLabel.Text = "Type the product details manually or scan a barcode while this window is open.";
            scanStatusLabel.ForeColor = SystemColors.ControlText;
            productNameTextBox.Focus();
        }

        private async Task SaveAsync()
        {
            var name = productNameTextBox.Text.Trim();
            var barcode = productBarcodeTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Product name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                productNameTextBox.Focus();
                return;
            }

            try
            {
                ToggleBusyState(true);
                CreatedProduct = await _productService.AddProductAsync(new CreateProductDto
                {
                    Name = name,
                    Barcode = barcode
                });

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (DuplicateException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Duplicate Barcode",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                productBarcodeTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not save product.\n\n{ex.Message}",
                    "Product",
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
            productNameTextBox.Enabled = !isBusy;
            productBarcodeTextBox.Enabled = !isBusy;
            saveBtn.Enabled = !isBusy;
            clearBtn.Enabled = !isBusy;
            cancelBtn.Enabled = !isBusy;
        }
    }
}

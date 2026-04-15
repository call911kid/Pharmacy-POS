using BLL.DTOs.Product;
using BLL.Exceptions;
using BLL.Interfaces;
using UI.Events;
using UI.Theme;

namespace UI.Forms
{
    public sealed class ProductEditorForm : Form
    {
        private readonly IProductService _productService;
        private readonly ScannerEventBus _eventBus;

        private readonly TextBox _txtName;
        private readonly TextBox _txtBarcode;
        private readonly Label _lblHelp;
        private readonly Button _btnSave;
        private readonly Button _btnCancel;

        public ProductDto? CreatedProduct { get; private set; }

        public ProductEditorForm(IProductService productService, ScannerEventBus eventBus, string? initialBarcode = null)
        {
            _productService = productService;
            _eventBus = eventBus;

            const int contentInset = 18;
            const int contentWidth = 382;

            Text = "Add Product";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(500, 360);
            BackColor = UiPalette.AppBackground;

            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = UiPalette.AppBackground
            };

            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UiPalette.Surface,
                Padding = new Padding(24)
            };

            var footer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                BackColor = UiPalette.Surface
            };

            var lblTitle = new Label
            {
                AutoSize = true,
                Font = UiPalette.TitleFont(17F),
                ForeColor = UiPalette.TextPrimary,
                Text = "Add Product",
                Location = new Point(contentInset, 0)
            };

            var lblName = new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(9.5F),
                ForeColor = UiPalette.TextMuted,
                Text = "Product Name",
                Location = new Point(contentInset, 60)
            };

            _txtName = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = UiPalette.BodyFont(10F),
                Location = new Point(contentInset, 88),
                Size = new Size(contentWidth, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblBarcode = new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(9.5F),
                ForeColor = UiPalette.TextMuted,
                Text = "Barcode",
                Location = new Point(contentInset, 142)
            };

            _txtBarcode = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = UiPalette.BodyFont(10F),
                Location = new Point(contentInset, 170),
                Size = new Size(contentWidth, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = initialBarcode ?? string.Empty
            };

            _lblHelp = new Label
            {
                AutoSize = false,
                Font = UiPalette.BodyFont(9.25F),
                ForeColor = UiPalette.TextMuted,
                Text = "Type the product details manually or scan a barcode while this window is open.",
                Location = new Point(contentInset, 220),
                Size = new Size(contentWidth, 42)
            };

            _btnSave = new Button
            {
                Text = "Save",
                Size = new Size(128, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(154, 4),
                FlatStyle = FlatStyle.Flat,
                BackColor = UiPalette.Primary,
                ForeColor = Color.White,
                Font = UiPalette.BodyFont(10F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            _btnSave.FlatAppearance.BorderSize = 0;
            _btnSave.Click += async (_, _) => await SaveAsync();

            _btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(128, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(294, 4),
                FlatStyle = FlatStyle.Flat,
                BackColor = UiPalette.Surface,
                ForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F),
                UseVisualStyleBackColor = false
            };
            _btnCancel.FlatAppearance.BorderColor = UiPalette.Border;
            _btnCancel.FlatAppearance.BorderSize = 1;
            _btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

            footer.Controls.Add(_btnSave);
            footer.Controls.Add(_btnCancel);

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblName);
            card.Controls.Add(_txtName);
            card.Controls.Add(lblBarcode);
            card.Controls.Add(_txtBarcode);
            card.Controls.Add(_lblHelp);
            card.Controls.Add(footer);

            card.Resize += (_, _) =>
            {
                _txtName.Width = Math.Max(320, card.ClientSize.Width - (contentInset * 2));
                _txtBarcode.Width = Math.Max(320, card.ClientSize.Width - (contentInset * 2));
                _lblHelp.Width = Math.Max(320, card.ClientSize.Width - (contentInset * 2));
                _btnCancel.Left = footer.ClientSize.Width - contentInset - _btnCancel.Width;
                _btnSave.Left = _btnCancel.Left - 12 - _btnSave.Width;
            };

            outer.Controls.Add(card);
            Controls.Add(outer);

            AcceptButton = _btnSave;
            CancelButton = _btnCancel;
            Shown += (_, _) => _txtName.Focus();
            FormClosed += (_, _) => _eventBus.BarcodeScanned -= OnBarcodeScanned;
            _eventBus.BarcodeScanned += OnBarcodeScanned;
        }

        private void OnBarcodeScanned(object? sender, string barcode)
        {
            if (IsDisposed || string.IsNullOrWhiteSpace(barcode))
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

        private void ApplyScannedBarcode(string barcode)
        {
            _txtBarcode.Text = barcode;
            _txtBarcode.SelectionStart = _txtBarcode.TextLength;
            _lblHelp.Text = "Barcode captured from scanner. Review the name, then save the product.";
            _lblHelp.ForeColor = UiPalette.TextPrimary;
        }

        private async Task SaveAsync()
        {
            var name = _txtName.Text.Trim();
            var barcode = _txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Product name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtName.Focus();
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
                MessageBox.Show(ex.Message, "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtBarcode.Focus();
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
            UseWaitCursor = isBusy;
            _btnSave.Enabled = !isBusy;
            _btnCancel.Enabled = !isBusy;
            _txtName.Enabled = !isBusy;
            _txtBarcode.Enabled = !isBusy;
        }
    }
}

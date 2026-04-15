using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.DTOs.BatchItem;
using BLL.Interfaces;
using Common.Enums;
using DAL.Models;
using UI.Events;
using UI.Theme;
using BLL.DTOs.Invoice;
using BLL.DTOs.InvoiceItem;

namespace UI.Views
{
    public sealed class InvoiceView : UserControl
    {
        private readonly IBatchService _batchService;
        private readonly IInvoiceService _invoiceService;
        private readonly ScannerEventBus _scannerEventBus;
        
        private readonly BindingSource _bindingSource = new();
        private readonly BindingList<InvoiceItem> _invoiceItems = new();
        private Invoice _currentInvoice;

        private readonly TextBox _txtSearch;
        private readonly Button _btnSearch;
        private readonly DataGridView _grid;

        
        private readonly Label _lblTotalAmount;
        private readonly Label _lblTotalDiscount;
        private readonly Label _lblNetAmount;
        private bool _isLoading;

        public InvoiceView(IBatchService batchService, IInvoiceService invoiceService, ScannerEventBus scannerEventBus)
        {
            _batchService = batchService;
            _invoiceService = invoiceService;
            _scannerEventBus = scannerEventBus;

            _currentInvoice = new Invoice
            {
                InvoiceDate = DateTime.Now,
                Status = InvoiceStatus.Finalized,
                InvoiceItems = new List<InvoiceItem>()
            };

            Dock = DockStyle.Fill;
            BackColor = UiPalette.AppBackground;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(0)
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F)); 

            
            var topGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "POS / Checkout",
                Font = UiPalette.BodyFont(10F, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary,
                Padding = new Padding(12, 16, 12, 12)
            };

            var topLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                BackColor = UiPalette.AppBackground,
                Padding = new Padding(8, 6, 8, 6),
                Height = 52,
                Margin = new Padding(0)
            };
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));

            _txtSearch = new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                PlaceholderText = "Scan Barcode or Enter Manually",
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = UiPalette.AppBackground,
                ForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F),
                Margin = new Padding(0, 0, 12, 0)
            };
            
            _txtSearch.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await HandleScanAsync(_txtSearch.Text.Trim());
                    _txtSearch.Clear();
                }
            };

            _btnSearch = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Search"
            };
            StylePrimaryButton(_btnSearch);
            _btnSearch.Click += async (_, _) => 
            {
                await HandleScanAsync(_txtSearch.Text.Trim());
                _txtSearch.Clear();
            };

            topLayout.Controls.Add(_txtSearch, 0, 0);
            topLayout.Controls.Add(_btnSearch, 1, 0);
            topGroup.Controls.Add(topLayout);


            
            var listGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "Cart Items",
                Font = UiPalette.BodyFont(10F, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary,
                Padding = new Padding(12, 10, 12, 12)
            };

            _grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                MultiSelect = false,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                BackgroundColor = UiPalette.Surface,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = UiPalette.Border,
                ColumnHeadersHeight = 40,
                Font = UiPalette.BodyFont(10F)
            };
            _grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiPalette.PrimaryMuted,
                ForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F, FontStyle.Bold),
                SelectionBackColor = UiPalette.PrimaryMuted,
                SelectionForeColor = UiPalette.TextPrimary,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            _grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiPalette.Surface,
                ForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F),
                SelectionBackColor = UiPalette.PrimaryMuted,
                SelectionForeColor = UiPalette.TextPrimary,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 8, 0)
            };
            _grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = UiPalette.RowAlternate,
                ForeColor = UiPalette.TextPrimary,
                Font = UiPalette.BodyFont(10F),
                SelectionBackColor = UiPalette.PrimaryMuted,
                SelectionForeColor = UiPalette.TextPrimary,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 8, 0)
            };
            _grid.RowTemplate.Height = 36;
            
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Product Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceItem.Quantity),
                HeaderText = "Qty",
                Width = 80
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceItem.OriginalPrice),
                HeaderText = "Original Price (EGP)",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(InvoiceItem.DiscountedPrice),
                HeaderText = "Discount Price (EGP)",
                Width = 160,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalLinePrice",
                HeaderText = "Total (EGP)",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            _bindingSource.DataSource = _invoiceItems;
            _grid.DataSource = _bindingSource;

            
            _grid.CellFormatting += (s, e) =>
            {
                if (_grid.Rows[e.RowIndex].DataBoundItem is InvoiceItem item)
                {
                    if (_grid.Columns[e.ColumnIndex].DataPropertyName == "ProductName")
                    {
                        
                        e.Value = item.BatchItem?.Product?.Name ?? "";
                    }
                    if (_grid.Columns[e.ColumnIndex].DataPropertyName == "TotalLinePrice")
                    {
                        e.Value = item.Quantity * item.DiscountedPrice;
                    }
                }
            };

            listGroup.Controls.Add(_grid);

            
            var totalsGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "Totals",
                Font = UiPalette.BodyFont(10F, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary,
                Padding = new Padding(12, 10, 12, 12)
            };

            var totalsLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = UiPalette.AppBackground
            };
            totalsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            totalsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            totalsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            totalsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            totalsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            
            _lblTotalAmount = CreateTotalLabel("Amount: 0.00 EGP");
            _lblTotalDiscount = CreateTotalLabel("Discount: 0.00 EGP");
            _lblNetAmount = CreateTotalLabel("Net: 0.00 EGP");
            _lblNetAmount.Font = UiPalette.BodyFont(16F, FontStyle.Bold); 
            
            totalsLayout.Controls.Add(_lblTotalAmount, 0, 0);
            totalsLayout.Controls.Add(_lblTotalDiscount, 1, 0);
            totalsLayout.Controls.Add(_lblNetAmount, 2, 0);

            var btnCheckout = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Save"
            };
            StylePrimaryButton(btnCheckout);
            btnCheckout.Click += async (s, e) => {
                if (InvokeRequired) Invoke(new Action(async () => await ProcessCheckoutAsync()));
                else await ProcessCheckoutAsync();
            };
            totalsLayout.Controls.Add(btnCheckout, 3, 0);

            totalsGroup.Controls.Add(totalsLayout);

            root.Controls.Add(topGroup, 0, 0);
            root.Controls.Add(listGroup, 0, 1);
            root.Controls.Add(totalsGroup, 0, 2);
            Controls.Add(root);

            
            Load += OnLoad;
        }

        private void OnLoad(object? sender, EventArgs e)
        {
            if (_scannerEventBus != null)
            {
                _scannerEventBus.BarcodeScanned += ScannerEventBus_BarcodeScanned;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _scannerEventBus != null)
            {
                
                _scannerEventBus.BarcodeScanned -= ScannerEventBus_BarcodeScanned;
            }
            base.Dispose(disposing);
        }

        private void ScannerEventBus_BarcodeScanned(object? sender, string barcode)
        {
            
            if (InvokeRequired)
            {
                Invoke(new Action(async () => await HandleScanAsync(barcode)));
            }
            else
            {
                _ = HandleScanAsync(barcode);
            }
        }

        private async Task HandleScanAsync(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return;
            if (_isLoading) return;

            try
            {
                _isLoading = true;
                _txtSearch.Enabled = false;
                UseWaitCursor = true;

                
                var batchItemDto = await _batchService.GetBatchItemByBarcodeAsync(barcode);

                if (batchItemDto == null)
                {
                    MessageBox.Show(
                        $"Product with barcode '{barcode}' not found or out of stock.",
                        "Scanner",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                
                var existingItem = _invoiceItems.FirstOrDefault(i => i.BatchItemId == batchItemDto.Id);
                
                if (existingItem != null)
                {
                    existingItem.Quantity += 1;
                    
                    var index = _invoiceItems.IndexOf(existingItem);
                    _invoiceItems.ResetItem(index);
                }
                else
                {
                    var newItem = new InvoiceItem
                    {
                        BatchItemId = batchItemDto.Id,
                        Quantity = 1,
                        ReturnedQuantity = 0,
                        OriginalPrice = batchItemDto.MandatorySellingPrice,
                        DiscountedPrice = batchItemDto.MandatorySellingPrice, 
                        BatchItem = new BatchItem 
                        { 
                            Id = batchItemDto.Id,
                            Product = new Product { 
                                Id = batchItemDto.ProductId,
                                Name = batchItemDto.ProductName 
                            }
                        } 
                    };
                    _invoiceItems.Add(newItem);
                }

                RecalculateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
                _txtSearch.Enabled = true;
                UseWaitCursor = false;
                _txtSearch.Focus();
            }
        }

        private void RecalculateTotals()
        {
            _currentInvoice.TotalAmount = _invoiceItems.Sum(i => i.Quantity * i.OriginalPrice);
            _currentInvoice.TotalDiscount = _invoiceItems.Sum(i => i.Quantity * (i.OriginalPrice - i.DiscountedPrice));
            _currentInvoice.NetAmount = _currentInvoice.TotalAmount - _currentInvoice.TotalDiscount;
            
            _lblTotalAmount.Text = $"Amount: {_currentInvoice.TotalAmount:N2} EGP";
            _lblTotalDiscount.Text = $"Discount: {_currentInvoice.TotalDiscount:N2} EGP";
            _lblNetAmount.Text = $"Net: {_currentInvoice.NetAmount:N2} EGP";
        }

        private async Task ProcessCheckoutAsync()
        {
            if (_invoiceItems.Count == 0)
            {
                MessageBox.Show("Cart is empty.", "Save Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UseWaitCursor = true;
                _isLoading = true;

                var dto = new CreateInvoiceDto
                {
                    CustomerId = 1,
                    Status = InvoiceStatus.Finalized,
                    InvoiceDate = DateTime.Now,
                    InvoiceItems = _invoiceItems.Select(i => new CreateInvoiceItemDto
                    {
                        ProductId = i.BatchItem.Product.Id,
                        BatchItemId = i.BatchItemId,
                        Quantity = i.Quantity,
                        UnitPrice = i.DiscountedPrice
                    }).ToList()
                };

                var success = await _invoiceService.CreateSaleInvoiceAsync(dto);

                if (success)
                {
                    MessageBox.Show("Invoice has been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    _invoiceItems.Clear();
                    _currentInvoice = new Invoice
                    {
                        InvoiceDate = DateTime.Now,
                        Status = InvoiceStatus.Finalized,
                        InvoiceItems = new List<InvoiceItem>()
                    };
                    RecalculateTotals();
                }
                else
                {
                    MessageBox.Show("Failed to save invoice. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during save: {ex.Message}\n{ex.InnerException?.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                _isLoading = false;
            }
        }

        private Label CreateTotalLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UiPalette.BodyFont(12F, FontStyle.Bold),
                ForeColor = UiPalette.TextPrimary
            };
        }

        private static void StylePrimaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
            button.BackColor = UiPalette.Primary;
            button.ForeColor = Color.White;
            button.Font = UiPalette.BodyFont(10F, FontStyle.Bold);
            button.Margin = new Padding(0, 4, 0, 4);
            button.Cursor = Cursors.Hand;
        }
    }
}

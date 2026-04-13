using System.Drawing;
using System.Windows.Forms;
using BLL.DTOs.Batch;
using BLL.DTOs.BatchItem;
using BLL.Interfaces;
using UI.Theme;
using System.Linq;

namespace UI.Forms
{
    public sealed class InventoryBatchEditorForm : Form
    {
        private readonly IBatchService _batchService;
        private readonly ISupplierService _supplierService;
        private readonly int? _batchId;

        private ComboBox _supplierCombo;
        private DataGridView _itemsGrid;
        private Button _btnSave;
        private Button _btnCancel;

        public InventoryBatchEditorForm(IBatchService batchService, ISupplierService supplierService, int? batchId = null)
        {
            _batchService = batchService;
            _supplierService = supplierService;
            _batchId = batchId;

            Text = batchId is null ? "Add Batch" : "Edit Batch";
            Width = 800;
            Height = 600;

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var topPanel = new Panel { Dock = DockStyle.Top, Height = 120 };
            _supplierCombo = new ComboBox { Left = 12, Top = 12, Width = 400, DropDownStyle = ComboBoxStyle.DropDownList };
            topPanel.Controls.Add(_supplierCombo);

            _itemsGrid = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Product ID", DataPropertyName = nameof(CreateBatchItemDto.ProductId) });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Qty", DataPropertyName = nameof(CreateBatchItemDto.QuantityReceived) });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Expiry", DataPropertyName = nameof(CreateBatchItemDto.ExpirationDate) });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cost", DataPropertyName = nameof(CreateBatchItemDto.CostPrice) });
            _itemsGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Price", DataPropertyName = nameof(CreateBatchItemDto.MandatorySellingPrice) });

            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 60 };
            _btnSave = new Button { Text = "Save", Left = 12, Width = 120 };
            _btnSave.Click += async (_, _) => await SaveAsync();
            _btnCancel = new Button { Text = "Cancel", Left = 142, Width = 120 };
            _btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

            bottomPanel.Controls.Add(_btnSave);
            bottomPanel.Controls.Add(_btnCancel);

            root.Controls.Add(topPanel, 0, 0);
            root.Controls.Add(_itemsGrid, 0, 1);
            Controls.Add(root);

            Load += async (_, _) => await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            // minimal load: suppliers list for combo
            var suppliers = (await _supplierService.GetAllSuppliersAsync(1, 500)).ToList();
            _supplierCombo.DataSource = suppliers;
            _supplierCombo.DisplayMember = nameof(BLL.DTOs.Supplier.SupplierDto.Name);
            _supplierCombo.ValueMember = nameof(BLL.DTOs.Supplier.SupplierDto.Id);

            if (_batchId is not null)
            {
                var batch = await _batchService.GetBatchByIdAsync(_batchId.Value);
                _supplierCombo.SelectedValue = batch.SupplierId;
                _itemsGrid.DataSource = batch.Items.Select(i => new CreateBatchItemDto
                {
                    ProductId = i.ProductId,
                    QuantityReceived = i.QuantityReceived,
                    ExpirationDate = i.ExpirationDate,
                    CostPrice = i.CostPrice,
                    MandatorySellingPrice = i.MandatorySellingPrice
                }).ToList();
            }
            else
            {
                _itemsGrid.DataSource = new List<CreateBatchItemDto>();
            }
        }

        private async Task SaveAsync()
        {
            var create = new CreateBatchDto
            {
                SupplierId = (int)_supplierCombo.SelectedValue,
                PurchaseDate = DateTime.Now,
                BatchItems = _itemsGrid.Rows.Cast<DataGridViewRow>().Where(r => r.DataBoundItem is CreateBatchItemDto).Select(r => r.DataBoundItem as CreateBatchItemDto).Where(i => i is not null).Select(i => i!).ToList()
            };

            try
            {
                await _batchService.AddBatchAsync(create);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Batch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

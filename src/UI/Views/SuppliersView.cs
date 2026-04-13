using BLL.DTOs.Supplier;
using BLL.Interfaces;
using UI.Forms;

namespace UI.Views
{
    public sealed class SuppliersView : UserControl
    {
        private readonly ISupplierService _supplierService;
        private readonly BindingSource _bindingSource = new();

        private readonly TextBox _txtSearch;
        private readonly Button _btnRefresh;
        private readonly Button _btnAdd;
        private readonly Button _btnEdit;
        private readonly Button _btnDelete;
        private readonly DataGridView _grid;

        private List<SupplierDto> _allSuppliers = new();
        private bool _isLoading;

        public SuppliersView(ISupplierService supplierService)
        {
            _supplierService = supplierService;

            Dock = DockStyle.Fill;
            BackColor = SystemColors.Control;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(0)
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var topGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "Suppliers"
            };

            var topLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5
            };
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));

            _txtSearch = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Search by supplier name or phone"
            };
            _txtSearch.TextChanged += (_, _) => ApplyFilter();

            _btnRefresh = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Refresh"
            };
            _btnRefresh.Click += async (_, _) => await LoadSuppliersAsync();

            _btnAdd = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Add"
            };
            _btnAdd.Click += async (_, _) => await OpenSupplierFormAsync();

            _btnEdit = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Edit",
                Enabled = false
            };
            _btnEdit.Click += async (_, _) => await OpenSupplierFormAsync(GetSelectedSupplier());

            _btnDelete = new Button
            {
                Dock = DockStyle.Fill,
                Text = "Delete",
                Enabled = false
            };
            _btnDelete.Click += async (_, _) => await DeleteSelectedSupplierAsync();

            topLayout.Controls.Add(_txtSearch, 0, 0);
            topLayout.Controls.Add(_btnRefresh, 1, 0);
            topLayout.Controls.Add(_btnAdd, 2, 0);
            topLayout.Controls.Add(_btnEdit, 3, 0);
            topLayout.Controls.Add(_btnDelete, 4, 0);
            topGroup.Controls.Add(topLayout);

            var listGroup = new GroupBox
            {
                Dock = DockStyle.Fill,
                Text = "Supplier List"
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
                RowHeadersVisible = false
            };
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SupplierDto.Id),
                HeaderText = "ID",
                Width = 70
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SupplierDto.Name),
                HeaderText = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(SupplierDto.Phone),
                HeaderText = "Phone",
                Width = 180
            });
            _grid.DataSource = _bindingSource;
            _grid.SelectionChanged += (_, _) => UpdateSelectionState();
            _grid.CellDoubleClick += async (_, _) => await OpenSupplierFormAsync(GetSelectedSupplier());

            listGroup.Controls.Add(_grid);

            root.Controls.Add(topGroup, 0, 0);
            root.Controls.Add(listGroup, 0, 1);
            Controls.Add(root);

            Load += async (_, _) => await LoadSuppliersAsync();
        }

        private async Task LoadSuppliersAsync()
        {
            if (_isLoading)
            {
                return;
            }

            try
            {
                _isLoading = true;
                ToggleBusyState(true);
                _allSuppliers = (await _supplierService.GetAllSuppliersAsync(1, 500)).ToList();
                ApplyFilter();
                UpdateSelectionState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not load suppliers.\n\n{ex.Message}",
                    "Suppliers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                ToggleBusyState(false);
                _isLoading = false;
            }
        }

        private void ApplyFilter()
        {
            IEnumerable<SupplierDto> filtered = _allSuppliers;
            var searchTerm = _txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filtered = filtered.Where(s =>
                    (!string.IsNullOrWhiteSpace(s.Name) && s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(s.Phone) && s.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
            }

            _bindingSource.DataSource = filtered
                .OrderBy(s => s.Name)
                .ToList();
        }

        private SupplierDto? GetSelectedSupplier()
        {
            return _grid.CurrentRow?.DataBoundItem as SupplierDto;
        }

        private void UpdateSelectionState()
        {
            var hasSelection = GetSelectedSupplier() is not null;
            _btnEdit.Enabled = hasSelection && !_isLoading;
            _btnDelete.Enabled = hasSelection && !_isLoading;
        }

        private async Task OpenSupplierFormAsync(SupplierDto? supplier = null)
        {
            using var form = new SupplierEditorForm(_supplierService, supplier);
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
            UseWaitCursor = isBusy;
            _txtSearch.Enabled = !isBusy;
            _btnRefresh.Enabled = !isBusy;
            _btnAdd.Enabled = !isBusy;
            UpdateSelectionState();
        }
    }
}

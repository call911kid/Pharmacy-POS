using BLL.DTOs.Customer;
using BLL.DTOs.Invoice;
using BLL.Interfaces;
using UI.Forms.CustomerDialog;

namespace UI.Views.Customers
{
    public partial class CustomersPage : UserControl
    {
        private readonly ICustomerService _customerService;
        private readonly IInvoiceService _invoiceService;
        private readonly BindingSource _customersBindingSource = new();
        private readonly BindingSource _invoicesBindingSource = new();
        private IReadOnlyList<CustomerDto> _customers = Array.Empty<CustomerDto>();
        private bool _isLoading;

        public CustomersPage(ICustomerService customerService, IInvoiceService invoiceService)
        {
            _customerService = customerService;
            _invoiceService = invoiceService;
            InitializeComponent();
            ConfigureGrids();
            SubscribeToEvents();
        }

        private void ConfigureGrids()
        {
            customersGrid.AutoGenerateColumns = false;
            customersGrid.DataSource = _customersBindingSource;

            customerInvoicesGrid.AutoGenerateColumns = false;
            customerInvoicesGrid.DataSource = _invoicesBindingSource;

            Id.DataPropertyName = nameof(InvoiceDto.Id);
            InvoiceDate.DataPropertyName = nameof(InvoiceDto.InvoiceDate);
            TotalAmount.DataPropertyName = nameof(InvoiceDto.TotalAmount);
            Status.DataPropertyName = nameof(InvoiceDto.Status);

            TotalAmount.DefaultCellStyle.Format = "0.00 'EGP'";
            TotalAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SubscribeToEvents()
        {
            Load += async (_, _) => await LoadCustomersAsync();
            searchTextBox.TextChanged += (_, _) => ApplyFilter();
            customersGrid.SelectionChanged += async (_, _) => await LoadSelectedCustomerInvoicesAsync();
            addBtn.Click += async (_, _) => await OpenCustomerDialogAsync();
            editBtn.Click += async (_, _) => await OpenCustomerDialogAsync(GetSelectedCustomer());
            deleteBtn.Click += async (_, _) => await DeleteSelectedCustomerAsync();
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                _isLoading = true;
                ToggleBusyState(true);
                _customers = await _customerService.GetAllCustomersAsync();
                ApplyFilter();
                await LoadSelectedCustomerInvoicesAsync();
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
            IEnumerable<CustomerDto> filtered = _customers;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filtered = filtered.Where(customer =>
                    customer.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    customer.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            _customersBindingSource.DataSource = filtered.ToList();
        }

        private CustomerDto? GetSelectedCustomer()
        {
            return customersGrid.CurrentRow?.DataBoundItem as CustomerDto;
        }

        private async Task LoadSelectedCustomerInvoicesAsync()
        {
            var selectedCustomer = GetSelectedCustomer();

            if (selectedCustomer is null)
            {
                _invoicesBindingSource.DataSource = new List<InvoiceDto>();
                return;
            }

            try
            {
                var invoices = await _invoiceService.GetCustomerInvoicesAsync(selectedCustomer.Id);
                _invoicesBindingSource.DataSource = invoices.ToList();
            }
            catch
            {
                _invoicesBindingSource.DataSource = new List<InvoiceDto>();
            }
        }

        private async Task OpenCustomerDialogAsync(CustomerDto? customer = null)
        {
            using var dialog = new CustomerDialog(_customerService, customer);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                await LoadCustomersAsync();
            }
        }

        private async Task DeleteSelectedCustomerAsync()
        {
            var customer = GetSelectedCustomer();

            if (customer is null)
            {
                MessageBox.Show(
                    "Select a customer first.",
                    "Customers",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var confirmation = MessageBox.Show(
                $"Delete customer '{customer.Name}'?",
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
                var deleted = await _customerService.DeleteCustomerAsync(customer.Id);

                if (!deleted)
                {
                    MessageBox.Show(
                        "Could not delete customer. The customer may already have invoices.",
                        "Customers",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                await LoadCustomersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not delete customer.\n\n{ex.Message}",
                    "Customers",
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
            addBtn.Enabled = !isBusy;
            editBtn.Enabled = !isBusy;
            deleteBtn.Enabled = !isBusy;
            customersGrid.Enabled = !isBusy;
            customerInvoicesGrid.Enabled = !isBusy;
        }
    }
}

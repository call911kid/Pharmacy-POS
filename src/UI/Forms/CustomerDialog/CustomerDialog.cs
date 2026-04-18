using BLL.DTOs.Customer;
using BLL.Interfaces;

namespace UI.Forms.CustomerDialog
{
    public partial class CustomerDialog : Form
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerDto? _customer;

        public CustomerDialog(ICustomerService customerService, CustomerDto? customer = null)
        {
            _customerService = customerService;
            _customer = customer;
            InitializeComponent();
            ConfigureForm();
            SubscribeToEvents();
            PopulateFields();
        }

        private void ConfigureForm()
        {
            Text = _customer is null ? "Add Customer" : "Edit Customer";
            titleLbl.Text = _customer is null ? "Add Customer" : $"Edit Customer #{_customer.Id}";
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = saveBtn;
            CancelButton = cancelBtn;
        }

        private void SubscribeToEvents()
        {
            saveBtn.Click += async (_, _) => await SaveAsync();
            clearBtn.Click += (_, _) => ClearFields();
            cancelBtn.Click += (_, _) => Close();
        }

        private void PopulateFields()
        {
            if (_customer is null)
            {
                return;
            }

            nameTextBox.Text = _customer.Name;
            phoneTextBox.Text = _customer.Phone;
        }

        private void ClearFields()
        {
            nameTextBox.Clear();
            phoneTextBox.Clear();
            nameTextBox.Focus();
        }

        private async Task SaveAsync()
        {
            var name = nameTextBox.Text.Trim();
            var phone = phoneTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Customer name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show(
                    "Phone number is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                phoneTextBox.Focus();
                return;
            }

            try
            {
                ToggleBusyState(true);

                if (_customer is null)
                {
                    var createdCustomer = await _customerService.CreateCustomerAsync(new CreateCustomerByPhoneDto
                    {
                        Name = name,
                        Phone = phone
                    });

                    if (createdCustomer is null)
                    {
                        MessageBox.Show(
                            "Could not create customer.",
                            "Customer",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var updatedCustomer = await _customerService.UpdateContactInfoAsync(_customer.Id, name, phone);

                    if (updatedCustomer is null)
                    {
                        MessageBox.Show(
                            "Could not update customer.",
                            "Customer",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not save customer.\n\n{ex.Message}",
                    "Customer",
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
            nameTextBox.Enabled = !isBusy;
            phoneTextBox.Enabled = !isBusy;
            saveBtn.Enabled = !isBusy;
            clearBtn.Enabled = !isBusy;
            cancelBtn.Enabled = !isBusy;
        }
    }
}

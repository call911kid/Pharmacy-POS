using BLL.DTOs.Supplier;
using BLL.Exceptions;
using BLL.Interfaces;

namespace UI.Forms.SupplierDialog
{
    public partial class SupplierDialog : Form
    {
        private readonly ISupplierService _supplierService;
        private readonly SupplierDto? _supplier;

        public SupplierDialog(ISupplierService supplierService, SupplierDto? supplier = null)
        {
            _supplierService = supplierService;
            _supplier = supplier;
            InitializeComponent();
            ConfigureForm();
            SubscribeToEvents();
            PopulateFields();
        }

        private void ConfigureForm()
        {
            Text = _supplier is null ? "Add Supplier" : "Edit Supplier";
            titleLbl.Text = _supplier is null ? "Add Supplier" : $"Edit Supplier #{_supplier.Id}";
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = saveBtn;
            CancelButton = cancelBtn;
        }

        private void SubscribeToEvents()
        {
            saveBtn.Click += async (_, _) => await SaveAsync();
            clearBtn.Click += (_, _) => ClearFields();
            cancelBtn.Click += (_, _) => Close();
            nameTextBox.KeyPress += NameTextBox_KeyPress;
            phoneTextBox.KeyPress += PhoneTextBox_KeyPress;
            nameTextBox.TextChanged += (_, _) => SanitizeNameInput();
            phoneTextBox.TextChanged += (_, _) => SanitizePhoneInput();
        }

        private void PopulateFields()
        {
            if (_supplier is null)
            {
                return;
            }

            nameTextBox.Text = _supplier.Name;
            phoneTextBox.Text = _supplier.Phone;
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
                    "Supplier name is required.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                nameTextBox.Focus();
                return;
            }

            try
            {
                ToggleBusyState(true);

                if (_supplier is null)
                {
                    await _supplierService.AddSupplierAsync(new CreateSupplierDto
                    {
                        Name = name,
                        Phone = phone
                    });
                }
                else
                {
                    await _supplierService.UpdateSupplierAsync(new UpdateSupplierDto
                    {
                        Id = _supplier.Id,
                        Name = name,
                        Phone = phone
                    });
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (DuplicateException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Duplicate Supplier",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                phoneTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not save supplier.\n\n{ex.Message}",
                    "Supplier",
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

        private static void NameTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static void PhoneTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SanitizeNameInput()
        {
            var sanitized = new string(nameTextBox.Text
                .Where(ch => char.IsLetter(ch) || char.IsWhiteSpace(ch))
                .ToArray());

            if (nameTextBox.Text == sanitized)
            {
                return;
            }

            var selectionStart = Math.Min(nameTextBox.SelectionStart, sanitized.Length);
            nameTextBox.Text = sanitized;
            nameTextBox.SelectionStart = selectionStart;
        }

        private void SanitizePhoneInput()
        {
            var sanitized = new string(phoneTextBox.Text
                .Where(char.IsDigit)
                .Take(11)
                .ToArray());

            if (phoneTextBox.Text == sanitized)
            {
                return;
            }

            var selectionStart = Math.Min(phoneTextBox.SelectionStart, sanitized.Length);
            phoneTextBox.Text = sanitized;
            phoneTextBox.SelectionStart = selectionStart;
        }
    }
}

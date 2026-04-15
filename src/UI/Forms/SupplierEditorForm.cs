using BLL.DTOs.Supplier;
using BLL.Exceptions;
using BLL.Interfaces;
using UI.Theme;

namespace UI.Forms
{
    public sealed class SupplierEditorForm : Form
    {
        private readonly ISupplierService _supplierService;
        private readonly SupplierDto? _supplier;

        private readonly TextBox _txtName;
        private readonly TextBox _txtPhone;
        private readonly Button _btnSave;
        private readonly Button _btnCancel;

        public SupplierEditorForm(ISupplierService supplierService, SupplierDto? supplier = null)
        {
            _supplierService = supplierService;
            _supplier = supplier;
            const int contentInset = 18;
            const int contentWidth = 382;

            Text = supplier is null ? "Add Supplier" : "Edit Supplier";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(500, 320);
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
                Text = supplier is null ? "Add Supplier" : $"Edit Supplier #{supplier.Id}",
                Location = new Point(contentInset, 0)
            };

            var lblName = new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(9.5F),
                ForeColor = UiPalette.TextMuted,
                Text = "Supplier Name",
                Location = new Point(contentInset, 60)
            };

            _txtName = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = UiPalette.BodyFont(10F),
                Location = new Point(contentInset, 88),
                Size = new Size(contentWidth, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = supplier?.Name ?? string.Empty
            };

            var lblPhone = new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(9.5F),
                ForeColor = UiPalette.TextMuted,
                Text = "Phone",
                Location = new Point(contentInset, 142)
            };

            _txtPhone = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Font = UiPalette.BodyFont(10F),
                Location = new Point(contentInset, 170),
                Size = new Size(contentWidth, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Text = supplier?.Phone ?? string.Empty
            };

            _btnSave = new Button
            {
                Text = supplier is null ? "Save" : "Update",
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
            _btnSave.FlatAppearance.MouseDownBackColor = UiPalette.Primary;
            _btnSave.FlatAppearance.MouseOverBackColor = UiPalette.Primary;
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
            _btnCancel.FlatAppearance.MouseDownBackColor = UiPalette.Surface;
            _btnCancel.FlatAppearance.MouseOverBackColor = UiPalette.Surface;
            _btnCancel.Click += (_, _) => DialogResult = DialogResult.Cancel;

            footer.Controls.Add(_btnSave);
            footer.Controls.Add(_btnCancel);

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblName);
            card.Controls.Add(_txtName);
            card.Controls.Add(lblPhone);
            card.Controls.Add(_txtPhone);
            card.Controls.Add(footer);

            card.Resize += (_, _) =>
            {
                _txtName.Width = Math.Max(320, card.ClientSize.Width - (contentInset * 2));
                _txtPhone.Width = Math.Max(320, card.ClientSize.Width - (contentInset * 2));
                _btnCancel.Left = footer.ClientSize.Width - contentInset - _btnCancel.Width;
                _btnSave.Left = _btnCancel.Left - 12 - _btnSave.Width;
            };

            outer.Controls.Add(card);
            Controls.Add(outer);

            AcceptButton = _btnSave;
            CancelButton = _btnCancel;
            Shown += (_, _) => _txtName.Focus();
        }

        private async Task SaveAsync()
        {
            var name = _txtName.Text.Trim();
            var phone = _txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Supplier name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtName.Focus();
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
                MessageBox.Show(ex.Message, "Duplicate Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPhone.Focus();
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
            UseWaitCursor = isBusy;
            _btnSave.Enabled = !isBusy;
            _btnCancel.Enabled = !isBusy;
            _txtName.Enabled = !isBusy;
            _txtPhone.Enabled = !isBusy;
        }
    }
}

using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using UI.Theme;
using UI.Utils;
using UI.Views.Customers;
using UI.Views.Dashboard;
using UI.Views.Inventory;
using UI.Views.POS;
using UI.Views.Suppliers;

namespace UI.Forms.Main
{
    public partial class Main : Form
    {
        private readonly Control _dashboardPage;
        private readonly Control _customersPage;
        private readonly Control _inventoryPage;
        private readonly Control _posPage;
        private readonly Control _suppliersPage;
        private readonly IServiceProvider _serviceProvider;
        private readonly Button[] _navigationButtons;
        private string _scannerUrl = string.Empty;

        public Main(
            DashboardPage dashboardPage,
            CustomersPage customersPage,
            InventoryPage inventoryPage,
            PosPage posPage,
            SuppliersPage suppliersPage,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _dashboardPage = dashboardPage;
            _customersPage = customersPage;
            _inventoryPage = inventoryPage;
            _posPage = posPage;
            _suppliersPage = suppliersPage;
            _serviceProvider = serviceProvider;
            _navigationButtons = new[] { dashboardBtn, posBtn, inventoryBtn, suppliersBtn, customersBtn };
            ApplyShellStyle();
            LoadScannerCard();
            SubscribeToNavigationEvents();
            OpenScreen(_dashboardPage, dashboardBtn);
        }

        private void SubscribeToNavigationEvents()
        {
            dashboardBtn.Click += (_, _) => OpenScreen(_dashboardPage, dashboardBtn);
            customersBtn.Click += (_, _) => OpenScreen(_customersPage, customersBtn);
            inventoryBtn.Click += (_, _) => OpenScreen(_inventoryPage, inventoryBtn);
            posBtn.Click += (_, _) => OpenScreen(_posPage, posBtn);
            suppliersBtn.Click += (_, _) => OpenScreen(_suppliersPage, suppliersBtn);
            scannerLinkLbl.LinkClicked += (_, _) => OpenScannerUrl();
            scannerQrPictureBox.Click += (_, _) => OpenScannerUrl();
        }

        private void ApplyShellStyle()
        {
            BackColor = UiPalette.AppBackground;
            tableLayoutPanel1.BackColor = UiPalette.AppBackground;
            sidebarPanel.BackColor = Color.FromArgb(241, 245, 249);
            contentPanel.BackColor = UiPalette.AppBackground;

            pharmacyLbl.Font = UiPalette.TitleFont(14f);
            pharmacyLbl.ForeColor = UiPalette.TextPrimary;
            pharmacyLbl.TextAlign = ContentAlignment.MiddleLeft;
            pharmacyLbl.Padding = new Padding(14, 0, 0, 0);

            foreach (var button in _navigationButtons)
            {
                button.Dock = DockStyle.Fill;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(214, 234, 248);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(226, 232, 240);
                button.BackColor = Color.Transparent;
                button.ForeColor = UiPalette.TextPrimary;
                button.Font = UiPalette.BodyFont(10f);
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Padding = Padding.Empty;
                button.Margin = new Padding(10, 6, 10, 6);
            }
            scannerPanel.BackColor = Color.FromArgb(241, 245, 249);
            scannerTitleLbl.Font = UiPalette.BodyFont(9.5f);
            scannerTitleLbl.ForeColor = UiPalette.TextMuted;
            scannerLinkLbl.Font = UiPalette.BodyFont(8.5f);
            scannerLinkLbl.LinkColor = UiPalette.Primary;
            scannerLinkLbl.ActiveLinkColor = Color.FromArgb(3, 105, 161);
            scannerLinkLbl.VisitedLinkColor = UiPalette.Primary;
        }

        private void OpenScreen(Control screen, Button activeButton)
        {
            contentPanel.SuspendLayout();
            contentPanel.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(screen);
            contentPanel.ResumeLayout();
            SetActiveButton(activeButton);
        }

        private void SetActiveButton(Button activeButton)
        {
            foreach (var button in _navigationButtons)
            {
                var isActive = button == activeButton;
                button.BackColor = isActive ? UiPalette.PrimaryMuted : Color.Transparent;
                button.ForeColor = isActive ? UiPalette.Primary : UiPalette.TextPrimary;
                button.Font = UiPalette.BodyFont(10f, isActive ? FontStyle.Bold : FontStyle.Regular);
            }
        }

        private void LoadScannerCard()
        {
            try
            {
                _scannerUrl = ScannerConnectionProvider.GetMobileScannerUrl();
                scannerLinkLbl.Text = _scannerUrl;
                scannerLinkLbl.Links.Clear();
                scannerLinkLbl.Links.Add(0, _scannerUrl.Length, _scannerUrl);
                scannerQrPictureBox.Image = ScannerConnectionProvider.GenerateLinkQRCode(_scannerUrl);
                sidebarLayout.RowStyles[7].Height = 170F;
            }
            catch
            {
                scannerTitleLbl.Text = "Scanner unavailable";
                scannerLinkLbl.Text = "Could not generate scanner link.";
                sidebarLayout.RowStyles[7].Height = 72F;
            }
        }

        private void OpenScannerUrl()
        {
            if (string.IsNullOrWhiteSpace(_scannerUrl))
            {
                return;
            }

            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = _scannerUrl,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Scanner",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}

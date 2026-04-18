using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using UI.Views.Customers;
using UI.Views.Inventory;
using UI.Views.Suppliers;

namespace UI.Forms.Main
{
    public partial class Main : Form
    {
        private readonly Control _customersPage;
        private readonly Control _inventoryPage;
        private readonly Control _suppliersPage;
        private readonly IServiceProvider _serviceProvider;

        public Main(
            CustomersPage customersPage,
            InventoryPage inventoryPage,
            SuppliersPage suppliersPage,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _customersPage = customersPage;
            _inventoryPage = inventoryPage;
            _suppliersPage = suppliersPage;
            _serviceProvider = serviceProvider;
            SubscribeToNavigationEvents();
            OpenScreen(_customersPage);
        }

        private void SubscribeToNavigationEvents()
        {
            customersBtn.Click += (_, _) => OpenScreen(_customersPage);
            inventoryBtn.Click += (_, _) => OpenScreen(_inventoryPage);
            suppliersBtn.Click += (_, _) => OpenScreen(_suppliersPage);
            scannerBtn.Click += (_, _) => OpenScannerConnection();
        }

        private void OpenScreen(Control screen)
        {
            contentPanel.SuspendLayout();
            contentPanel.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(screen);
            contentPanel.ResumeLayout();
        }

        private void OpenScannerConnection()
        {
            using var scannerForm = _serviceProvider.GetRequiredService<ScannerConnectionForm>();
            scannerForm.ShowDialog(this);
        }
    }
}

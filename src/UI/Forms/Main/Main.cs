using System.Windows.Forms;
using UI.Views.Customers;
using UI.Views.Suppliers;

namespace UI.Forms.Main
{
    public partial class Main : Form
    {
        private readonly Control _customersPage;
        private readonly Control _suppliersPage;

        public Main(CustomersPage customersPage, SuppliersPage suppliersPage)
        {
            InitializeComponent();
            _customersPage = customersPage;
            _suppliersPage = suppliersPage;
            SubscribeToNavigationEvents();
            OpenScreen(_customersPage);
        }

        private void SubscribeToNavigationEvents()
        {
            customersBtn.Click += (_, _) => OpenScreen(_customersPage);
            suppliersBtn.Click += (_, _) => OpenScreen(_suppliersPage);
        }

        private void OpenScreen(Control screen)
        {
            contentPanel.SuspendLayout();
            contentPanel.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(screen);
            contentPanel.ResumeLayout();
        }
    }
}

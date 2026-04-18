using System.Windows.Forms;
using UI.Interfaces;
using UI.Views.Customers;

namespace UI.Forms.Main
{
    public partial class Main : Form
    {
        private readonly Control _customersPage;

        public Main(ICustomersPage customersPage)
        {
            InitializeComponent();
            _customersPage = (Control)customersPage;
            SubscribeToNavigationEvents();
            OpenScreen(_customersPage);
        }

        private void SubscribeToNavigationEvents()
        {
            customersBtn.Click += (_, _) => OpenScreen(_customersPage);
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

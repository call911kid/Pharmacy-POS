using UI.Events;
using UI.Forms;
using UI.Controls;
using UI.Theme;
using UI.Views;
using BLL.Interfaces;

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly ScannerEventBus _eventBus;
        private readonly ISupplierService _supplierService;
        private readonly IBatchService _batchService;
        private readonly ICustomerService _customerService;
        private readonly Dictionary<string, RoundedButton> _navButtons = new();
        private readonly Dictionary<string, ShellModuleView> _views = new();
        private readonly Dictionary<string, UserControl> _customViews = new();
        private TextBox searchTextBox = null!;
        private Label lblSectionTitle = null!;
        private Label lblSectionSubtitle = null!;
        private Panel contentPanel = null!;
        private RoundedButton btnDashboard = null!;
        private RoundedButton btnPos = null!;
        private RoundedButton btnInventory = null!;
        private RoundedButton btnSuppliers = null!;
        private RoundedButton btnCustomers = null!;
        private RoundedButton btnAdjustments = null!;
        private RoundedButton btnScanner = null!;

        public MainForm(ScannerEventBus eventBus, ISupplierService supplierService, IBatchService batchService, ICustomerService customerService)
        {
            InitializeComponent();
            BuildShellLayout();

            _eventBus = eventBus;
            _supplierService = supplierService;
            _batchService = batchService;
            _customerService = customerService;
            _eventBus.BarcodeScanned += OnBarcodeScanned;

            ApplyTheme();
            BuildViewRegistry();
            SwitchView("dashboard");
        }

        private void ApplyTheme()
        {
            RightToLeft = RightToLeft.No;
            searchTextBox.Text = "Search barcode, patient, supplier, or batch";
            searchTextBox.ForeColor = UiPalette.TextMuted;
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.Font = UiPalette.BodyFont(10.5F);
            searchTextBox.BackColor = UiPalette.Surface;
        }

        private void BuildShellLayout()
        {
            SuspendLayout();
            Controls.Clear();

            BackColor = UiPalette.AppBackground;
            MinimumSize = new Size(1320, 760);
            ClientSize = new Size(1484, 861);

            var rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(20),
                BackColor = UiPalette.AppBackground
            };
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            var sidebarCard = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                CornerRadius = 12,
                BorderColor = UiPalette.Border,
                BackColor = UiPalette.Surface,
                Margin = new Padding(0, 0, 16, 0)
            };

            var sidebarLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 8,
                BackColor = Color.Transparent
            };
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            for (var index = 1; index <= 6; index++)
            {
                sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            }
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var navHeaderPanel = new Panel { Dock = DockStyle.Fill };
            navHeaderPanel.Controls.Add(new Label
            {
                AutoSize = true,
                Font = UiPalette.BodyFont(10F),
                ForeColor = UiPalette.TextMuted,
                Text = "Pharmacy POS workspace",
                Location = new Point(0, 56)
            });
            navHeaderPanel.Controls.Add(new Label
            {
                AutoSize = true,
                Font = UiPalette.TitleFont(18F),
                ForeColor = UiPalette.TextPrimary,
                Text = "PharmacyPOS",
                Location = new Point(0, 12)
            });
            sidebarLayout.Controls.Add(navHeaderPanel, 0, 0);

            btnDashboard = CreateNavButton("Dashboard");
            btnPos = CreateNavButton("POS / Register");
            btnInventory = CreateNavButton("Inventory Batches");
            btnSuppliers = CreateNavButton("Suppliers");
            btnCustomers = CreateNavButton("Customers");
            btnAdjustments = CreateNavButton("Stock Adjustments");

            sidebarLayout.Controls.Add(CreateNavRow(btnDashboard), 0, 1);
            sidebarLayout.Controls.Add(CreateNavRow(btnPos), 0, 2);
            sidebarLayout.Controls.Add(CreateNavRow(btnInventory), 0, 3);
            sidebarLayout.Controls.Add(CreateNavRow(btnSuppliers), 0, 4);
            sidebarLayout.Controls.Add(CreateNavRow(btnCustomers), 0, 5);
            sidebarLayout.Controls.Add(CreateNavRow(btnAdjustments), 0, 6);

            sidebarCard.Controls.Add(sidebarLayout);

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = UiPalette.AppBackground
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var searchPanel = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 14, 18, 14),
                CornerRadius = 12,
                BorderColor = UiPalette.Border,
                BackColor = UiPalette.Surface,
                Margin = new Padding(0, 0, 0, 16)
            };

            var searchIcon = new SectionIcon
            {
                Kind = SectionIconKind.Scanner,
                AccentColor = UiPalette.TextMuted,
                Location = new Point(18, 20)
            };
            searchTextBox = new TextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Location = new Point(48, 17),
                Width = 1000
            };
            searchPanel.Controls.Add(searchIcon);
            searchPanel.Controls.Add(searchTextBox);

            var headerPanel = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                CornerRadius = 12,
                BorderColor = UiPalette.Border,
                BackColor = UiPalette.Surface,
                Margin = new Padding(0, 0, 0, 16)
            };

            var headerLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 420F));

            var titleBlockPanel = new Panel { Dock = DockStyle.Fill };
            lblSectionTitle = new Label
            {
                AutoSize = true,
                Font = UiPalette.TitleFont(22F),
                ForeColor = UiPalette.TextPrimary,
                Text = "Dashboard",
                Location = new Point(0, 0)
            };
            lblSectionSubtitle = new Label
            {
                AutoSize = false,
                Font = UiPalette.BodyFont(10.2F),
                ForeColor = UiPalette.TextMuted,
                Text = "Section summary",
                Location = new Point(0, 42),
                MaximumSize = new Size(600, 0),
                Size = new Size(600, 40)
            };
            titleBlockPanel.Controls.Add(lblSectionTitle);
            titleBlockPanel.Controls.Add(lblSectionSubtitle);

            var actionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UiPalette.Surface
            };
            btnScanner = new RoundedButton
            {
                Anchor = AnchorStyles.None,
                Location = new Point(124, 16),
                Size = new Size(160, 42),
                Text = "Scanner QR",
                ButtonColor = UiPalette.Primary,
                BorderColor = UiPalette.Primary,
                TextColor = Color.White,
                CornerRadius = 10
            };
            btnScanner.Click += btnScanner_Click;

            actionPanel.Controls.Add(btnScanner);

            headerLayout.Controls.Add(titleBlockPanel, 0, 0);
            headerLayout.Controls.Add(actionPanel, 1, 0);
            headerPanel.Controls.Add(headerLayout);

            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UiPalette.AppBackground
            };

            mainLayout.Controls.Add(searchPanel, 0, 0);
            mainLayout.Controls.Add(headerPanel, 0, 1);
            mainLayout.Controls.Add(contentPanel, 0, 2);

            rootLayout.Controls.Add(sidebarCard, 0, 0);
            rootLayout.Controls.Add(mainLayout, 1, 0);

            Controls.Add(rootLayout);

            btnDashboard.Click += btnDashboard_Click;
            btnPos.Click += btnPos_Click;
            btnInventory.Click += btnInventory_Click;
            btnSuppliers.Click += btnSuppliers_Click;
            btnCustomers.Click += btnCustomers_Click;
            btnAdjustments.Click += btnAdjustments_Click;
            searchTextBox.Enter += searchTextBox_Enter;
            searchTextBox.Leave += searchTextBox_Leave;

            ResumeLayout();
        }

        private static RoundedButton CreateNavButton(string text)
        {
            return new RoundedButton
            {
                Font = UiPalette.BodyFont(10.5F, FontStyle.Bold),
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(18, 0, 0, 0),
                Dock = DockStyle.Fill,
                CornerRadius = 10,
                ButtonColor = UiPalette.Surface,
                BorderColor = UiPalette.Border,
                TextColor = UiPalette.TextMuted
            };
        }

        private static Panel CreateNavRow(RoundedButton button)
        {
            var row = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = UiPalette.Surface,
                Padding = new Padding(0, 4, 12, 4)
            };

            row.Controls.Add(button);
            return row;
        }

        private void BuildViewRegistry()
        {
            _navButtons["dashboard"] = btnDashboard;
            _navButtons["pos"] = btnPos;
            _navButtons["inventory"] = btnInventory;
            _navButtons["suppliers"] = btnSuppliers;
            _navButtons["customers"] = btnCustomers;
            _navButtons["adjustments"] = btnAdjustments;
            _customViews["suppliers"] = new SuppliersView(_supplierService);
            _customViews["inventory"] = new Views.InventoryBatchesView(_batchService, _supplierService);
            _customViews["customers"] = new Views.CustomersView(_customerService);

            _views["dashboard"] = new ShellModuleView(
                "Clinical operations overview",
                "A light, pharmacy-first workspace that keeps selling, replenishment, supplier follow-up, and stock safety visible in one frame.",
                "Operational priorities",
                new[]
                {
                    "Use the top search box for patient, barcode, supplier, or batch lookup.",
                    "Keep the register flow focused on the next sale and remove non-essential clicks.",
                    "Treat expiry and stock discrepancies as visible operational tasks, not hidden reports."
                },
                new ShellModuleLedger(
                    "Shift snapshot",
                    "Example operational data cards can later be replaced by live service calls.",
                    new[]
                    {
                        new ShellModuleColumn("metric", "Metric", 120F),
                        new ShellModuleColumn("status", "Status", 90F),
                        new ShellModuleColumn("value", "Value", 80F, true),
                        new ShellModuleColumn("owner", "Workflow", 110F)
                    },
                    new[]
                    {
                        new[] { "Active register", "Open", "1", "POS / Register" },
                        new[] { "Pending batches", "Review", "4", "Inventory Batches" },
                        new[] { "Supplier follow-up", "Today", "2", "Suppliers" },
                        new[] { "Stock variances", "Watch", "3", "Stock Adjustments" }
                    }));

            _views["pos"] = new ShellModuleView(
                "POS / Register",
                "The register should feel calm and fast: patient first, medicine second, totals always clear.",
                "Register design goals",
                new[]
                {
                    "Keep customer identification visible before invoice completion.",
                    "Reserve the largest area for the cart ledger and totals scan.",
                    "Use the scanner button as a side action, never the center of the workflow."
                },
                new ShellModuleLedger(
                    "Register preview",
                    "Styled as a clean pharmacy ledger with right-aligned quantity and price columns.",
                    new[]
                    {
                        new ShellModuleColumn("item", "Medicine", 150F),
                        new ShellModuleColumn("batch", "Lot", 85F),
                        new ShellModuleColumn("qty", "Qty", 55F, true),
                        new ShellModuleColumn("price", "Price", 75F, true),
                        new ShellModuleColumn("net", "Net", 75F, true)
                    },
                    new[]
                    {
                        new[] { "Augmentin 1g", "B-204", "2", "72.50", "145.00" },
                        new[] { "Panadol Extra", "B-311", "1", "38.00", "38.00" },
                        new[] { "Vitamin C", "B-188", "3", "24.00", "72.00" }
                    }));

            _views["inventory"] = new ShellModuleView(
                "Inventory Batches",
                "Batch intake is where legal selling price, quantity received, and expiry integrity are protected.",
                "Receiving design goals",
                new[]
                {
                    "Keep batch header details separate from the item grid.",
                    "Make expiry date and selling price impossible to miss during entry.",
                    "Show supplier context and current stock impact in the same workspace."
                },
                new ShellModuleLedger(
                    "Incoming batch preview",
                    "A receiving ledger can evolve from this structure without changing the visual language.",
                    new[]
                    {
                        new ShellModuleColumn("product", "Product", 145F),
                        new ShellModuleColumn("expiry", "Expiry", 85F),
                        new ShellModuleColumn("qty", "Qty Received", 70F, true),
                        new ShellModuleColumn("cost", "Cost", 65F, true),
                        new ShellModuleColumn("price", "Sell Price", 75F, true)
                    },
                    new[]
                    {
                        new[] { "Concor 5 mg", "2027-05-01", "40", "18.20", "24.00" },
                        new[] { "Flagyl Syrup", "2026-12-15", "24", "31.00", "41.50" },
                        new[] { "Voltaren Gel", "2027-08-20", "18", "49.00", "63.00" }
                    }));

            _views["suppliers"] = new ShellModuleView(
                "Suppliers",
                "Supplier work should feel like a dependable contact ledger, not a back-office afterthought.",
                "Supplier design goals",
                new[]
                {
                    "Support quick search by name or phone from the shared top search bar.",
                    "Keep contact details, recent deliveries, and outstanding follow-up in one place.",
                    "Preserve plenty of spacing so the form feels trustworthy rather than crowded."
                },
                new ShellModuleLedger(
                    "Supplier ledger preview",
                    "Contacts and delivery context should remain easy to skim in a bright clinical layout.",
                    new[]
                    {
                        new ShellModuleColumn("supplier", "Supplier", 130F),
                        new ShellModuleColumn("phone", "Phone", 95F),
                        new ShellModuleColumn("batches", "Batches", 65F, true),
                        new ShellModuleColumn("last", "Last Delivery", 85F)
                    },
                    new[]
                    {
                        new[] { "Eva Pharma", "01012345678", "6", "2026-04-10" },
                        new[] { "CID", "01199887766", "4", "2026-04-08" },
                        new[] { "Rameda", "01234567890", "3", "2026-04-07" }
                    }));

            _views["customers"] = new ShellModuleView(
                "Customers",
                "Patient lookup must stay fast because it unlocks both sales and returns.",
                "Customer design goals",
                new[]
                {
                    "Favor phone-first retrieval for repeat patients at the counter.",
                    "Keep create and edit forms compact with a clean validation tone.",
                    "Expose invoice history without taking the cashier out of context."
                },
                new ShellModuleLedger(
                    "Patient ledger preview",
                    "The same table styling can support lookup, selection, and return retrieval.",
                    new[]
                    {
                        new ShellModuleColumn("customer", "Customer", 140F),
                        new ShellModuleColumn("phone", "Phone", 95F),
                        new ShellModuleColumn("invoices", "Invoices", 60F, true),
                        new ShellModuleColumn("recent", "Recent Visit", 85F)
                    },
                    new[]
                    {
                        new[] { "Mona Adel", "01076543210", "8", "2026-04-13" },
                        new[] { "Ahmed Samir", "01188776655", "3", "2026-04-12" },
                        new[] { "Nour Hossam", "01233445566", "5", "2026-04-11" }
                    }));

            _views["adjustments"] = new ShellModuleView(
                "Stock Adjustments",
                "Variances, damaged items, and reconciliations deserve the same calm structure as selling and receiving.",
                "Adjustment design goals",
                new[]
                {
                    "Separate reasons clearly so quantity changes stay auditable.",
                    "Keep before and after quantities aligned for fast review.",
                    "Use this space later for expiries, breakage, and manual corrections."
                },
                new ShellModuleLedger(
                    "Adjustment ledger preview",
                    "Designed for quiet review of stock differences and corrective actions.",
                    new[]
                    {
                        new ShellModuleColumn("product", "Product", 135F),
                        new ShellModuleColumn("reason", "Reason", 110F),
                        new ShellModuleColumn("before", "Before", 60F, true),
                        new ShellModuleColumn("delta", "Delta", 60F, true),
                        new ShellModuleColumn("after", "After", 60F, true)
                    },
                    new[]
                    {
                        new[] { "Catafast Sachets", "Damaged", "22", "-2", "20" },
                        new[] { "Brufen Suspension", "Count Correction", "15", "+1", "16" },
                        new[] { "Xithrone 500", "Expired Removal", "9", "-3", "6" }
                    }));
        }

        private void OnBarcodeScanned(object? sender, string barcode)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateScannerStatus(barcode)));
                return;
            }

            UpdateScannerStatus(barcode);
        }

        private void UpdateScannerStatus(string barcode)
        {
        }

        private void OpenScannerConnection()
        {
            using var scannerForm = new ScannerConnectionForm();
            scannerForm.ShowDialog(this);
        }

        private void SwitchView(string key)
        {
            UserControl? view = null;

            if (_customViews.TryGetValue(key, out var customView))
            {
                view = customView;
            }
            else if (_views.TryGetValue(key, out var shellView))
            {
                view = shellView;
            }

            if (view is null)
            {
                return;
            }

            foreach (var navButton in _navButtons.Values)
            {
                navButton.ButtonColor = UiPalette.Surface;
                navButton.BorderColor = UiPalette.Border;
                navButton.TextColor = UiPalette.TextMuted;
                navButton.Invalidate();
            }

            if (_navButtons.TryGetValue(key, out var activeButton))
            {
                activeButton.ButtonColor = UiPalette.PrimaryMuted;
                activeButton.BorderColor = UiPalette.PrimaryMuted;
                activeButton.TextColor = UiPalette.Primary;
                activeButton.Invalidate();
            }

            contentPanel.SuspendLayout();
            contentPanel.Controls.Clear();
            view.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(view);
            contentPanel.ResumeLayout();

            if (view is ShellModuleView shellModuleView)
            {
                lblSectionTitle.Text = shellModuleView.Title;
                lblSectionSubtitle.Text = shellModuleView.Subtitle;
            }
            else if (key == "customers")
            {
                lblSectionTitle.Text = "Customers";
                lblSectionSubtitle.Text = "Search, create, and update customer records.";
            }
            else
            {
                lblSectionTitle.Text = "Suppliers";
                lblSectionSubtitle.Text = "Create, search, update, and delete supplier records.";
            }
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Search barcode, patient, supplier, or batch")
            {
                searchTextBox.Text = string.Empty;
                searchTextBox.ForeColor = UiPalette.TextPrimary;
            }
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search barcode, patient, supplier, or batch";
                searchTextBox.ForeColor = UiPalette.TextMuted;
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e) => SwitchView("dashboard");
        private void btnPos_Click(object sender, EventArgs e) => SwitchView("pos");
        private void btnInventory_Click(object sender, EventArgs e) => SwitchView("inventory");
        private void btnSuppliers_Click(object sender, EventArgs e) => SwitchView("suppliers");
        private void btnCustomers_Click(object sender, EventArgs e) => SwitchView("customers");
        private void btnAdjustments_Click(object sender, EventArgs e) => SwitchView("adjustments");
        private void btnScanner_Click(object sender, EventArgs e) => OpenScannerConnection();

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _eventBus.BarcodeScanned -= OnBarcodeScanned;
            base.OnFormClosed(e);
        }
    }
}

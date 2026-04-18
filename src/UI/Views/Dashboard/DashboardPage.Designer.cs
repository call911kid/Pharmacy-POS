namespace UI.Views.Dashboard
{
    partial class DashboardPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            summaryLayout = new TableLayoutPanel();
            customersSummary = new TableLayoutPanel();
            customersValueLbl = new Label();
            customersTitleLbl = new Label();
            suppliersSummary = new TableLayoutPanel();
            suppliersValueLbl = new Label();
            suppliersTitleLbl = new Label();
            productsSummary = new TableLayoutPanel();
            productsValueLbl = new Label();
            productsTitleLbl = new Label();
            batchesSummary = new TableLayoutPanel();
            batchesValueLbl = new Label();
            batchesTitleLbl = new Label();
            middleLayout = new TableLayoutPanel();
            recentInvoicesLayout = new TableLayoutPanel();
            recentInvoicesTitleLbl = new Label();
            recentInvoicesGrid = new DataGridView();
            invoiceIdColumn = new DataGridViewTextBoxColumn();
            invoiceCustomerIdColumn = new DataGridViewTextBoxColumn();
            invoiceDateColumn = new DataGridViewTextBoxColumn();
            invoiceTotalAmountColumn = new DataGridViewTextBoxColumn();
            invoiceStatusColumn = new DataGridViewTextBoxColumn();
            recentBatchesLayout = new TableLayoutPanel();
            recentBatchesTitleLbl = new Label();
            recentBatchesGrid = new DataGridView();
            batchIdColumn = new DataGridViewTextBoxColumn();
            supplierNameColumn = new DataGridViewTextBoxColumn();
            purchaseDateColumn = new DataGridViewTextBoxColumn();
            itemsCountColumn = new DataGridViewTextBoxColumn();
            totalQuantityRemainingColumn = new DataGridViewTextBoxColumn();
            alertsLayout = new TableLayoutPanel();
            lowStockLayout = new TableLayoutPanel();
            lowStockTitleLbl = new Label();
            lowStockGrid = new DataGridView();
            lowStockProductColumn = new DataGridViewTextBoxColumn();
            lowStockQtyColumn = new DataGridViewTextBoxColumn();
            lowStockExpiryColumn = new DataGridViewTextBoxColumn();
            expiringLayout = new TableLayoutPanel();
            expiringTitleLbl = new Label();
            expiringGrid = new DataGridView();
            expiringProductColumn = new DataGridViewTextBoxColumn();
            expiringQtyColumn = new DataGridViewTextBoxColumn();
            expiringDateColumn = new DataGridViewTextBoxColumn();
            rootLayout.SuspendLayout();
            summaryLayout.SuspendLayout();
            customersSummary.SuspendLayout();
            suppliersSummary.SuspendLayout();
            productsSummary.SuspendLayout();
            batchesSummary.SuspendLayout();
            middleLayout.SuspendLayout();
            recentInvoicesLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)recentInvoicesGrid).BeginInit();
            recentBatchesLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)recentBatchesGrid).BeginInit();
            alertsLayout.SuspendLayout();
            lowStockLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lowStockGrid).BeginInit();
            expiringLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)expiringGrid).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(summaryLayout, 0, 0);
            rootLayout.Controls.Add(middleLayout, 0, 1);
            rootLayout.Controls.Add(alertsLayout, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(12);
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            rootLayout.Size = new Size(980, 620);
            rootLayout.TabIndex = 0;
            // 
            // summaryLayout
            // 
            summaryLayout.ColumnCount = 4;
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.Controls.Add(customersSummary, 0, 0);
            summaryLayout.Controls.Add(suppliersSummary, 1, 0);
            summaryLayout.Controls.Add(productsSummary, 2, 0);
            summaryLayout.Controls.Add(batchesSummary, 3, 0);
            summaryLayout.Dock = DockStyle.Fill;
            summaryLayout.Location = new Point(15, 15);
            summaryLayout.Name = "summaryLayout";
            summaryLayout.RowCount = 1;
            summaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            summaryLayout.Size = new Size(950, 66);
            summaryLayout.TabIndex = 0;
            // 
            // customersSummary
            // 
            customersSummary.ColumnCount = 1;
            customersSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            customersSummary.Controls.Add(customersTitleLbl, 0, 0);
            customersSummary.Controls.Add(customersValueLbl, 0, 1);
            customersSummary.Dock = DockStyle.Fill;
            customersSummary.Location = new Point(6, 6);
            customersSummary.Margin = new Padding(6);
            customersSummary.Name = "customersSummary";
            customersSummary.RowCount = 2;
            customersSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            customersSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            customersSummary.Size = new Size(225, 54);
            customersSummary.TabIndex = 0;
            // 
            // customersValueLbl
            // 
            customersValueLbl.Dock = DockStyle.Fill;
            customersValueLbl.Location = new Point(3, 20);
            customersValueLbl.Name = "customersValueLbl";
            customersValueLbl.Size = new Size(219, 34);
            customersValueLbl.TabIndex = 1;
            customersValueLbl.Text = "0";
            customersValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customersTitleLbl
            // 
            customersTitleLbl.Dock = DockStyle.Fill;
            customersTitleLbl.Location = new Point(3, 0);
            customersTitleLbl.Name = "customersTitleLbl";
            customersTitleLbl.Size = new Size(219, 20);
            customersTitleLbl.TabIndex = 0;
            customersTitleLbl.Text = "Customers";
            // 
            // suppliersSummary
            // 
            suppliersSummary.ColumnCount = 1;
            suppliersSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            suppliersSummary.Controls.Add(suppliersTitleLbl, 0, 0);
            suppliersSummary.Controls.Add(suppliersValueLbl, 0, 1);
            suppliersSummary.Dock = DockStyle.Fill;
            suppliersSummary.Location = new Point(243, 6);
            suppliersSummary.Margin = new Padding(6);
            suppliersSummary.Name = "suppliersSummary";
            suppliersSummary.RowCount = 2;
            suppliersSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            suppliersSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            suppliersSummary.Size = new Size(225, 54);
            suppliersSummary.TabIndex = 1;
            // 
            // suppliersValueLbl
            // 
            suppliersValueLbl.Dock = DockStyle.Fill;
            suppliersValueLbl.Location = new Point(3, 20);
            suppliersValueLbl.Name = "suppliersValueLbl";
            suppliersValueLbl.Size = new Size(219, 34);
            suppliersValueLbl.TabIndex = 1;
            suppliersValueLbl.Text = "0";
            suppliersValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // suppliersTitleLbl
            // 
            suppliersTitleLbl.Dock = DockStyle.Fill;
            suppliersTitleLbl.Location = new Point(3, 0);
            suppliersTitleLbl.Name = "suppliersTitleLbl";
            suppliersTitleLbl.Size = new Size(219, 20);
            suppliersTitleLbl.TabIndex = 0;
            suppliersTitleLbl.Text = "Suppliers";
            // 
            // productsSummary
            // 
            productsSummary.ColumnCount = 1;
            productsSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            productsSummary.Controls.Add(productsTitleLbl, 0, 0);
            productsSummary.Controls.Add(productsValueLbl, 0, 1);
            productsSummary.Dock = DockStyle.Fill;
            productsSummary.Location = new Point(480, 6);
            productsSummary.Margin = new Padding(6);
            productsSummary.Name = "productsSummary";
            productsSummary.RowCount = 2;
            productsSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            productsSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            productsSummary.Size = new Size(225, 54);
            productsSummary.TabIndex = 2;
            // 
            // productsValueLbl
            // 
            productsValueLbl.Dock = DockStyle.Fill;
            productsValueLbl.Location = new Point(3, 20);
            productsValueLbl.Name = "productsValueLbl";
            productsValueLbl.Size = new Size(219, 34);
            productsValueLbl.TabIndex = 1;
            productsValueLbl.Text = "0";
            productsValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // productsTitleLbl
            // 
            productsTitleLbl.Dock = DockStyle.Fill;
            productsTitleLbl.Location = new Point(3, 0);
            productsTitleLbl.Name = "productsTitleLbl";
            productsTitleLbl.Size = new Size(219, 20);
            productsTitleLbl.TabIndex = 0;
            productsTitleLbl.Text = "Products";
            // 
            // batchesSummary
            // 
            batchesSummary.ColumnCount = 1;
            batchesSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            batchesSummary.Controls.Add(batchesTitleLbl, 0, 0);
            batchesSummary.Controls.Add(batchesValueLbl, 0, 1);
            batchesSummary.Dock = DockStyle.Fill;
            batchesSummary.Location = new Point(717, 6);
            batchesSummary.Margin = new Padding(6);
            batchesSummary.Name = "batchesSummary";
            batchesSummary.RowCount = 2;
            batchesSummary.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            batchesSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            batchesSummary.Size = new Size(227, 54);
            batchesSummary.TabIndex = 3;
            // 
            // batchesValueLbl
            // 
            batchesValueLbl.Dock = DockStyle.Fill;
            batchesValueLbl.Location = new Point(3, 20);
            batchesValueLbl.Name = "batchesValueLbl";
            batchesValueLbl.Size = new Size(221, 34);
            batchesValueLbl.TabIndex = 1;
            batchesValueLbl.Text = "0";
            batchesValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // batchesTitleLbl
            // 
            batchesTitleLbl.Dock = DockStyle.Fill;
            batchesTitleLbl.Location = new Point(3, 0);
            batchesTitleLbl.Name = "batchesTitleLbl";
            batchesTitleLbl.Size = new Size(221, 20);
            batchesTitleLbl.TabIndex = 0;
            batchesTitleLbl.Text = "Batches";
            // 
            // middleLayout
            // 
            middleLayout.ColumnCount = 2;
            middleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            middleLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            middleLayout.Controls.Add(recentInvoicesLayout, 0, 0);
            middleLayout.Controls.Add(recentBatchesLayout, 1, 0);
            middleLayout.Dock = DockStyle.Fill;
            middleLayout.Location = new Point(15, 87);
            middleLayout.Name = "middleLayout";
            middleLayout.RowCount = 1;
            middleLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            middleLayout.Size = new Size(950, 298);
            middleLayout.TabIndex = 1;
            // 
            // recentInvoicesLayout
            // 
            recentInvoicesLayout.ColumnCount = 1;
            recentInvoicesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            recentInvoicesLayout.Controls.Add(recentInvoicesTitleLbl, 0, 0);
            recentInvoicesLayout.Controls.Add(recentInvoicesGrid, 0, 1);
            recentInvoicesLayout.Dock = DockStyle.Fill;
            recentInvoicesLayout.Location = new Point(3, 3);
            recentInvoicesLayout.Name = "recentInvoicesLayout";
            recentInvoicesLayout.RowCount = 2;
            recentInvoicesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            recentInvoicesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            recentInvoicesLayout.Size = new Size(469, 332);
            recentInvoicesLayout.TabIndex = 0;
            // 
            // recentInvoicesTitleLbl
            // 
            recentInvoicesTitleLbl.Dock = DockStyle.Fill;
            recentInvoicesTitleLbl.Location = new Point(3, 0);
            recentInvoicesTitleLbl.Name = "recentInvoicesTitleLbl";
            recentInvoicesTitleLbl.Size = new Size(463, 34);
            recentInvoicesTitleLbl.TabIndex = 0;
            recentInvoicesTitleLbl.Text = "Recent Invoices";
            recentInvoicesTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // recentInvoicesGrid
            // 
            recentInvoicesGrid.AllowUserToAddRows = false;
            recentInvoicesGrid.AllowUserToDeleteRows = false;
            recentInvoicesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            recentInvoicesGrid.Columns.AddRange(new DataGridViewColumn[] { invoiceIdColumn, invoiceCustomerIdColumn, invoiceDateColumn, invoiceTotalAmountColumn, invoiceStatusColumn });
            recentInvoicesGrid.Dock = DockStyle.Fill;
            recentInvoicesGrid.Location = new Point(3, 37);
            recentInvoicesGrid.Name = "recentInvoicesGrid";
            recentInvoicesGrid.ReadOnly = true;
            recentInvoicesGrid.RowHeadersVisible = false;
            recentInvoicesGrid.RowHeadersWidth = 51;
            recentInvoicesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            recentInvoicesGrid.Size = new Size(463, 244);
            recentInvoicesGrid.TabIndex = 1;
            // 
            // invoiceIdColumn
            // 
            invoiceIdColumn.HeaderText = "Id";
            invoiceIdColumn.MinimumWidth = 6;
            invoiceIdColumn.Name = "invoiceIdColumn";
            invoiceIdColumn.ReadOnly = true;
            invoiceIdColumn.Width = 60;
            // 
            // invoiceCustomerIdColumn
            // 
            invoiceCustomerIdColumn.HeaderText = "Customer";
            invoiceCustomerIdColumn.MinimumWidth = 6;
            invoiceCustomerIdColumn.Name = "invoiceCustomerIdColumn";
            invoiceCustomerIdColumn.ReadOnly = true;
            invoiceCustomerIdColumn.Width = 80;
            // 
            // invoiceDateColumn
            // 
            invoiceDateColumn.HeaderText = "Date";
            invoiceDateColumn.MinimumWidth = 6;
            invoiceDateColumn.Name = "invoiceDateColumn";
            invoiceDateColumn.ReadOnly = true;
            invoiceDateColumn.Width = 125;
            // 
            // invoiceTotalAmountColumn
            // 
            invoiceTotalAmountColumn.HeaderText = "Total";
            invoiceTotalAmountColumn.MinimumWidth = 6;
            invoiceTotalAmountColumn.Name = "invoiceTotalAmountColumn";
            invoiceTotalAmountColumn.ReadOnly = true;
            invoiceTotalAmountColumn.Width = 90;
            // 
            // invoiceStatusColumn
            // 
            invoiceStatusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            invoiceStatusColumn.HeaderText = "Status";
            invoiceStatusColumn.MinimumWidth = 6;
            invoiceStatusColumn.Name = "invoiceStatusColumn";
            invoiceStatusColumn.ReadOnly = true;
            // 
            // recentBatchesLayout
            // 
            recentBatchesLayout.ColumnCount = 1;
            recentBatchesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            recentBatchesLayout.Controls.Add(recentBatchesTitleLbl, 0, 0);
            recentBatchesLayout.Controls.Add(recentBatchesGrid, 0, 1);
            recentBatchesLayout.Dock = DockStyle.Fill;
            recentBatchesLayout.Location = new Point(478, 3);
            recentBatchesLayout.Name = "recentBatchesLayout";
            recentBatchesLayout.RowCount = 2;
            recentBatchesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            recentBatchesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            recentBatchesLayout.Size = new Size(469, 332);
            recentBatchesLayout.TabIndex = 1;
            // 
            // recentBatchesTitleLbl
            // 
            recentBatchesTitleLbl.Dock = DockStyle.Fill;
            recentBatchesTitleLbl.Location = new Point(3, 0);
            recentBatchesTitleLbl.Name = "recentBatchesTitleLbl";
            recentBatchesTitleLbl.Size = new Size(463, 34);
            recentBatchesTitleLbl.TabIndex = 0;
            recentBatchesTitleLbl.Text = "Recent Batches";
            recentBatchesTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // recentBatchesGrid
            // 
            recentBatchesGrid.AllowUserToAddRows = false;
            recentBatchesGrid.AllowUserToDeleteRows = false;
            recentBatchesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            recentBatchesGrid.Columns.AddRange(new DataGridViewColumn[] { batchIdColumn, supplierNameColumn, purchaseDateColumn, itemsCountColumn, totalQuantityRemainingColumn });
            recentBatchesGrid.Dock = DockStyle.Fill;
            recentBatchesGrid.Location = new Point(3, 37);
            recentBatchesGrid.Name = "recentBatchesGrid";
            recentBatchesGrid.ReadOnly = true;
            recentBatchesGrid.RowHeadersVisible = false;
            recentBatchesGrid.RowHeadersWidth = 51;
            recentBatchesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            recentBatchesGrid.Size = new Size(463, 244);
            recentBatchesGrid.TabIndex = 1;
            // 
            // batchIdColumn
            // 
            batchIdColumn.HeaderText = "Id";
            batchIdColumn.MinimumWidth = 6;
            batchIdColumn.Name = "batchIdColumn";
            batchIdColumn.ReadOnly = true;
            batchIdColumn.Width = 60;
            // 
            // supplierNameColumn
            // 
            supplierNameColumn.HeaderText = "Supplier";
            supplierNameColumn.MinimumWidth = 6;
            supplierNameColumn.Name = "supplierNameColumn";
            supplierNameColumn.ReadOnly = true;
            supplierNameColumn.Width = 120;
            // 
            // purchaseDateColumn
            // 
            purchaseDateColumn.HeaderText = "Purchase Date";
            purchaseDateColumn.MinimumWidth = 6;
            purchaseDateColumn.Name = "purchaseDateColumn";
            purchaseDateColumn.ReadOnly = true;
            purchaseDateColumn.Width = 120;
            // 
            // itemsCountColumn
            // 
            itemsCountColumn.HeaderText = "Items";
            itemsCountColumn.MinimumWidth = 6;
            itemsCountColumn.Name = "itemsCountColumn";
            itemsCountColumn.ReadOnly = true;
            itemsCountColumn.Width = 70;
            // 
            // totalQuantityRemainingColumn
            // 
            totalQuantityRemainingColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            totalQuantityRemainingColumn.HeaderText = "Remaining";
            totalQuantityRemainingColumn.MinimumWidth = 6;
            totalQuantityRemainingColumn.Name = "totalQuantityRemainingColumn";
            totalQuantityRemainingColumn.ReadOnly = true;
            // 
            // alertsLayout
            // 
            alertsLayout.ColumnCount = 2;
            alertsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            alertsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            alertsLayout.Controls.Add(lowStockLayout, 0, 0);
            alertsLayout.Controls.Add(expiringLayout, 1, 0);
            alertsLayout.Dock = DockStyle.Fill;
            alertsLayout.Location = new Point(15, 431);
            alertsLayout.Name = "alertsLayout";
            alertsLayout.RowCount = 1;
            alertsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            alertsLayout.Size = new Size(950, 214);
            alertsLayout.TabIndex = 2;
            // 
            // lowStockLayout
            // 
            lowStockLayout.ColumnCount = 1;
            lowStockLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            lowStockLayout.Controls.Add(lowStockTitleLbl, 0, 0);
            lowStockLayout.Controls.Add(lowStockGrid, 0, 1);
            lowStockLayout.Dock = DockStyle.Fill;
            lowStockLayout.Location = new Point(3, 3);
            lowStockLayout.Name = "lowStockLayout";
            lowStockLayout.RowCount = 2;
            lowStockLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            lowStockLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lowStockLayout.Size = new Size(469, 208);
            lowStockLayout.TabIndex = 0;
            // 
            // lowStockTitleLbl
            // 
            lowStockTitleLbl.Dock = DockStyle.Fill;
            lowStockTitleLbl.Location = new Point(3, 0);
            lowStockTitleLbl.Name = "lowStockTitleLbl";
            lowStockTitleLbl.Size = new Size(463, 30);
            lowStockTitleLbl.TabIndex = 0;
            lowStockTitleLbl.Text = "Low Stock";
            lowStockTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lowStockGrid
            // 
            lowStockGrid.AllowUserToAddRows = false;
            lowStockGrid.AllowUserToDeleteRows = false;
            lowStockGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            lowStockGrid.Columns.AddRange(new DataGridViewColumn[] { lowStockProductColumn, lowStockQtyColumn, lowStockExpiryColumn });
            lowStockGrid.Dock = DockStyle.Fill;
            lowStockGrid.Location = new Point(3, 33);
            lowStockGrid.Name = "lowStockGrid";
            lowStockGrid.ReadOnly = true;
            lowStockGrid.RowHeadersVisible = false;
            lowStockGrid.RowHeadersWidth = 51;
            lowStockGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lowStockGrid.Size = new Size(463, 172);
            lowStockGrid.TabIndex = 1;
            // 
            // lowStockProductColumn
            // 
            lowStockProductColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            lowStockProductColumn.HeaderText = "Product";
            lowStockProductColumn.MinimumWidth = 6;
            lowStockProductColumn.Name = "lowStockProductColumn";
            lowStockProductColumn.ReadOnly = true;
            // 
            // lowStockQtyColumn
            // 
            lowStockQtyColumn.HeaderText = "Remaining";
            lowStockQtyColumn.MinimumWidth = 6;
            lowStockQtyColumn.Name = "lowStockQtyColumn";
            lowStockQtyColumn.ReadOnly = true;
            lowStockQtyColumn.Width = 90;
            // 
            // lowStockExpiryColumn
            // 
            lowStockExpiryColumn.HeaderText = "Expiry";
            lowStockExpiryColumn.MinimumWidth = 6;
            lowStockExpiryColumn.Name = "lowStockExpiryColumn";
            lowStockExpiryColumn.ReadOnly = true;
            lowStockExpiryColumn.Width = 120;
            // 
            // expiringLayout
            // 
            expiringLayout.ColumnCount = 1;
            expiringLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            expiringLayout.Controls.Add(expiringTitleLbl, 0, 0);
            expiringLayout.Controls.Add(expiringGrid, 0, 1);
            expiringLayout.Dock = DockStyle.Fill;
            expiringLayout.Location = new Point(478, 3);
            expiringLayout.Name = "expiringLayout";
            expiringLayout.RowCount = 2;
            expiringLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            expiringLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            expiringLayout.Size = new Size(469, 208);
            expiringLayout.TabIndex = 1;
            // 
            // expiringTitleLbl
            // 
            expiringTitleLbl.Dock = DockStyle.Fill;
            expiringTitleLbl.Location = new Point(3, 0);
            expiringTitleLbl.Name = "expiringTitleLbl";
            expiringTitleLbl.Size = new Size(463, 30);
            expiringTitleLbl.TabIndex = 0;
            expiringTitleLbl.Text = "Expiring Soon";
            expiringTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // expiringGrid
            // 
            expiringGrid.AllowUserToAddRows = false;
            expiringGrid.AllowUserToDeleteRows = false;
            expiringGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            expiringGrid.Columns.AddRange(new DataGridViewColumn[] { expiringProductColumn, expiringQtyColumn, expiringDateColumn });
            expiringGrid.Dock = DockStyle.Fill;
            expiringGrid.Location = new Point(3, 33);
            expiringGrid.Name = "expiringGrid";
            expiringGrid.ReadOnly = true;
            expiringGrid.RowHeadersVisible = false;
            expiringGrid.RowHeadersWidth = 51;
            expiringGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            expiringGrid.Size = new Size(463, 172);
            expiringGrid.TabIndex = 1;
            // 
            // expiringProductColumn
            // 
            expiringProductColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            expiringProductColumn.HeaderText = "Product";
            expiringProductColumn.MinimumWidth = 6;
            expiringProductColumn.Name = "expiringProductColumn";
            expiringProductColumn.ReadOnly = true;
            // 
            // expiringQtyColumn
            // 
            expiringQtyColumn.HeaderText = "Remaining";
            expiringQtyColumn.MinimumWidth = 6;
            expiringQtyColumn.Name = "expiringQtyColumn";
            expiringQtyColumn.ReadOnly = true;
            expiringQtyColumn.Width = 90;
            // 
            // expiringDateColumn
            // 
            expiringDateColumn.HeaderText = "Expiry";
            expiringDateColumn.MinimumWidth = 6;
            expiringDateColumn.Name = "expiringDateColumn";
            expiringDateColumn.ReadOnly = true;
            expiringDateColumn.Width = 120;
            // 
            // DashboardPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "DashboardPage";
            Size = new Size(980, 620);
            rootLayout.ResumeLayout(false);
            summaryLayout.ResumeLayout(false);
            customersSummary.ResumeLayout(false);
            suppliersSummary.ResumeLayout(false);
            productsSummary.ResumeLayout(false);
            batchesSummary.ResumeLayout(false);
            middleLayout.ResumeLayout(false);
            recentInvoicesLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)recentInvoicesGrid).EndInit();
            recentBatchesLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)recentBatchesGrid).EndInit();
            alertsLayout.ResumeLayout(false);
            lowStockLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lowStockGrid).EndInit();
            expiringLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)expiringGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private TableLayoutPanel summaryLayout;
        private TableLayoutPanel customersSummary;
        private Label customersValueLbl;
        private Label customersTitleLbl;
        private TableLayoutPanel suppliersSummary;
        private Label suppliersValueLbl;
        private Label suppliersTitleLbl;
        private TableLayoutPanel productsSummary;
        private Label productsValueLbl;
        private Label productsTitleLbl;
        private TableLayoutPanel batchesSummary;
        private Label batchesValueLbl;
        private Label batchesTitleLbl;
        private TableLayoutPanel middleLayout;
        private TableLayoutPanel recentInvoicesLayout;
        private Label recentInvoicesTitleLbl;
        private DataGridView recentInvoicesGrid;
        private TableLayoutPanel recentBatchesLayout;
        private Label recentBatchesTitleLbl;
        private DataGridView recentBatchesGrid;
        private TableLayoutPanel alertsLayout;
        private TableLayoutPanel lowStockLayout;
        private Label lowStockTitleLbl;
        private DataGridView lowStockGrid;
        private TableLayoutPanel expiringLayout;
        private Label expiringTitleLbl;
        private DataGridView expiringGrid;
        private DataGridViewTextBoxColumn invoiceIdColumn;
        private DataGridViewTextBoxColumn invoiceCustomerIdColumn;
        private DataGridViewTextBoxColumn invoiceDateColumn;
        private DataGridViewTextBoxColumn invoiceTotalAmountColumn;
        private DataGridViewTextBoxColumn invoiceStatusColumn;
        private DataGridViewTextBoxColumn batchIdColumn;
        private DataGridViewTextBoxColumn supplierNameColumn;
        private DataGridViewTextBoxColumn purchaseDateColumn;
        private DataGridViewTextBoxColumn itemsCountColumn;
        private DataGridViewTextBoxColumn totalQuantityRemainingColumn;
        private DataGridViewTextBoxColumn lowStockProductColumn;
        private DataGridViewTextBoxColumn lowStockQtyColumn;
        private DataGridViewTextBoxColumn lowStockExpiryColumn;
        private DataGridViewTextBoxColumn expiringProductColumn;
        private DataGridViewTextBoxColumn expiringQtyColumn;
        private DataGridViewTextBoxColumn expiringDateColumn;
    }
}

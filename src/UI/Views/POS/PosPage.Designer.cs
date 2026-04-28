namespace UI.Views.POS
{
    partial class PosPage
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
            mainLayout = new TableLayoutPanel();
            searchLayout = new TableLayoutPanel();
            searchTextBox = new TextBox();
            searchBtn = new Button();
            cartSectionLayout = new TableLayoutPanel();
            cartTitleLbl = new Label();
            cartGrid = new DataGridView();
            productColumn = new DataGridViewTextBoxColumn();
            quantityColumn = new DataGridViewTextBoxColumn();
            unitPriceColumn = new DataGridViewTextBoxColumn();
            lineTotalColumn = new DataGridViewTextBoxColumn();
            sidebarLayout = new TableLayoutPanel();
            customerPhoneTitleLbl = new Label();
            customerPhoneTextBox = new TextBox();
            customerNameTitleLbl = new Label();
            customerNameTextBox = new TextBox();
            invoiceDateTitleLbl = new Label();
            invoiceDateValueLbl = new Label();
            totalsLayout = new TableLayoutPanel();
            totalAmountTitleLbl = new Label();
            totalAmountValueLbl = new Label();
            totalItemsTitleLbl = new Label();
            totalItemsValueLbl = new Label();
            actionsLayout = new TableLayoutPanel();
            saveInvoiceBtn = new Button();
            clearCartBtn = new Button();
            removeSelectedBtn = new Button();
            rootLayout.SuspendLayout();
            mainLayout.SuspendLayout();
            searchLayout.SuspendLayout();
            cartSectionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cartGrid).BeginInit();
            sidebarLayout.SuspendLayout();
            totalsLayout.SuspendLayout();
            actionsLayout.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 2;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            rootLayout.Controls.Add(mainLayout, 0, 0);
            rootLayout.Controls.Add(sidebarLayout, 1, 0);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(12);
            rootLayout.RowCount = 1;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(980, 620);
            rootLayout.TabIndex = 0;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(searchLayout, 0, 0);
            mainLayout.Controls.Add(cartSectionLayout, 0, 1);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(15, 15);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(642, 590);
            mainLayout.TabIndex = 0;
            // 
            // searchLayout
            // 
            searchLayout.ColumnCount = 2;
            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            searchLayout.Controls.Add(searchTextBox, 0, 0);
            searchLayout.Controls.Add(searchBtn, 1, 0);
            searchLayout.Dock = DockStyle.Fill;
            searchLayout.Location = new Point(3, 3);
            searchLayout.Name = "searchLayout";
            searchLayout.RowCount = 1;
            searchLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchLayout.Size = new Size(636, 46);
            searchLayout.TabIndex = 0;
            // 
            // searchTextBox
            // 
            searchTextBox.Dock = DockStyle.Fill;
            searchTextBox.Location = new Point(6, 8);
            searchTextBox.Margin = new Padding(6, 8, 6, 8);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Scan barcode or enter manually";
            searchTextBox.Size = new Size(514, 27);
            searchTextBox.TabIndex = 0;
            // 
            // searchBtn
            // 
            searchBtn.Dock = DockStyle.Fill;
            searchBtn.Location = new Point(535, 6);
            searchBtn.Margin = new Padding(9, 6, 3, 6);
            searchBtn.Name = "searchBtn";
            searchBtn.Size = new Size(98, 34);
            searchBtn.TabIndex = 1;
            searchBtn.Text = "Add";
            searchBtn.UseVisualStyleBackColor = true;
            // 
            // cartSectionLayout
            // 
            cartSectionLayout.ColumnCount = 1;
            cartSectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            cartSectionLayout.Controls.Add(cartTitleLbl, 0, 0);
            cartSectionLayout.Controls.Add(cartGrid, 0, 1);
            cartSectionLayout.Dock = DockStyle.Fill;
            cartSectionLayout.Location = new Point(3, 55);
            cartSectionLayout.Name = "cartSectionLayout";
            cartSectionLayout.RowCount = 2;
            cartSectionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            cartSectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cartSectionLayout.Size = new Size(636, 532);
            cartSectionLayout.TabIndex = 1;
            // 
            // cartTitleLbl
            // 
            cartTitleLbl.Dock = DockStyle.Fill;
            cartTitleLbl.Location = new Point(3, 0);
            cartTitleLbl.Name = "cartTitleLbl";
            cartTitleLbl.Size = new Size(630, 32);
            cartTitleLbl.TabIndex = 0;
            cartTitleLbl.Text = "Current Invoice";
            cartTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cartGrid
            // 
            cartGrid.AllowUserToAddRows = false;
            cartGrid.AllowUserToDeleteRows = false;
            cartGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cartGrid.Columns.AddRange(new DataGridViewColumn[] { productColumn, quantityColumn, unitPriceColumn, lineTotalColumn });
            cartGrid.Dock = DockStyle.Fill;
            cartGrid.Location = new Point(3, 35);
            cartGrid.MultiSelect = false;
            cartGrid.Name = "cartGrid";
            cartGrid.ReadOnly = true;
            cartGrid.RowHeadersVisible = false;
            cartGrid.RowHeadersWidth = 51;
            cartGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cartGrid.Size = new Size(630, 494);
            cartGrid.TabIndex = 1;
            // 
            // productColumn
            // 
            productColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            productColumn.HeaderText = "Product";
            productColumn.MinimumWidth = 6;
            productColumn.Name = "productColumn";
            productColumn.ReadOnly = true;
            // 
            // quantityColumn
            // 
            quantityColumn.HeaderText = "Qty";
            quantityColumn.MinimumWidth = 6;
            quantityColumn.Name = "quantityColumn";
            quantityColumn.ReadOnly = true;
            quantityColumn.Width = 70;
            // 
            // unitPriceColumn
            // 
            unitPriceColumn.HeaderText = "Unit Price";
            unitPriceColumn.MinimumWidth = 6;
            unitPriceColumn.Name = "unitPriceColumn";
            unitPriceColumn.ReadOnly = true;
            unitPriceColumn.Width = 110;
            // 
            // lineTotalColumn
            // 
            lineTotalColumn.HeaderText = "Total";
            lineTotalColumn.MinimumWidth = 6;
            lineTotalColumn.Name = "lineTotalColumn";
            lineTotalColumn.ReadOnly = true;
            lineTotalColumn.Width = 120;
            // 
            // sidebarLayout
            // 
            sidebarLayout.ColumnCount = 1;
            sidebarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sidebarLayout.Controls.Add(customerPhoneTitleLbl, 0, 0);
            sidebarLayout.Controls.Add(customerPhoneTextBox, 0, 1);
            sidebarLayout.Controls.Add(customerNameTitleLbl, 0, 2);
            sidebarLayout.Controls.Add(customerNameTextBox, 0, 3);
            sidebarLayout.Controls.Add(invoiceDateTitleLbl, 0, 4);
            sidebarLayout.Controls.Add(invoiceDateValueLbl, 0, 5);
            sidebarLayout.Controls.Add(totalsLayout, 0, 6);
            sidebarLayout.Controls.Add(actionsLayout, 0, 8);
            sidebarLayout.Dock = DockStyle.Fill;
            sidebarLayout.Location = new Point(663, 15);
            sidebarLayout.Name = "sidebarLayout";
            sidebarLayout.RowCount = 9;
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            sidebarLayout.Size = new Size(302, 590);
            sidebarLayout.TabIndex = 1;
            // 
            // customerPhoneTitleLbl
            // 
            customerPhoneTitleLbl.Dock = DockStyle.Fill;
            customerPhoneTitleLbl.Location = new Point(3, 0);
            customerPhoneTitleLbl.Name = "customerPhoneTitleLbl";
            customerPhoneTitleLbl.Size = new Size(296, 28);
            customerPhoneTitleLbl.TabIndex = 0;
            customerPhoneTitleLbl.Text = "Customer Phone";
            customerPhoneTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customerPhoneTextBox
            // 
            customerPhoneTextBox.Dock = DockStyle.Fill;
            customerPhoneTextBox.Location = new Point(3, 31);
            customerPhoneTextBox.MaxLength = 11;
            customerPhoneTextBox.Name = "customerPhoneTextBox";
            customerPhoneTextBox.Size = new Size(296, 27);
            customerPhoneTextBox.TabIndex = 1;
            // 
            // customerNameTitleLbl
            // 
            customerNameTitleLbl.Dock = DockStyle.Fill;
            customerNameTitleLbl.Location = new Point(3, 70);
            customerNameTitleLbl.Name = "customerNameTitleLbl";
            customerNameTitleLbl.Size = new Size(296, 28);
            customerNameTitleLbl.TabIndex = 2;
            customerNameTitleLbl.Text = "Customer Name";
            customerNameTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // customerNameTextBox
            // 
            customerNameTextBox.Dock = DockStyle.Fill;
            customerNameTextBox.Enabled = false;
            customerNameTextBox.Location = new Point(3, 101);
            customerNameTextBox.Name = "customerNameTextBox";
            customerNameTextBox.Size = new Size(296, 27);
            customerNameTextBox.TabIndex = 3;
            // 
            // invoiceDateTitleLbl
            // 
            invoiceDateTitleLbl.Dock = DockStyle.Fill;
            invoiceDateTitleLbl.Location = new Point(3, 140);
            invoiceDateTitleLbl.Name = "invoiceDateTitleLbl";
            invoiceDateTitleLbl.Size = new Size(296, 28);
            invoiceDateTitleLbl.TabIndex = 4;
            invoiceDateTitleLbl.Text = "Invoice Date";
            invoiceDateTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // invoiceDateValueLbl
            // 
            invoiceDateValueLbl.Dock = DockStyle.Fill;
            invoiceDateValueLbl.Location = new Point(3, 168);
            invoiceDateValueLbl.Name = "invoiceDateValueLbl";
            invoiceDateValueLbl.Size = new Size(296, 36);
            invoiceDateValueLbl.TabIndex = 5;
            invoiceDateValueLbl.Text = "--";
            invoiceDateValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // totalsLayout
            // 
            totalsLayout.ColumnCount = 2;
            totalsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            totalsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            totalsLayout.Controls.Add(totalAmountTitleLbl, 0, 0);
            totalsLayout.Controls.Add(totalAmountValueLbl, 1, 0);
            totalsLayout.Controls.Add(totalItemsTitleLbl, 0, 1);
            totalsLayout.Controls.Add(totalItemsValueLbl, 1, 1);
            totalsLayout.Dock = DockStyle.Fill;
            totalsLayout.Location = new Point(3, 207);
            totalsLayout.Name = "totalsLayout";
            totalsLayout.RowCount = 2;
            totalsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            totalsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            totalsLayout.Size = new Size(296, 154);
            totalsLayout.TabIndex = 6;
            // 
            // totalAmountTitleLbl
            // 
            totalAmountTitleLbl.Dock = DockStyle.Fill;
            totalAmountTitleLbl.Location = new Point(3, 0);
            totalAmountTitleLbl.Name = "totalAmountTitleLbl";
            totalAmountTitleLbl.Size = new Size(147, 77);
            totalAmountTitleLbl.TabIndex = 0;
            totalAmountTitleLbl.Text = "Amount";
            totalAmountTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // totalAmountValueLbl
            // 
            totalAmountValueLbl.Dock = DockStyle.Fill;
            totalAmountValueLbl.Location = new Point(156, 0);
            totalAmountValueLbl.Name = "totalAmountValueLbl";
            totalAmountValueLbl.Size = new Size(137, 77);
            totalAmountValueLbl.TabIndex = 1;
            totalAmountValueLbl.Text = "0.00 EGP";
            totalAmountValueLbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // totalItemsTitleLbl
            // 
            totalItemsTitleLbl.Dock = DockStyle.Fill;
            totalItemsTitleLbl.Location = new Point(3, 77);
            totalItemsTitleLbl.Name = "totalItemsTitleLbl";
            totalItemsTitleLbl.Size = new Size(147, 77);
            totalItemsTitleLbl.TabIndex = 2;
            totalItemsTitleLbl.Text = "Items";
            totalItemsTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // totalItemsValueLbl
            // 
            totalItemsValueLbl.Dock = DockStyle.Fill;
            totalItemsValueLbl.Location = new Point(156, 77);
            totalItemsValueLbl.Name = "totalItemsValueLbl";
            totalItemsValueLbl.Size = new Size(137, 77);
            totalItemsValueLbl.TabIndex = 3;
            totalItemsValueLbl.Text = "0";
            totalItemsValueLbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // actionsLayout
            // 
            actionsLayout.ColumnCount = 1;
            actionsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            actionsLayout.Controls.Add(saveInvoiceBtn, 0, 0);
            actionsLayout.Controls.Add(clearCartBtn, 0, 1);
            actionsLayout.Controls.Add(removeSelectedBtn, 0, 2);
            actionsLayout.Dock = DockStyle.Fill;
            actionsLayout.Location = new Point(3, 473);
            actionsLayout.Name = "actionsLayout";
            actionsLayout.RowCount = 3;
            actionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            actionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            actionsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            actionsLayout.Size = new Size(296, 114);
            actionsLayout.TabIndex = 7;
            // 
            // saveInvoiceBtn
            // 
            saveInvoiceBtn.Dock = DockStyle.Fill;
            saveInvoiceBtn.Location = new Point(3, 3);
            saveInvoiceBtn.Name = "saveInvoiceBtn";
            saveInvoiceBtn.Size = new Size(290, 32);
            saveInvoiceBtn.TabIndex = 0;
            saveInvoiceBtn.Text = "Save Invoice";
            saveInvoiceBtn.UseVisualStyleBackColor = true;
            // 
            // clearCartBtn
            // 
            clearCartBtn.Dock = DockStyle.Fill;
            clearCartBtn.Location = new Point(3, 41);
            clearCartBtn.Name = "clearCartBtn";
            clearCartBtn.Size = new Size(290, 32);
            clearCartBtn.TabIndex = 1;
            clearCartBtn.Text = "Clear Cart";
            clearCartBtn.UseVisualStyleBackColor = true;
            // 
            // removeSelectedBtn
            // 
            removeSelectedBtn.Dock = DockStyle.Fill;
            removeSelectedBtn.Location = new Point(3, 79);
            removeSelectedBtn.Name = "removeSelectedBtn";
            removeSelectedBtn.Size = new Size(290, 32);
            removeSelectedBtn.TabIndex = 2;
            removeSelectedBtn.Text = "Remove Selected";
            removeSelectedBtn.UseVisualStyleBackColor = true;
            // 
            // PosPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "PosPage";
            Size = new Size(980, 620);
            rootLayout.ResumeLayout(false);
            mainLayout.ResumeLayout(false);
            searchLayout.ResumeLayout(false);
            searchLayout.PerformLayout();
            cartSectionLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cartGrid).EndInit();
            sidebarLayout.ResumeLayout(false);
            totalsLayout.ResumeLayout(false);
            actionsLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private TableLayoutPanel mainLayout;
        private TableLayoutPanel searchLayout;
        private TextBox searchTextBox;
        private Button searchBtn;
        private TableLayoutPanel cartSectionLayout;
        private Label cartTitleLbl;
        private DataGridView cartGrid;
        private TableLayoutPanel sidebarLayout;
        private Label customerPhoneTitleLbl;
        private TextBox customerPhoneTextBox;
        private Label customerNameTitleLbl;
        private TextBox customerNameTextBox;
        private Label invoiceDateTitleLbl;
        private Label invoiceDateValueLbl;
        private TableLayoutPanel totalsLayout;
        private Label totalAmountTitleLbl;
        private Label totalAmountValueLbl;
        private Label totalItemsTitleLbl;
        private Label totalItemsValueLbl;
        private TableLayoutPanel actionsLayout;
        private Button saveInvoiceBtn;
        private Button clearCartBtn;
        private Button removeSelectedBtn;
        private DataGridViewTextBoxColumn productColumn;
        private DataGridViewTextBoxColumn quantityColumn;
        private DataGridViewTextBoxColumn unitPriceColumn;
        private DataGridViewTextBoxColumn lineTotalColumn;
    }
}

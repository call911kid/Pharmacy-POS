namespace UI.Forms.BatchDialog
{
    partial class BatchDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _scannerEventBus.BarcodeScanned -= OnBarcodeScanned;
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            headerLayout = new TableLayoutPanel();
            supplierLabel = new Label();
            supplierComboBox = new ComboBox();
            addSupplierBtn = new Button();
            purchaseDateLabel = new Label();
            purchaseDatePicker = new DateTimePicker();
            scannerLayout = new TableLayoutPanel();
            barcodeLabel = new Label();
            barcodeTextBox = new TextBox();
            useBarcodeBtn = new Button();
            addProductBtn = new Button();
            scanStatusLabel = new Label();
            itemsSectionLayout = new TableLayoutPanel();
            batchItemsLabel = new Label();
            itemsGrid = new DataGridView();
            productIdColumn = new DataGridViewTextBoxColumn();
            barcodeColumn = new DataGridViewTextBoxColumn();
            productColumn = new DataGridViewTextBoxColumn();
            qtyReceivedColumn = new DataGridViewTextBoxColumn();
            qtyRemainingColumn = new DataGridViewTextBoxColumn();
            expiryColumn = new DataGridViewTextBoxColumn();
            costPriceColumn = new DataGridViewTextBoxColumn();
            sellingPriceColumn = new DataGridViewTextBoxColumn();
            actionsPanel = new FlowLayoutPanel();
            addItemBtn = new Button();
            removeItemBtn = new Button();
            saveBtn = new Button();
            clearBtn = new Button();
            cancelBtn = new Button();
            rootLayout.SuspendLayout();
            headerLayout.SuspendLayout();
            scannerLayout.SuspendLayout();
            itemsSectionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)itemsGrid).BeginInit();
            actionsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerLayout, 0, 0);
            rootLayout.Controls.Add(scannerLayout, 0, 1);
            rootLayout.Controls.Add(itemsSectionLayout, 0, 2);
            rootLayout.Controls.Add(actionsPanel, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(14);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            rootLayout.Size = new Size(1120, 700);
            rootLayout.TabIndex = 0;
            // 
            // headerLayout
            // 
            headerLayout.ColumnCount = 3;
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            headerLayout.Controls.Add(supplierLabel, 0, 0);
            headerLayout.Controls.Add(supplierComboBox, 1, 0);
            headerLayout.Controls.Add(addSupplierBtn, 2, 0);
            headerLayout.Controls.Add(purchaseDateLabel, 0, 1);
            headerLayout.Controls.Add(purchaseDatePicker, 1, 1);
            headerLayout.Dock = DockStyle.Fill;
            headerLayout.Location = new Point(17, 17);
            headerLayout.Name = "headerLayout";
            headerLayout.RowCount = 2;
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            headerLayout.Size = new Size(1086, 80);
            headerLayout.TabIndex = 0;
            // 
            // supplierLabel
            // 
            supplierLabel.Dock = DockStyle.Fill;
            supplierLabel.Location = new Point(3, 0);
            supplierLabel.Name = "supplierLabel";
            supplierLabel.Size = new Size(114, 40);
            supplierLabel.TabIndex = 0;
            supplierLabel.Text = "Supplier";
            supplierLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // supplierComboBox
            // 
            supplierComboBox.Dock = DockStyle.Fill;
            supplierComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            supplierComboBox.FormattingEnabled = true;
            supplierComboBox.Location = new Point(123, 6);
            supplierComboBox.Margin = new Padding(3, 6, 3, 6);
            supplierComboBox.Name = "supplierComboBox";
            supplierComboBox.Size = new Size(800, 28);
            supplierComboBox.TabIndex = 1;
            // 
            // addSupplierBtn
            // 
            addSupplierBtn.Dock = DockStyle.Fill;
            addSupplierBtn.Location = new Point(932, 6);
            addSupplierBtn.Margin = new Padding(6);
            addSupplierBtn.Name = "addSupplierBtn";
            addSupplierBtn.Size = new Size(148, 28);
            addSupplierBtn.TabIndex = 2;
            addSupplierBtn.Text = "Add Supplier";
            addSupplierBtn.UseVisualStyleBackColor = true;
            // 
            // purchaseDateLabel
            // 
            purchaseDateLabel.Dock = DockStyle.Fill;
            purchaseDateLabel.Location = new Point(3, 40);
            purchaseDateLabel.Name = "purchaseDateLabel";
            purchaseDateLabel.Size = new Size(114, 40);
            purchaseDateLabel.TabIndex = 3;
            purchaseDateLabel.Text = "Purchase Date";
            purchaseDateLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // purchaseDatePicker
            // 
            purchaseDatePicker.Dock = DockStyle.Left;
            purchaseDatePicker.Format = DateTimePickerFormat.Short;
            purchaseDatePicker.Location = new Point(123, 46);
            purchaseDatePicker.Margin = new Padding(3, 6, 3, 6);
            purchaseDatePicker.Name = "purchaseDatePicker";
            purchaseDatePicker.Size = new Size(180, 27);
            purchaseDatePicker.TabIndex = 4;
            // 
            // scannerLayout
            // 
            scannerLayout.ColumnCount = 4;
            scannerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            scannerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            scannerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            scannerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            scannerLayout.Controls.Add(barcodeLabel, 0, 0);
            scannerLayout.Controls.Add(barcodeTextBox, 1, 0);
            scannerLayout.Controls.Add(useBarcodeBtn, 2, 0);
            scannerLayout.Controls.Add(addProductBtn, 3, 0);
            scannerLayout.Controls.Add(scanStatusLabel, 1, 1);
            scannerLayout.Dock = DockStyle.Fill;
            scannerLayout.Location = new Point(17, 103);
            scannerLayout.Name = "scannerLayout";
            scannerLayout.RowCount = 2;
            scannerLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            scannerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            scannerLayout.Size = new Size(1086, 94);
            scannerLayout.TabIndex = 1;
            // 
            // barcodeLabel
            // 
            barcodeLabel.Dock = DockStyle.Fill;
            barcodeLabel.Location = new Point(3, 0);
            barcodeLabel.Name = "barcodeLabel";
            barcodeLabel.Size = new Size(114, 42);
            barcodeLabel.TabIndex = 0;
            barcodeLabel.Text = "Barcode";
            barcodeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // barcodeTextBox
            // 
            barcodeTextBox.Dock = DockStyle.Fill;
            barcodeTextBox.Location = new Point(123, 7);
            barcodeTextBox.Margin = new Padding(3, 7, 3, 7);
            barcodeTextBox.Name = "barcodeTextBox";
            barcodeTextBox.PlaceholderText = "Scan or type a product barcode";
            barcodeTextBox.Size = new Size(680, 27);
            barcodeTextBox.TabIndex = 1;
            // 
            // useBarcodeBtn
            // 
            useBarcodeBtn.Dock = DockStyle.Fill;
            useBarcodeBtn.Location = new Point(812, 6);
            useBarcodeBtn.Margin = new Padding(6);
            useBarcodeBtn.Name = "useBarcodeBtn";
            useBarcodeBtn.Size = new Size(128, 30);
            useBarcodeBtn.TabIndex = 2;
            useBarcodeBtn.Text = "Use Barcode";
            useBarcodeBtn.UseVisualStyleBackColor = true;
            // 
            // addProductBtn
            // 
            addProductBtn.Dock = DockStyle.Fill;
            addProductBtn.Location = new Point(952, 6);
            addProductBtn.Margin = new Padding(6);
            addProductBtn.Name = "addProductBtn";
            addProductBtn.Size = new Size(128, 30);
            addProductBtn.TabIndex = 3;
            addProductBtn.Text = "Add Product";
            addProductBtn.UseVisualStyleBackColor = true;
            // 
            // scanStatusLabel
            // 
            scanStatusLabel.AutoEllipsis = true;
            scannerLayout.SetColumnSpan(scanStatusLabel, 3);
            scanStatusLabel.Dock = DockStyle.Fill;
            scanStatusLabel.Location = new Point(123, 42);
            scanStatusLabel.Name = "scanStatusLabel";
            scanStatusLabel.Size = new Size(960, 52);
            scanStatusLabel.TabIndex = 4;
            scanStatusLabel.Text = "Select a row and scan a product barcode to attach it quickly.";
            scanStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // itemsSectionLayout
            // 
            itemsSectionLayout.ColumnCount = 1;
            itemsSectionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            itemsSectionLayout.Controls.Add(batchItemsLabel, 0, 0);
            itemsSectionLayout.Controls.Add(itemsGrid, 0, 1);
            itemsSectionLayout.Dock = DockStyle.Fill;
            itemsSectionLayout.Location = new Point(17, 203);
            itemsSectionLayout.Name = "itemsSectionLayout";
            itemsSectionLayout.RowCount = 2;
            itemsSectionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            itemsSectionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            itemsSectionLayout.Size = new Size(1086, 422);
            itemsSectionLayout.TabIndex = 2;
            // 
            // batchItemsLabel
            // 
            batchItemsLabel.Dock = DockStyle.Fill;
            batchItemsLabel.Location = new Point(3, 0);
            batchItemsLabel.Name = "batchItemsLabel";
            batchItemsLabel.Size = new Size(1080, 34);
            batchItemsLabel.TabIndex = 0;
            batchItemsLabel.Text = "Batch Items";
            batchItemsLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // itemsGrid
            // 
            itemsGrid.AllowUserToAddRows = false;
            itemsGrid.AllowUserToDeleteRows = false;
            itemsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            itemsGrid.Columns.AddRange(new DataGridViewColumn[] { productIdColumn, barcodeColumn, productColumn, qtyReceivedColumn, qtyRemainingColumn, expiryColumn, costPriceColumn, sellingPriceColumn });
            itemsGrid.Dock = DockStyle.Fill;
            itemsGrid.Location = new Point(3, 37);
            itemsGrid.MultiSelect = false;
            itemsGrid.Name = "itemsGrid";
            itemsGrid.RowHeadersVisible = false;
            itemsGrid.RowHeadersWidth = 51;
            itemsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            itemsGrid.Size = new Size(1080, 382);
            itemsGrid.TabIndex = 1;
            // 
            // productIdColumn
            // 
            productIdColumn.HeaderText = "Product ID";
            productIdColumn.MinimumWidth = 6;
            productIdColumn.Name = "productIdColumn";
            productIdColumn.ReadOnly = true;
            productIdColumn.Width = 90;
            // 
            // barcodeColumn
            // 
            barcodeColumn.HeaderText = "Barcode";
            barcodeColumn.MinimumWidth = 6;
            barcodeColumn.Name = "barcodeColumn";
            barcodeColumn.ReadOnly = true;
            barcodeColumn.Width = 150;
            // 
            // productColumn
            // 
            productColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            productColumn.HeaderText = "Product";
            productColumn.MinimumWidth = 6;
            productColumn.Name = "productColumn";
            productColumn.ReadOnly = true;
            // 
            // qtyReceivedColumn
            // 
            qtyReceivedColumn.HeaderText = "Qty Received";
            qtyReceivedColumn.MinimumWidth = 6;
            qtyReceivedColumn.Name = "qtyReceivedColumn";
            qtyReceivedColumn.Width = 110;
            // 
            // qtyRemainingColumn
            // 
            qtyRemainingColumn.HeaderText = "Remaining";
            qtyRemainingColumn.MinimumWidth = 6;
            qtyRemainingColumn.Name = "qtyRemainingColumn";
            qtyRemainingColumn.ReadOnly = true;
            qtyRemainingColumn.Width = 95;
            // 
            // expiryColumn
            // 
            expiryColumn.HeaderText = "Expiry";
            expiryColumn.MinimumWidth = 6;
            expiryColumn.Name = "expiryColumn";
            expiryColumn.Width = 120;
            // 
            // costPriceColumn
            // 
            costPriceColumn.HeaderText = "Cost";
            costPriceColumn.MinimumWidth = 6;
            costPriceColumn.Name = "costPriceColumn";
            costPriceColumn.Width = 95;
            // 
            // sellingPriceColumn
            // 
            sellingPriceColumn.HeaderText = "Sell Price";
            sellingPriceColumn.MinimumWidth = 6;
            sellingPriceColumn.Name = "sellingPriceColumn";
            sellingPriceColumn.Width = 95;
            // 
            // actionsPanel
            // 
            actionsPanel.Controls.Add(addItemBtn);
            actionsPanel.Controls.Add(removeItemBtn);
            actionsPanel.Controls.Add(saveBtn);
            actionsPanel.Controls.Add(clearBtn);
            actionsPanel.Controls.Add(cancelBtn);
            actionsPanel.Dock = DockStyle.Fill;
            actionsPanel.FlowDirection = FlowDirection.RightToLeft;
            actionsPanel.Location = new Point(17, 631);
            actionsPanel.Name = "actionsPanel";
            actionsPanel.Padding = new Padding(0, 8, 0, 0);
            actionsPanel.Size = new Size(1086, 52);
            actionsPanel.TabIndex = 3;
            actionsPanel.WrapContents = false;
            // 
            // addItemBtn
            // 
            addItemBtn.Location = new Point(995, 11);
            addItemBtn.Name = "addItemBtn";
            addItemBtn.Size = new Size(88, 34);
            addItemBtn.TabIndex = 0;
            addItemBtn.Text = "Add Item";
            addItemBtn.UseVisualStyleBackColor = true;
            // 
            // removeItemBtn
            // 
            removeItemBtn.Location = new Point(898, 11);
            removeItemBtn.Name = "removeItemBtn";
            removeItemBtn.Size = new Size(91, 34);
            removeItemBtn.TabIndex = 1;
            removeItemBtn.Text = "Remove";
            removeItemBtn.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(817, 11);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(75, 34);
            saveBtn.TabIndex = 2;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            clearBtn.Location = new Point(736, 11);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(75, 34);
            clearBtn.TabIndex = 3;
            clearBtn.Text = "Clear";
            clearBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(655, 11);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 34);
            cancelBtn.TabIndex = 4;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // BatchDialog
            // 
            AcceptButton = saveBtn;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelBtn;
            ClientSize = new Size(1120, 700);
            Controls.Add(rootLayout);
            MinimizeBox = false;
            MinimumSize = new Size(980, 620);
            Name = "BatchDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Batch";
            rootLayout.ResumeLayout(false);
            headerLayout.ResumeLayout(false);
            scannerLayout.ResumeLayout(false);
            scannerLayout.PerformLayout();
            itemsSectionLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)itemsGrid).EndInit();
            actionsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private TableLayoutPanel headerLayout;
        private Label supplierLabel;
        private ComboBox supplierComboBox;
        private Button addSupplierBtn;
        private Label purchaseDateLabel;
        private DateTimePicker purchaseDatePicker;
        private TableLayoutPanel scannerLayout;
        private Label barcodeLabel;
        private TextBox barcodeTextBox;
        private Button useBarcodeBtn;
        private Button addProductBtn;
        private Label scanStatusLabel;
        private TableLayoutPanel itemsSectionLayout;
        private Label batchItemsLabel;
        private DataGridView itemsGrid;
        private DataGridViewTextBoxColumn productIdColumn;
        private DataGridViewTextBoxColumn barcodeColumn;
        private DataGridViewTextBoxColumn productColumn;
        private DataGridViewTextBoxColumn qtyReceivedColumn;
        private DataGridViewTextBoxColumn qtyRemainingColumn;
        private DataGridViewTextBoxColumn expiryColumn;
        private DataGridViewTextBoxColumn costPriceColumn;
        private DataGridViewTextBoxColumn sellingPriceColumn;
        private FlowLayoutPanel actionsPanel;
        private Button addItemBtn;
        private Button removeItemBtn;
        private Button saveBtn;
        private Button clearBtn;
        private Button cancelBtn;
    }
}

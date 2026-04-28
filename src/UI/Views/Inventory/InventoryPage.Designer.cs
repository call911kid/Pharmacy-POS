namespace UI.Views.Inventory
{
    partial class InventoryPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            toolbarLayout = new TableLayoutPanel();
            searchTextBox = new TextBox();
            addBatchBtn = new Button();
            editBatchBtn = new Button();
            deleteBatchBtn = new Button();
            summaryLayout = new TableLayoutPanel();
            batchIdCard = new Panel();
            batchIdValueLbl = new Label();
            batchIdTitleLbl = new Label();
            supplierCard = new Panel();
            supplierValueLbl = new Label();
            supplierTitleLbl = new Label();
            purchaseDateCard = new Panel();
            purchaseDateValueLbl = new Label();
            purchaseDateTitleLbl = new Label();
            itemsCountCard = new Panel();
            itemsCountValueLbl = new Label();
            itemsCountTitleLbl = new Label();
            totalRemainingCard = new Panel();
            totalRemainingValueLbl = new Label();
            totalRemainingTitleLbl = new Label();
            batchesSection = new TableLayoutPanel();
            batchesTitleLbl = new Label();
            batchesGrid = new DataGridView();
            batchIdColumn = new DataGridViewTextBoxColumn();
            supplierColumn = new DataGridViewTextBoxColumn();
            purchaseDateColumn = new DataGridViewTextBoxColumn();
            itemsCountColumn = new DataGridViewTextBoxColumn();
            totalRemainingColumn = new DataGridViewTextBoxColumn();
            batchItemsSection = new TableLayoutPanel();
            batchItemsTitleLbl = new Label();
            batchItemsGrid = new DataGridView();
            productColumn = new DataGridViewTextBoxColumn();
            quantityReceivedColumn = new DataGridViewTextBoxColumn();
            quantityRemainingColumn = new DataGridViewTextBoxColumn();
            expirationDateColumn = new DataGridViewTextBoxColumn();
            costPriceColumn = new DataGridViewTextBoxColumn();
            sellingPriceColumn = new DataGridViewTextBoxColumn();
            rootLayout.SuspendLayout();
            toolbarLayout.SuspendLayout();
            summaryLayout.SuspendLayout();
            batchIdCard.SuspendLayout();
            supplierCard.SuspendLayout();
            purchaseDateCard.SuspendLayout();
            itemsCountCard.SuspendLayout();
            totalRemainingCard.SuspendLayout();
            batchesSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)batchesGrid).BeginInit();
            batchItemsSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)batchItemsGrid).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(toolbarLayout, 0, 0);
            rootLayout.Controls.Add(summaryLayout, 0, 1);
            rootLayout.Controls.Add(batchesSection, 0, 2);
            rootLayout.Controls.Add(batchItemsSection, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            rootLayout.Size = new Size(980, 620);
            rootLayout.TabIndex = 0;
            // 
            // toolbarLayout
            // 
            toolbarLayout.ColumnCount = 4;
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            toolbarLayout.Controls.Add(searchTextBox, 0, 0);
            toolbarLayout.Controls.Add(addBatchBtn, 1, 0);
            toolbarLayout.Controls.Add(editBatchBtn, 2, 0);
            toolbarLayout.Controls.Add(deleteBatchBtn, 3, 0);
            toolbarLayout.Dock = DockStyle.Fill;
            toolbarLayout.Location = new Point(3, 3);
            toolbarLayout.Name = "toolbarLayout";
            toolbarLayout.RowCount = 1;
            toolbarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            toolbarLayout.Size = new Size(974, 44);
            toolbarLayout.TabIndex = 0;
            // 
            // searchTextBox
            // 
            searchTextBox.Dock = DockStyle.Fill;
            searchTextBox.Location = new Point(8, 8);
            searchTextBox.Margin = new Padding(8);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(628, 27);
            searchTextBox.TabIndex = 0;
            // 
            // addBatchBtn
            // 
            addBatchBtn.Dock = DockStyle.Fill;
            addBatchBtn.Location = new Point(650, 6);
            addBatchBtn.Margin = new Padding(6);
            addBatchBtn.Name = "addBatchBtn";
            addBatchBtn.Size = new Size(98, 32);
            addBatchBtn.TabIndex = 1;
            addBatchBtn.Text = "Add Batch";
            addBatchBtn.UseVisualStyleBackColor = true;
            // 
            // editBatchBtn
            // 
            editBatchBtn.Dock = DockStyle.Fill;
            editBatchBtn.Location = new Point(760, 6);
            editBatchBtn.Margin = new Padding(6);
            editBatchBtn.Name = "editBatchBtn";
            editBatchBtn.Size = new Size(98, 32);
            editBatchBtn.TabIndex = 2;
            editBatchBtn.Text = "Edit Batch";
            editBatchBtn.UseVisualStyleBackColor = true;
            // 
            // deleteBatchBtn
            // 
            deleteBatchBtn.Dock = DockStyle.Fill;
            deleteBatchBtn.Location = new Point(870, 6);
            deleteBatchBtn.Margin = new Padding(6);
            deleteBatchBtn.Name = "deleteBatchBtn";
            deleteBatchBtn.Size = new Size(98, 32);
            deleteBatchBtn.TabIndex = 3;
            deleteBatchBtn.Text = "Delete";
            deleteBatchBtn.UseVisualStyleBackColor = true;
            // 
            // summaryLayout
            // 
            summaryLayout.ColumnCount = 5;
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            summaryLayout.Controls.Add(batchIdCard, 0, 0);
            summaryLayout.Controls.Add(supplierCard, 1, 0);
            summaryLayout.Controls.Add(purchaseDateCard, 2, 0);
            summaryLayout.Controls.Add(itemsCountCard, 3, 0);
            summaryLayout.Controls.Add(totalRemainingCard, 4, 0);
            summaryLayout.Dock = DockStyle.Fill;
            summaryLayout.Location = new Point(3, 53);
            summaryLayout.Name = "summaryLayout";
            summaryLayout.RowCount = 1;
            summaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            summaryLayout.Size = new Size(974, 86);
            summaryLayout.TabIndex = 1;
            // 
            // batchIdCard
            // 
            batchIdCard.Controls.Add(batchIdValueLbl);
            batchIdCard.Controls.Add(batchIdTitleLbl);
            batchIdCard.Dock = DockStyle.Fill;
            batchIdCard.Location = new Point(6, 6);
            batchIdCard.Margin = new Padding(6);
            batchIdCard.Name = "batchIdCard";
            batchIdCard.Padding = new Padding(12);
            batchIdCard.Size = new Size(182, 74);
            batchIdCard.TabIndex = 0;
            // 
            // batchIdValueLbl
            // 
            batchIdValueLbl.Dock = DockStyle.Fill;
            batchIdValueLbl.Location = new Point(12, 32);
            batchIdValueLbl.Name = "batchIdValueLbl";
            batchIdValueLbl.Size = new Size(158, 30);
            batchIdValueLbl.TabIndex = 1;
            batchIdValueLbl.Text = "--";
            batchIdValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // batchIdTitleLbl
            // 
            batchIdTitleLbl.Dock = DockStyle.Top;
            batchIdTitleLbl.Location = new Point(12, 12);
            batchIdTitleLbl.Name = "batchIdTitleLbl";
            batchIdTitleLbl.Size = new Size(158, 20);
            batchIdTitleLbl.TabIndex = 0;
            batchIdTitleLbl.Text = "Batch ID";
            // 
            // supplierCard
            // 
            supplierCard.Controls.Add(supplierValueLbl);
            supplierCard.Controls.Add(supplierTitleLbl);
            supplierCard.Dock = DockStyle.Fill;
            supplierCard.Location = new Point(200, 6);
            supplierCard.Margin = new Padding(6);
            supplierCard.Name = "supplierCard";
            supplierCard.Padding = new Padding(12);
            supplierCard.Size = new Size(182, 74);
            supplierCard.TabIndex = 1;
            // 
            // supplierValueLbl
            // 
            supplierValueLbl.Dock = DockStyle.Fill;
            supplierValueLbl.Location = new Point(12, 32);
            supplierValueLbl.Name = "supplierValueLbl";
            supplierValueLbl.Size = new Size(158, 30);
            supplierValueLbl.TabIndex = 1;
            supplierValueLbl.Text = "--";
            supplierValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // supplierTitleLbl
            // 
            supplierTitleLbl.Dock = DockStyle.Top;
            supplierTitleLbl.Location = new Point(12, 12);
            supplierTitleLbl.Name = "supplierTitleLbl";
            supplierTitleLbl.Size = new Size(158, 20);
            supplierTitleLbl.TabIndex = 0;
            supplierTitleLbl.Text = "Supplier";
            // 
            // purchaseDateCard
            // 
            purchaseDateCard.Controls.Add(purchaseDateValueLbl);
            purchaseDateCard.Controls.Add(purchaseDateTitleLbl);
            purchaseDateCard.Dock = DockStyle.Fill;
            purchaseDateCard.Location = new Point(394, 6);
            purchaseDateCard.Margin = new Padding(6);
            purchaseDateCard.Name = "purchaseDateCard";
            purchaseDateCard.Padding = new Padding(12);
            purchaseDateCard.Size = new Size(182, 74);
            purchaseDateCard.TabIndex = 2;
            // 
            // purchaseDateValueLbl
            // 
            purchaseDateValueLbl.Dock = DockStyle.Fill;
            purchaseDateValueLbl.Location = new Point(12, 32);
            purchaseDateValueLbl.Name = "purchaseDateValueLbl";
            purchaseDateValueLbl.Size = new Size(158, 30);
            purchaseDateValueLbl.TabIndex = 1;
            purchaseDateValueLbl.Text = "--";
            purchaseDateValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // purchaseDateTitleLbl
            // 
            purchaseDateTitleLbl.Dock = DockStyle.Top;
            purchaseDateTitleLbl.Location = new Point(12, 12);
            purchaseDateTitleLbl.Name = "purchaseDateTitleLbl";
            purchaseDateTitleLbl.Size = new Size(158, 20);
            purchaseDateTitleLbl.TabIndex = 0;
            purchaseDateTitleLbl.Text = "Purchase Date";
            // 
            // itemsCountCard
            // 
            itemsCountCard.Controls.Add(itemsCountValueLbl);
            itemsCountCard.Controls.Add(itemsCountTitleLbl);
            itemsCountCard.Dock = DockStyle.Fill;
            itemsCountCard.Location = new Point(588, 6);
            itemsCountCard.Margin = new Padding(6);
            itemsCountCard.Name = "itemsCountCard";
            itemsCountCard.Padding = new Padding(12);
            itemsCountCard.Size = new Size(182, 74);
            itemsCountCard.TabIndex = 3;
            // 
            // itemsCountValueLbl
            // 
            itemsCountValueLbl.Dock = DockStyle.Fill;
            itemsCountValueLbl.Location = new Point(12, 32);
            itemsCountValueLbl.Name = "itemsCountValueLbl";
            itemsCountValueLbl.Size = new Size(158, 30);
            itemsCountValueLbl.TabIndex = 1;
            itemsCountValueLbl.Text = "--";
            itemsCountValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // itemsCountTitleLbl
            // 
            itemsCountTitleLbl.Dock = DockStyle.Top;
            itemsCountTitleLbl.Location = new Point(12, 12);
            itemsCountTitleLbl.Name = "itemsCountTitleLbl";
            itemsCountTitleLbl.Size = new Size(158, 20);
            itemsCountTitleLbl.TabIndex = 0;
            itemsCountTitleLbl.Text = "Items Count";
            // 
            // totalRemainingCard
            // 
            totalRemainingCard.Controls.Add(totalRemainingValueLbl);
            totalRemainingCard.Controls.Add(totalRemainingTitleLbl);
            totalRemainingCard.Dock = DockStyle.Fill;
            totalRemainingCard.Location = new Point(782, 6);
            totalRemainingCard.Margin = new Padding(6);
            totalRemainingCard.Name = "totalRemainingCard";
            totalRemainingCard.Padding = new Padding(12);
            totalRemainingCard.Size = new Size(186, 74);
            totalRemainingCard.TabIndex = 4;
            // 
            // totalRemainingValueLbl
            // 
            totalRemainingValueLbl.Dock = DockStyle.Fill;
            totalRemainingValueLbl.Location = new Point(12, 32);
            totalRemainingValueLbl.Name = "totalRemainingValueLbl";
            totalRemainingValueLbl.Size = new Size(162, 30);
            totalRemainingValueLbl.TabIndex = 1;
            totalRemainingValueLbl.Text = "--";
            totalRemainingValueLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // totalRemainingTitleLbl
            // 
            totalRemainingTitleLbl.Dock = DockStyle.Top;
            totalRemainingTitleLbl.Location = new Point(12, 12);
            totalRemainingTitleLbl.Name = "totalRemainingTitleLbl";
            totalRemainingTitleLbl.Size = new Size(162, 20);
            totalRemainingTitleLbl.TabIndex = 0;
            totalRemainingTitleLbl.Text = "Qty Remaining";
            // 
            // batchesSection
            // 
            batchesSection.ColumnCount = 1;
            batchesSection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            batchesSection.Controls.Add(batchesTitleLbl, 0, 0);
            batchesSection.Controls.Add(batchesGrid, 0, 1);
            batchesSection.Dock = DockStyle.Fill;
            batchesSection.Location = new Point(3, 145);
            batchesSection.Name = "batchesSection";
            batchesSection.RowCount = 2;
            batchesSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            batchesSection.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            batchesSection.Size = new Size(974, 194);
            batchesSection.TabIndex = 2;
            // 
            // batchesTitleLbl
            // 
            batchesTitleLbl.Dock = DockStyle.Fill;
            batchesTitleLbl.Location = new Point(6, 6);
            batchesTitleLbl.Margin = new Padding(6);
            batchesTitleLbl.Name = "batchesTitleLbl";
            batchesTitleLbl.Size = new Size(962, 24);
            batchesTitleLbl.TabIndex = 0;
            batchesTitleLbl.Text = "Batches";
            batchesTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // batchesGrid
            // 
            batchesGrid.AllowUserToAddRows = false;
            batchesGrid.AllowUserToDeleteRows = false;
            batchesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            batchesGrid.Columns.AddRange(new DataGridViewColumn[] { batchIdColumn, supplierColumn, purchaseDateColumn, itemsCountColumn, totalRemainingColumn });
            batchesGrid.Dock = DockStyle.Fill;
            batchesGrid.Location = new Point(6, 42);
            batchesGrid.Margin = new Padding(6);
            batchesGrid.MultiSelect = false;
            batchesGrid.Name = "batchesGrid";
            batchesGrid.ReadOnly = true;
            batchesGrid.RowHeadersVisible = false;
            batchesGrid.RowHeadersWidth = 51;
            batchesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            batchesGrid.Size = new Size(962, 146);
            batchesGrid.TabIndex = 1;
            // 
            // batchIdColumn
            // 
            batchIdColumn.HeaderText = "Batch Id";
            batchIdColumn.MinimumWidth = 6;
            batchIdColumn.Name = "batchIdColumn";
            batchIdColumn.ReadOnly = true;
            batchIdColumn.Width = 80;
            // 
            // supplierColumn
            // 
            supplierColumn.HeaderText = "Supplier";
            supplierColumn.MinimumWidth = 6;
            supplierColumn.Name = "supplierColumn";
            supplierColumn.ReadOnly = true;
            supplierColumn.Width = 180;
            // 
            // purchaseDateColumn
            // 
            purchaseDateColumn.HeaderText = "Purchase Date";
            purchaseDateColumn.MinimumWidth = 6;
            purchaseDateColumn.Name = "purchaseDateColumn";
            purchaseDateColumn.ReadOnly = true;
            purchaseDateColumn.Width = 150;
            // 
            // itemsCountColumn
            // 
            itemsCountColumn.HeaderText = "Items Count";
            itemsCountColumn.MinimumWidth = 6;
            itemsCountColumn.Name = "itemsCountColumn";
            itemsCountColumn.ReadOnly = true;
            itemsCountColumn.Width = 110;
            // 
            // totalRemainingColumn
            // 
            totalRemainingColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            totalRemainingColumn.HeaderText = "Qty Remaining";
            totalRemainingColumn.MinimumWidth = 6;
            totalRemainingColumn.Name = "totalRemainingColumn";
            totalRemainingColumn.ReadOnly = true;
            // 
            // batchItemsSection
            // 
            batchItemsSection.ColumnCount = 1;
            batchItemsSection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            batchItemsSection.Controls.Add(batchItemsTitleLbl, 0, 0);
            batchItemsSection.Controls.Add(batchItemsGrid, 0, 1);
            batchItemsSection.Dock = DockStyle.Fill;
            batchItemsSection.Location = new Point(3, 345);
            batchItemsSection.Name = "batchItemsSection";
            batchItemsSection.RowCount = 2;
            batchItemsSection.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            batchItemsSection.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            batchItemsSection.Size = new Size(974, 272);
            batchItemsSection.TabIndex = 3;
            // 
            // batchItemsTitleLbl
            // 
            batchItemsTitleLbl.Dock = DockStyle.Fill;
            batchItemsTitleLbl.Location = new Point(6, 6);
            batchItemsTitleLbl.Margin = new Padding(6);
            batchItemsTitleLbl.Name = "batchItemsTitleLbl";
            batchItemsTitleLbl.Size = new Size(962, 24);
            batchItemsTitleLbl.TabIndex = 0;
            batchItemsTitleLbl.Text = "Batch Items";
            batchItemsTitleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // batchItemsGrid
            // 
            batchItemsGrid.AllowUserToAddRows = false;
            batchItemsGrid.AllowUserToDeleteRows = false;
            batchItemsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            batchItemsGrid.Columns.AddRange(new DataGridViewColumn[] { productColumn, quantityReceivedColumn, quantityRemainingColumn, expirationDateColumn, costPriceColumn, sellingPriceColumn });
            batchItemsGrid.Dock = DockStyle.Fill;
            batchItemsGrid.Location = new Point(6, 42);
            batchItemsGrid.Margin = new Padding(6);
            batchItemsGrid.MultiSelect = false;
            batchItemsGrid.Name = "batchItemsGrid";
            batchItemsGrid.ReadOnly = true;
            batchItemsGrid.RowHeadersVisible = false;
            batchItemsGrid.RowHeadersWidth = 51;
            batchItemsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            batchItemsGrid.Size = new Size(962, 224);
            batchItemsGrid.TabIndex = 1;
            // 
            // productColumn
            // 
            productColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            productColumn.HeaderText = "Product";
            productColumn.MinimumWidth = 6;
            productColumn.Name = "productColumn";
            productColumn.ReadOnly = true;
            // 
            // quantityReceivedColumn
            // 
            quantityReceivedColumn.HeaderText = "Received";
            quantityReceivedColumn.MinimumWidth = 6;
            quantityReceivedColumn.Name = "quantityReceivedColumn";
            quantityReceivedColumn.ReadOnly = true;
            quantityReceivedColumn.Width = 110;
            // 
            // quantityRemainingColumn
            // 
            quantityRemainingColumn.HeaderText = "Remaining";
            quantityRemainingColumn.MinimumWidth = 6;
            quantityRemainingColumn.Name = "quantityRemainingColumn";
            quantityRemainingColumn.ReadOnly = true;
            quantityRemainingColumn.Width = 110;
            // 
            // expirationDateColumn
            // 
            expirationDateColumn.HeaderText = "Expiry";
            expirationDateColumn.MinimumWidth = 6;
            expirationDateColumn.Name = "expirationDateColumn";
            expirationDateColumn.ReadOnly = true;
            expirationDateColumn.Width = 120;
            // 
            // costPriceColumn
            // 
            costPriceColumn.HeaderText = "Cost";
            costPriceColumn.MinimumWidth = 6;
            costPriceColumn.Name = "costPriceColumn";
            costPriceColumn.ReadOnly = true;
            costPriceColumn.Width = 90;
            // 
            // sellingPriceColumn
            // 
            sellingPriceColumn.HeaderText = "Sell Price";
            sellingPriceColumn.MinimumWidth = 6;
            sellingPriceColumn.Name = "sellingPriceColumn";
            sellingPriceColumn.ReadOnly = true;
            sellingPriceColumn.Width = 95;
            // 
            // InventoryPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(rootLayout);
            Name = "InventoryPage";
            Size = new Size(980, 620);
            rootLayout.ResumeLayout(false);
            toolbarLayout.ResumeLayout(false);
            toolbarLayout.PerformLayout();
            summaryLayout.ResumeLayout(false);
            batchIdCard.ResumeLayout(false);
            supplierCard.ResumeLayout(false);
            purchaseDateCard.ResumeLayout(false);
            itemsCountCard.ResumeLayout(false);
            totalRemainingCard.ResumeLayout(false);
            batchesSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)batchesGrid).EndInit();
            batchItemsSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)batchItemsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private TableLayoutPanel toolbarLayout;
        private TextBox searchTextBox;
        private Button addBatchBtn;
        private Button editBatchBtn;
        private Button deleteBatchBtn;
        private TableLayoutPanel summaryLayout;
        private Panel batchIdCard;
        private Label batchIdTitleLbl;
        private Label batchIdValueLbl;
        private Panel supplierCard;
        private Label supplierTitleLbl;
        private Label supplierValueLbl;
        private Panel purchaseDateCard;
        private Label purchaseDateTitleLbl;
        private Label purchaseDateValueLbl;
        private Panel itemsCountCard;
        private Label itemsCountTitleLbl;
        private Label itemsCountValueLbl;
        private Panel totalRemainingCard;
        private Label totalRemainingTitleLbl;
        private Label totalRemainingValueLbl;
        private TableLayoutPanel batchesSection;
        private Label batchesTitleLbl;
        private DataGridView batchesGrid;
        private DataGridViewTextBoxColumn batchIdColumn;
        private DataGridViewTextBoxColumn supplierColumn;
        private DataGridViewTextBoxColumn purchaseDateColumn;
        private DataGridViewTextBoxColumn itemsCountColumn;
        private DataGridViewTextBoxColumn totalRemainingColumn;
        private TableLayoutPanel batchItemsSection;
        private Label batchItemsTitleLbl;
        private DataGridView batchItemsGrid;
        private DataGridViewTextBoxColumn productColumn;
        private DataGridViewTextBoxColumn quantityReceivedColumn;
        private DataGridViewTextBoxColumn quantityRemainingColumn;
        private DataGridViewTextBoxColumn expirationDateColumn;
        private DataGridViewTextBoxColumn costPriceColumn;
        private DataGridViewTextBoxColumn sellingPriceColumn;
    }
}

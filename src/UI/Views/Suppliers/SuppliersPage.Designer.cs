namespace UI.Views.Suppliers
{
    partial class SuppliersPage
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
            tableLayoutPanel1 = new TableLayoutPanel();
            toolbarLayout = new TableLayoutPanel();
            searchTextBox = new TextBox();
            addBtn = new Button();
            editBtn = new Button();
            deleteBtn = new Button();
            contentLayout = new TableLayoutPanel();
            suppliersListLayout = new TableLayoutPanel();
            suppliersLbl = new Label();
            suppliersGrid = new DataGridView();
            idColumn = new DataGridViewTextBoxColumn();
            nameColumn = new DataGridViewTextBoxColumn();
            phoneColumn = new DataGridViewTextBoxColumn();
            supplierDetails = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            supplierBatchesLbl = new Label();
            supplierBatchesGrid = new DataGridView();
            batchIdColumn = new DataGridViewTextBoxColumn();
            purchaseDateColumn = new DataGridViewTextBoxColumn();
            itemsCountColumn = new DataGridViewTextBoxColumn();
            totalReceivedColumn = new DataGridViewTextBoxColumn();
            totalRemainingColumn = new DataGridViewTextBoxColumn();
            batchItemsLayout = new TableLayoutPanel();
            batchItemsLbl = new Label();
            batchItemsGrid = new DataGridView();
            itemProductColumn = new DataGridViewTextBoxColumn();
            itemReceivedColumn = new DataGridViewTextBoxColumn();
            itemRemainingColumn = new DataGridViewTextBoxColumn();
            itemExpiryColumn = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            toolbarLayout.SuspendLayout();
            contentLayout.SuspendLayout();
            suppliersListLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)suppliersGrid).BeginInit();
            supplierDetails.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)supplierBatchesGrid).BeginInit();
            batchItemsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)batchItemsGrid).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(toolbarLayout, 0, 0);
            tableLayoutPanel1.Controls.Add(contentLayout, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(821, 455);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // toolbarLayout
            // 
            toolbarLayout.ColumnCount = 4;
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            toolbarLayout.Controls.Add(searchTextBox, 0, 0);
            toolbarLayout.Controls.Add(addBtn, 1, 0);
            toolbarLayout.Controls.Add(editBtn, 2, 0);
            toolbarLayout.Controls.Add(deleteBtn, 3, 0);
            toolbarLayout.Dock = DockStyle.Fill;
            toolbarLayout.Location = new Point(3, 3);
            toolbarLayout.Name = "toolbarLayout";
            toolbarLayout.RowCount = 1;
            toolbarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            toolbarLayout.Size = new Size(815, 44);
            toolbarLayout.TabIndex = 0;
            // 
            // searchTextBox
            // 
            searchTextBox.Dock = DockStyle.Fill;
            searchTextBox.Location = new Point(8, 8);
            searchTextBox.Margin = new Padding(8);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(529, 27);
            searchTextBox.TabIndex = 0;
            // 
            // addBtn
            // 
            addBtn.Dock = DockStyle.Fill;
            addBtn.Location = new Point(549, 6);
            addBtn.Margin = new Padding(4, 6, 4, 6);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(82, 32);
            addBtn.TabIndex = 1;
            addBtn.Text = "Add";
            addBtn.UseVisualStyleBackColor = true;
            // 
            // editBtn
            // 
            editBtn.Dock = DockStyle.Fill;
            editBtn.Location = new Point(639, 6);
            editBtn.Margin = new Padding(4, 6, 4, 6);
            editBtn.Name = "editBtn";
            editBtn.Size = new Size(82, 32);
            editBtn.TabIndex = 2;
            editBtn.Text = "Edit";
            editBtn.UseVisualStyleBackColor = true;
            // 
            // deleteBtn
            // 
            deleteBtn.Dock = DockStyle.Fill;
            deleteBtn.Location = new Point(729, 6);
            deleteBtn.Margin = new Padding(4, 6, 4, 6);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(82, 32);
            deleteBtn.TabIndex = 3;
            deleteBtn.Text = "Delete";
            deleteBtn.UseVisualStyleBackColor = true;
            // 
            // contentLayout
            // 
            contentLayout.ColumnCount = 2;
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            contentLayout.Controls.Add(suppliersListLayout, 0, 0);
            contentLayout.Controls.Add(supplierDetails, 1, 0);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(3, 53);
            contentLayout.Name = "contentLayout";
            contentLayout.RowCount = 1;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentLayout.Size = new Size(815, 399);
            contentLayout.TabIndex = 1;
            // 
            // suppliersListLayout
            // 
            suppliersListLayout.ColumnCount = 1;
            suppliersListLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            suppliersListLayout.Controls.Add(suppliersLbl, 0, 0);
            suppliersListLayout.Controls.Add(suppliersGrid, 0, 1);
            suppliersListLayout.Dock = DockStyle.Fill;
            suppliersListLayout.Location = new Point(3, 3);
            suppliersListLayout.Name = "suppliersListLayout";
            suppliersListLayout.RowCount = 2;
            suppliersListLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            suppliersListLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            suppliersListLayout.Size = new Size(265, 393);
            suppliersListLayout.TabIndex = 0;
            // 
            // suppliersLbl
            // 
            suppliersLbl.Dock = DockStyle.Fill;
            suppliersLbl.Location = new Point(6, 6);
            suppliersLbl.Margin = new Padding(6);
            suppliersLbl.Name = "suppliersLbl";
            suppliersLbl.Padding = new Padding(4, 0, 0, 0);
            suppliersLbl.Size = new Size(253, 38);
            suppliersLbl.TabIndex = 0;
            suppliersLbl.Text = "Suppliers";
            suppliersLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // suppliersGrid
            // 
            suppliersGrid.AllowUserToAddRows = false;
            suppliersGrid.AllowUserToDeleteRows = false;
            suppliersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            suppliersGrid.Columns.AddRange(new DataGridViewColumn[] { idColumn, nameColumn, phoneColumn });
            suppliersGrid.Dock = DockStyle.Fill;
            suppliersGrid.Location = new Point(3, 53);
            suppliersGrid.Margin = new Padding(3);
            suppliersGrid.MultiSelect = false;
            suppliersGrid.Name = "suppliersGrid";
            suppliersGrid.ReadOnly = true;
            suppliersGrid.RowHeadersVisible = false;
            suppliersGrid.RowHeadersWidth = 51;
            suppliersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            suppliersGrid.Size = new Size(259, 337);
            suppliersGrid.TabIndex = 1;
            // 
            // idColumn
            // 
            idColumn.DataPropertyName = "Id";
            idColumn.HeaderText = "Id";
            idColumn.MinimumWidth = 6;
            idColumn.Name = "idColumn";
            idColumn.ReadOnly = true;
            idColumn.Width = 50;
            // 
            // nameColumn
            // 
            nameColumn.DataPropertyName = "Name";
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumn.FillWeight = 55F;
            nameColumn.HeaderText = "Name";
            nameColumn.MinimumWidth = 6;
            nameColumn.Name = "nameColumn";
            nameColumn.ReadOnly = true;
            // 
            // phoneColumn
            // 
            phoneColumn.DataPropertyName = "Phone";
            phoneColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            phoneColumn.FillWeight = 45F;
            phoneColumn.HeaderText = "Phone";
            phoneColumn.MinimumWidth = 6;
            phoneColumn.Name = "phoneColumn";
            phoneColumn.ReadOnly = true;
            // 
            // supplierDetails
            // 
            supplierDetails.Controls.Add(tableLayoutPanel2);
            supplierDetails.Dock = DockStyle.Fill;
            supplierDetails.Location = new Point(274, 3);
            supplierDetails.Name = "supplierDetails";
            supplierDetails.Size = new Size(538, 393);
            supplierDetails.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(supplierBatchesLbl, 0, 0);
            tableLayoutPanel2.Controls.Add(supplierBatchesGrid, 0, 1);
            tableLayoutPanel2.Controls.Add(batchItemsLayout, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.Size = new Size(538, 393);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // supplierBatchesLbl
            // 
            supplierBatchesLbl.Dock = DockStyle.Fill;
            supplierBatchesLbl.Location = new Point(6, 6);
            supplierBatchesLbl.Margin = new Padding(6);
            supplierBatchesLbl.Name = "supplierBatchesLbl";
            supplierBatchesLbl.Padding = new Padding(4, 0, 0, 0);
            supplierBatchesLbl.Size = new Size(526, 38);
            supplierBatchesLbl.TabIndex = 0;
            supplierBatchesLbl.Text = "Supplier Batches";
            supplierBatchesLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // supplierBatchesGrid
            // 
            supplierBatchesGrid.AllowUserToAddRows = false;
            supplierBatchesGrid.AllowUserToDeleteRows = false;
            supplierBatchesGrid.AllowUserToResizeColumns = false;
            supplierBatchesGrid.AllowUserToResizeRows = false;
            supplierBatchesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            supplierBatchesGrid.Columns.AddRange(new DataGridViewColumn[] { batchIdColumn, purchaseDateColumn, itemsCountColumn, totalReceivedColumn, totalRemainingColumn });
            supplierBatchesGrid.Dock = DockStyle.Fill;
            supplierBatchesGrid.Location = new Point(3, 53);
            supplierBatchesGrid.MultiSelect = false;
            supplierBatchesGrid.Name = "supplierBatchesGrid";
            supplierBatchesGrid.ReadOnly = true;
            supplierBatchesGrid.RowHeadersVisible = false;
            supplierBatchesGrid.RowHeadersWidth = 51;
            supplierBatchesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            supplierBatchesGrid.Size = new Size(532, 183);
            supplierBatchesGrid.TabIndex = 1;
            // 
            // batchIdColumn
            // 
            batchIdColumn.HeaderText = "Id";
            batchIdColumn.MinimumWidth = 6;
            batchIdColumn.Name = "batchIdColumn";
            batchIdColumn.ReadOnly = true;
            batchIdColumn.Width = 55;
            // 
            // purchaseDateColumn
            // 
            purchaseDateColumn.DataPropertyName = "PurchaseDate";
            purchaseDateColumn.HeaderText = "Purchase Date";
            purchaseDateColumn.MinimumWidth = 6;
            purchaseDateColumn.Name = "purchaseDateColumn";
            purchaseDateColumn.ReadOnly = true;
            purchaseDateColumn.Width = 120;
            // 
            // itemsCountColumn
            // 
            itemsCountColumn.DataPropertyName = "ItemsCount";
            itemsCountColumn.HeaderText = "Items Count";
            itemsCountColumn.MinimumWidth = 6;
            itemsCountColumn.Name = "itemsCountColumn";
            itemsCountColumn.ReadOnly = true;
            itemsCountColumn.Width = 110;
            // 
            // totalReceivedColumn
            // 
            totalReceivedColumn.DataPropertyName = "TotalQuantityReceived";
            totalReceivedColumn.HeaderText = "Qty Received";
            totalReceivedColumn.MinimumWidth = 6;
            totalReceivedColumn.Name = "totalReceivedColumn";
            totalReceivedColumn.ReadOnly = true;
            totalReceivedColumn.Width = 115;
            // 
            // totalRemainingColumn
            // 
            totalRemainingColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            totalRemainingColumn.DataPropertyName = "TotalQuantityRemaining";
            totalRemainingColumn.HeaderText = "Remaining";
            totalRemainingColumn.MinimumWidth = 6;
            totalRemainingColumn.Name = "totalRemainingColumn";
            totalRemainingColumn.ReadOnly = true;
            // 
            // batchItemsLayout
            // 
            batchItemsLayout.ColumnCount = 1;
            batchItemsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            batchItemsLayout.Controls.Add(batchItemsLbl, 0, 0);
            batchItemsLayout.Controls.Add(batchItemsGrid, 0, 1);
            batchItemsLayout.Dock = DockStyle.Fill;
            batchItemsLayout.Location = new Point(3, 239);
            batchItemsLayout.Name = "batchItemsLayout";
            batchItemsLayout.RowCount = 2;
            batchItemsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            batchItemsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            batchItemsLayout.Size = new Size(532, 151);
            batchItemsLayout.TabIndex = 2;
            // 
            // batchItemsLbl
            // 
            batchItemsLbl.Dock = DockStyle.Fill;
            batchItemsLbl.Location = new Point(6, 6);
            batchItemsLbl.Margin = new Padding(6);
            batchItemsLbl.Name = "batchItemsLbl";
            batchItemsLbl.Padding = new Padding(4, 0, 0, 0);
            batchItemsLbl.Size = new Size(520, 30);
            batchItemsLbl.TabIndex = 0;
            batchItemsLbl.Text = "Batch Items";
            batchItemsLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // batchItemsGrid
            // 
            batchItemsGrid.AllowUserToAddRows = false;
            batchItemsGrid.AllowUserToDeleteRows = false;
            batchItemsGrid.AllowUserToResizeColumns = false;
            batchItemsGrid.AllowUserToResizeRows = false;
            batchItemsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            batchItemsGrid.Columns.AddRange(new DataGridViewColumn[] { itemProductColumn, itemReceivedColumn, itemRemainingColumn, itemExpiryColumn });
            batchItemsGrid.Dock = DockStyle.Fill;
            batchItemsGrid.Location = new Point(3, 45);
            batchItemsGrid.MultiSelect = false;
            batchItemsGrid.Name = "batchItemsGrid";
            batchItemsGrid.ReadOnly = true;
            batchItemsGrid.RowHeadersVisible = false;
            batchItemsGrid.RowHeadersWidth = 51;
            batchItemsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            batchItemsGrid.Size = new Size(526, 103);
            batchItemsGrid.TabIndex = 1;
            // 
            // itemProductColumn
            // 
            itemProductColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            itemProductColumn.FillWeight = 45F;
            itemProductColumn.HeaderText = "Product";
            itemProductColumn.MinimumWidth = 6;
            itemProductColumn.Name = "itemProductColumn";
            itemProductColumn.ReadOnly = true;
            // 
            // itemReceivedColumn
            // 
            itemReceivedColumn.HeaderText = "Received";
            itemReceivedColumn.MinimumWidth = 6;
            itemReceivedColumn.Name = "itemReceivedColumn";
            itemReceivedColumn.ReadOnly = true;
            itemReceivedColumn.Width = 95;
            // 
            // itemRemainingColumn
            // 
            itemRemainingColumn.HeaderText = "Remaining";
            itemRemainingColumn.MinimumWidth = 6;
            itemRemainingColumn.Name = "itemRemainingColumn";
            itemRemainingColumn.ReadOnly = true;
            itemRemainingColumn.Width = 95;
            // 
            // itemExpiryColumn
            // 
            itemExpiryColumn.HeaderText = "Expiry";
            itemExpiryColumn.MinimumWidth = 6;
            itemExpiryColumn.Name = "itemExpiryColumn";
            itemExpiryColumn.ReadOnly = true;
            itemExpiryColumn.Width = 120;
            // 
            // SuppliersPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "SuppliersPage";
            Size = new Size(821, 455);
            tableLayoutPanel1.ResumeLayout(false);
            toolbarLayout.ResumeLayout(false);
            toolbarLayout.PerformLayout();
            contentLayout.ResumeLayout(false);
            suppliersListLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)suppliersGrid).EndInit();
            supplierDetails.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)supplierBatchesGrid).EndInit();
            batchItemsLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)batchItemsGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel toolbarLayout;
        private TextBox searchTextBox;
        private Button addBtn;
        private Button editBtn;
        private Button deleteBtn;
        private TableLayoutPanel contentLayout;
        private TableLayoutPanel suppliersListLayout;
        private Label suppliersLbl;
        private DataGridView suppliersGrid;
        private DataGridViewTextBoxColumn idColumn;
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewTextBoxColumn phoneColumn;
        private Panel supplierDetails;
        private TableLayoutPanel tableLayoutPanel2;
        private Label supplierBatchesLbl;
        private DataGridView supplierBatchesGrid;
        private DataGridViewTextBoxColumn batchIdColumn;
        private DataGridViewTextBoxColumn purchaseDateColumn;
        private DataGridViewTextBoxColumn itemsCountColumn;
        private DataGridViewTextBoxColumn totalReceivedColumn;
        private DataGridViewTextBoxColumn totalRemainingColumn;
        private TableLayoutPanel batchItemsLayout;
        private Label batchItemsLbl;
        private DataGridView batchItemsGrid;
        private DataGridViewTextBoxColumn itemProductColumn;
        private DataGridViewTextBoxColumn itemReceivedColumn;
        private DataGridViewTextBoxColumn itemRemainingColumn;
        private DataGridViewTextBoxColumn itemExpiryColumn;
    }
}

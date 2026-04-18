namespace UI.Views.Customers
{
    partial class CustomersPage
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
            customersGrid = new DataGridView();
            idColumn = new DataGridViewTextBoxColumn();
            nameColumn = new DataGridViewTextBoxColumn();
            phoneColumn = new DataGridViewTextBoxColumn();
            customerDetails = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            customerInvoicesLbl = new Label();
            customerInvoicesGrid = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            InvoiceDate = new DataGridViewTextBoxColumn();
            TotalAmount = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            toolbarLayout.SuspendLayout();
            contentLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customersGrid).BeginInit();
            customerDetails.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customerInvoicesGrid).BeginInit();
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
            contentLayout.Controls.Add(customersGrid, 0, 0);
            contentLayout.Controls.Add(customerDetails, 1, 0);
            contentLayout.Dock = DockStyle.Fill;
            contentLayout.Location = new Point(3, 53);
            contentLayout.Name = "contentLayout";
            contentLayout.RowCount = 1;
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentLayout.Size = new Size(815, 399);
            contentLayout.TabIndex = 1;
            // 
            // customersGrid
            // 
            customersGrid.AllowUserToAddRows = false;
            customersGrid.AllowUserToDeleteRows = false;
            customersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customersGrid.Columns.AddRange(new DataGridViewColumn[] { idColumn, nameColumn, phoneColumn });
            customersGrid.Dock = DockStyle.Fill;
            customersGrid.Location = new Point(8, 3);
            customersGrid.Margin = new Padding(8, 3, 8, 3);
            customersGrid.MultiSelect = false;
            customersGrid.Name = "customersGrid";
            customersGrid.ReadOnly = true;
            customersGrid.RowHeadersWidth = 51;
            customersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            customersGrid.Size = new Size(255, 393);
            customersGrid.TabIndex = 0;
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
            nameColumn.HeaderText = "Name";
            nameColumn.MinimumWidth = 6;
            nameColumn.Name = "nameColumn";
            nameColumn.ReadOnly = true;
            nameColumn.Width = 75;
            // 
            // phoneColumn
            // 
            phoneColumn.DataPropertyName = "Phone";
            phoneColumn.HeaderText = "Phone";
            phoneColumn.MinimumWidth = 6;
            phoneColumn.Name = "phoneColumn";
            phoneColumn.ReadOnly = true;
            phoneColumn.Width = 75;
            // 
            // customerDetails
            // 
            customerDetails.Controls.Add(tableLayoutPanel2);
            customerDetails.Dock = DockStyle.Fill;
            customerDetails.Location = new Point(274, 3);
            customerDetails.Name = "customerDetails";
            customerDetails.Size = new Size(538, 393);
            customerDetails.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(customerInvoicesLbl, 0, 0);
            tableLayoutPanel2.Controls.Add(customerInvoicesGrid, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(538, 393);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // customerInvoicesLbl
            // 
            customerInvoicesLbl.Dock = DockStyle.Fill;
            customerInvoicesLbl.Location = new Point(6, 6);
            customerInvoicesLbl.Margin = new Padding(6);
            customerInvoicesLbl.Name = "customerInvoicesLbl";
            customerInvoicesLbl.Padding = new Padding(4, 0, 0, 0);
            customerInvoicesLbl.Size = new Size(526, 38);
            customerInvoicesLbl.TabIndex = 0;
            customerInvoicesLbl.Text = "Customer Invoices";
            customerInvoicesLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // customerInvoicesGrid
            // 
            customerInvoicesGrid.AllowUserToAddRows = false;
            customerInvoicesGrid.AllowUserToDeleteRows = false;
            customerInvoicesGrid.AllowUserToResizeColumns = false;
            customerInvoicesGrid.AllowUserToResizeRows = false;
            customerInvoicesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            customerInvoicesGrid.Columns.AddRange(new DataGridViewColumn[] { Id, InvoiceDate, TotalAmount, Status });
            customerInvoicesGrid.Dock = DockStyle.Fill;
            customerInvoicesGrid.Location = new Point(3, 53);
            customerInvoicesGrid.MultiSelect = false;
            customerInvoicesGrid.Name = "customerInvoicesGrid";
            customerInvoicesGrid.ReadOnly = true;
            customerInvoicesGrid.RowHeadersWidth = 51;
            customerInvoicesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            customerInvoicesGrid.Size = new Size(532, 337);
            customerInvoicesGrid.TabIndex = 1;
            // 
            // Id
            // 
            Id.HeaderText = "Invoice Id";
            Id.MinimumWidth = 6;
            Id.Name = "Id";
            Id.ReadOnly = true;
            Id.Width = 125;
            // 
            // InvoiceDate
            // 
            InvoiceDate.HeaderText = "Date";
            InvoiceDate.MinimumWidth = 6;
            InvoiceDate.Name = "InvoiceDate";
            InvoiceDate.ReadOnly = true;
            InvoiceDate.Width = 125;
            // 
            // TotalAmount
            // 
            TotalAmount.HeaderText = "TotalAmount";
            TotalAmount.MinimumWidth = 6;
            TotalAmount.Name = "TotalAmount";
            TotalAmount.ReadOnly = true;
            TotalAmount.Width = 125;
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Width = 125;
            // 
            // CustomersPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "CustomersPage";
            Size = new Size(821, 455);
            tableLayoutPanel1.ResumeLayout(false);
            toolbarLayout.ResumeLayout(false);
            toolbarLayout.PerformLayout();
            contentLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customersGrid).EndInit();
            customerDetails.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customerInvoicesGrid).EndInit();
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
        private DataGridView customersGrid;
        private Panel customerDetails;
        private TableLayoutPanel tableLayoutPanel2;
        private Label customerInvoicesLbl;
        private DataGridView customerInvoicesGrid;
        private DataGridViewTextBoxColumn idColumn;
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewTextBoxColumn phoneColumn;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn InvoiceDate;
        private DataGridViewTextBoxColumn TotalAmount;
        private DataGridViewTextBoxColumn Status;
    }
}

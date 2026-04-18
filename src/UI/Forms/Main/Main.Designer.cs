namespace UI.Forms.Main
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            sidebarPanel = new Panel();
            sidebarLayout = new TableLayoutPanel();
            customersBtn = new Button();
            pharmacyLbl = new Label();
            suppliersBtn = new Button();
            dashboardBtn = new Button();
            inventoryBtn = new Button();
            posBtn = new Button();
            contentPanel = new Panel();
            tableLayoutPanel1.SuspendLayout();
            sidebarPanel.SuspendLayout();
            sidebarLayout.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(sidebarPanel, 0, 0);
            tableLayoutPanel1.Controls.Add(contentPanel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1156, 743);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // sidebarPanel
            // 
            sidebarPanel.Controls.Add(sidebarLayout);
            sidebarPanel.Dock = DockStyle.Fill;
            sidebarPanel.Location = new Point(3, 3);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(214, 737);
            sidebarPanel.TabIndex = 0;
            // 
            // sidebarLayout
            // 
            sidebarLayout.ColumnCount = 1;
            sidebarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            sidebarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            sidebarLayout.Controls.Add(customersBtn, 0, 5);
            sidebarLayout.Controls.Add(pharmacyLbl, 0, 0);
            sidebarLayout.Controls.Add(suppliersBtn, 0, 4);
            sidebarLayout.Controls.Add(dashboardBtn, 0, 1);
            sidebarLayout.Controls.Add(inventoryBtn, 0, 3);
            sidebarLayout.Controls.Add(posBtn, 0, 2);
            sidebarLayout.Dock = DockStyle.Fill;
            sidebarLayout.Location = new Point(0, 0);
            sidebarLayout.Name = "sidebarLayout";
            sidebarLayout.RowCount = 7;
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            sidebarLayout.Size = new Size(214, 737);
            sidebarLayout.TabIndex = 0;
            // 
            // customersBtn
            // 
            customersBtn.FlatAppearance.BorderSize = 0;
            customersBtn.FlatStyle = FlatStyle.Flat;
            customersBtn.ImageAlign = ContentAlignment.MiddleLeft;
            customersBtn.Location = new Point(10, 275);
            customersBtn.Margin = new Padding(10, 5, 10, 5);
            customersBtn.Name = "customersBtn";
            customersBtn.Size = new Size(94, 29);
            customersBtn.TabIndex = 5;
            customersBtn.Text = "Customers";
            customersBtn.UseVisualStyleBackColor = true;
            // 
            // pharmacyLbl
            // 
            pharmacyLbl.AutoSize = true;
            pharmacyLbl.Dock = DockStyle.Fill;
            pharmacyLbl.ImageAlign = ContentAlignment.MiddleLeft;
            pharmacyLbl.Location = new Point(10, 10);
            pharmacyLbl.Margin = new Padding(10);
            pharmacyLbl.Name = "pharmacyLbl";
            pharmacyLbl.Size = new Size(194, 50);
            pharmacyLbl.TabIndex = 0;
            pharmacyLbl.Text = "Pharmacy POS";
            pharmacyLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // suppliersBtn
            // 
            suppliersBtn.FlatAppearance.BorderSize = 0;
            suppliersBtn.FlatStyle = FlatStyle.Flat;
            suppliersBtn.ImageAlign = ContentAlignment.MiddleLeft;
            suppliersBtn.Location = new Point(10, 225);
            suppliersBtn.Margin = new Padding(10, 5, 10, 5);
            suppliersBtn.Name = "suppliersBtn";
            suppliersBtn.Size = new Size(94, 29);
            suppliersBtn.TabIndex = 4;
            suppliersBtn.Text = "Suppliers";
            suppliersBtn.UseVisualStyleBackColor = true;
            // 
            // dashboardBtn
            // 
            dashboardBtn.FlatAppearance.BorderSize = 0;
            dashboardBtn.FlatStyle = FlatStyle.Flat;
            dashboardBtn.ImageAlign = ContentAlignment.MiddleLeft;
            dashboardBtn.Location = new Point(10, 75);
            dashboardBtn.Margin = new Padding(10, 5, 10, 5);
            dashboardBtn.Name = "dashboardBtn";
            dashboardBtn.Size = new Size(94, 29);
            dashboardBtn.TabIndex = 1;
            dashboardBtn.Text = "Dashboard";
            dashboardBtn.UseVisualStyleBackColor = true;
            // 
            // inventoryBtn
            // 
            inventoryBtn.FlatAppearance.BorderSize = 0;
            inventoryBtn.FlatStyle = FlatStyle.Flat;
            inventoryBtn.ImageAlign = ContentAlignment.MiddleLeft;
            inventoryBtn.Location = new Point(10, 175);
            inventoryBtn.Margin = new Padding(10, 5, 10, 5);
            inventoryBtn.Name = "inventoryBtn";
            inventoryBtn.Size = new Size(94, 29);
            inventoryBtn.TabIndex = 3;
            inventoryBtn.Text = "Inventory";
            inventoryBtn.UseVisualStyleBackColor = true;
            // 
            // posBtn
            // 
            posBtn.FlatAppearance.BorderSize = 0;
            posBtn.FlatStyle = FlatStyle.Flat;
            posBtn.ImageAlign = ContentAlignment.MiddleLeft;
            posBtn.Location = new Point(10, 125);
            posBtn.Margin = new Padding(10, 5, 10, 5);
            posBtn.Name = "posBtn";
            posBtn.Size = new Size(94, 29);
            posBtn.TabIndex = 2;
            posBtn.Text = "POS";
            posBtn.UseVisualStyleBackColor = true;
            // 
            // contentPanel
            // 
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(223, 3);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(930, 737);
            contentPanel.TabIndex = 1;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1156, 743);
            Controls.Add(tableLayoutPanel1);
            Name = "Main";
            Text = "Main";
            tableLayoutPanel1.ResumeLayout(false);
            sidebarPanel.ResumeLayout(false);
            sidebarLayout.ResumeLayout(false);
            sidebarLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel sidebarPanel;
        private Panel contentPanel;
        private Label pharmacyLbl;
        private Button dashboardBtn;
        private Button customersBtn;
        private Button suppliersBtn;
        private Button inventoryBtn;
        private Button posBtn;
        private TableLayoutPanel sidebarLayout;
    }
}
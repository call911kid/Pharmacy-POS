namespace UI.Forms.ProductDialog
{
    partial class ProductDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            titleLabel = new Label();
            nameLayout = new TableLayoutPanel();
            nameLabel = new Label();
            productNameTextBox = new TextBox();
            barcodeLayout = new TableLayoutPanel();
            barcodeLabel = new Label();
            productBarcodeTextBox = new TextBox();
            scanStatusLabel = new Label();
            actionsPanel = new FlowLayoutPanel();
            saveBtn = new Button();
            clearBtn = new Button();
            cancelBtn = new Button();
            rootLayout.SuspendLayout();
            nameLayout.SuspendLayout();
            barcodeLayout.SuspendLayout();
            actionsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(titleLabel, 0, 0);
            rootLayout.Controls.Add(nameLayout, 0, 1);
            rootLayout.Controls.Add(barcodeLayout, 0, 2);
            rootLayout.Controls.Add(scanStatusLabel, 0, 3);
            rootLayout.Controls.Add(actionsPanel, 0, 4);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(14);
            rootLayout.RowCount = 5;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            rootLayout.Size = new Size(460, 290);
            rootLayout.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.Location = new Point(17, 14);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(426, 42);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Add Product";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nameLayout
            // 
            nameLayout.ColumnCount = 1;
            nameLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            nameLayout.Controls.Add(nameLabel, 0, 0);
            nameLayout.Controls.Add(productNameTextBox, 0, 1);
            nameLayout.Dock = DockStyle.Fill;
            nameLayout.Location = new Point(17, 59);
            nameLayout.Name = "nameLayout";
            nameLayout.RowCount = 2;
            nameLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            nameLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            nameLayout.Size = new Size(426, 72);
            nameLayout.TabIndex = 1;
            // 
            // nameLabel
            // 
            nameLabel.Dock = DockStyle.Fill;
            nameLabel.Location = new Point(3, 0);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(420, 26);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Product Name";
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // productNameTextBox
            // 
            productNameTextBox.Dock = DockStyle.Fill;
            productNameTextBox.Location = new Point(3, 29);
            productNameTextBox.Name = "productNameTextBox";
            productNameTextBox.Size = new Size(420, 27);
            productNameTextBox.TabIndex = 1;
            // 
            // barcodeLayout
            // 
            barcodeLayout.ColumnCount = 1;
            barcodeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            barcodeLayout.Controls.Add(barcodeLabel, 0, 0);
            barcodeLayout.Controls.Add(productBarcodeTextBox, 0, 1);
            barcodeLayout.Dock = DockStyle.Fill;
            barcodeLayout.Location = new Point(17, 137);
            barcodeLayout.Name = "barcodeLayout";
            barcodeLayout.RowCount = 2;
            barcodeLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            barcodeLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            barcodeLayout.Size = new Size(426, 72);
            barcodeLayout.TabIndex = 2;
            // 
            // barcodeLabel
            // 
            barcodeLabel.Dock = DockStyle.Fill;
            barcodeLabel.Location = new Point(3, 0);
            barcodeLabel.Name = "barcodeLabel";
            barcodeLabel.Size = new Size(420, 26);
            barcodeLabel.TabIndex = 0;
            barcodeLabel.Text = "Barcode";
            barcodeLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // productBarcodeTextBox
            // 
            productBarcodeTextBox.Dock = DockStyle.Fill;
            productBarcodeTextBox.Location = new Point(3, 29);
            productBarcodeTextBox.Name = "productBarcodeTextBox";
            productBarcodeTextBox.Size = new Size(420, 27);
            productBarcodeTextBox.TabIndex = 1;
            // 
            // scanStatusLabel
            // 
            scanStatusLabel.Dock = DockStyle.Fill;
            scanStatusLabel.Location = new Point(17, 215);
            scanStatusLabel.Name = "scanStatusLabel";
            scanStatusLabel.Size = new Size(426, 54);
            scanStatusLabel.TabIndex = 3;
            scanStatusLabel.Text = "Type the product details manually or scan a barcode while this window is open.";
            scanStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // actionsPanel
            // 
            actionsPanel.Controls.Add(saveBtn);
            actionsPanel.Controls.Add(clearBtn);
            actionsPanel.Controls.Add(cancelBtn);
            actionsPanel.Dock = DockStyle.Fill;
            actionsPanel.FlowDirection = FlowDirection.RightToLeft;
            actionsPanel.Location = new Point(17, 269);
            actionsPanel.Name = "actionsPanel";
            actionsPanel.Padding = new Padding(0, 8, 0, 0);
            actionsPanel.Size = new Size(426, 52);
            actionsPanel.TabIndex = 4;
            actionsPanel.WrapContents = false;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(348, 11);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(75, 34);
            saveBtn.TabIndex = 0;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            clearBtn.Location = new Point(267, 11);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(75, 34);
            clearBtn.TabIndex = 1;
            clearBtn.Text = "Clear";
            clearBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(186, 11);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(75, 34);
            cancelBtn.TabIndex = 2;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // ProductDialog
            // 
            AcceptButton = saveBtn;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelBtn;
            ClientSize = new Size(460, 338);
            Controls.Add(rootLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProductDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Product";
            rootLayout.ResumeLayout(false);
            nameLayout.ResumeLayout(false);
            nameLayout.PerformLayout();
            barcodeLayout.ResumeLayout(false);
            barcodeLayout.PerformLayout();
            actionsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Label titleLabel;
        private TableLayoutPanel nameLayout;
        private Label nameLabel;
        private TextBox productNameTextBox;
        private TableLayoutPanel barcodeLayout;
        private Label barcodeLabel;
        private TextBox productBarcodeTextBox;
        private Label scanStatusLabel;
        private FlowLayoutPanel actionsPanel;
        private Button saveBtn;
        private Button clearBtn;
        private Button cancelBtn;
    }
}

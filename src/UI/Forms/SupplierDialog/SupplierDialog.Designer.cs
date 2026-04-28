namespace UI.Forms.SupplierDialog
{
    partial class SupplierDialog
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
            titleLbl = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            nameTextBox = new TextBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            label2 = new Label();
            phoneTextBox = new TextBox();
            actionsPanel = new FlowLayoutPanel();
            cancelBtn = new Button();
            clearBtn = new Button();
            saveBtn = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            actionsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(titleLbl, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel1.Controls.Add(actionsPanel, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(473, 235);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // titleLbl
            // 
            titleLbl.AutoSize = true;
            titleLbl.Dock = DockStyle.Fill;
            titleLbl.Location = new Point(12, 8);
            titleLbl.Margin = new Padding(12, 8, 12, 4);
            titleLbl.Name = "titleLbl";
            titleLbl.Size = new Size(449, 28);
            titleLbl.TabIndex = 0;
            titleLbl.Text = "Supplier";
            titleLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(nameTextBox, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 43);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(467, 64);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(461, 32);
            label1.TabIndex = 0;
            label1.Text = "Name";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // nameTextBox
            // 
            nameTextBox.Dock = DockStyle.Fill;
            nameTextBox.Location = new Point(12, 36);
            nameTextBox.Margin = new Padding(12, 4, 12, 8);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(443, 27);
            nameTextBox.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label2, 0, 0);
            tableLayoutPanel3.Controls.Add(phoneTextBox, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 113);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(467, 64);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(461, 32);
            label2.TabIndex = 0;
            label2.Text = "Phone";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // phoneTextBox
            // 
            phoneTextBox.Dock = DockStyle.Fill;
            phoneTextBox.Location = new Point(12, 36);
            phoneTextBox.Margin = new Padding(12, 4, 12, 8);
            phoneTextBox.MaxLength = 11;
            phoneTextBox.Name = "phoneTextBox";
            phoneTextBox.Size = new Size(443, 27);
            phoneTextBox.TabIndex = 1;
            // 
            // actionsPanel
            // 
            actionsPanel.Controls.Add(cancelBtn);
            actionsPanel.Controls.Add(clearBtn);
            actionsPanel.Controls.Add(saveBtn);
            actionsPanel.Dock = DockStyle.Fill;
            actionsPanel.FlowDirection = FlowDirection.RightToLeft;
            actionsPanel.Location = new Point(3, 183);
            actionsPanel.Name = "actionsPanel";
            actionsPanel.Padding = new Padding(12, 8, 12, 12);
            actionsPanel.Size = new Size(467, 49);
            actionsPanel.TabIndex = 3;
            actionsPanel.WrapContents = false;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(346, 11);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(94, 29);
            cancelBtn.TabIndex = 0;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            clearBtn.Location = new Point(246, 11);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(94, 29);
            clearBtn.TabIndex = 1;
            clearBtn.Text = "Clear";
            clearBtn.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(146, 11);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 2;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // SupplierDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(473, 235);
            Controls.Add(tableLayoutPanel1);
            Name = "SupplierDialog";
            Text = "SupplierDialog";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            actionsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label titleLbl;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private TextBox nameTextBox;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label2;
        private TextBox phoneTextBox;
        private FlowLayoutPanel actionsPanel;
        private Button cancelBtn;
        private Button clearBtn;
        private Button saveBtn;
    }
}

namespace UI.Forms
{
    partial class ScannerConnectionForm
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
            picQRCode = new PictureBox();
            lnkScannerUrl = new LinkLabel();
            lblInstructions = new Label();
            ((System.ComponentModel.ISupportInitialize)picQRCode).BeginInit();
            SuspendLayout();
            // 
            // picQRCode
            // 
            picQRCode.Location = new Point(351, 147);
            picQRCode.Name = "picQRCode";
            picQRCode.Size = new Size(125, 62);
            picQRCode.TabIndex = 0;
            picQRCode.TabStop = false;
            // 
            // lnkScannerUrl
            // 
            lnkScannerUrl.AutoSize = true;
            lnkScannerUrl.Location = new Point(378, 274);
            lnkScannerUrl.Name = "lnkScannerUrl";
            lnkScannerUrl.Size = new Size(99, 20);
            lnkScannerUrl.TabIndex = 1;
            lnkScannerUrl.TabStop = true;
            lnkScannerUrl.Text = "lnkScannerUrl";
            lnkScannerUrl.LinkClicked += lnkScannerUrl_LinkClicked;
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.Location = new Point(394, 83);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(50, 20);
            lblInstructions.TabIndex = 2;
            lblInstructions.Text = "label1";
            // 
            // ScannerConnectionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblInstructions);
            Controls.Add(lnkScannerUrl);
            Controls.Add(picQRCode);
            Name = "ScannerConnectionForm";
            Text = "ScannerConnectionForm";
            Load += ScannerConnectionForm_Load;
            ((System.ComponentModel.ISupportInitialize)picQRCode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picQRCode;
        private LinkLabel lnkScannerUrl;
        private Label lblInstructions;
    }
}
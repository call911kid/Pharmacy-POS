namespace UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblCurrentBarcode = new Label();
            lstBarcodeLogs = new ListBox();
            picQRCode = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)picQRCode).BeginInit();
            SuspendLayout();
            // 
            // lblCurrentBarcode
            // 
            lblCurrentBarcode.AutoSize = true;
            lblCurrentBarcode.Location = new Point(446, 332);
            lblCurrentBarcode.Name = "lblCurrentBarcode";
            lblCurrentBarcode.Size = new Size(140, 20);
            lblCurrentBarcode.TabIndex = 0;
            lblCurrentBarcode.Text = "waiting for barcode";
            // 
            // lstBarcodeLogs
            // 
            lstBarcodeLogs.FormattingEnabled = true;
            lstBarcodeLogs.Location = new Point(345, 37);
            lstBarcodeLogs.Name = "lstBarcodeLogs";
            lstBarcodeLogs.Size = new Size(364, 264);
            lstBarcodeLogs.TabIndex = 1;
            // 
            // picQRCode
            // 
            picQRCode.Location = new Point(51, 52);
            picQRCode.Name = "picQRCode";
            picQRCode.Size = new Size(237, 173);
            picQRCode.TabIndex = 2;
            picQRCode.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 237);
            label1.Name = "label1";
            label1.Size = new Size(65, 20);
            label1.TabIndex = 3;
            label1.Text = "Scan Me";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(picQRCode);
            Controls.Add(lstBarcodeLogs);
            Controls.Add(lblCurrentBarcode);
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)picQRCode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCurrentBarcode;
        private ListBox lstBarcodeLogs;
        private PictureBox picQRCode;
        private Label label1;
    }
}

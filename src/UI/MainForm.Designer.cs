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
            scannerBtn = new Button();
            SuspendLayout();
            // 
            // scannerBtn
            // 
            scannerBtn.Location = new Point(12, 12);
            scannerBtn.Name = "scannerBtn";
            scannerBtn.Size = new Size(149, 29);
            scannerBtn.TabIndex = 0;
            scannerBtn.Text = "Open Scanner QR";
            scannerBtn.UseVisualStyleBackColor = true;
            scannerBtn.Click += scannerBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(scannerBtn);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button scannerBtn;
    }
}

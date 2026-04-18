using Microsoft.Web.WebView2.WinForms;

namespace UI.Forms.InvoicePreviewForm
{
    partial class InvoicePreviewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            toolbarLayout = new TableLayoutPanel();
            titleLbl = new Label();
            openBtn = new Button();
            printBtn = new Button();
            previewBrowser = new WebView2();
            rootLayout.SuspendLayout();
            toolbarLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewBrowser).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(toolbarLayout, 0, 0);
            rootLayout.Controls.Add(previewBrowser, 0, 1);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.RowCount = 2;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Size = new Size(980, 720);
            rootLayout.TabIndex = 0;
            // 
            // toolbarLayout
            // 
            toolbarLayout.ColumnCount = 3;
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            toolbarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            toolbarLayout.Controls.Add(titleLbl, 0, 0);
            toolbarLayout.Controls.Add(openBtn, 1, 0);
            toolbarLayout.Controls.Add(printBtn, 2, 0);
            toolbarLayout.Dock = DockStyle.Fill;
            toolbarLayout.Location = new Point(3, 3);
            toolbarLayout.Name = "toolbarLayout";
            toolbarLayout.RowCount = 1;
            toolbarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            toolbarLayout.Size = new Size(974, 42);
            toolbarLayout.TabIndex = 0;
            // 
            // titleLbl
            // 
            titleLbl.Dock = DockStyle.Fill;
            titleLbl.Location = new Point(8, 0);
            titleLbl.Margin = new Padding(8, 0, 3, 0);
            titleLbl.Name = "titleLbl";
            titleLbl.Size = new Size(753, 42);
            titleLbl.TabIndex = 0;
            titleLbl.Text = "Invoice Preview";
            titleLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // openBtn
            // 
            openBtn.Dock = DockStyle.Fill;
            openBtn.Location = new Point(770, 6);
            openBtn.Margin = new Padding(6);
            openBtn.Name = "openBtn";
            openBtn.Size = new Size(98, 30);
            openBtn.TabIndex = 1;
            openBtn.Text = "Open";
            openBtn.UseVisualStyleBackColor = true;
            openBtn.Click += (_, _) => OpenExternally();
            // 
            // printBtn
            // 
            printBtn.Dock = DockStyle.Fill;
            printBtn.Location = new Point(880, 6);
            printBtn.Margin = new Padding(6);
            printBtn.Name = "printBtn";
            printBtn.Size = new Size(88, 30);
            printBtn.TabIndex = 2;
            printBtn.Text = "Print";
            printBtn.UseVisualStyleBackColor = true;
            printBtn.Click += (_, _) => PrintDocument();
            // 
            // previewBrowser
            // 
            previewBrowser.AllowExternalDrop = true;
            previewBrowser.CreationProperties = null;
            previewBrowser.DefaultBackgroundColor = Color.White;
            previewBrowser.Dock = DockStyle.Fill;
            previewBrowser.Location = new Point(3, 51);
            previewBrowser.Name = "previewBrowser";
            previewBrowser.Size = new Size(974, 666);
            previewBrowser.TabIndex = 1;
            previewBrowser.ZoomFactor = 1D;
            // 
            // InvoicePreviewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 720);
            Controls.Add(rootLayout);
            Name = "InvoicePreviewForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Invoice Preview";
            rootLayout.ResumeLayout(false);
            toolbarLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewBrowser).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel rootLayout;
        private TableLayoutPanel toolbarLayout;
        private Label titleLbl;
        private Button openBtn;
        private Button printBtn;
        private WebView2 previewBrowser;
    }
}

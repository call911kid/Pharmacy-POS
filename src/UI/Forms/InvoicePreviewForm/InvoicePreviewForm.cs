namespace UI.Forms.InvoicePreviewForm
{
    public partial class InvoicePreviewForm : Form
    {
        private readonly string _pdfPath;

        public InvoicePreviewForm(string pdfPath)
        {
            _pdfPath = pdfPath;
            InitializeComponent();
            Shown += async (_, _) => await LoadPdfAsync();
        }

        private async Task LoadPdfAsync()
        {
            if (!File.Exists(_pdfPath))
            {
                MessageBox.Show(
                    "The invoice PDF could not be found.",
                    "Invoice Preview",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Close();
                return;
            }

            await previewBrowser.EnsureCoreWebView2Async();
            previewBrowser.Source = new Uri(_pdfPath);
        }

        private void OpenExternally()
        {
            if (!File.Exists(_pdfPath))
            {
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = _pdfPath,
                UseShellExecute = true
            });
        }

        private void PrintDocument()
        {
            previewBrowser.CoreWebView2?.ShowPrintUI();
        }
    }
}

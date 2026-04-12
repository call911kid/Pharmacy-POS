using System.Net;
using System.Net.Sockets;
using QRCoder;
using UI.Events;
using UI.Forms;
using UI.Utils;

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly ScannerEventBus _eventBus;

        public MainForm(ScannerEventBus eventBus)
        {

            InitializeComponent();

            _eventBus = eventBus;
            _eventBus.BarcodeScanned += OnBarcodeScanned;


        }



        private void OnBarcodeScanned(object sender, string barcode)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateUI(barcode)));
            }
            else
            {
                UpdateUI(barcode);
            }
        }

        private void UpdateUI(string barcode)
        {
            //lblCurrentBarcode.Text = barcode;
            //string logEntry = $"{DateTime.Now:HH:mm:ss} - Scanned: {barcode}";
            //lstBarcodeLogs.Items.Insert(0, logEntry);
        }

        private void scannerBtn_Click(object sender, EventArgs e)
        {
            var scannerForm = new ScannerConnectionForm();
            scannerForm.Show();
        }
    }
}
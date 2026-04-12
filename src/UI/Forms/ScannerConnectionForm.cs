using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Utils;

namespace UI.Forms
{
    public partial class ScannerConnectionForm : Form
    {
        public ScannerConnectionForm()
        {
            InitializeComponent();

            
            lblInstructions.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            lblInstructions.Text = "Scan to Connect Mobile Device";

            lnkScannerUrl.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            lnkScannerUrl.LinkColor = Color.FromArgb(37, 99, 235); 
            lnkScannerUrl.ActiveLinkColor = Color.FromArgb(29, 78, 216); 
            lnkScannerUrl.VisitedLinkColor = Color.FromArgb(37, 99, 235);
            lnkScannerUrl.LinkBehavior = LinkBehavior.HoverUnderline;

            picQRCode.SizeMode = PictureBoxSizeMode.AutoSize;

            this.Text = "Mobile Scanner Connection";


        }

        private void ScannerConnectionForm_Load(object sender, EventArgs e)
        {
            SetupConnection();
            CenterContent();
        }

        private void SetupConnection()
        {
            try
            {
                
                string url = ScannerConnectionProvider.GetMobileScannerUrl();
                lnkScannerUrl.Text = url;
                
                picQRCode.Image = ScannerConnectionProvider.GenerateLinkQRCode(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CenterContent()
        {
            if (picQRCode.Image == null) return;

            
            picQRCode.Left = (this.ClientSize.Width - picQRCode.Width) / 2;
            picQRCode.Top = (this.ClientSize.Height - picQRCode.Height) / 2 - 30;

            lnkScannerUrl.Left = (this.ClientSize.Width - lnkScannerUrl.Width) / 2;
            lnkScannerUrl.Top = picQRCode.Bottom + 20;

            lblInstructions.Left = (this.ClientSize.Width - lblInstructions.Width) / 2;
            lblInstructions.Top = picQRCode.Top - 50;
        }

        private void lnkScannerUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = lnkScannerUrl.Text,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open browser: {ex.Message}");
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Events
{
    public class ScannerEventBus
    {
        public event EventHandler<string> BarcodeScanned;

        public void PublishScan(string barcode)
        {
            BarcodeScanned?.Invoke(this, barcode);
        }
    }
}

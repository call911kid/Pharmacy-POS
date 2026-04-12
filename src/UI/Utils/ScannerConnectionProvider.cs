using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Utils
{
    public static class ScannerConnectionProvider
    {
        private const int Port = 8443;

        public static string GetMobileScannerUrl()
        {
            return $"https://{GetActiveLocalIPAddress()}:{Port}";
        }

        public static Bitmap GenerateLinkQRCode(string url)
        {
            using var qrGenerator = new QRCoder.QRCodeGenerator();
            using var data = qrGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCoder.QRCode(data);
            return qrCode.GetGraphic(4);
        }

        private static string GetActiveLocalIPAddress()
        {
            using var socket = new System.Net.Sockets.Socket(
                System.Net.Sockets.AddressFamily.InterNetwork,
                System.Net.Sockets.SocketType.Dgram, 0);
            try
            {
                socket.Connect("8.8.8.8", 65530);
                return (socket.LocalEndPoint as System.Net.IPEndPoint).Address.ToString();
            }
            catch
            { 
                return "127.0.0.1"; 
            }
        }
    }
}

using System.Drawing;
using QRCoder;

namespace PrescriptionScanner.Server
{
    public static class QRHelper
    {
        public static Bitmap Generate(string url, int pixelsPerModule = 6)
        {
            using var generator = new QRCodeGenerator();
            using var data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(data);
            using var bmp = qrCode.GetGraphic(pixelsPerModule);
            return (Bitmap)bmp.Clone();
        }
    }
}

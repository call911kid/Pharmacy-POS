using System;

namespace BLL.Utils
{
    public static class BarcodeGenerator
    {
        private static readonly Random _random = new Random();

        
        public static string GenerateInvoiceBarcode()
        {
            
            string prefix = "INV";
            
            string datePart = DateTime.UtcNow.ToString("yyMMdd");

            int randomValue;
            lock (_random)
            {
                randomValue = _random.Next(100, 1000); 
            }
            
            string randomPart = randomValue.ToString();

            return $"{prefix}{datePart}{randomPart}";
        }
    }
}

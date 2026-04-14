namespace BLL.DTOs
{
    public class ProductDto
    {
            public int Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        // إجمالي الكمية المتاحة الشغاله (مش منتهية)
        public int TotalStock { get; set; }

        // سعر أقرب دُفعة هتنتهي (FIFO - الأقرب للانتهاء يتباع الأول)
        public decimal CurrentPrice { get; set; }

        // أقرب تاريخ انتهاء صلاحية   
        public DateTime? NearestExpiryDate { get; set; }
    }
}

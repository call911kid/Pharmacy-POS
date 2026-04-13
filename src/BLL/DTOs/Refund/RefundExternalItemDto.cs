namespace BLL.DTOs.Refund
{
    public class RefundExternalItemDto
    {
        public int BatchItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

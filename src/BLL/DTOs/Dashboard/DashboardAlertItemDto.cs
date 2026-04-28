namespace BLL.DTOs.Dashboard
{
    public class DashboardAlertItemDto
    {
        public int BatchItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int QuantityRemaining { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

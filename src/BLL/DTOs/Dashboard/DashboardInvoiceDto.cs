namespace BLL.DTOs.Dashboard
{
    public class DashboardInvoiceDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } 
    }
}

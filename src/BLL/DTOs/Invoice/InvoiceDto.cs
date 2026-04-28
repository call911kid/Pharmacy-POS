using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.InvoiceItem;
using Common.Enums;
namespace BLL.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<InvoiceItemDto> InvoiceItems { get; set; }
        public InvoiceDto()
        {
            InvoiceItems = new List<InvoiceItemDto>();
        }
    }
}

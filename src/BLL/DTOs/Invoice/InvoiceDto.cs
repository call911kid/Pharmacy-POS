using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.InvoiceItem;
namespace BLL.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceItemDto> InvoiceItems { get; set; }
        public InvoiceDto()
        {
            InvoiceItems = new List<InvoiceItemDto>();
        }
    }
}

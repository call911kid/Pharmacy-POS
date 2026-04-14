using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.InvoiceItem;
using Common.Enums;
namespace BLL.DTOs.Invoice
{
    public class CreateInvoiceDto
    {
        public int CustomerId { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<CreateInvoiceItemDto> InvoiceItems { get; set; }
        public CreateInvoiceDto()
        {
            InvoiceItems = new List<CreateInvoiceItemDto>();
        }
    }
}

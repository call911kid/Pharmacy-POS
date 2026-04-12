using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.InvoiceItem
{
    public class CreateInvoiceItemDto
    {
        
        public int ProductId { get; set; }
        public int BatchItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        public CreateInvoiceItemDto()
        {
        }
    }
}

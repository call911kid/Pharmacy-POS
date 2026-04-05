using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int BatchItemId { get; set; }
        public BatchItem BatchItem { get; set; }
        public int Quantity { get; set; } 
        public decimal OriginalPrice { get; set; } 
        public decimal DiscountedPrice { get; set; } 
    }
}

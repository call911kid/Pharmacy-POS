using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; } 
        public decimal TotalDiscount { get; set; } 
        public decimal NetAmount { get; set; } 
        public ICollection<InvoiceItem> InvoiceItems { get; set; }



    }
}

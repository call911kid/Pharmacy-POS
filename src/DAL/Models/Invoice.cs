using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
namespace DAL.Models
{
    public class Invoice
    {
        
        public int Id { get; set; }
        public string Barcode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal TotalAmount { get; set; } 
        public decimal TotalDiscount { get; set; } 
        public decimal NetAmount { get; set; }
        public int ReturnedQuantity { get; set; }
        public InvoiceStatus Status { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }


        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BatchItem
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityRemaining { get; set; }
        public DateTime ExpirationDate { get; set; } 
        public decimal CostPrice { get; set; } 
        public decimal MandatorySellingPrice { get; set; }
    }
}

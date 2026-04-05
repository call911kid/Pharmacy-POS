using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    internal class Batch
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<BatchItem> BatchItems { get; set; }

        
    }
}

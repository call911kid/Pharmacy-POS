using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<BatchItem> BatchItems { get; set; }

        public Batch()
        {
            BatchItems = new HashSet<BatchItem>();
        }
    }
}

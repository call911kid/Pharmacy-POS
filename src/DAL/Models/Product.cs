using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BatchItem> BatchItems { get; set; }

        public Product()
        {
            BatchItems = new HashSet<BatchItem>();
        }
    }
    
}

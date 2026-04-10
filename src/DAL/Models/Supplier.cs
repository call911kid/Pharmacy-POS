using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Supplier
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public Supplier()
        {
            Batches = new HashSet<Batch>();
        }

    }
}

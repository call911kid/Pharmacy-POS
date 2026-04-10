using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Phone { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }


        public Customer()
        {
            Invoices= new HashSet<Invoice>();
        }
    }
}

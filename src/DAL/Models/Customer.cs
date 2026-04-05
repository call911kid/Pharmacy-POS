using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Phone { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}

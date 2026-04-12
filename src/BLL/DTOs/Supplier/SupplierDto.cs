using BLL.DTOs.Batch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Supplier
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ICollection<BatchDto>? Batches { get; set; }
       
    }
}

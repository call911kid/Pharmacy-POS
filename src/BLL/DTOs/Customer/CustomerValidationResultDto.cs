using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Customer
{
    public class CustomerValidationResultDto
    {
        public int CustomerId { get; set; }
        public bool IsValid { get; set; }
        public string? Reason { get; set; }
    }
}

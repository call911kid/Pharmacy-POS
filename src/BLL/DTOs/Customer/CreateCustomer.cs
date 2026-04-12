using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Customer
{
    public class CreateCustomerByPhoneDto
    {
        public string? Phone { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

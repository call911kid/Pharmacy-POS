using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class InvoiceItemNotFoundException : Exception
    {
        public InvoiceItemNotFoundException(string message) : base(message)
        {
        }
    }
}

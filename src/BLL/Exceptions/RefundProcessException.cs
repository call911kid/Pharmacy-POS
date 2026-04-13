using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class RefundProcessException : Exception
    {
        public RefundProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RefundProcessException(string message) : base(message)
        {
        }

    }
}

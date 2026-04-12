using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    internal class SupplierHasActiveBatchesException : Exception
    {
        public SupplierHasActiveBatchesException(int id)
            : base($"Supplier {id} cannot be deleted because they have active batches.") { }
    }
}

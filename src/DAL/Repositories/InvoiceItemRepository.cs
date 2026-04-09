using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    internal class InvoiceItemRepository:GenericRepository<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(PharmacyDbContext context): base(context)
        {
            
        }
    }
}

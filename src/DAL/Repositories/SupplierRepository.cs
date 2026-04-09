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
    internal class SupplierRepository:GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(PharmacyDbContext context) : base(context)
        {

        }
    }
}

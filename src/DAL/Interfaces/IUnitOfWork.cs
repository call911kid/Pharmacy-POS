using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ISupplierRepository Suppliers { get; }
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IBatchRepository Batches { get; }
        IInvoiceRepository Invoices { get; }
        IBatchItemRepository BatchItems { get; }
        IInvoiceItemRepository InvoiceItems { get; }
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}

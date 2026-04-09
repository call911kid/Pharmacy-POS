using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly PharmacyDbContext _context;

        public ISupplierRepository Suppliers { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public IBatchRepository Batches { get; private set; }
        public IInvoiceRepository Invoices { get; private set; }
        public IBatchItemRepository BatchItems { get; private set; }
        public IInvoiceItemRepository InvoiceItems { get; private set; }


        public UnitOfWork(PharmacyDbContext context,
            ISupplierRepository supplierRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IBatchRepository batchRepository,
            IInvoiceRepository invoiceRepository,
            IBatchItemRepository batchItemRepository,
            IInvoiceItemRepository invoiceItemRepository)
        {
            _context = context;
            Suppliers = supplierRepository;
            Customers = customerRepository;
            Products = productRepository;
            Batches = batchRepository;
            Invoices = invoiceRepository;
            BatchItems = batchItemRepository;
            InvoiceItems = invoiceItemRepository;
        }

        public async Task<int> SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync() =>
            await _context.Database.BeginTransactionAsync();

        public void Dispose() => _context.Dispose();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    internal class BatchItemRepository:GenericRepository<BatchItem>, IBatchItemRepository
    {
        public BatchItemRepository(PharmacyDbContext context) : base(context)
        {
           
        }

        public async Task<BatchItem> GetActiveBatchItemByBarcodeAsync(string barcode)
        {
            return await _context.Set<BatchItem>()
                .Include(b => b.Product)
                .Where(b => b.Product.Barcode == barcode && b.QuantityRemaining > 0 && b.ExpirationDate > DateTime.Now)
                .OrderBy(b => b.ExpirationDate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BatchItem>> GetLowStockItemsAsync(int threshold, int take)
        {
            return await _context.Set<BatchItem>()
                .Include(item => item.Product)
                .Where(item => item.QuantityRemaining > 0 && item.QuantityRemaining <= threshold)
                .OrderBy(item => item.QuantityRemaining)
                .ThenBy(item => item.ExpirationDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<BatchItem>> GetExpiringItemsAsync(DateTime untilDate, int take)
        {
            var today = DateTime.Today;

            return await _context.Set<BatchItem>()
                .Include(item => item.Product)
                .Where(item => item.QuantityRemaining > 0
                    && item.ExpirationDate >= today
                    && item.ExpirationDate <= untilDate)
                .OrderBy(item => item.ExpirationDate)
                .Take(take)
                .ToListAsync();
        }
    }
}

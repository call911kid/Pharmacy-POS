using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
namespace DAL.Interfaces
{
    public interface IBatchItemRepository : IGenericRepository<BatchItem>
    {
        Task<BatchItem> GetActiveBatchItemByBarcodeAsync(string barcode);
        Task<IEnumerable<BatchItem>> GetLowStockItemsAsync(int threshold, int take);
        Task<IEnumerable<BatchItem>> GetExpiringItemsAsync(DateTime untilDate, int take);
    }
}

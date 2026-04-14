using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Batch;

namespace BLL.Interfaces
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchSummaryDto>> GetBatchSummariesAsync(int pageNumber, int pageSize);
        Task<BatchDto> GetBatchByIdAsync(int batchId);
        Task AddBatchAsync(CreateBatchDto createBatchDto);
        Task UpdateBatchAsync(int batchId, CreateBatchDto createBatchDto);
        Task DeleteBatchAsync(int batchId);
        Task<IEnumerable<BatchSummaryDto>> GetBatchesBySupplierAsync(int supplierId);
        Task<BLL.DTOs.BatchItem.BatchItemDto> GetBatchItemByBarcodeAsync(string barcode);
    }
}

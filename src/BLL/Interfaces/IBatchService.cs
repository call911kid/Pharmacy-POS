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
        Task<IEnumerable<BatchSummaryDto>> GetBatchSummariesAsync();
        Task<BatchDto> GetBatchById(int batchId);
        Task AddBatchAsync(CreateBatchDto createBatchDto);
    }
}

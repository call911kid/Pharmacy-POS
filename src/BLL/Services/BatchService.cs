using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Batch;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class BatchService:IBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BatchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<BatchSummaryDto>> GetBatchSummariesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BatchDto> GetBatchById(int batchId)
        {
            var batch =await _unitOfWork.Batches.GetByIdAsync(batchId)
                ?? throw new KeyNotFoundException($"Batch with ID {batchId} not found.");

            return new BatchDto
            {
                // Map properties from batch to BatchDto here
            };


        }

        public Task AddBatchAsync(CreateBatchDto createBatchDto)
        {
            throw new NotImplementedException();
        }
    }
}

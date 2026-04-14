using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Batch;
using BLL.DTOs.BatchItem;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    internal class BatchService:IBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BatchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<BatchSummaryDto>> GetBatchSummariesAsync(int pageNumber, int pageSize)
        {
            var batches = await _unitOfWork.Batches.GetAllAsync(pageNumber, pageSize);

            return batches.Select(batch => new BatchSummaryDto
            {
                Id = batch.Id,
                SupplierId = batch.SupplierId,
                SupplierName = batch.Supplier.Name,
                PurchaseDate = batch.PurchaseDate
                
            }).ToList();
        }

        public async Task<BatchDto> GetBatchByIdAsync(int batchId)
        {
            var batch =await _unitOfWork.Batches.GetByIdAsync(batchId)
                ?? throw new EntityNotFoundException($"Batch with ID {batchId} not found.");

            return new BatchDto
            {
                Id = batch.Id,
                SupplierId = batch.SupplierId,
                PurchaseDate = batch.PurchaseDate,

                
                Items = batch.BatchItems.Select(item => new BatchItemDto
                {
                    Id = item.Id,
                    BatchId = item.BatchId,
                    ProductId = item.ProductId,
                    QuantityReceived = item.QuantityReceived,
                    QuantityRemaining = item.QuantityRemaining,
                    ExpirationDate = item.ExpirationDate,
                    CostPrice = item.CostPrice,
                    MandatorySellingPrice = item.MandatorySellingPrice
                }).ToList()
            };
        }


        public async Task AddBatchAsync(CreateBatchDto createBatchDto)
        {
            var batch = new Batch
            {
                SupplierId = createBatchDto.SupplierId,
                PurchaseDate = createBatchDto.PurchaseDate,
                
                BatchItems = createBatchDto.BatchItems.Select(item => new BatchItem
                {
                    ProductId = item.ProductId,
                    QuantityReceived = item.QuantityReceived,
                    
                    QuantityRemaining = item.QuantityReceived,

                    ExpirationDate = item.ExpirationDate,
                    CostPrice = item.CostPrice,
                    MandatorySellingPrice = item.MandatorySellingPrice
                }).ToList()
            };

            await _unitOfWork.Batches.AddAsync(batch);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateBatchAsync(int batchId, CreateBatchDto createBatchDto)
        {
            var batch = await _unitOfWork.Batches.GetByIdAsync(batchId)
                ?? throw new EntityNotFoundException($"Batch with ID {batchId} not found.");

            foreach (var existingItem in batch.BatchItems.ToList())
            {
                _unitOfWork.BatchItems.Delete(existingItem);
            }

            batch.SupplierId = createBatchDto.SupplierId;
            batch.PurchaseDate = createBatchDto.PurchaseDate;
            batch.BatchItems = createBatchDto.BatchItems.Select(item => new BatchItem
            {
                ProductId = item.ProductId,
                QuantityReceived = item.QuantityReceived,
                QuantityRemaining = item.QuantityReceived,
                ExpirationDate = item.ExpirationDate,
                CostPrice = item.CostPrice,
                MandatorySellingPrice = item.MandatorySellingPrice
            }).ToList();

            _unitOfWork.Batches.Update(batch);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBatchAsync(int batchId)
        {
            var batch = await _unitOfWork.Batches.GetByIdAsync(batchId)
                ?? throw new EntityNotFoundException($"Batch with ID {batchId} not found.");

            _unitOfWork.Batches.Delete(batch);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BatchSummaryDto>> GetBatchesBySupplierAsync(int supplierId)
        {
            var batches = await _unitOfWork.Batches.FindAsync(b => b.SupplierId == supplierId);

            return batches.Select(batch => new BatchSummaryDto
            {
                Id = batch.Id,
                SupplierId = batch.SupplierId,
                PurchaseDate = batch.PurchaseDate
            }).ToList();
        }


    }
}

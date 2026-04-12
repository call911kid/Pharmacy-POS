using BLL.DTOs.Invoice;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Exceptions;
using UI.Exceptions;
using BLL.DTOs.InvoiceItem;
namespace BLL.Services
{

    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateSaleInvoiceAsync(CreateInvoiceDto invoiceDto)
        {

            var invoice = new Invoice
            {
                CustomerId = invoiceDto.CustomerId,
                InvoiceDate = invoiceDto.InvoiceDate,
                InvoiceItems = new List<InvoiceItem>()
            };
            var modifiedBatches = new List<(BatchItem batch, int subtractedQuantity)>();
            decimal totalInvoicePrice = 0;

            try
            {
                foreach (var itemDto in invoiceDto.InvoiceItems)
                {
                    var batchItem = await _unitOfWork.BatchItems.GetByIdAsync(itemDto.BatchItemId);
                    //the error in line 42 
                    if (batchItem == null) throw new Exception($"Batch {itemDto.BatchItemId} not found");
                    if (batchItem.QuantityRemaining < itemDto.Quantity) throw new InsufficientStockException($"Batch {itemDto.BatchItemId} does not have enough quantity. Requested: {itemDto.Quantity}, Available: {batchItem.QuantityRemaining}");

                    decimal itemPrice = batchItem.MandatorySellingPrice;
                    totalInvoicePrice += itemDto.Quantity * itemPrice;

                    batchItem.QuantityRemaining -= itemDto.Quantity;
                    modifiedBatches.Add((batchItem, itemDto.Quantity));
                    _unitOfWork.BatchItems.Update(batchItem);

                    invoice.InvoiceItems.Add(new InvoiceItem
                    {
                        BatchItemId = itemDto.BatchItemId,
                        Quantity = itemDto.Quantity,
                        OriginalPrice = itemPrice
                    });
                }

                invoice.TotalAmount = totalInvoicePrice;

                await _unitOfWork.Invoices.AddAsync(invoice);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                foreach (var track in modifiedBatches)
                {
                    track.batch.QuantityRemaining += track.subtractedQuantity;
                    _unitOfWork.BatchItems.Update(track.batch);
                }

                await _unitOfWork.SaveChangesAsync();
                throw new Exception("Operation failed. Changes were rolled back manually.", ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IEnumerable<InvoiceDto>> GetCustomerInvoicesAsync(int customerId)
        {
            var invoices = await _unitOfWork.Invoices.FindAsync(i => i.CustomerId == customerId);

            var result = invoices.Select(inv => new InvoiceDto
            {
                Id = inv.Id,
                CustomerId = inv.CustomerId,
                InvoiceDate = inv.InvoiceDate,
                TotalAmount = inv.TotalAmount,
                InvoiceItems = inv.InvoiceItems?.Select(ii => new InvoiceItemDto
                {
                    Id = ii.Id,
                    InvoiceId = ii.InvoiceId,
                    ProductId = ii.BatchItem?.ProductId ?? 0,
                    Quantity = ii.Quantity,
                    UnitPrice = ii.OriginalPrice
                }).ToList() ?? new List<InvoiceItemDto>()
            });

            return result;
        }



        public async Task<IEnumerable<InvoiceDto>> GetCustomerInvoicesAsync(string phoneNumber)
        {
            var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Phone == phoneNumber);
            if (customer == null) return Enumerable.Empty<InvoiceDto>();

            return await GetCustomerInvoicesAsync(customer.Id);
        }

    }
}
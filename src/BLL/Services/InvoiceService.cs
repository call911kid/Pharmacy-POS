using BLL.DTOs.Invoice;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using BLL.Exceptions;
using BLL.DTOs.InvoiceItem;
using Common.Enums;
using BLL.Utils;
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
            ArgumentNullException.ThrowIfNull(invoiceDto);

            if (invoiceDto.InvoiceItems is null || invoiceDto.InvoiceItems.Count == 0)
            {
                throw new InvalidInvoiceStatusException("Invoice must contain at least one item.");
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(invoiceDto.CustomerId);
            if (customer is null)
            {
                throw new EntityNotFoundException($"Customer '{invoiceDto.CustomerId}' was not found.");
            }

            var invoice = new Invoice
            {
                CustomerId = invoiceDto.CustomerId,
                InvoiceDate = invoiceDto.InvoiceDate,
                Status = InvoiceStatus.Finalized,
                InvoiceItems = new List<InvoiceItem>(),
                Barcode = BarcodeGenerator.GenerateInvoiceBarcode()
            };

            decimal totalInvoicePrice = 0;

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                foreach (var itemDto in invoiceDto.InvoiceItems)
                {
                    if (itemDto.Quantity <= 0)
                    {
                        throw new InvalidInvoiceStatusException(
                            $"Invoice item for batch '{itemDto.BatchItemId}' must have a quantity greater than zero.");
                    }

                    var batchItem = await _unitOfWork.BatchItems.GetByIdAsync(itemDto.BatchItemId);

                    if (batchItem == null)
                    {
                        throw new BatchNotFoundException($"Batch item '{itemDto.BatchItemId}' was not found.");
                    }

                    if (batchItem.ProductId != itemDto.ProductId)
                    {
                        throw new EntityNotFoundException(
                            $"Batch item '{itemDto.BatchItemId}' does not belong to product '{itemDto.ProductId}'.");
                    }

                    if (batchItem.QuantityRemaining < itemDto.Quantity)
                    {
                        throw new InsufficientStockException(
                            $"Batch item '{itemDto.BatchItemId}' does not have enough quantity. Requested: {itemDto.Quantity}, Available: {batchItem.QuantityRemaining}.");
                    }

                    decimal itemPrice = batchItem.MandatorySellingPrice;
                    totalInvoicePrice += itemDto.Quantity * itemPrice;

                    batchItem.QuantityRemaining -= itemDto.Quantity;
                    _unitOfWork.BatchItems.Update(batchItem);

                    invoice.InvoiceItems.Add(new InvoiceItem
                    {
                        BatchItemId = itemDto.BatchItemId,
                        Quantity = itemDto.Quantity,
                        OriginalPrice = itemPrice,
                        DiscountedPrice = itemPrice
                    });
                }

                invoice.TotalAmount = totalInvoicePrice;
                invoice.TotalDiscount = 0m;
                invoice.NetAmount = totalInvoicePrice;

                await _unitOfWork.Invoices.AddAsync(invoice);
                var result = await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                return result > 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<InvoiceDto>> GetCustomerInvoicesAsync(int customerId)
        {
            var invoices = await _unitOfWork.Invoices.FindAsync(i => i.CustomerId == customerId);

            var result = invoices.Select(inv => new InvoiceDto
            {
                Id = inv.Id,
                CustomerId = inv.CustomerId,
                Status = inv.Status.ToString(),
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

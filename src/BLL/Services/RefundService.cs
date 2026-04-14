using BLL.DTOs.Refund;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common.Enums;
using BLL.Exceptions;

namespace BLL.Services
{
    public class RefundService : IRefundService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefundService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> RefundByInvoiceAsync(RefundRequestDto request)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(request.InvoiceId);
            if (invoice == null) throw new InvoiceNotFoundException($"Invoice {request.InvoiceId} not found");

            if (invoice.Status != InvoiceStatus.Finalized && invoice.Status != InvoiceStatus.PartiallyReturned)
                throw new InvalidInvoiceStatusException("Only finalized invoices can be used as the basis for a return.");

            var modifiedBatches = new List<(BatchItem batch, int delta)>();

            try
            {
                decimal refundAmount = 0m;

                
                foreach (var returned in request.ReturnedInvoiceItems)
                {
                    var invItem = invoice.InvoiceItems.FirstOrDefault(ii => ii.Id == returned.InvoiceItemId);
                    if (invItem == null) throw new InvoiceItemNotFoundException($"Invoice item {returned.InvoiceItemId} not found on invoice {invoice.Id}");

                    var remainingReturnable = invItem.Quantity - invItem.ReturnedQuantity;
                    if (returned.Quantity <= 0 || returned.Quantity > remainingReturnable)
                        throw new ExceededReturnQuantityException($"Invalid return quantity for invoice item {invItem.Id}");

                    invItem.ReturnedQuantity += returned.Quantity;

                    var batch = await _unitOfWork.BatchItems.GetByIdAsync(invItem.BatchItemId);
                    if (batch == null) throw new BatchNotFoundException($"Batch {invItem.BatchItemId} not found");

                    batch.QuantityRemaining += returned.Quantity;
                    modifiedBatches.Add((batch, returned.Quantity));
                    _unitOfWork.BatchItems.Update(batch);

                    refundAmount += invItem.DiscountedPrice * returned.Quantity;
                    _unitOfWork.InvoiceItems.Update(invItem);
                }

                
                foreach (var ext in request.ExternalReturnedItems)
                {
                    if (ext.Quantity <= 0) continue;

                    var batch = await _unitOfWork.BatchItems.GetByIdAsync(ext.BatchItemId);
                    if (batch == null) throw new BatchNotFoundException($"Batch {ext.BatchItemId} not found");

                    batch.QuantityRemaining += ext.Quantity;
                    modifiedBatches.Add((batch, ext.Quantity));
                    _unitOfWork.BatchItems.Update(batch);

                    refundAmount += ext.UnitPrice * ext.Quantity;
                }

                invoice.TotalDiscount += refundAmount;
                invoice.NetAmount -= refundAmount;

                var allReturned = invoice.InvoiceItems.All(ii => ii.ReturnedQuantity >= ii.Quantity);
                invoice.Status = allReturned ? InvoiceStatus.Returned : InvoiceStatus.PartiallyReturned;

                _unitOfWork.Invoices.Update(invoice);

                var changed = await _unitOfWork.SaveChangesAsync();
                return changed > 0;
            }
            catch (Exception ex)
            {
                foreach (var m in modifiedBatches)
                {
                    m.batch.QuantityRemaining -= m.delta;
                    _unitOfWork.BatchItems.Update(m.batch);
                }

                await _unitOfWork.SaveChangesAsync();
                throw new RefundProcessException("Refund operation failed and was rolled back.", ex);
            }
            
        }
    }
}

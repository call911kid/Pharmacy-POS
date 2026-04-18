using BLL.DTOs.InvoiceItem;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<InvoiceItemDto?> GetInvoiceItemByIdAsync(int id)
        {
            var item = await _unitOfWork.InvoiceItems.GetByIdAsync(id);
            if (item == null) return null;

            return new InvoiceItemDto
            {
                Id = item.Id,
                InvoiceId = item.InvoiceId,
                ProductId = item.BatchItem?.ProductId ?? 0,
                ProductName = item.BatchItem?.Product?.Name ?? $"Product #{item.BatchItem?.ProductId ?? 0}",
                Quantity = item.Quantity,
                UnitPrice = item.OriginalPrice
            };
        }

        public async Task<IEnumerable<InvoiceItemDto>> GetInvoiceItemByIdAsync(IEnumerable<int> ids)
        {
            var items = await _unitOfWork.InvoiceItems.FindAsync(i => ids.Contains(i.Id));

            return items.Select(item => new InvoiceItemDto
            {
                Id = item.Id,
                InvoiceId = item.InvoiceId,
                ProductId = item.BatchItem?.ProductId ?? 0,
                ProductName = item.BatchItem?.Product?.Name ?? $"Product #{item.BatchItem?.ProductId ?? 0}",
                Quantity = item.Quantity,
                UnitPrice = item.OriginalPrice
            });
        }
    }
}

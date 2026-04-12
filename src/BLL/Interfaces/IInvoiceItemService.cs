using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.InvoiceItem;
namespace BLL.Interfaces
{
    public interface IInvoiceItemService
    {
        Task<InvoiceItemDto?> GetInvoiceItemByIdAsync(int id);
        Task<IEnumerable<InvoiceItemDto>> GetInvoiceItemByIdAsync(IEnumerable<int> ids);
    }
}

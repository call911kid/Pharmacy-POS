using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs.Refund;

namespace BLL.Interfaces
{
    public interface IRefundService
    {
        Task<bool> RefundByInvoiceAsync(RefundRequestDto request);
    }
}

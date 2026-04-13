using System.Collections.Generic;

namespace BLL.DTOs.Refund
{
    public class RefundRequestDto
    {
        public int InvoiceId { get; set; }
        
        public List<RefundInvoiceItemDto> ReturnedInvoiceItems { get; set; }
        
        public List<RefundExternalItemDto> ExternalReturnedItems { get; set; }

        public RefundRequestDto()
        {
            ReturnedInvoiceItems = new List<RefundInvoiceItemDto>();
            ExternalReturnedItems = new List<RefundExternalItemDto>();
        }
    }
}

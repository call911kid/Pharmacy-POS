using BLL.DTOs.Invoice;

namespace UI.Printing
{
    public interface IInvoicePdfGenerator
    {
        Task<string> GenerateAsync(InvoiceDto invoice, CancellationToken cancellationToken = default);
    }
}

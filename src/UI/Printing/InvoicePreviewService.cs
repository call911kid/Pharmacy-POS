using BLL.DTOs.Invoice;
using UI.Forms.InvoicePreviewForm;

namespace UI.Printing
{
    public interface IInvoicePreviewService
    {
        Task PreviewAsync(IWin32Window? owner, InvoiceDto invoice, CancellationToken cancellationToken = default);
    }

    public sealed class InvoicePreviewService : IInvoicePreviewService
    {
        private readonly IInvoicePdfGenerator _invoicePdfGenerator;

        public InvoicePreviewService(IInvoicePdfGenerator invoicePdfGenerator)
        {
            _invoicePdfGenerator = invoicePdfGenerator;
        }

        public async Task PreviewAsync(IWin32Window? owner, InvoiceDto invoice, CancellationToken cancellationToken = default)
        {
            var pdfPath = await _invoicePdfGenerator.GenerateAsync(invoice, cancellationToken);

            using var form = new InvoicePreviewForm(pdfPath);
            if (owner is Control control)
            {
                form.ShowDialog(control);
                return;
            }

            form.ShowDialog();
        }
    }
}

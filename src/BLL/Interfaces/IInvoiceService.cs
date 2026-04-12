using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Invoice;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IInvoiceService
    {        
        Task<bool> CreateSaleInvoiceAsync(CreateInvoiceDto createInvoice);
        Task<IEnumerable<InvoiceDto>> GetCustomerInvoicesAsync(int customerId);
    }
}

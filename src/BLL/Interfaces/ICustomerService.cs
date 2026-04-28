using BLL.DTOs;
using BLL.DTOs.Customer;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<IReadOnlyList<CustomerDto>> GetAllCustomersAsync();

        Task<IReadOnlyList<Customer?>> GetCustomersByBatchItemAsync(int batchItemId);

        Task<IReadOnlyList<Invoice?>> GetInvoiceHistoryAsync(int customerId);

        Task<CustomerValidationResultDto> ValidateForSaleAsync(int customerId);

        Task<CustomerDto> CreateCustomerAsync(CreateCustomerByPhoneDto dto);

        Task<CustomerDto?> GetCustomerByPhoneAsync(string phone);

        Task<CustomerDto?> GetCustomerAsync(int customerId);

        Task<IReadOnlyList<CustomerDto>> FindByNameAsync(string name);

        Task<CustomerDto?> UpdateContactInfoAsync(int customerId, string name, string phone);

        Task<bool> DeleteCustomerAsync(int customerId);
    }
}

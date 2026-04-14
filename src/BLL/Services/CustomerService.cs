using BLL.DTOs;
using BLL.DTOs.Customer;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        private static CustomerDto? MapToDto(Customer? customer)
        {
            if (customer == null) return null;
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone
            };
        }


        public async Task<CustomerDto?> CreateCustomerAsync(CreateCustomerByPhoneDto dto)
        {
            var existing = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Phone == dto.Phone);
            if (existing != null)
                return MapToDto(existing);

            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone ?? string.Empty
            };

            try
            {
                await _unitOfWork.Customers.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                return MapToDto(customer);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<CustomerDto>> FindByNameAsync(string name)
        {
            try
            {
                var customers = await _unitOfWork.Customers.FindAsync(c => c.Name.Contains(name));
                return customers.Select(MapToDto).OfType<CustomerDto>().ToList();
            }
            catch
            {
                return new List<CustomerDto>();
            }
        }

        public async Task<CustomerDto?> GetCustomerByPhoneAsync(string phone)
        {
            try
            {
                var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Phone == phone);
                return MapToDto(customer);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CustomerDto?> GetCustomerAsync(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
                return MapToDto(customer);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<Customer?>> GetCustomersByBatchItemAsync(int batchItemId)
        {
            try
            {
                var invoiceItems = await _unitOfWork.InvoiceItems.FindAsync(ii => ii.BatchItemId == batchItemId);
                return invoiceItems
                    .Select(ii => ii.Invoice.Customer)
                    .DistinctBy(c => c.Id)
                    .ToList();
            }
            catch
            {
                return new List<Customer?>();
            }
        }

        public async Task<IReadOnlyList<Invoice?>> GetInvoiceHistoryAsync(int customerId)
        {
            try
            {
                var invoices = await _unitOfWork.Invoices.FindAsync(i => i.CustomerId == customerId);
                return invoices.ToList();
            }
            catch
            {
                return new List<Invoice?>();
            }
        }

        public async Task<CustomerDto?> UpdateContactInfoAsync(int customerId, string name, string phone)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
                if (customer == null) return null;

                customer.Name = name;
                customer.Phone = phone;

                _unitOfWork.Customers.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return MapToDto(customer);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
                if (customer == null)
                    return false;

                var hasInvoices = await _unitOfWork.Invoices.FirstOrDefaultAsync(i => i.CustomerId == customerId) is not null;
                if (hasInvoices)
                    return false;

                _unitOfWork.Customers.Delete(customer);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CustomerValidationResultDto> ValidateForSaleAsync(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);

                if (customer == null)
                    return new CustomerValidationResultDto
                    {
                        CustomerId = customerId,
                        IsValid = false,
                        Reason = "Customer not found."
                    };

                bool nameOk = !string.IsNullOrEmpty(customer.Name);
                bool phoneOk = !string.IsNullOrEmpty(customer.Phone);

                return new CustomerValidationResultDto
                {
                    CustomerId = customerId,
                    IsValid = nameOk && phoneOk,
                    Reason = (!nameOk && !phoneOk) ? "Name and phone are missing."
                               : !nameOk ? "Name is missing."
                               : !phoneOk ? "Phone is missing."
                               : null
                };
            }
            catch
            {
                return new CustomerValidationResultDto
                {
                    CustomerId = customerId,
                    IsValid = false,
                    Reason = "An unexpected error occurred during validation."
                };
            }
        }
    }
}

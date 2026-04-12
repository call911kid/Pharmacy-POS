using BLL.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync(int pageNumber, int pageSize);
        Task<SupplierDto> GetSupplierByIdAsync(int id);
        Task<SupplierDto> GetSupplierByNameAsync(string name);
        Task<SupplierDto> GetSupplierByPhoneAsync(string phone);
        Task AddSupplierAsync(CreateSupplierDto Suplier);
        Task UpdateSupplierAsync(UpdateSupplierDto Supplier);
        Task DeleteSupplierAsync(int id);
    }
}

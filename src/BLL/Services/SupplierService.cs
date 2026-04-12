using BLL.DTOs.Supplier;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSupplierAsync(CreateSupplierDto Supplier)
        {
            if (!string.IsNullOrWhiteSpace(Supplier.Phone))
            {
                var supplier = await _unitOfWork.Suppliers.FirstOrDefaultAsync(s=> s.Phone == Supplier.Phone);
                if (supplier != null) 
                    throw new DuplicateException($"{Supplier.Phone} is already exists");
                
            } 
            var NewSupplier = new Supplier { Name = Supplier.Name, Phone = Supplier.Phone };
            await _unitOfWork.Suppliers.AddAsync(NewSupplier);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id)
                ?? throw new EntityNotFoundException($"Supplier with ID {id} was not found.");
            if (supplier.Batches is not null && supplier.Batches.Any())
                throw new SupplierHasActiveBatchesException(id);
            _unitOfWork.Suppliers.Delete(supplier);
            await _unitOfWork.SaveChangesAsync();


        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync(int pageNumber, int pageSize)
        {
            var supplier = await _unitOfWork.Suppliers.GetAllAsync(pageNumber, pageSize);
            return supplier.Select(s => new SupplierDto 
            {
                Id = s.Id,
                Name = s.Name,
                Phone = s.Phone,
               
            });
        }

        public async Task<SupplierDto> GetSupplierByIdAsync(int id)
        {
            var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id)
                ?? throw new EntityNotFoundException($"Supplier with ID {id} was not found.");
            return new SupplierDto { Id = supplier.Id, Name = supplier.Name, Phone = supplier.Phone };
        
        }

        public async Task<SupplierDto> GetSupplierByNameAsync(string name)
        {
            var supplier = await _unitOfWork.Suppliers.FirstOrDefaultAsync(s => s.Name == name)
                       ?? throw new EntityNotFoundException($"No supplier found with name '{name}'.");
            return new SupplierDto { Id = supplier.Id, Name = name, Phone = supplier.Phone };

        }

        public async Task<SupplierDto> GetSupplierByPhoneAsync(string phone)
        {
            var supplier = await _unitOfWork.Suppliers.FirstOrDefaultAsync(s=> s.Phone == phone)
                ?? throw new EntityNotFoundException($"No supplier found with this number phone '{phone}'.");
            return new SupplierDto { Id = supplier.Id, Name = supplier.Name, Phone = phone };
        }

        public async Task UpdateSupplierAsync(UpdateSupplierDto Supplier)
        {
            var NewSupplier = await _unitOfWork.Suppliers.FirstOrDefaultAsync(s=> s.Id == Supplier.Id)
                ?? throw new EntityNotFoundException($"Supplier with ID {Supplier.Id} was not found.");
            if (!string.IsNullOrWhiteSpace(Supplier.Phone) && Supplier.Phone != NewSupplier.Phone)
            {
                var conflict = await _unitOfWork.Suppliers
                                   .FirstOrDefaultAsync(s => s.Phone == Supplier.Phone);
                if (conflict is not null)
                    throw new DuplicateException($"{Supplier.Phone} is already exists");
            }
            NewSupplier.Name = Supplier.Name;
            NewSupplier.Phone = Supplier.Phone;

            _unitOfWork.Suppliers.Update(NewSupplier);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

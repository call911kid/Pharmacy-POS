using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Products.GetAllAsync(pageNumber, pageSize);
            return products.Select(ToDto);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id)
                ?? throw new ProductNotFoundException(id);
            return ToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string keyword)
        {
            var products = await _unitOfWork.Products
                .FindAsync(p => p.Name.Contains(keyword) || p.Barcode.Contains(keyword));
            return products.Select(ToDto);
        }

        public async Task AddAsync(ProductDto dto)
        {
            var existing = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Barcode == dto.Barcode);
            if (existing != null)
                throw new DuplicateException($"Barcode '{dto.Barcode}' already exists.");

            await _unitOfWork.Products.AddAsync(new Product { Barcode = dto.Barcode, Name = dto.Name });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetNearExpiryAsync(int daysThreshold)
        {
            var limitDate = DateTime.Today.AddDays(daysThreshold);
            var products = await _unitOfWork.Products
                .FindAsync(p => p.BatchItems.Any(bi => bi.ExpirationDate <= limitDate && bi.QuantityRemaining > 0));
            return products.Select(ToDto);
        }

        private static ProductDto ToDto(Product p)
        {
            var today = DateTime.Today;
            var active = p.BatchItems?
                .Where(bi => bi.ExpirationDate >= today && bi.QuantityRemaining > 0)
                .OrderBy(bi => bi.ExpirationDate)
                .ToList();

            return new ProductDto
            {
                Id = p.Id,
                Barcode = p.Barcode,
                Name = p.Name,
                TotalStock = active?.Sum(bi => bi.QuantityRemaining) ?? 0,
                CurrentPrice = active?.FirstOrDefault()?.MandatorySellingPrice ?? 0,
                NearestExpiryDate = p.BatchItems?.Any() == true ? p.BatchItems.Min(bi => bi.ExpirationDate) : null
            };
        }
    }
}

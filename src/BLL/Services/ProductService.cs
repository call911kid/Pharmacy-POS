using BLL.DTOs.Product;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    internal sealed class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Products.GetAllAsync(pageNumber, pageSize);
            return products.Select(Map).ToList();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return product is null ? null : Map(product);
        }

        public async Task<ProductDto?> GetProductByBarcodeAsync(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return null;
            }

            var normalizedBarcode = barcode.Trim();
            var product = await _unitOfWork.Products.FirstOrDefaultAsync(
                p => p.Barcode == normalizedBarcode);

            return product is null ? null : Map(product);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllProductsAsync(pageNumber, pageSize);
            }

            var normalizedSearch = searchTerm.Trim();
            var products = await _unitOfWork.Products.FindAsync(p =>
                p.Name.Contains(normalizedSearch) ||
                p.Barcode.Contains(normalizedSearch));

            return products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(Map)
                .ToList();
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto createProductDto)
        {
            var name = createProductDto.Name.Trim();
            var barcode = createProductDto.Barcode.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name is required.", nameof(createProductDto));
            }

            if (!string.IsNullOrWhiteSpace(barcode))
            {
                var existingProduct = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Barcode == barcode);
                if (existingProduct is not null)
                {
                    throw new DuplicateException($"Barcode '{barcode}' already belongs to '{existingProduct.Name}'.");
                }
            }

            var product = new Product
            {
                Name = name,
                Barcode = barcode
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return Map(product);
        }

        private static ProductDto Map(DAL.Models.Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Barcode = product.Barcode,
                Name = product.Name
            };
        }
    }
}

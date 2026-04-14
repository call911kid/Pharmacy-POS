using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> GetProductByBarcodeAsync(string barcode);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm, int pageNumber, int pageSize);
        Task<ProductDto> AddProductAsync(CreateProductDto createProductDto);
    }
}

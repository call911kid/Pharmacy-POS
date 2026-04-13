using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> SearchAsync(string keyword);
        Task AddAsync(ProductDto dto);
        Task<IEnumerable<ProductDto>> GetNearExpiryAsync(int daysThreshold);
    }
}

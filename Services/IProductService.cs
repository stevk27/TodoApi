using TodoApi.DTOs;

namespace TodoApi.Service
{
    // Services/IProductService.cs
public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductCreateDto productDto);
        Task<ProductResponseDto?> UpdateProductAsync(int id, ProductCreateDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
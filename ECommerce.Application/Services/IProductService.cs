using ECommerce.Application.DTOs.Products;

namespace ECommerce.Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(Guid id);
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
}
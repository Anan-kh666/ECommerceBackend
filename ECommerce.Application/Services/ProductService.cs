using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            SKU = p.SKU
        });
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            SKU = product.SKU
        };
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {

        var productEntity = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            SKU = dto.SKU
        };


        var savedProduct = await _productRepository.AddAsync(productEntity);


        return new ProductDto
        {
            Id = savedProduct.Id,
            Name = savedProduct.Name,
            Description = savedProduct.Description,
            Price = savedProduct.Price,
            StockQuantity = savedProduct.StockQuantity,
            SKU = savedProduct.SKU
        };
    }
}
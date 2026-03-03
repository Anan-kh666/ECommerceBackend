using ECommerce.Application.DTOs.Cart;

namespace ECommerce.Application.Services;

public interface ICartService
{
    Task<CartDto> GetCartAsync(Guid userId);
    Task<CartDto> AddToCartAsync(Guid userId, AddToCartDto dto);
    Task<CartDto> RemoveFromCartAsync(Guid userId, Guid productId);
    Task ClearCartAsync(Guid userId);
}
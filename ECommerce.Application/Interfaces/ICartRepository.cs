using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
    Task<Cart> CreateCartAsync(Cart cart);
    Task UpdateAsync();
    Task AddCartItemAsync(CartItem item);
    Task UpdateCartItemQuantityAsync(Guid itemId, int newQuantity);
    Task ClearCartItemsAsync(Guid cartId);
}
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart> CreateCartAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
        return cart;
    }


    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task AddCartItemAsync(CartItem item)
    {
        await _context.CartItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCartItemQuantityAsync(Guid itemId, int newQuantity)
    {
        await _context.CartItems
            .Where(i => i.Id == itemId)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.Quantity, newQuantity));
    }
    public async Task ClearCartItemsAsync(Guid cartId)
    {
        await _context.CartItems
            .Where(i => i.CartId == cartId)
            .ExecuteDeleteAsync();
    }
}
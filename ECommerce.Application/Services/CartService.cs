using ECommerce.Application.DTOs.Cart;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<CartDto> GetCartAsync(Guid userId)
    {

        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = await _cartRepository.CreateCartAsync(new Cart { UserId = userId });
        }
        return MapToDto(cart);
    }

    public async Task<CartDto> AddToCartAsync(Guid userId, AddToCartDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.ProductId);
        if (product == null) throw new Exception("Product not found.");
        
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null)
        {
            cart = await _cartRepository.CreateCartAsync(new Cart { UserId = userId });
        }

        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        
        if (existingItem != null)
        {

            var newQuantity = existingItem.Quantity + dto.Quantity;
            
            if (product.StockQuantity < newQuantity) 
                throw new Exception($"Not enough stock available.");
                
            await _cartRepository.UpdateCartItemQuantityAsync(existingItem.Id, newQuantity);
            

            existingItem.Quantity = newQuantity; 
        }
        else
        {
            if (product.StockQuantity < dto.Quantity) throw new Exception("Not enough stock available.");
            

            var newItem = new CartItem
            {
                CartId = cart.Id, 
                ProductId = product.Id,
                Quantity = dto.Quantity
            };
            
            await _cartRepository.AddCartItemAsync(newItem);
            cart.Items.Add(newItem);
        }

        return MapToDto(cart);
    }

    public async Task<CartDto> RemoveFromCartAsync(Guid userId, Guid productId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) throw new Exception("Cart not found.");

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            cart.Items.Remove(item);
            await _cartRepository.UpdateAsync();
        }

        return MapToDto(cart);
    }

   public async Task ClearCartAsync(Guid userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart != null)
        {
            await _cartRepository.ClearCartItemsAsync(cart.Id);
        }
    }

    private CartDto MapToDto(Cart cart)
    {
        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,

            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? "Unknown Product",
                UnitPrice = i.Product?.Price ?? 0,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}
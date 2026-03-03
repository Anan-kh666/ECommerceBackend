namespace ECommerce.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Links this item to a specific Cart
    public Guid CartId { get; set; }
    public Cart? Cart { get; set; }
    
    // Links this item to a specific Product
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int Quantity { get; set; }
}
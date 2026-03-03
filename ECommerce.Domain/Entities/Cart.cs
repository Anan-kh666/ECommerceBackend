namespace ECommerce.Domain.Entities;

public class Cart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    // Links this cart to a specific user
    public Guid UserId { get; set; } 
    
    // A cart can have many items
    public List<CartItem> Items { get; set; } = new();
}
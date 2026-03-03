namespace ECommerce.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; 

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
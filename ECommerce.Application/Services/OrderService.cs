using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(Guid userId, CreateOrderDto dto)
    {

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            TotalAmount = 0,
            OrderItems = new List<OrderItem>()
        };

        foreach (var itemDto in dto.Items)
        {

            var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
            
            if (product == null)
                throw new Exception($"Product with ID {itemDto.ProductId} not found.");

            if (product.StockQuantity < itemDto.Quantity)
                throw new Exception($"Not enough stock for {product.Name}. Available: {product.StockQuantity}");

            var itemTotal = product.Price * itemDto.Quantity;
            order.TotalAmount += itemTotal;


            product.StockQuantity -= itemDto.Quantity;
            await _productRepository.UpdateAsync(product);


            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price 
            });
        }

        var savedOrder = await _orderRepository.CreateOrderAsync(order);


        return MapToDto(savedOrder);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        return orders.Select(MapToDto);
    }


    private OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            Items = order.OrderItems.Select(oi => new OrderItemDto
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product?.Name ?? "Unknown Product",
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
    }
}
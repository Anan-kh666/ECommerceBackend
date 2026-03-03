using ECommerce.Application.DTOs.Orders;

namespace ECommerce.Application.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(Guid userId, CreateOrderDto createOrderDto);
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync(Guid userId);
    Task<OrderDto> CheckoutCartAsync(Guid userId);
}
using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; 

namespace ECommerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpPost("checkout")]
    public async Task<ActionResult<OrderDto>> Checkout()
    {
        try
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { message = "Invalid token payload." });
            }

            var order = await _orderService.CheckoutCartAsync(userId);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            return Unauthorized(new { message = "Invalid token payload." });
        }

        var orders = await _orderService.GetUserOrdersAsync(userId);
        return Ok(orders);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using DbRepos;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : Controller
{
    private readonly OrderDbRepos _orderRepo;

    public OrderController(OrderDbRepos orderRepo)
    {
        _orderRepo = orderRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDTO dto)
    {
        var order = await _orderRepo.CreateOrderAsync(dto);
        return Ok(order);
    }

    [HttpGet] 
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderRepo.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _orderRepo.GetOrderByIdAsync(id);
        if (order == null) return NotFound("Order not found.");
        return Ok(order);
    }
    
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out userId))
            return Unauthorized();
        
        var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);
        if (orders == null || orders.Count == 0) return NotFound("No orders found for this user.");
        return Ok(orders);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderDTO dto)
    {
        var success = await _orderRepo.UpdateOrderAsync(id, dto);
        if (!success) return NotFound("Order not found for update.");
        return Ok("Order updated.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _orderRepo.DeleteOrderAsync(id);
        if (!success) return NotFound("Order not found for deletion.");
        return Ok("Order deleted.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        await _orderRepo.DeleteAllOrdersAsync();
        return Ok("All orders deleted.");
    }

}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using DbRepos;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = "Admin,User")]
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
    [Authorize(Roles = "User")]
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
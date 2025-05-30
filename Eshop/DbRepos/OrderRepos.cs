using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class OrderDbRepos
{
    private readonly ILogger<OrderDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    
    public OrderDbRepos(ILogger<OrderDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.Products)
            .Include(o => o.User)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _dbContext.Orders
            .Include(o => o.Products)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _dbContext.Orders
            .Include(o => o.Products)
            .Include(o => o.User)
            .Where(o => o.userId == userId)
            .ToListAsync();
    }

    public async Task<Order> CreateOrderAsync(OrderDTO dto)
    {
        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            OrderDetails = dto.OrderDetails,
            OrderDate = dto.OrderDate,
            OrderStatus = dto.OrderStatus,
            OrderAmount = dto.OrderAmount,
            userId = dto.UserId ?? Guid.Empty,
            Products = await _dbContext.Products
                .Where(p => dto.ProductsId.Contains(p.ProductId))
                .ToListAsync()
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<bool> UpdateOrderAsync(Guid id, OrderDTO dto)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) return false;

        order.OrderDetails = dto.OrderDetails;
        order.OrderDate = dto.OrderDate;
        order.OrderStatus = dto.OrderStatus;
        order.OrderAmount = dto.OrderAmount;
        order.userId = dto.UserId ?? Guid.Empty;

        // Uppdatera produkter
        order.Products = await _dbContext.Products
            .Where(p => dto.ProductsId.Contains(p.ProductId))
            .ToListAsync();

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var order = await _dbContext.Orders.FindAsync(id);
        if (order == null) return false;

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAllOrdersAsync()
    {
        var allOrders = _dbContext.Orders;
        _dbContext.Orders.RemoveRange(allOrders);
        await _dbContext.SaveChangesAsync();
    }

}
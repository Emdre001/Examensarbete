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
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Include(o => o.User)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<List<Order>> GetOrdersByUserNameAsync(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        if (user == null) return [];

        return await _dbContext.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .Include(o => o.User)
            .Where(o => o.userId == user.UserId)
        .ToListAsync();
    }

    public async Task<Order> CreateOrderAsync(OrderDTO dto, string userName)
    {
        if (string.IsNullOrEmpty(userName))
            throw new ArgumentNullException(nameof(userName), "Username cannot be null when creating an order.");

        var userId = await _dbContext.Users
            .Where(u => u.UserName == userName)
            .Select(u => u.UserId)
            .FirstOrDefaultAsync();

        if (userId == Guid.Empty)
            throw new InvalidOperationException("User not found when creating an order.");

        var order = new Order
        {
            OrderId = Guid.NewGuid(),
            OrderDetails = dto.OrderDetails,
            OrderDate = DateTime.UtcNow,
            OrderStatus = dto.OrderStatus,
            OrderAmount = dto.OrderAmount,
            userId = userId,
            OrderProducts = []
        };

        foreach (var pid in dto.ProductIds)
        {
            order.OrderProducts.Add(new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId = pid
            });
        }

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<bool> UpdateOrderAsync(Guid id, OrderDTO dto)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) return false;

        order.OrderDetails = dto.OrderDetails;
        order.OrderDate = dto.OrderDate;
        order.OrderStatus = dto.OrderStatus;
        order.OrderAmount = dto.OrderAmount;
        order.userId = dto.UserId ?? Guid.Empty;

        order.OrderProducts.RemoveAll(op => !dto.ProductIds.Contains(op.ProductId));
        foreach (var pid in dto.ProductIds)
        {
            if (!order.OrderProducts.Any(op => op.ProductId == pid))
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = pid
                });
            }
        }

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
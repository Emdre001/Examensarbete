using Microsoft.EntityFrameworkCore;
using System.Data;
using Models;
using Models.DTO;
using DbContext;

namespace DbRepos;

public class UserDbRepos
{
    private readonly ILogger<UserDbRepos> _logger;
    private readonly MainDbContext _dbContext;
    
    public UserDbRepos(ILogger<UserDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users
            .Include(u => u.Orders)
            .ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _dbContext.Users
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    public async Task<User> CreateUserAsync(UserDTO dto)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.UserEmail,
            Password = dto.UserPassword,
            Address = dto.UserAddress,
            PhoneNr = dto.UserPhoneNr,
            Role = dto.UserRole,
            Orders = await _dbContext.Orders
                .Where(o => dto.OrdersId.Contains(o.OrderId))
                .ToListAsync()
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateUserAsync(Guid id, UserDTO dto)
    {
        var user = await _dbContext.Users
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null) return false;

        user.UserName = dto.UserName;
        user.Email = dto.UserEmail;
        user.Password = dto.UserPassword;
        user.Address = dto.UserAddress;
        user.PhoneNr = dto.UserPhoneNr;
        user.Role = dto.UserRole;

        user.Orders = await _dbContext.Orders
            .Where(o => dto.OrdersId.Contains(o.OrderId))
            .ToListAsync();

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null) return false;

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAllUsersAsync()
    {
        var allUsers = _dbContext.Users;
        _dbContext.Users.RemoveRange(allUsers);
        await _dbContext.SaveChangesAsync();
    }
}

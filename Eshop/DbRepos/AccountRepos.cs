using DbContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace Eshop.DbRepos
{
    public class AccountRepos
    {
        private readonly MainDbContext _context;

        public AccountRepos(MainDbContext context)
        {
            _context = context;
        }

        public async Task<User?> LoginAsync(string identifier, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == identifier || u.Email == identifier && u.Password == password);
        }

        public async Task<bool> RegisterAsync(User newUser)
        {
            var exists = await _context.Users.AnyAsync(u => u.UserName == newUser.UserName);
            if (exists)
                return false;

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<UserDTO?> GetUserByNameAsync(string userName)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return null;

            return new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.Email,
                UserAddress = user.Address ?? string.Empty,
                UserPhoneNr = user.PhoneNr ?? string.Empty,
                OrdersId = user.Orders != null ? [.. user.Orders.Select(o => o.OrderId)] : [],
                UserRole = user.Role
            };
        }

        public async Task<bool> UpdateUserAsync(Guid id, AccountDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Address = dto.Address;
            user.PhoneNr = dto.PhoneNumber;
            user.Orders = [.. dto.Orders.Select(o => new Order
            {
                OrderDetails = o.OrderDetails,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                OrderAmount = o.OrderAmount,
                userId = o.UserId ?? Guid.Empty,
                Products = [.. _context.Products.Where(p => o.ProductIds.Contains(p.ProductId))]
            })];

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
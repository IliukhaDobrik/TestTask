using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;

namespace TestTask.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUser()
        {
            var userWithMaxOrder = await _context.Users.AsNoTracking()
                .Join(_context.Orders.AsNoTracking(),
                    u => u.Id,
                    o => o.UserId,
                    (u, o) => new { User = u, OrderCount = u.Orders.Count() })
                .OrderByDescending(x => x.OrderCount)
                .FirstOrDefaultAsync();

            return userWithMaxOrder?.User;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.AsNoTracking()
                                       .Where(x => x.Status == UserStatus.Inactive)
                                       .ToListAsync();
        }
    }
}

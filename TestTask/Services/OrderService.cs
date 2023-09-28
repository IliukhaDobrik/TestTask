using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private const int minQuantity = 10;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrder()
        {
            return await _context.Orders.AsNoTracking()
                                        .OrderByDescending(x => x.Price * x.Quantity)
                                        .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.AsNoTracking()
                                        .Where(x => x.Quantity > minQuantity)
                                        .ToListAsync();
        }
    }
}

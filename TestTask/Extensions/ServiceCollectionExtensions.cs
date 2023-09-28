using TestTask.Services.Interfaces;
using TestTask.Services;

namespace TestTask.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}

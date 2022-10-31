using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PaymentDbContext>(ops => ops.UseNpgsql(connection));
            services.AddScoped<IPaymentDbContext>(provider => provider.GetService<PaymentDbContext>());
            return services;
        }
    }
}
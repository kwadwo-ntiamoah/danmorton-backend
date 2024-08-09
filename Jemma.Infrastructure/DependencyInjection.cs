using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Application.Common.Abstractions.Services;
using Jemma.Domain.Aggregates;
using Jemma.Infrastructure.Persistence;
using Jemma.Infrastructure.Persistence.Repositories;
using Jemma.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jemma.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDbContext<AppDbContext>(options => options.UseNpgsql(config.GetConnectionString("DbConnection")))
                .AddPersistence()
                .AddCustomIdentity()
                .AddExternalServices();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }

        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 8;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
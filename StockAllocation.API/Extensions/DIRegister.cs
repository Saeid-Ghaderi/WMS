using StockAllocation.Application.Interfaces;
using StockAllocation.Application.MappingProfiles;
using StockAllocation.Application.Services;
using StockAllocation.Infrastructure.Repositories;

namespace StockAllocation.API.Extensions
{
    public static class DIRegister
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISkuRepository, SkuRepository>();
            services.AddScoped<ISkuService, SkuService>();
            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddScoped<IStockAllocatorService, StockAllocatorService>();
            services.AddScoped<IOrderCancellationService, OrderCancellationService>();
            services.AddScoped<ISkuQuantityCorrectionService, SkuQuantityCorrectionService>();
            services.AddScoped<ICustomerOrderService, CustomerOrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

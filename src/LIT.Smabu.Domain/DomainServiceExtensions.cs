using LIT.Smabu.Domain.CustomerAggregate.Services;
using LIT.Smabu.Domain.OrderAggregate.Services;
using LIT.Smabu.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Domain
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<DeleteCustomerService>();
            services.AddScoped<UpdateReferencesService>();
            services.AddScoped<SalesStatisticsService>();
            return services;
        }
    }
}
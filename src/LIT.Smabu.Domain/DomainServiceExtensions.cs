using LIT.Smabu.Domain.CustomerAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Domain
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<DeleteCustomerService>();
            return services;
        }
    }
}
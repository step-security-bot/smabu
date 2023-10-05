using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Service.Business;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LIT.Smabu.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReadModels(this IServiceCollection services)
        {
            var baseClassType = typeof(EntityReadModel<,>);

            var readModelTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.IsClass && x.Name.Contains("ReadModel") && x.BaseType?.Name == baseClassType.Name)
                .ToList();

            foreach (var readModelType in readModelTypes)
            {
                services.AddSingleton(readModelType);
            }
            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<CustomerService, CustomerService>()
                .AddSingleton<InvoiceService, InvoiceService>();
        }
    }
}

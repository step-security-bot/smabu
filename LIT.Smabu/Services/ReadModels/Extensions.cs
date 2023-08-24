using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Service.ReadModels
{
    public static class Extensions
    {
        public static IServiceCollection AddReadModels(this IServiceCollection services)
        {
            return services
                .AddSingleton<CustomerReadModel, CustomerReadModel>()
                .AddSingleton<InvoiceReadModel, InvoiceReadModel>();
        }
    }
}

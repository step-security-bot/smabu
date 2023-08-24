using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Service.Business
{
    public static class Extensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<CustomerService, CustomerService>()
                .AddSingleton<InvoiceService, InvoiceService>();
        }
    }
}

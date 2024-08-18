using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.UseCases
{
    public static class UseCasesServiceExtensions
    {
        public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
        {
            RegisterMediatR(services);
            return services;
        }

        private static void RegisterMediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(UseCasesServiceExtensions)));
        }
    }
}

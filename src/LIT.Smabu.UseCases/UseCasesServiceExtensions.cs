using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.UseCases
{
    public static class UseCasesServiceExtensions
    {
        public static IServiceCollection AddUseCasesServices(this IServiceCollection services, bool isDevelopment)
        {
            RegisterMediatR(services);

            //logger.LogInformation("{Project} services registered", "Infrastructure");
            return services;
        }

        private static void RegisterMediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(UseCasesServiceExtensions)));
        }
    }
}

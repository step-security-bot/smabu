using LIT.Smabu.Infrastructure.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddFileAggregateStore(this IServiceCollection services)
        {
            return services.AddScoped<IAggregateStore, FileAggregateStore>();
        }

        public static IServiceCollection AddAggregateResolver(this IServiceCollection services)
        {
            return services.AddScoped(s => (IAggregateResolver)s.GetRequiredService<IAggregateStore>());
        }
    }
}

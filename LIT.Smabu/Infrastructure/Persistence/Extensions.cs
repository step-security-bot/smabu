using LIT.Smabu.Infrastructure.DDD;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddFileAggregateStore(this IServiceCollection services)
        {
            return services.AddSingleton<IAggregateStore, FileAggregateStore>();
        }
    }
}

using LIT.Smabu.Infrastructure.Cache;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddFileAggregateStore(this IServiceCollection services)
        {
            services.AddSingleton<IUserCache, MemoryUserCache>();
            return services.AddScoped<IAggregateStore, FileAggregateStore>();
        }
    }
}

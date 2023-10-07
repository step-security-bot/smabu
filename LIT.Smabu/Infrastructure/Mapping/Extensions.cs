using LIT.Smabu.Business.Service.Mapping;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Infrastructure.Mapping
{
    public static class Extensions
    {
        public static IServiceCollection AddMapping<TConfiguration>(this IServiceCollection services) where TConfiguration : class, IMapperSettings
        {
            services.AddScoped<IMapperSettings, TConfiguration>();
            services.AddScoped<IMapper, Mapper>();
            return services;
        }
    }
}

using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Infrastructure.Identity.Services;
using LIT.Smabu.Infrastructure.Persistence;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Seed;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LIT.Smabu.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUser, CurrentUserService>();
            RegisterAggregateStore(services);
            RegisterMediatR(services);

            return services;
        }

        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var aggregateStore = scope.ServiceProvider.GetRequiredService<IAggregateStore>();

            var legacyImporter = new ImportLegacyData(aggregateStore);
            await legacyImporter.StartAsync();
        }

        private static void RegisterAggregateStore(IServiceCollection services)
        {
            services.AddScoped<IAggregateStore, CosmosAggregateStore>();
        }

        private static void RegisterMediatR(IServiceCollection services)
        {
            var mediatrOpenTypes = new[]
{
              typeof(IRequestHandler<,>),
              typeof(ICommandHandler<,>),
              typeof(IQueryHandler<,>),
              typeof(INotificationHandler<>),
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}

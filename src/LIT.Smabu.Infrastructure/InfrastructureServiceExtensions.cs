using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Infrastructure.Identity.Services;
using LIT.Smabu.Infrastructure.Persistence;
using LIT.Smabu.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LIT.Smabu.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, bool isDevelopment)
        {
            services.AddScoped<ICurrentUser, CurrentUserService>();

            RegisterAggregateStore(services, isDevelopment);

            RegisterMediatR(services);

            //logger.LogInformation("{Project} services registered", "Infrastructure");
            return services;
        }

        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            //var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

            //await initialiser.InitializeAsync();

            //await initialiser.SeedAsync();
        }
        private static void RegisterAggregateStore(IServiceCollection services, bool isDevelopment)
        {
            services.AddScoped<IAggregateStore, FileAggregateStore>();
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
            //services.AddMediatR(cfg => {
            //    cfg.RegisterServicesFromAssembly(Assembly.GetCallingAssembly());
            //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Customer))!);
            //    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(InfrastructureServiceExtensions))!);

            //    cfg.reg
            //});

            //Assembly.GetAssembly(typeof(Customer))!,
            //Assembly.GetAssembly(typeof(InfrastructureServiceExtensions))!,
            //Assembly.GetAssembly(typeof(CreateCustomerCommand))!
            //foreach (var mediatrOpenType in mediatrOpenTypes)
            //{
            //    builder
            //      .RegisterAssemblyTypes([.. assemblies])
            //      .AsClosedTypesOf(mediatrOpenType)
            //      .AsImplementedInterfaces();
            //}
        }
    }
}

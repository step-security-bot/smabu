using Autofac;
using LIT.Smabu.Infrastructure.Identity.Services;
using LIT.Smabu.Infrastructure.Persistence;
using LIT.Smabu.Shared.Identity;
using LIT.Smabu.Shared.Interfaces;
using MediatR;
using System.Reflection;
using Module = Autofac.Module;

namespace LIT.Smabu.Infrastructure
{
    public class AutofacInfrastructureModule : Module
    {
        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = [];

        public AutofacInfrastructureModule(bool isDevelopment, List<Assembly> assemblies)
        {
            _isDevelopment = isDevelopment;
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCurrentUser(builder);
            RegisterAggregateStore(builder);
            RegisterMediatR(builder);
        }

        private void RegisterCurrentUser(ContainerBuilder builder)
        {
            builder
                .RegisterType<CurrentUserService>()
                .As<ICurrentUser>()
                .InstancePerLifetimeScope();
        }

        private void RegisterAggregateStore(ContainerBuilder builder)
        {
            builder
                .RegisterType<FileAggregateStore>()
                .As<IAggregateStore>()
                .InstancePerLifetimeScope();
        }

        private void RegisterMediatR(ContainerBuilder builder)
        {
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            //builder
            //  .RegisterGeneric(typeof(LoggingBehavior<,>))
            //  .As(typeof(IPipelineBehavior<,>))
            //  .InstancePerLifetimeScope();

            //builder
            //  .RegisterType<MediatRDomainEventDispatcher>()
            //  .As<IDomainEventDispatcher>()
            //  .InstancePerLifetimeScope();

            var mediatrOpenTypes = new[]
            {
              typeof(IRequestHandler<,>),
              typeof(ICommandHandler<,>),
              typeof(IQueryHandler<,>),
              //typeof(IRequestExceptionHandler<,,>),
              //typeof(IRequestExceptionAction<,>),
              typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                  .RegisterAssemblyTypes([.. _assemblies])
                  .AsClosedTypesOf(mediatrOpenType)
                  .AsImplementedInterfaces();
            }
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {

        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {

        }
    }
}

using Autofac;
using LIT.Smabu.UseCases.Customers.Delete;

namespace LIT.Smabu.Domain
{
    public class DefaultDomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DeleteCustomerService>()
                .As<DeleteCustomerService>().InstancePerLifetimeScope();
        }
    }
}
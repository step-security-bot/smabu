using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Customers.Update
{
    public class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand, CustomerId>
    {
        private readonly IAggregateStore aggregateStore;

        public UpdateCustomerHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerId> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.Id);
            customer.Update(request.Name, request.IndustryBranch ?? "", request.MainAddress, request.Communication);
            await aggregateStore.UpdateAsync(customer);
            return customer.Id;
        }
    }
}

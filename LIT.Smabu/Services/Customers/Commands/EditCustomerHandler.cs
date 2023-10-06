using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Commands
{
    public class EditCustomerHandler : IRequestHandler<EditCustomerCommand, CustomerId>
    {
        private readonly IAggregateStore aggregateStore;

        public EditCustomerHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerId> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetAsync(request.Id);
            customer.Edit(request.Name, request.IndustryBranch ?? "", request.MainAddress, request.Communication);
            await aggregateStore.AddOrUpdateAsync(customer);
            return customer.Id;
        }
    }
}

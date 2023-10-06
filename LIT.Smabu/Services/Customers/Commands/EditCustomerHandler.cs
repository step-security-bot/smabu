using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Customers.Commands
{
    public class EditCustomerHandler : RequestHandler<EditCustomerCommand, CustomerId>
    {
        public EditCustomerHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<CustomerId> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await AggregateStore.GetAsync(request.Id);
            customer.Edit(request.Name, request.IndustryBranch ?? "", request.MainAddress, request.Communication);
            await AggregateStore.AddOrUpdateAsync(customer);
            return customer.Id;
        }
    }
}

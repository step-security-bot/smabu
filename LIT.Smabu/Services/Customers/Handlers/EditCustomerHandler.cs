using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Commands;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class EditCustomerHandler : RequestHandler<EditCustomerCommand, CustomerId>
    {
        public EditCustomerHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<CustomerId> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = this.AggregateStore.Get(request.Id);
            customer.Edit(request.Name, request.IndustryBranch ?? "", request.MainAddress, request.Communication);
            await this.AggregateStore.AddOrUpdateAsync(customer);
            return customer.Id;
        }
    }
}

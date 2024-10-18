using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Customers.Update
{
    public class UpdateCustomerHandler(IAggregateStore aggregateStore) : ICommandHandler<UpdateCustomerCommand, CustomerId>
    {
        public async Task<Result<CustomerId>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.Id);
            customer.Update(request.Name, request.IndustryBranch ?? "", request.MainAddress, request.Communication);
            await aggregateStore.UpdateAsync(customer);
            return customer.Id;
        }
    }
}

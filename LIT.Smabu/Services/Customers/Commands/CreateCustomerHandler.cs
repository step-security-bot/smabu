using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Business.Service.Customers.Commands
{
    public class CreateCustomerHandler : RequestHandler<CreateCustomerCommand, CustomerId>
    {
        public CreateCustomerHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<CustomerId> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var number = await CreateNewNumberAsync();
            request.Number = number;
            var customer = Customer.Create(request.Id, request.Number, request.Name, "");
            await AggregateStore.AddOrUpdateAsync(customer);
            return customer.Id;
        }

        private async Task<CustomerNumber> CreateNewNumberAsync()
        {
            var lastNumber = (await AggregateStore.GetAllAsync<Customer>())
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();
            return lastNumber == null ? CustomerNumber.CreateFirst() : CustomerNumber.CreateNext(lastNumber);
        }
    }
}

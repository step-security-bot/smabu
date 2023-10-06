using LIT.Smabu.Domain.Shared.Customers;
using LIT.Smabu.Infrastructure.DDD;
using MediatR;

namespace LIT.Smabu.Business.Service.Customers.Commands
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerId>
    {
        private readonly IAggregateStore aggregateStore;

        public CreateCustomerHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<CustomerId> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var number = await CreateNewNumberAsync();
            request.Number = number;
            var customer = Customer.Create(request.Id, request.Number, request.Name, "");
            await aggregateStore.AddOrUpdateAsync(customer);
            return customer.Id;
        }

        private async Task<CustomerNumber> CreateNewNumberAsync()
        {
            var lastNumber = (await aggregateStore.GetAllAsync<Customer>())
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();
            return lastNumber == null ? CustomerNumber.CreateFirst() : CustomerNumber.CreateNext(lastNumber);
        }
    }
}

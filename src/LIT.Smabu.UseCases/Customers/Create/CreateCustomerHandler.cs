using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;
using MediatR;

namespace LIT.Smabu.UseCases.Customers.Create
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, CustomerId>
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
            await aggregateStore.CreateAsync(customer);
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

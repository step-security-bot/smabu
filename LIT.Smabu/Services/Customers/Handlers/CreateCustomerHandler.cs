using LIT.Smabu.Infrastructure.CQRS;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Domain.Customers;
using LIT.Smabu.Shared.Domain.Customers.Commands;

namespace LIT.Smabu.Business.Service.Customers.Queries
{
    public class CreateCustomerHandler : RequestHandler<CreateCustomerCommand, CustomerId>
    {
        public CreateCustomerHandler(IAggregateStore aggregateStore) : base(aggregateStore)
        {

        }

        public override async Task<CustomerId> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var number = CreateNewNumber();
            request.Number = number;
            var customer = Customer.Create(request.Id, request.Number, request.Name, "");
            await this.AggregateStore.AddOrUpdateAsync(customer);
            return customer.Id;
        }

        private CustomerNumber CreateNewNumber()
        {
            var lastNumber = this.AggregateStore.GetAll<Customer>()
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();
            return lastNumber == null ? CustomerNumber.CreateFirst() : CustomerNumber.CreateNext(lastNumber);
        }
    }
}

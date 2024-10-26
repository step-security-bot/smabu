using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.CustomerAggregate.Specifications;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Customers.Create
{
    public class CreateCustomerHandler(IAggregateStore store) : ICommandHandler<CreateCustomerCommand, CustomerId>
    {
        public async Task<Result<CustomerId>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var number = await CreateNewNumberAsync();
            request.Number = number;
            var customer = Customer.Create(request.Id, request.Number, request.Name, "");
            await store.CreateAsync(customer);
            return customer.Id;
        }

        private async Task<CustomerNumber> CreateNewNumberAsync()
        {
            var lastCustomer = (await store.ApplySpecification(new LastCustomerByNumberSpec())).SingleOrDefault();
            var lastNumber = lastCustomer?.Number;
            return lastNumber == null ? CustomerNumber.CreateFirst() : CustomerNumber.CreateNext(lastNumber);
        }
    }
}

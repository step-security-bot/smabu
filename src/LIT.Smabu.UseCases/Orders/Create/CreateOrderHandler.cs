using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.Domain.OrderAggregate.Specifications;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.Create
{
    public class CreateOrderHandler(IAggregateStore store) : ICommandHandler<CreateOrderCommand, OrderId>
    {
        public async Task<Result<OrderId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var number = request.Number ?? await CreateNewNumberAsync(request.OrderDate.Year);
            var order = Order.Create(request.Id, number, request.CustomerId, request.Name, DateOnly.FromDateTime(request.OrderDate));
            await store.CreateAsync(order);
            return order.Id;
        }

        private async Task<OrderNumber> CreateNewNumberAsync(int year)
        {
            var lastOrder = (await store.ApplySpecification(new LastOrderInYearSpec(year))).SingleOrDefault();
            var lastNumber = lastOrder?.Number;
            return lastNumber == null ? OrderNumber.CreateFirst(year) : OrderNumber.CreateNext(lastNumber);
        }
    }
}

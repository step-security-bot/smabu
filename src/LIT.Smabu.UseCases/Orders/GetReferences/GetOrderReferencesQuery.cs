using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.GetReferences
{
    public record GetOrderReferencesQuery(OrderId OrderId, bool OnlyForCustomer = true) : IQuery<GetOrderReferencesReadModel>
    {
    }
}

using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.List
{
    public record ListOrdersQuery(bool WithReferences = false) : IQuery<OrderDTO[]>
    {

    }
}

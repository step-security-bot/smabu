using LIT.Smabu.Domain.OrderAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Orders.AddReferences
{
    public record UpdateReferencesToOrderCommand(OrderId OrderId, 
        OrderReferences References) : ICommand
    {

    }
}

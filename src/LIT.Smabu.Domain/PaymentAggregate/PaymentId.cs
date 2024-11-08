using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.PaymentAggregate
{
    public record PaymentId(Guid Value)  : EntityId<Payment>(Value)
    {

    }
}
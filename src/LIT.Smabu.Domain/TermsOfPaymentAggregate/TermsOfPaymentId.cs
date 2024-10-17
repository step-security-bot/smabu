using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.TermsOfPaymentAggregate
{
    public record TermsOfPaymentId(Guid Value) : EntityId<TermsOfPayment>(Value);
}
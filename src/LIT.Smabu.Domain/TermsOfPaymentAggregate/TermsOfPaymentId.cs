using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.TermsOfPaymentAggregate
{
    public record TermsOfPaymentId(Guid Value) : EntityId<TermsOfPayment>(Value);
}
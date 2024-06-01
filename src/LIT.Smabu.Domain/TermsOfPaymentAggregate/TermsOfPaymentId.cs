using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.TermsOfPaymentAggregate
{
    public record TermsOfPaymentId(Guid Value) : EntityId<TermsOfPayment>(Value);
}
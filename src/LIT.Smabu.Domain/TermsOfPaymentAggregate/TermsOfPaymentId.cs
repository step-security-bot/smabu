using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.TermsOfPaymentAggregate
{
    public class TermsOfPaymentId(Guid value) : EntityId<TermsOfPayment>(value);
}
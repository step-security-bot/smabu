using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.TermsOfPaymentAggregate
{
    public class TermsOfPaymentId : EntityId<TermsOfPayment>
    {
        public TermsOfPaymentId(Guid value) : base(value)
        {
        }
    }
}
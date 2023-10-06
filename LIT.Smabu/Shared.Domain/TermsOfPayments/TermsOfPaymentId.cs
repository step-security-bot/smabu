using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.TermsOfPayments
{
    public class TermsOfPaymentId : EntityId<TermsOfPayment>
    {
        public TermsOfPaymentId(Guid value) : base(value)
        {
        }
    }
}
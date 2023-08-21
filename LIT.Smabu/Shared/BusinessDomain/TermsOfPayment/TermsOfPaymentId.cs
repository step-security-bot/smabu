using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.TermsOfPayment
{
    public class TermsOfPaymentId : EntityId<ITermsOfPayment>
    {
        public TermsOfPaymentId(Guid value) : base(value)
        {
        }
    }
}
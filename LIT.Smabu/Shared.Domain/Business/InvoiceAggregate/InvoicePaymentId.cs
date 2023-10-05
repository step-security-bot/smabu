using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.InvoiceAggregate
{
    public class InvoicePaymentId : EntityId<InvoicePayment>
    {
        public InvoicePaymentId(Guid value) : base(value)
        {
        }
    }
}
using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class InvoicePaymentId : EntityId<InvoicePayment>
    {
        public InvoicePaymentId(Guid value) : base(value)
        {
        }
    }
}
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class InvoicePaymentId : EntityId<IInvoicePayment>
    {
        public InvoicePaymentId(Guid value) : base(value)
        {
        }
    }
}
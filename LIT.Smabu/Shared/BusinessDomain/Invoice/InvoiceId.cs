using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class InvoiceId : EntityId<IInvoice>
    {
        public InvoiceId(Guid value) : base(value)
        {
        }
    }
}
using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class InvoiceId : EntityId<Invoice>
    {
        public InvoiceId(Guid value) : base(value)
        {
        }
    }
}
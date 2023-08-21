using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Invoice
{
    public class InvoiceLineId : EntityId<InvoiceLine>
    {
        public InvoiceLineId(Guid value) : base(value)
        {
        }
    }
}
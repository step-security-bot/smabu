using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class InvoiceLineId : EntityId<InvoiceLine>
    {
        public InvoiceLineId(Guid value) : base(value)
        {
        }
    }
}
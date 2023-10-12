using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class InvoiceItemId : EntityId<InvoiceItem>
    {
        public InvoiceItemId(Guid value) : base(value)
        {
        }
    }
}
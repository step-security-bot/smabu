using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Invoices
{
    public class InvoiceId : EntityId<Invoice>
    {
        public InvoiceId(Guid value) : base(value)
        {
        }
    }
}
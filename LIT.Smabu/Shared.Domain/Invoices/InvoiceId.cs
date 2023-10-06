using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class InvoiceId : EntityId<Invoice>
    {
        public InvoiceId(Guid value) : base(value)
        {
        }
    }
}
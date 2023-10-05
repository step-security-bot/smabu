using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.InvoiceAggregate
{
    public class InvoiceLineId : EntityId<InvoiceLine>
    {
        public InvoiceLineId(Guid value) : base(value)
        {
        }
    }
}
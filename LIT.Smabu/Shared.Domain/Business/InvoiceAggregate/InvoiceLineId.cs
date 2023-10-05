using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.InvoiceAggregate
{
    public class InvoiceLineId : EntityId<InvoiceLine>
    {
        public InvoiceLineId(Guid value) : base(value)
        {
        }
    }
}
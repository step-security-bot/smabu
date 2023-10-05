using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.Business.InvoiceAggregate
{
    public class InvoiceId : EntityId<Invoice>
    {
        public InvoiceId(Guid value) : base(value)
        {
        }
    }
}
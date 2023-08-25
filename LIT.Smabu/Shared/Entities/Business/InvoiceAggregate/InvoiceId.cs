namespace LIT.Smabu.Shared.Entities.Business.InvoiceAggregate
{
    public class InvoiceId : EntityId<Invoice>
    {
        public InvoiceId(Guid value) : base(value)
        {
        }
    }
}
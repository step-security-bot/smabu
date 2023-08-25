namespace LIT.Smabu.Shared.Entities.Business.InvoiceAggregate
{
    public class InvoiceLineId : EntityId<InvoiceLine>
    {
        public InvoiceLineId(Guid value) : base(value)
        {
        }
    }
}
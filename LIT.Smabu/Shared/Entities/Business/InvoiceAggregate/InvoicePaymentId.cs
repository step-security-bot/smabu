namespace LIT.Smabu.Shared.Entities.Business.InvoiceAggregate
{
    public class InvoicePaymentId : EntityId<InvoicePayment>
    {
        public InvoicePaymentId(Guid value) : base(value)
        {
        }
    }
}
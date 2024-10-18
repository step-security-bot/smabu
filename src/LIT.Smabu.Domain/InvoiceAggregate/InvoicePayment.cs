using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class InvoicePayment : Entity<InvoicePaymentId>
    {
        public override InvoicePaymentId Id => throw new NotImplementedException();
    }
}
using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class InvoicePayment : Entity<InvoicePaymentId>
    {
        public override InvoicePaymentId Id => throw new NotImplementedException();
    }
}
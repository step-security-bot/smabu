using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.InvoiceAggregate
{
    public class InvoicePayment : Entity<InvoicePaymentId>
    {
        public override InvoicePaymentId Id => throw new NotImplementedException();
    }
}
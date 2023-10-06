using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class InvoicePayment : Entity<InvoicePaymentId>
    {
        public override InvoicePaymentId Id => throw new NotImplementedException();
    }
}
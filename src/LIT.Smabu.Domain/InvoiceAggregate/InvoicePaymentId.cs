using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class InvoicePaymentId(Guid value) : EntityId<InvoicePayment>(value);
}
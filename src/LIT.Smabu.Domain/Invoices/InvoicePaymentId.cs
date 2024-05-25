using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoicePaymentId(Guid Value) : EntityId<InvoicePayment>(Value);
}
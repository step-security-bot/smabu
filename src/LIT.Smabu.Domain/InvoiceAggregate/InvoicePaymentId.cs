using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoicePaymentId(Guid Value) : EntityId<InvoicePayment>(Value);
}
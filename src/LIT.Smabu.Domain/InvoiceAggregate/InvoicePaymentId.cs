using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoicePaymentId(Guid Value) : EntityId<InvoicePayment>(Value);
}
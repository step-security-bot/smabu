using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceId(Guid Value) : EntityId<Invoice>(Value);
}
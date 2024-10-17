using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceId(Guid Value) : EntityId<Invoice>(Value);
}
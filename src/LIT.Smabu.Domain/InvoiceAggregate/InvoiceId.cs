using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceId(Guid Value) : EntityId<Invoice>(Value);
}
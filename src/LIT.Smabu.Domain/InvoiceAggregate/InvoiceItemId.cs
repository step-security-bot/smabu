using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceItemId(Guid Value) : EntityId<InvoiceItem>(Value);
}
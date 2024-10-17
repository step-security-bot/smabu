using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceItemId(Guid Value) : EntityId<InvoiceItem>(Value);
}
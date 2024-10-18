using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceItemId(Guid Value) : EntityId<InvoiceItem>(Value);
}
using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceItemId(Guid Value) : EntityId<InvoiceItem>(Value);
}
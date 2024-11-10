using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate.Events
{
    public record InvoiceReleasedEvent(InvoiceId InvoiceId) : IDomainEvent
    {

    }
}

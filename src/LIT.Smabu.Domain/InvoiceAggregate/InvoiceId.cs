using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class InvoiceId(Guid value) : EntityId<Invoice>(value);
}
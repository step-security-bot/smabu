using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public class InvoiceItemId(Guid value) : EntityId<InvoiceItem>(value)
    {

    }
}
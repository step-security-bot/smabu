using LIT.Smabu.Domain.InvoiceAggregate;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public record OrderReferences(HashSet<OfferId> OfferIds, HashSet<InvoiceId> InvoiceIds) : IValueObject
    {
        public IEnumerable<IEntityId> GetAllReferenceIds() => OfferIds.Cast<IEntityId>().Concat(InvoiceIds.Cast<IEntityId>());

        public static OrderReferences Empty => new([], []);
    }
}

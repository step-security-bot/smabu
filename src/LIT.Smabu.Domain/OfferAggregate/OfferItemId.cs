using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public class OfferItemId(Guid value) : EntityId<OfferItem>(value);
}
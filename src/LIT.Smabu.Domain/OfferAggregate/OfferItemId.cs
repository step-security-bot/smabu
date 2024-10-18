using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public record OfferItemId(Guid Value) : EntityId<OfferItem>(Value);
}
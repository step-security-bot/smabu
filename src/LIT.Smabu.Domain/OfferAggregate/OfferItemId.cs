using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public record OfferItemId(Guid Value) : EntityId<OfferItem>(Value);
}
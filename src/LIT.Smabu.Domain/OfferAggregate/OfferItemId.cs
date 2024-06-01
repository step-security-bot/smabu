using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public record OfferItemId(Guid Value) : EntityId<OfferItem>(Value);
}
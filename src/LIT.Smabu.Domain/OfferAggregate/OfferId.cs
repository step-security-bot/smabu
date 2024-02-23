using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public class OfferId(Guid value) : EntityId<Offer>(value)
    {
    }
}

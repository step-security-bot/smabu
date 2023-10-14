using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Offers
{
    public class OfferItemId : EntityId<OfferItem>
    {
        public OfferItemId(Guid value) : base(value)
        {
        }
    }
}
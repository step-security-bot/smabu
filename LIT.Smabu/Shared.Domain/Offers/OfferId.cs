using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Offers
{
    public class OfferId : EntityId<Offer>
    {
        public OfferId(Guid value) : base(value)
        {
        }
    }
}

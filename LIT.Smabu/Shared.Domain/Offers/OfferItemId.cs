using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public class OfferItemId : EntityId<OfferItem>
    {
        public OfferItemId(Guid value) : base(value)
        {
        }
    }
}
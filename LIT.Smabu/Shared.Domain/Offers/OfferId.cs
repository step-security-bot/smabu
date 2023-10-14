using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Domain.Shared.Invoices;

namespace LIT.Smabu.Domain.Shared.Offers
{
    public class OfferId : EntityId<Offer>
    {
        public OfferId(Guid value) : base(value)
        {
        }
    }
}

using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Offers;

namespace LIT.Smabu.Shared.Offers
{
    public class AddOfferItemDTO
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
        public string Details { get; set; } = string.Empty;
        public Quantity Quantity { get; set; } = Quantity.Empty();
        public decimal UnitPrice { get; set; } = 0;
    }
}

using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Domain.Shared.Offers;

namespace LIT.Smabu.Shared.Offers
{
    public class EditOfferItemDTO
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}

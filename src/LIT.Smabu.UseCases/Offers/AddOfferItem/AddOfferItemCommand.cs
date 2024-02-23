using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Offers.AddOfferItem
{
    public record AddOfferItemCommand : ICommand<OfferItemDTO>
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}

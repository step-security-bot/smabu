using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.UpdateOfferItem
{
    public record UpdateOfferItemCommand : ICommand
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
        public required string Details { get; set; }
        public required Quantity Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}

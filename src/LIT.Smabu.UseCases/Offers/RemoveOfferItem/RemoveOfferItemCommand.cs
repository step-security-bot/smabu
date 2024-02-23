using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Offers.RemoveOfferItem
{
    public record RemoveOfferItemCommand : ICommand<bool>
    {
        public required OfferItemId Id { get; set; }
        public required OfferId OfferId { get; set; }
    }
}

using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers
{
    public record OfferItemDTO(OfferItemId Id, OfferId OfferId, int Position, string Details, Quantity Quantity, decimal UnitPrice, decimal TotalPrice, ProductId? ProductId) : IDTO
    {
        public string DisplayName => Position.ToString();

        public static OfferItemDTO CreateFrom(OfferItem offerItem)
        {
            return new(offerItem.Id, offerItem.OfferId, offerItem.Position, offerItem.Details,
                offerItem.Quantity, offerItem.UnitPrice, offerItem.TotalPrice, offerItem.ProductId);
        }
    }
}
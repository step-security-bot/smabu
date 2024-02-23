using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.ProductAggregate;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.UseCases.Offers
{
    public class OfferItemDTO : IDTO
    {
        public string DisplayName => Position.ToString();
        public OfferItemId Id { get; set; }
        public OfferId OfferId { get; set; }
        public int Position { get; set; }
        public string Details { get; set; }
        public Quantity Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductId? ProductId { get; set; }


        public static OfferItemDTO CreateFrom(OfferItem offerItem)
        {
            return new()
            {
                Id = offerItem.Id,
                OfferId = offerItem.OfferId,
                Position = offerItem.Position,
                Details = offerItem.Details,
                Quantity = offerItem.Quantity,
                UnitPrice = offerItem.UnitPrice,
                TotalPrice = offerItem.TotalPrice,
                ProductId = offerItem.ProductId,
            };
        }
    }
}
using LIT.Smabu.Domain.Shared.Common;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Domain.Shared.Products;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Offers
{
    public class OfferItemDTO : IDTO
    {
        public OfferItemId Id { get; set; }
        public OfferId OfferId { get; set; }
        public int Position { get; set; }
        public string Details { get; set; }
        public Quantity Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductId? ProductId { get; set; }
    }
}
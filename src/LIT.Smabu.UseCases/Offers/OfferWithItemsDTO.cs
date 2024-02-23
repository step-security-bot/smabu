using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Domain.OfferAggregate;

namespace LIT.Smabu.UseCases.Offers
{
    public record OfferWithItemsDTO : OfferDTO
    {
        protected OfferWithItemsDTO(OfferDTO original, List<OfferItemDTO> items) : base(original)
        {
            Items = items;
        }

        public List<OfferItemDTO> Items { get; set; }

        public new static OfferWithItemsDTO CreateFrom(Offer offer, Customer customer)
        {
            return new(OfferDTO.CreateFrom(offer, customer), offer.Items.Select(OfferItemDTO.CreateFrom).ToList());
        }
    }
}
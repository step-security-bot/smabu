using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Offers;

namespace LIT.Smabu.UseCases.Offers.GetWithItems
{
    public record GetOfferWithItemsQuery : IQuery<OfferWithItemsDTO>
    {
        public GetOfferWithItemsQuery(OfferId id)
        {
            Id = id;
        }

        public OfferId Id { get; }
    }
}
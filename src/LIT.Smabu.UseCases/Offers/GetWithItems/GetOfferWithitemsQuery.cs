using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.UseCases.Offers;
using LIT.Smabu.UseCases.SeedWork;

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
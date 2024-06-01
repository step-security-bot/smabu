using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.GetWithItems
{
    public class GetOfferWithItemsHandler(IAggregateStore aggregateStore) : IQueryHandler<GetOfferWithItemsQuery, OfferWithItemsDTO>
    {
        public async Task<OfferWithItemsDTO> Handle(GetOfferWithItemsQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            return OfferWithItemsDTO.CreateFrom(offer, customer);
        }
    }
}

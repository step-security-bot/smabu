using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.List
{
    public class GetOffersHandler(IAggregateStore aggregateStore) : IQueryHandler<ListOffersQuery, OfferDTO[]>
    {
        public async Task<Result<OfferDTO[]>> Handle(ListOffersQuery request, CancellationToken cancellationToken)
        {
            var offers = await aggregateStore.GetAllAsync<Offer>();
            var customers = await aggregateStore.GetByAsync(offers.Select(x => x.CustomerId).Distinct());
            return offers.Select(x => OfferDTO.Create(x, customers[x.CustomerId])).OrderByDescending(x => x.Number).ToArray();
        }
    }
}

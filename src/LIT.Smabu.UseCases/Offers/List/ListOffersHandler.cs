using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.Offers;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.List
{
    public class GetOffersHandler : IRequestHandler<ListOffersQuery, OfferDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public GetOffersHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO[]> Handle(ListOffersQuery request, CancellationToken cancellationToken)
        {
            var offers = await aggregateStore.GetAllAsync<Offer>();
            var customers = await aggregateStore.GetByAsync(offers.Select(x => x.CustomerId).Distinct());
            return offers.Select(x => OfferDTO.CreateFrom(x, customers[x.CustomerId]))
                .OrderByDescending(x => x.Number)
                .ToArray();
        }
    }
}

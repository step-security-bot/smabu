using LIT.Smabu.Business.Service.Offers.Mappers;
using LIT.Smabu.Domain.Shared.Invoices;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Queries
{
    public class GetOffersHandler : IRequestHandler<GetOffersQuery, OfferDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public GetOffersHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO[]> Handle(GetOffersQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetAllAsync<Offer>();
            var result = (await new OfferMapper(aggregateStore).MapAsync(offer)).Values
                .OrderByDescending(x => x.Number)
                .ToArray();
            return result;
        }
    }
}

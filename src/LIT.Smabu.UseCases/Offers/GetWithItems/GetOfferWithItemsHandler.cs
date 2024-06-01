using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.Offers;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.GetWithItems
{
    public class GetOfferWithItemsHandler : IRequestHandler<GetOfferWithItemsQuery, OfferWithItemsDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetOfferWithItemsHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferWithItemsDTO> Handle(GetOfferWithItemsQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            return OfferWithItemsDTO.CreateFrom(offer, customer);
        }
    }
}

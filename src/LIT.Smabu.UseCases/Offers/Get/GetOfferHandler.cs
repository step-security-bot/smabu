using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.Offers;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.Get
{
    public class GetOfferHandler : IRequestHandler<GetOfferQuery, OfferDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetOfferHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            return OfferDTO.CreateFrom(offer, customer);
        }
    }
}

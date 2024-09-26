using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.Get
{
    public class GetOfferHandler(IAggregateStore aggregateStore) : IQueryHandler<GetOfferQuery, OfferDTO>
    {
        public async Task<Result<OfferDTO>> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            return OfferDTO.CreateFrom(offer, customer);
        }
    }
}

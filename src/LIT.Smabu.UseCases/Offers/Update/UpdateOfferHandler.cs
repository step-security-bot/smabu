using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Offers;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.Update
{
    public class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, OfferDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public UpdateOfferHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            var customer = await aggregateStore.GetByAsync(offer.CustomerId);
            offer.Update(request.Tax, request.TaxDetails, request.OfferDate, request.ExpiresOn);
            await aggregateStore.UpdateAsync(offer);
            return OfferDTO.CreateFrom(offer, customer);
        }
    }
}

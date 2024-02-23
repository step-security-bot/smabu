using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.Offers;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.AddOfferItem
{
    public class AddOfferItemHandler : IRequestHandler<AddOfferItemCommand, OfferItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public AddOfferItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferItemDTO> Handle(AddOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            var offerItem = offer.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(offer);
            return OfferItemDTO.CreateFrom(offerItem);
        }
    }
}

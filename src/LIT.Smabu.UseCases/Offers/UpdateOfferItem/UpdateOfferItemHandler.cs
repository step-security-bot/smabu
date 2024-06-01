using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.UpdateOfferItem
{
    public class EditOfferItemHandler(IAggregateStore aggregateStore) : ICommandHandler<UpdateOfferItemCommand, OfferItemDTO>
    {
        public async Task<OfferItemDTO> Handle(UpdateOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            var offerItem = offer.UpdateItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(offer);
            return OfferItemDTO.CreateFrom(offerItem);
        }
    }
}

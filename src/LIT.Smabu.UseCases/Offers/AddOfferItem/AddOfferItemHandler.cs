using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.AddOfferItem
{
    public class AddOfferItemHandler(IAggregateStore aggregateStore) : ICommandHandler<AddOfferItemCommand, OfferItemDTO>
    {
        public async Task<OfferItemDTO> Handle(AddOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            var offerItem = offer.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(offer);
            return OfferItemDTO.CreateFrom(offerItem);
        }
    }
}

using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.AddOfferItem
{
    public class AddOfferItemHandler(IAggregateStore aggregateStore) : ICommandHandler<AddOfferItemCommand, OfferItemId>
    {
        public async Task<Result<OfferItemId>> Handle(AddOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            offer.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(offer);
            return request.Id;
        }
    }
}

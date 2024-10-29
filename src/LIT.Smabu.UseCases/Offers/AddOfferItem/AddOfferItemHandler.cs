using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Offers.AddOfferItem
{
    public class AddOfferItemHandler(IAggregateStore store) : ICommandHandler<AddOfferItemCommand, OfferItemId>
    {
        public async Task<Result<OfferItemId>> Handle(AddOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await store.GetByAsync(request.OfferId);
            offer.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await store.UpdateAsync(offer);
            return request.Id;
        }
    }
}

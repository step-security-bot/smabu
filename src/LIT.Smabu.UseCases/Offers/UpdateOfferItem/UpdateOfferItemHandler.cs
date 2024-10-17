using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.UpdateOfferItem
{
    public class EditOfferItemHandler(IAggregateStore aggregateStore) : ICommandHandler<UpdateOfferItemCommand>
    {
        public async Task<Result> Handle(UpdateOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            offer.UpdateItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.UpdateAsync(offer);
            return Result.Success();
        }
    }
}

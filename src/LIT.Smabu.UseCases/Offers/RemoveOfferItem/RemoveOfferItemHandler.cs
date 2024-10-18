using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Offers.RemoveOfferItem
{
    public class RemoveOfferItemHandler(IAggregateStore aggregateStore) : ICommandHandler<RemoveOfferItemCommand, bool>
    {
        public async Task<Result<bool>> Handle(RemoveOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            offer.RemoveItem(request.Id);
            await aggregateStore.UpdateAsync(offer);
            return true;
        }
    }
}

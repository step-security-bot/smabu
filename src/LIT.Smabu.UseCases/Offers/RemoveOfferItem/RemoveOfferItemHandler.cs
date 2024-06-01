using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.RemoveOfferItem
{
    public class RemoveOfferItemHandler : ICommandHandler<RemoveOfferItemCommand, bool>
    {
        private readonly IAggregateStore aggregateStore;

        public RemoveOfferItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<bool> Handle(RemoveOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            offer.RemoveItem(request.Id);
            await aggregateStore.UpdateAsync(offer);
            return true;
        }
    }
}

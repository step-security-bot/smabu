using LIT.Smabu.Infrastructure.Shared.Contracts;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public class RemoveOfferItemHandler : IRequestHandler<RemoveOfferItemCommand>
    {
        private readonly IAggregateStore aggregateStore;

        public RemoveOfferItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task Handle(RemoveOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            offer.RemoveItem(request.Id);
            await aggregateStore.AddOrUpdateAsync(offer);
        }
    }
}

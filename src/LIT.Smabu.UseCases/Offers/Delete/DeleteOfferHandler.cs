using LIT.Smabu.Shared.Interfaces;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.Delete
{
    public class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand, bool>
    {
        private readonly IAggregateStore aggregateStore;

        public DeleteOfferHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            offer.Delete();
            await aggregateStore.DeleteAsync(offer);
            return true;
        }
    }
}

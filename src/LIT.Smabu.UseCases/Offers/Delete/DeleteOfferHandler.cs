using LIT.Smabu.Domain.SeedWork;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.Delete
{
    public class DeleteOfferHandler(IAggregateStore aggregateStore) : IRequestHandler<DeleteOfferCommand, bool>
    {
        private readonly IAggregateStore aggregateStore = aggregateStore;

        public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            offer.Delete();
            await aggregateStore.DeleteAsync(offer);
            return true;
        }
    }
}

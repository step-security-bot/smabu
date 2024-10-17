using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.Delete
{
    public class DeleteOfferHandler(IAggregateStore aggregateStore) : ICommandHandler<DeleteOfferCommand, bool>
    {
        public async Task<Result<bool>> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            offer.Delete();
            await aggregateStore.DeleteAsync(offer);
            return true;
        }
    }
}

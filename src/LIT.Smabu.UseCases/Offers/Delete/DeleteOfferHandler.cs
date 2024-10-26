using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Offers.Delete
{
    public class DeleteOfferHandler(IAggregateStore store) : ICommandHandler<DeleteOfferCommand, bool>
    {
        public async Task<Result<bool>> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await store.GetByAsync(request.Id);
            offer.Delete();
            await store.DeleteAsync(offer);
            return true;
        }
    }
}

using LIT.Smabu.Business.Service.Offers.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public class EditOfferItemHandler : IRequestHandler<EditOfferItemCommand, OfferItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public EditOfferItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferItemDTO> Handle(EditOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            var offerItem = offer.EditItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.AddOrUpdateAsync(offer);
            return new OfferMapper(aggregateStore).Map(offerItem);
        }
    }
}

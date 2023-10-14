using LIT.Smabu.Business.Service.Invoices.Mappings;
using LIT.Smabu.Business.Service.Offers.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public class AddOfferItemHandler : IRequestHandler<AddOfferItemCommand, OfferItemDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public AddOfferItemHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferItemDTO> Handle(AddOfferItemCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.OfferId);
            var offerItem = offer.AddItem(request.Id, request.Details, request.Quantity, request.UnitPrice);
            await aggregateStore.AddOrUpdateAsync(offer);
            return new OfferMapper(aggregateStore).Map(offerItem);
        }
    }
}

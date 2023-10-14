using LIT.Smabu.Business.Service.Invoices.Mappings;
using LIT.Smabu.Business.Service.Offers.Mappers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public class EditOfferHandler : IRequestHandler<EditOfferCommand, OfferDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public EditOfferHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO> Handle(EditOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await aggregateStore.GetByAsync(request.Id);
            offer.Edit(request.Tax, request.TaxDetails, request.OfferDate, request.ExpiresOn);
            await aggregateStore.AddOrUpdateAsync(offer);
            return await new OfferMapper(aggregateStore).MapAsync(offer);
        }
    }
}

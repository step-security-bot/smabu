using LIT.Smabu.Business.Service.Offers.Mappers;
using LIT.Smabu.Domain.Shared.Offers;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Offers;
using MediatR;

namespace LIT.Smabu.Business.Service.Offers.Commands
{
    public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, OfferDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public CreateOfferHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<OfferDTO> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.CustomerId);
            var number = request.Number ?? await CreateNewNumberAsync();
            var offer = Offer.Create(request.Id, request.CustomerId, number, customer.MainAddress,
                request.Currency, request.Tax, request.TaxDetails);
            await aggregateStore.AddOrUpdateAsync(offer);
            return await new OfferMapper(aggregateStore).MapAsync(offer);
        }

        private async Task<OfferNumber> CreateNewNumberAsync()
        {
            var lastNumber = (await aggregateStore.GetAllAsync<Offer>())
                .Select(x => x.Number)
                .OrderByDescending(x => x)
                .FirstOrDefault();

            return lastNumber == null ? new OfferNumber(202) : new OfferNumber(lastNumber.Value + 1);
        }
    }
}

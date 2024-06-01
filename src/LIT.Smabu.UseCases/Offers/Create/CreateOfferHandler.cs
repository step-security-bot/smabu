using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.Offers.Create
{
    public class CreateOfferHandler(IAggregateStore aggregateStore) : ICommandHandler<CreateOfferCommand, OfferDTO>
    {
        public async Task<OfferDTO> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var customer = await aggregateStore.GetByAsync(request.CustomerId);
            var number = request.Number ?? await CreateNewNumberAsync();
            var offer = Offer.Create(request.Id, request.CustomerId, number, customer.MainAddress,
                request.Currency, request.Tax, request.TaxDetails ?? "");
            await aggregateStore.CreateAsync(offer);
            return OfferDTO.CreateFrom(offer, customer);
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

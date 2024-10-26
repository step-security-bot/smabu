using LIT.Smabu.Domain.Common;
using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.OfferAggregate.Specifications;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Offers.Create
{
    public class CreateOfferHandler(IAggregateStore store) : ICommandHandler<CreateOfferCommand, OfferId>
    {
        public async Task<Result<OfferId>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var customer = await store.GetByAsync(request.CustomerId);
            var number = request.Number ?? await CreateNewNumberAsync();
            var offer = Offer.Create(request.Id, request.CustomerId, number, customer.MainAddress,
                request.Currency, request.TaxRate ?? TaxRate.Default);
            await store.CreateAsync(offer);
            return offer.Id;
        }

        private async Task<OfferNumber> CreateNewNumberAsync()
        {
            var lastOffer = (await store.ApplySpecification(new LastOfferSpec())).SingleOrDefault();
            var lastNumber = lastOffer?.Number;

            return lastNumber == null ? new OfferNumber(202) : new OfferNumber(lastNumber.Value + 1);
        }
    }
}

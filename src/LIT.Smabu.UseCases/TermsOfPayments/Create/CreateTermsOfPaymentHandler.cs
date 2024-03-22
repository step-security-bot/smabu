using LIT.Smabu.Domain.OfferAggregate;
using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.TermsOfPayments;
using MediatR;

namespace LIT.Smabu.UseCases.TermsOfPayments.Create
{
    public class CreateTermsOfPaymentHandler : IRequestHandler<CreateTermsOfPaymentCommand, TermsOfPaymentDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public CreateTermsOfPaymentHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<TermsOfPaymentDTO> Handle(CreateTermsOfPaymentCommand request, CancellationToken cancellationToken)
        {
            var termsOfPayment = TermsOfPayment.Create(request.Id, request.Title, request.Details, request.DueDays);
            await aggregateStore.CreateAsync(termsOfPayment);
            return TermsOfPaymentDTO.CreateFrom(termsOfPayment);
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

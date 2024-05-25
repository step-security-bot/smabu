using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared.Interfaces;
using MediatR;

namespace LIT.Smabu.UseCases.TermsOfPayments.Create
{
    public class CreateTermsOfPaymentHandler(IAggregateStore aggregateStore) : IRequestHandler<CreateTermsOfPaymentCommand, TermsOfPaymentDTO>
    {
        private readonly IAggregateStore aggregateStore = aggregateStore;

        public async Task<TermsOfPaymentDTO> Handle(CreateTermsOfPaymentCommand request, CancellationToken cancellationToken)
        {
            var termsOfPayment = TermsOfPayment.Create(request.Id, request.Title, request.Details, request.DueDays);
            await aggregateStore.CreateAsync(termsOfPayment);
            return TermsOfPaymentDTO.CreateFrom(termsOfPayment);
        }
    }
}

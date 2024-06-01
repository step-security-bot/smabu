using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.TermsOfPayments.Create
{
    public class CreateTermsOfPaymentHandler(IAggregateStore aggregateStore) : ICommandHandler<CreateTermsOfPaymentCommand, TermsOfPaymentDTO>
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

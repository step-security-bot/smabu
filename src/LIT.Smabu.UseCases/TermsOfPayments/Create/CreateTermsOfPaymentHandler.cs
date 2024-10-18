using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.TermsOfPayments.Create
{
    public class CreateTermsOfPaymentHandler(IAggregateStore aggregateStore) : ICommandHandler<CreateTermsOfPaymentCommand, TermsOfPaymentDTO>
    {
        private readonly IAggregateStore aggregateStore = aggregateStore;

        public async Task<Result<TermsOfPaymentDTO>> Handle(CreateTermsOfPaymentCommand request, CancellationToken cancellationToken)
        {
            var termsOfPayment = TermsOfPayment.Create(request.Id, request.Title, request.Details, request.DueDays);
            await aggregateStore.CreateAsync(termsOfPayment);
            return TermsOfPaymentDTO.CreateFrom(termsOfPayment);
        }
    }
}

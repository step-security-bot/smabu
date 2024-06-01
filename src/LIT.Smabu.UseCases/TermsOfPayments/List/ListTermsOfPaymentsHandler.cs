using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.UseCases.TermsOfPayments;
using MediatR;

namespace LIT.Smabu.UseCases.TermsOfPayments.List
{
    public class GetTermsOfPaymentsHandler(IAggregateStore aggregateStore) : IRequestHandler<ListTermsOfPaymentsQuery, TermsOfPaymentDTO[]>
    {
        public async Task<TermsOfPaymentDTO[]> Handle(ListTermsOfPaymentsQuery request, CancellationToken cancellationToken)
        {
            var termsOfPayments = await aggregateStore.GetAllAsync<TermsOfPayment>();
            return [.. termsOfPayments.Select(x => TermsOfPaymentDTO.CreateFrom(x)).OrderByDescending(x => x.Title)];
        }
    }
}

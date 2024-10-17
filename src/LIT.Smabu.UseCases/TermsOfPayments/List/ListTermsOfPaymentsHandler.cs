using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.TermsOfPayments.List
{
    public class GetTermsOfPaymentsHandler(IAggregateStore aggregateStore) : IQueryHandler<ListTermsOfPaymentsQuery, TermsOfPaymentDTO[]>
    {
        public async Task<Result<TermsOfPaymentDTO[]>> Handle(ListTermsOfPaymentsQuery request, CancellationToken cancellationToken)
        {
            var termsOfPayments = await aggregateStore.GetAllAsync<TermsOfPayment>();
            return termsOfPayments.Select(x => TermsOfPaymentDTO.CreateFrom(x)).OrderByDescending(x => x.Title).ToArray();
        }
    }
}

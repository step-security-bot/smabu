using LIT.Smabu.Domain.Shared;
using LIT.Smabu.UseCases.SeedWork;

namespace LIT.Smabu.UseCases.TermsOfPayments.Get
{
    public class GetTermsOfPaymentHandler(IAggregateStore aggregateStore) : IQueryHandler<GetTermsOfPaymentQuery, TermsOfPaymentDTO>
    {
        public async Task<Result<TermsOfPaymentDTO>> Handle(GetTermsOfPaymentQuery request, CancellationToken cancellationToken)
        {
            var termsOfPayment = await aggregateStore.GetByAsync(request.Id);
            return TermsOfPaymentDTO.CreateFrom(termsOfPayment);
        }
    }
}

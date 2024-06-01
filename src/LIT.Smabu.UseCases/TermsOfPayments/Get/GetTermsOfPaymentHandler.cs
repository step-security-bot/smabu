using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.UseCases.TermsOfPayments;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.List
{
    public class GetTermsOfPaymentHandler : IRequestHandler<GetTermsOfPaymentQuery, TermsOfPaymentDTO>
    {
        private readonly IAggregateStore aggregateStore;

        public GetTermsOfPaymentHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<TermsOfPaymentDTO> Handle(GetTermsOfPaymentQuery request, CancellationToken cancellationToken)
        {
            var termsOfPayment = await aggregateStore.GetByAsync(request.Id);
            return TermsOfPaymentDTO.CreateFrom(termsOfPayment);
        }
    }
}

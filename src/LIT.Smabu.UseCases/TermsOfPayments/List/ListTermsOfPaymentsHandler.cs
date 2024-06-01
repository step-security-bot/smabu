using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.TermsOfPayments;
using MediatR;

namespace LIT.Smabu.UseCases.Offers.List
{
    public class GetTermsOfPaymentsHandler : IRequestHandler<ListTermsOfPaymentsQuery, TermsOfPaymentDTO[]>
    {
        private readonly IAggregateStore aggregateStore;

        public GetTermsOfPaymentsHandler(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        public async Task<TermsOfPaymentDTO[]> Handle(ListTermsOfPaymentsQuery request, CancellationToken cancellationToken)
        {
            var termsOfPayments = await aggregateStore.GetAllAsync<TermsOfPayment>();
            return termsOfPayments.Select(x => TermsOfPaymentDTO.CreateFrom(x))
                .OrderByDescending(x => x.Title)
                .ToArray();
        }
    }
}

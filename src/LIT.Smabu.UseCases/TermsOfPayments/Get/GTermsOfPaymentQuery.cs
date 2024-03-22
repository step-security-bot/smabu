using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.TermsOfPayments;

namespace LIT.Smabu.UseCases.Offers.List
{
    public record GetTermsOfPaymentQuery : IQuery<TermsOfPaymentDTO>
    {
        public GetTermsOfPaymentQuery(TermsOfPaymentId id)
        {
            Id = id;
        }

        public TermsOfPaymentId Id { get; }
    }
}

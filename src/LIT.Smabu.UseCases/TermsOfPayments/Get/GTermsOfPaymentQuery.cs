using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.UseCases.Shared;
using LIT.Smabu.UseCases.TermsOfPayments;

namespace LIT.Smabu.UseCases.TermsOfPayments.Get
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

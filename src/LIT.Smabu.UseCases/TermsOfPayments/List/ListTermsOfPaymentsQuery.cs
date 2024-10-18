using LIT.Smabu.UseCases.Shared;
using LIT.Smabu.UseCases.TermsOfPayments;

namespace LIT.Smabu.UseCases.TermsOfPayments.List
{
    public record ListTermsOfPaymentsQuery : IQuery<TermsOfPaymentDTO[]>
    {
    }
}

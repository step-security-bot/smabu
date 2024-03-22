using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.TermsOfPayments;

namespace LIT.Smabu.UseCases.Offers.List
{
    public record ListTermsOfPaymentsQuery : IQuery<TermsOfPaymentDTO[]>
    {
    }
}

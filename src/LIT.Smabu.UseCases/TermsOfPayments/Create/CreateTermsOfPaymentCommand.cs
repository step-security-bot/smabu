using LIT.Smabu.Domain.TermsOfPaymentAggregate;
using LIT.Smabu.Shared.Interfaces;
using LIT.Smabu.UseCases.TermsOfPayments;

namespace LIT.Smabu.UseCases.TermsOfPayments.Create
{
    public record CreateTermsOfPaymentCommand : ICommand<TermsOfPaymentDTO>
    {
        public CreateTermsOfPaymentCommand(TermsOfPaymentId id, string title, string details, int? dueDays)
        {
            Id = id;
            Title = title;
            Details = details;
            DueDays = dueDays;
        }

        public TermsOfPaymentId Id { get; }
        public string Title { get; }
        public string Details { get; }
        public int? DueDays { get; }
    }
}

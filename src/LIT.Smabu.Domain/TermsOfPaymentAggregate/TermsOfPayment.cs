using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.TermsOfPaymentAggregate
{
    public class TermsOfPayment : AggregateRoot<TermsOfPaymentId>
    {
        public override TermsOfPaymentId Id { get; }
        public string Title { get; private set; }
        public string Details { get; private set; }
        public int? DueDays { get; private set; }

        public TermsOfPayment(TermsOfPaymentId id, string title, string details, int? dueDays)
        {
            Id = id;
            Title = title;
            Details = details;
            DueDays = dueDays;
        }

        public static TermsOfPayment Create(TermsOfPaymentId id, string title, string details, int? dueDays)
        {
            return new(id, title, details, dueDays);
        }
    }
}

using LIT.Smabu.Domain.PaymentAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Payments.Complete
{
    public record CompletePaymentCommand : ICommand
    {
        public PaymentId Id { get; }
        public decimal Amount { get; }
        public DateTime PaidAt { get; }

        public CompletePaymentCommand(PaymentId id, decimal amount, DateTime paidAt)
        {
            Id = id;
            Amount = amount;
            PaidAt = paidAt;
        }
    }
}

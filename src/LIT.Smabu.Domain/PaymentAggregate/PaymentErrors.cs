using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.PaymentAggregate
{
    public static class PaymentErrors
    {
        public static Error StatusMustNotBeNull => new("Payment.StatusIsNull", "Status must not be null.");

        public static Error PaymentAlreadyPaid => new("Payment.AlreadyPaid", "Payment is already paid.");
    }
}

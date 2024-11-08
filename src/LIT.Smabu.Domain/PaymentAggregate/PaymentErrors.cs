using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.PaymentAggregate
{
    public static class PaymentErrors
    {
        public static Error StatusMustNotBeNull => new("Payment.StatusIsNull", "Status must not be null.");
        public static Error PaymentAlreadyPaid => new("Payment.AlreadyPaid", "Payment is already paid.");
        public static Error InvalidCreate => new("Payment.InvalidCreate", "Arguments for creation are invalid.");
        public static Error NotFound => new("Payment.NotFound", "Payment not found.");
    }
}

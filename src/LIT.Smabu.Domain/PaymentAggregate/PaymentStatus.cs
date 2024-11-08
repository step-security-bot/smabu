using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.PaymentAggregate
{
    public record PaymentStatus : SimpleValueObject<string>
    {
        private const string PendingKey = "Pending";
        private const string PaidKey = "Paid";
        private const string CancelledKey = "Cancelled";
        private const string PartialKey = "Partial";

        public PaymentStatus(string value) : base(value)
        {
            if (new[] { PendingKey, PaidKey, CancelledKey, PartialKey }.Contains(value) == false)
            {
                throw new ArgumentException("Invalid payment status value.");
            }
        }

        public static PaymentStatus Pending { get; } = new PaymentStatus(PendingKey);
        public static PaymentStatus Partial { get; } = new PaymentStatus(PartialKey);
        public static PaymentStatus Paid { get; } = new PaymentStatus(PaidKey);
        public static PaymentStatus Cancelled { get; } = new PaymentStatus(CancelledKey);

        public static PaymentStatus[] GetAll() => [Pending, Partial, Paid, Cancelled];
    }
}

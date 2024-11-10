using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.PaymentAggregate
{
    public record PaymentNumber(long Value) : BusinessNumber(Value)
    {
        public override string ShortForm => "PAY";

        public override int Digits => 8;

        public static PaymentNumber CreateFirst()
        {
            return new PaymentNumber(int.Parse(1.ToString("00000000")));
        }

        public static PaymentNumber CreateNext(PaymentNumber lastNumber)
        {
            return new PaymentNumber(lastNumber.Value + 1);
        }
    }
}

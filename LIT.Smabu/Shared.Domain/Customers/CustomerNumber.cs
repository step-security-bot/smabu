using LIT.Smabu.Domain.Shared.Common;

namespace LIT.Smabu.Domain.Shared.Customers
{
    public record CustomerNumber : BusinessNumber
    {
        public CustomerNumber(long value) : base(value) { }

        public override string ShortForm => "CUS";

        public override int Digits => 4;

        public static CustomerNumber CreateFirst()
        {
            return new CustomerNumber(1);
        }

        public static CustomerNumber CreateNext(CustomerNumber lastNumber)
        {
            return new CustomerNumber(lastNumber.Value + 1);
        }
    }
}
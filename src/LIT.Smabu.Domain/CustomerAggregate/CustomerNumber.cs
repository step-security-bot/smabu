using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.CustomerAggregate
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
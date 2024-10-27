using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.OrderAggregate
{
    public record OrderNumber(long Value) : BusinessNumber(Value)
    {
        public override string ShortForm => "ORD";

        public override int Digits => 8;

        public static OrderNumber CreateFirst(int year)
        {
            return new OrderNumber(int.Parse(year.ToString() + 1.ToString("0000")));
        }

        public static OrderNumber CreateNext(OrderNumber lastNumber)
        {
            return new OrderNumber(lastNumber.Value + 1);
        }
    }
}
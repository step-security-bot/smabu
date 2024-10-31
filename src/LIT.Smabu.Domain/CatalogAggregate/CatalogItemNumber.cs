using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public record class CatalogItemNumber(long Value) : BusinessNumber(Value)
    {
        public override string ShortForm { get; } = "CAT";

        public override int Digits { get; } = 4;

        public static CatalogItemNumber CreateFirst(int year)
        {
            return new CatalogItemNumber(int.Parse(1.ToString("0000")));
        }

        public static CatalogItemNumber CreateNext(CatalogItemNumber lastNumber)
        {
            return new CatalogItemNumber(lastNumber.Value + 1);
        }
    }
}
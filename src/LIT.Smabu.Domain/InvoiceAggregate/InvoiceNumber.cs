using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.InvoiceAggregate
{
    public record InvoiceNumber(long Value) : BusinessNumber(Value)
    {
        public override string ShortForm => "INV";

        public override int Digits => 8;

        public static InvoiceNumber CreateFirst(int year)
        {
            return new InvoiceNumber(int.Parse(year.ToString() + 1.ToString("0000")));
        }

        public static InvoiceNumber CreateNext(InvoiceNumber lastNumber)
        {
            return new InvoiceNumber(lastNumber.Value + 1);
        }

        public static InvoiceNumber CreateTmp()
        {
            return new InvoiceNumber(0);
        }

        public static InvoiceNumber CreateLegacy(long number)
        {
            return new InvoiceNumber(number);
        }
    }
}
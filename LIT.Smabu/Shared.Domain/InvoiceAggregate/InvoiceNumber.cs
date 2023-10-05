using LIT.Smabu.Shared.Domain.Common;

namespace LIT.Smabu.Shared.Domain.InvoiceAggregate
{
    public class InvoiceNumber : BusinessNumber
    {
        public InvoiceNumber(long value) : base(value)
        {
        }

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

        public static InvoiceNumber? CreateLegacy(long number)
        {
            return new InvoiceNumber(number);
        }
    }
}
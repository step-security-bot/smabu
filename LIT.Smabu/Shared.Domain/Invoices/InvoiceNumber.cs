using LIT.Smabu.Domain.Shared.Common;

namespace LIT.Smabu.Domain.Shared.Invoices
{
    public record InvoiceNumber : BusinessNumber
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
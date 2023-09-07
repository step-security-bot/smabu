namespace LIT.Smabu.Shared.Entities.Business.InvoiceAggregate
{
    public class InvoiceNumber : BusinessNumber
    {
        public InvoiceNumber(long value) : base(value)
        {
        }

        public override string ShortForm => "INV";

        public override int Digits => 8;
    }
}
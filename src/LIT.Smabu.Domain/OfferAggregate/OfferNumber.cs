using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public record OfferNumber : BusinessNumber
    {
        public OfferNumber(long value) : base(value)
        {

        }

        public override string ShortForm => "OFR";

        public override int Digits => 4;
    }
}

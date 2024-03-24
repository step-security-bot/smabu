using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public record OfferNumber(long Value) : BusinessNumber(Value)
    {
        public override string ShortForm => "OFR";

        public override int Digits => 4;
    }
}

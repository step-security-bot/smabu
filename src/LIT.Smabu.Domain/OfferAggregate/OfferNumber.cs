using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.OfferAggregate
{
    public record OfferNumber(long Value) : BusinessNumber(Value)
    {
        public override string ShortForm => "OFR";

        public override int Digits => 4;

        public static OfferNumber CreateLegacy(int id)
        {
            return new OfferNumber(id);
        }
    }
}

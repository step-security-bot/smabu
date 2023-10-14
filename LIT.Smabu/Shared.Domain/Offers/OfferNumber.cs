using LIT.Smabu.Domain.Shared.Common;

namespace LIT.Smabu.Domain.Shared.Offers
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

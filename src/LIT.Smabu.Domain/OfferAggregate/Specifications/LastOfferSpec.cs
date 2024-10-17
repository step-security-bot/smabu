using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.OfferAggregate.Specifications
{
    public class LastOfferSpec : Specification<Offer>
    {
        public LastOfferSpec() : base(x => true)
        {
            OrderByDescendingExpression = x => x.Number.Long;
            Take = 1;
        }
    }
}

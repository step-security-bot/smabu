using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.OfferAggregate.Specifications
{
    public class LastOfferSpec : Specification<Offer>
    {
        public LastOfferSpec() : base(x => true)
        {
            OrderByDescendingExpression = x => x.Number.DisplayName;
            Take = 1;
        }
    }
}

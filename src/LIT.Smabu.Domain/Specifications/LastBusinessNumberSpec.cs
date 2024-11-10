using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Specifications
{
    public class LastBusinessNumberSpec<TAggregate, TNumber> : Specification<TAggregate>
        where TAggregate : IAggregateRoot<IEntityId<TAggregate>>, IHasBusinessNumber<TNumber>
        where TNumber : BusinessNumber
    {
        public LastBusinessNumberSpec() : base(x => true)
        {
            OrderByDescendingExpression = x => x.Number.DisplayName;
            Take = 1;
        }

        public LastBusinessNumberSpec(int year) : base(x => x.Number.Value.ToString().StartsWith(year.ToString()))
        {
            OrderByDescendingExpression = x => x.Number.DisplayName;
            Take = 1;
        }
    }
}

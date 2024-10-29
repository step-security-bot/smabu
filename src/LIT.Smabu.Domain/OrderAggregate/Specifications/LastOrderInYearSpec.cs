using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.OrderAggregate.Specifications
{
    public class LastOrderInYearSpec : Specification<Order>
    {
        public LastOrderInYearSpec(int year) : base(x => x.Number.Value.ToString().StartsWith(year.ToString()))
        {
            OrderByDescendingExpression = x => x.Number.Long;
            Take = 1;
        }
    }
}

using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.CustomerAggregate.Specifications
{
    public class LastCustomerByNumberSpec : Specification<Customer>
    {
        public LastCustomerByNumberSpec() : base(x => true)
        {
            OrderByDescendingExpression = x => x.Number.DisplayName;
            Take = 1;
        }
    }
}

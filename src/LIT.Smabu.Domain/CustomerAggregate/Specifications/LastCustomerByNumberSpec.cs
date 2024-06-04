using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.CustomerAggregate.Specifications
{
    public class LastCustomerByNumberSpec : Specification<Customer>
    {
        public LastCustomerByNumberSpec() : base(x => true)
        {
            OrderByDescendingExpression = x => x.Number.Long;
            Take = 1;
        }
    }
}

using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Infrastructure.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TAggregate> GetQuery<TAggregate>(
            IQueryable<TAggregate> inputQueryable,
            Specification<TAggregate> specification)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            IQueryable<TAggregate> queryable = inputQueryable;
            queryable = queryable.Where(specification.Criteria);
            return queryable;
        }
    }
}

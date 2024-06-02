using System.Linq.Expressions;

namespace LIT.Smabu.Domain.SeedWork
{
    public abstract class Specification<TAggregate>(Expression<Func<TAggregate, bool>> criteria) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
    {
        public Expression<Func<TAggregate, bool>> Criteria { get; } = criteria;
    }
}

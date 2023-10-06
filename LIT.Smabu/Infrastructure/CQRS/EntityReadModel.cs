using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.DDD;

namespace LIT.Smabu.Infrastructure.CQRS
{
    public abstract class EntityReadModel<TEntity, TEntityId> where TEntity : IEntity<TEntityId> where TEntityId : IEntityId<TEntity>
    {
        public EntityReadModel(IAggregateStore aggregateStore)
        {
            this.AggregateStore = aggregateStore;
        }
        protected IAggregateStore AggregateStore { get; }

        protected IEnumerable<TEntity> Query
        {
            get
            {
                var query = BuildQuery(this.AggregateStore);
                return query;
            }
        }

        protected abstract IEnumerable<TEntity> BuildQuery(IAggregateStore aggregateStore);

        protected IEnumerable<TEntity> GetAll()
        {
            return this.Query;
        }

        protected TEntity? GetById(TEntityId id)
        {
            return this.Query.FirstOrDefault(x => x.Id.Value == id.Value);
        }

        protected IEnumerable<TEntity> Browse(Func<TEntity, bool> predicate)
        {
            return this.Query.Where(predicate);
        }
    }
}

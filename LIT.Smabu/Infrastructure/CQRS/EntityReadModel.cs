using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Entities;

namespace LIT.Smabu.Infrastructure.CQRS
{
    public abstract class EntityReadModel<TEntity, TEntityId> where TEntity : IEntity<TEntityId> where TEntityId : IEntityId<TEntity>
    {
        private readonly IAggregateStore aggregateStore;
        private IEnumerable<TEntity>? query;

        public EntityReadModel(IAggregateStore aggregateStore)
        {
            this.aggregateStore = aggregateStore;
        }

        protected IEnumerable<TEntity> Query
        {
            get
            {
                this.query ??= BuildQuery(this.aggregateStore);
                return this.query;
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

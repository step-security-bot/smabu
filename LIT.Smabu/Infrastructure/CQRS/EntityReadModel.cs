using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Shared.Common;

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

        public IEnumerable<TEntity> GetAll()
        {
            return this.Query;
        }

        public TEntity? GetById(TEntityId id)
        {
            return this.Query.FirstOrDefault(x => x.Id.Value == id.Value);
        }
    }
}

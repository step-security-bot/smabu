using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.DDD
{
    public class AggregateJsonStore : IAggregateStore
    {
        private readonly Dictionary<IEntityId, IAggregateRoot> cache;

        public AggregateJsonStore()
        {
            this.cache = new Dictionary<IEntityId, IAggregateRoot>();
        }

        public Task AddOrUpdateAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate)
            where TEntityId : IEntityId
        {
            this.cache.Add(aggregate.Id, aggregate);
            return Task.CompletedTask;
        }

        public Task<TAggregate> GetAsync<TAggregate, TEntityId>(TEntityId id)
            where TAggregate : class, IAggregateRoot<TEntityId>
            where TEntityId : IEntityId
        {
            var result = this.cache.ContainsKey(id) ? this.cache[id] as TAggregate : null;
            return result == null ? throw new AggregateNotFoundException(id) : Task.FromResult(result);
        }

        public Task<bool> DeleteAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            var result = this.cache.Remove(aggregate.Id);
            return Task.FromResult(result);
        }
    }
}

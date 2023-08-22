using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Dictionary<IEntityId, IAggregateRoot> cache;

        public FileAggregateStore(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            cache = new Dictionary<IEntityId, IAggregateRoot>();
        }

        public async Task AddOrUpdateAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate)
            where TEntityId : IEntityId
        {
            var user = httpContextAccessor.HttpContext.User;
            if (cache.ContainsKey(aggregate.Id))
            {
                var meta = aggregate.Meta;
                if (meta != null)
                {
                    aggregate.UpdateMeta(new AggregateMeta(meta.Version, meta.CreatedOn, meta.CreatedById, meta.CreatedByName, DateTime.Now, Guid.Empty, user.Identity?.Name ?? "?"));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                aggregate.UpdateMeta(new AggregateMeta(1, DateTime.Now, Guid.Empty, user.Identity?.Name ?? "?", null, null, null));
                cache.Add(aggregate.Id, aggregate);
            }
            await this.SaveToFileAsync(aggregate);
        }

        public Task<TAggregate> GetAsync<TAggregate, TEntityId>(TEntityId id)
            where TAggregate : class, IAggregateRoot<TEntityId>
            where TEntityId : IEntityId
        {
            var result = cache.ContainsKey(id) ? cache[id] as TAggregate : null;
            return result == null ? throw new AggregateNotFoundException(id) : Task.FromResult(result);
        }

        public Task<bool> DeleteAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            var result = cache.Remove(aggregate.Id);
            return Task.FromResult(result);
        }

        private Task SaveToFileAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            throw new NotImplementedException();
        }
    }
}

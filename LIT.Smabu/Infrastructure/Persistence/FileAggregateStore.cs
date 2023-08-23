using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Common;
using Microsoft.AspNetCore.Http;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Dictionary<IEntityId, IAggregateRoot> cache;
        private readonly string rootDirectory;

        public FileAggregateStore(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cache = new Dictionary<IEntityId, IAggregateRoot>();
            this.rootDirectory = Path.Combine(Environment.CurrentDirectory, "TmpData");
        }

        public async Task LoadAsync()
        {
            if (!Directory.Exists(rootDirectory))
            {
                Directory.CreateDirectory(rootDirectory);
            }
            else
            {
                var files = Directory.GetFiles(rootDirectory, "*.json", SearchOption.AllDirectories);
                foreach (var filename in files)
                {
                    var file = await File.ReadAllTextAsync(filename);
                    var aggregateType = Type.GetType(filename); // ToDo
                    if (aggregateType != null)
                    {
                        AggregateJsonConverter.ConvertToAggregate(file, aggregateType);
                    }
                }
            }
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

        public TAggregate Get<TAggregate, TEntityId>(TEntityId id)
            where TAggregate : class, IAggregateRoot<TEntityId>
            where TEntityId : IEntityId
        {
            var result = cache.ContainsKey(id) ? cache[id] as TAggregate : null;
            return result ?? throw new AggregateNotFoundException(id);
        }

        public IEnumerable<TAggregate> GetAll<TAggregate, TEntityId>()
            where TAggregate : class, IAggregateRoot<TEntityId>
            where TEntityId : IEntityId
        {
            var result = cache.Values.OfType<TAggregate>();
            return result;
        }

        public Task<bool> DeleteAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            var result = cache.Remove(aggregate.Id);
            return Task.FromResult(result);
        }

        private async Task SaveToFileAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            var typeName = aggregate.GetType().Name;
            var directory = Path.Combine(rootDirectory, typeName);
            var path = Path.Combine(directory, aggregate.Id.ToString() + ".json");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var json = AggregateJsonConverter.ConvertToJson(aggregate);

            await File.WriteAllTextAsync(path, json);
        }
    }
}

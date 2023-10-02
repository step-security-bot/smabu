using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Entities;
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
            var userId = user.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? "?";
            var userName = user.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "?";
            if (cache.ContainsKey(aggregate.Id))
            {
                var meta = aggregate.Meta;
                if (meta != null)
                {
                    aggregate.UpdateMeta(new AggregateMeta(meta.Version + 1, meta.CreatedOn, meta.CreatedById, meta.CreatedByName, DateTime.Now, userId, userName));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                aggregate.UpdateMeta(new AggregateMeta(1, DateTime.Now, userId, userName, null, null, null));
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
            if (cache?.Any() == false)
            {
                string directory = BuildDirectoryPath(typeof(TAggregate));
                var fileNames = Directory.GetFiles(directory);
                var files = fileNames.Select(x => File.ReadAllText(x));
                var aggregates = files.Select(x => AggregateJsonConverter.ConvertFromJson<TAggregate>(x)).ToList();
                foreach (var aggregate in aggregates)
                {
                    if (!cache.ContainsKey(aggregate.Id))
                    {
                        cache.Add(aggregate.Id, aggregate);
                    }
                }
            }
            var result = cache?.Values.OfType<TAggregate>().ToList() ?? new List<TAggregate>();
            return result;
        }

        public Task<bool> DeleteAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            var result = cache.Remove(aggregate.Id);
            return Task.FromResult(result);
        }

        private async Task SaveToFileAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId
        {
            string directory = BuildDirectoryPath(aggregate.GetType());
            var path = Path.Combine(directory, aggregate.Id.Value.ToString() + ".json");
            var json = AggregateJsonConverter.ConvertToJson(aggregate);

            await File.WriteAllTextAsync(path, json);
        }

        private string BuildDirectoryPath(Type aggregateType)
        {
            var typeName = aggregateType.Name;
            var directory = Path.Combine(rootDirectory, typeName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }
    }
}

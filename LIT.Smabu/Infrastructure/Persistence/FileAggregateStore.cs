using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Domain.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Dictionary<IEntityId, IAggregateRoot> cache = new();
        private readonly string rootDirectory;

        public FileAggregateStore(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Data");
        }

        public async Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
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

        public TAggregate Get<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var result = this.Browse<TAggregate>(x => x.Id.Equals(id)).SingleOrDefault();
            return result ?? throw new AggregateNotFoundException(id);
        }

        public List<TAggregate> GetAll<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            return this.Browse<TAggregate>(x => true);
        }

        public List<TAggregate> GetByIds<TAggregate, TEntityId>(List<TEntityId> ids)
            where TAggregate : class, IAggregateRoot<TEntityId> 
            where TEntityId : class, IEntityId<TAggregate>
        {
            return this.Browse<TAggregate>(x => ids.Contains(x.Id)).ToList();
        }

        public List<TAggregate> Browse<TAggregate>(Func<TAggregate, bool> predicate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var aggregatesInCache = cache?.Values.OfType<TAggregate>().ToList() ?? new List<TAggregate>();
            if (aggregatesInCache.Any() == false)
            {
                string directory = BuildDirectoryPath(typeof(TAggregate));
                var allFileNames = Directory.GetFiles(directory);
                var fileNamesToLoad = allFileNames.Where(x => !aggregatesInCache.Any(y => x.Contains(y.Id.Value.ToString()))).ToList();
                var files = fileNamesToLoad.Select(x => File.ReadAllText(x)).ToList();
                var aggregates = files.Select(x => AggregateJsonConverter.ConvertFromJson<TAggregate>(x)).ToList();
                foreach (var aggregate in aggregates.OfType<IAggregateRoot<IEntityId>>())
                {
                    if (cache != null && !cache.ContainsKey(aggregate.Id))
                    {
                        cache.Add(aggregate.Id, aggregate);
                    }
                }
            }
            var result = cache?.Values.OfType<TAggregate>().Where(predicate).ToList() ?? new List<TAggregate>();
            return result;
        }

        public Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate) 
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            var file = this.GetFilePath(aggregate);
            var result = cache.Remove(aggregate.Id);
            File.Delete(file);
            return Task.FromResult(result);
        }

        private async Task SaveToFileAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            string path = GetFilePath(aggregate);
            var json = AggregateJsonConverter.ConvertToJson(aggregate);

            await File.WriteAllTextAsync(path, json);
        }

        private string GetFilePath<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : class, IEntityId
        {
            string directory = BuildDirectoryPath(aggregate.GetType());
            var path = Path.Combine(directory, aggregate.Id.Value.ToString() + ".json");
            return path;
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

using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Identity;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore
    {
        private readonly Dictionary<IEntityId, IAggregateRoot> cache = new();
        private readonly string rootDirectory;
        private readonly ICurrentUser currentUser;

        public FileAggregateStore(ICurrentUser currentUser)
        {
            this.rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Data");
            this.currentUser = currentUser;
        }

        public async Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            if (cache.ContainsKey(aggregate.Id))
            {
                var meta = aggregate.Meta;
                if (meta != null)
                {
                    aggregate.UpdateMeta(new AggregateMeta(meta.Version + 1, meta.CreatedOn, meta.CreatedById, meta.CreatedByName,
                        DateTime.Now, currentUser.Id, currentUser.Name));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                aggregate.UpdateMeta(new AggregateMeta(1, DateTime.Now, currentUser.Id, currentUser.Name, null, null, null));
                cache.Add(aggregate.Id, aggregate);
            }
            await this.SaveToFileAsync(aggregate);
        }

        public async Task<TAggregate> GetAsync<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var result = (await this.BrowseAsync<TAggregate>(x => x.Id.Equals(id))).SingleOrDefault();
            return result ?? throw new AggregateNotFoundException(id);
        }

        public async Task<List<TAggregate>> GetAllAsync<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            return await this.BrowseAsync<TAggregate>(x => true);
        }

        public async Task<Dictionary<TEntityId, TAggregate>> GetByIdsAsync<TAggregate, TEntityId>(List<TEntityId> ids)
            where TAggregate : class, IAggregateRoot<TEntityId> 
            where TEntityId : class, IEntityId<TAggregate>
        {
            return (await this.BrowseAsync<TAggregate>(x => ids.Contains(x.Id))).ToDictionary(x => x.Id, x => x);
        }

        public async Task<List<TAggregate>> BrowseAsync<TAggregate>(Func<TAggregate, bool> predicate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            if (cache == null)
            {
                throw new SmabuException("Cache is null");
            }
            List<TAggregate> aggregatesInCache = GetFromCache<TAggregate>();
            if (aggregatesInCache.Any() == false)
            {
                await LoadForAggregateAsync<TAggregate>();
            }
            var result = cache?.Values.OfType<TAggregate>().Where(predicate).ToList() ?? new List<TAggregate>();
            return result;
        }

        private async Task LoadForAggregateAsync<TAggregate>() where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            string directory = BuildDirectoryPath(typeof(TAggregate));
            var aggregatesInCache = GetFromCache<TAggregate>();
            var aggregateIdsInCache = aggregatesInCache.Select(x => x.Id.Value.ToString()).ToList();
            var allFileNames = Directory.GetFiles(directory);
            var fileNamesToLoad = allFileNames.Where(x => !aggregateIdsInCache.Any(y => x.Contains(y))).ToList();
            foreach (var fileName in fileNamesToLoad)
            {
                var file = await File.ReadAllTextAsync(fileName);
                var aggregate = AggregateJsonConverter.ConvertFromJson<TAggregate>(file);
                if (aggregate != null)
                {
                    if (!cache.ContainsKey(aggregate.Id))
                    {
                        cache.Add(aggregate.Id, aggregate);
                    }
                }
            }
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

        private List<TAggregate> GetFromCache<TAggregate>() where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            return cache.Values.OfType<TAggregate>().ToList() ?? new List<TAggregate>();
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

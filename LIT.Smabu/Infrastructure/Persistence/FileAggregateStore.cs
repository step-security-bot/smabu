using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Identity;
using Microsoft.Extensions.Logging;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore, IAggregateResolver
    {
        private readonly Dictionary<IEntityId, IAggregateRoot> cache = new();
        private readonly string rootDirectory;
        private readonly ILogger<FileAggregateStore> logger;
        private readonly ICurrentUser currentUser;

        public FileAggregateStore(ILogger<FileAggregateStore> logger, ICurrentUser currentUser)
        {
            this.rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Data");
            this.logger = logger;
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

        public async Task<List<TAggregate>> GetAsync<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            await this.LoadAsync(typeof(TAggregate));
            return this.BrowseCache<TAggregate>(x => true);
        }

        public async Task<TAggregate> GetAsync<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            await this.LoadAsync(typeof(TAggregate));
            var result = this.BrowseCache<TAggregate>(x => x.Id.Equals(id)).SingleOrDefault();
            return result ?? throw new AggregateNotFoundException(id);
        }

        public async Task<Dictionary<TEntityId, TAggregate>> GetAsync<TAggregate, TEntityId>(List<TEntityId> ids)
            where TAggregate : class, IAggregateRoot<TEntityId> 
            where TEntityId : class, IEntityId<TAggregate>
        {
            await this.LoadAsync(typeof(TAggregate));
            return this.BrowseCache<TAggregate>(x => ids.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        }

        public List<TAggregate> BrowseCache<TAggregate>(Func<TAggregate, bool> predicate)
            where TAggregate : class, IAggregateRoot
        {
            if (cache == null)
            {
                throw new SmabuException("Cache is null");
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

        public Dictionary<IEntityId, IAggregateRoot> ResolveByIds(IEntityId[] ids)
        {
            var result = new Dictionary<IEntityId, IAggregateRoot>();
            var idGroups = ids.GroupBy(x => x.GetType()).ToList();
            foreach (var idGroup in idGroups)
            {
                var idType = idGroup.Key;
                var aggregateType = idType.BaseType?.GenericTypeArguments[0]!;
                this.LoadAsync(aggregateType, idGroup.ToArray()).GetAwaiter().GetResult();
                foreach (var id in idGroup)
                {
                    var entity = this.cache.ContainsKey(id) ? this.cache[id] : null;
                    if (entity != null)
                    {
                        result.Add(id, entity);
                    }
                }
            }
            return result;
        }

        private async Task LoadAsync(Type aggregateType, IEntityId[]? ids = null)
        {
            string directory = BuildDirectoryPath(aggregateType);
            var allFileNames = Directory.GetFiles(directory);
            var fileNamesToLoad = allFileNames.Where(x => ids == null || ids.Any(y => x.Contains(y.ToString()!))).ToList();
            fileNamesToLoad = fileNamesToLoad.Where(x => !this.cache.Keys.Any(y => x.Contains(y.ToString()!))).ToList();
            this.logger.LogInformation("{0} files to load", fileNamesToLoad.Count);
            foreach (var fileName in fileNamesToLoad)
            {
                this.logger.LogInformation("Read file {0}", fileName);
                var file = await File.ReadAllTextAsync(fileName);
                this.logger.LogInformation("Create aggregate: {0}", aggregateType.Name);
                var aggregate = AggregateJsonConverter.ConvertFromJson(aggregateType, file);
                if (aggregate != null && aggregate is IAggregateRoot<IEntityId> aggregateWithId)
                {
                    if (!cache.ContainsKey(aggregateWithId.Id))
                    {
                        this.logger.LogInformation("Add aggregate to cache: {0}", aggregateWithId.Id);
                        cache.Add(aggregateWithId.Id, aggregate);
                    }
                    else
                    {
                        this.logger.LogInformation("Replace aggregate in cache: {0}", aggregateWithId.Id);
                        cache[aggregateWithId.Id] = aggregate;
                    }
                }
            }
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
                this.logger.LogDebug("Directory created {0}", directory);
                Directory.CreateDirectory(directory);
            }
            return directory;
        }
    }
}

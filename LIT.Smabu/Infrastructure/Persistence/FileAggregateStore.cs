using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.Cache;
using LIT.Smabu.Infrastructure.DDD;
using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Shared.Identity;
using Microsoft.Extensions.Logging;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore
    {
        private readonly string rootDirectory;
        private readonly ILogger<FileAggregateStore> logger;
        private readonly ICurrentUser currentUser;
        private readonly IUserCache cache;
        private readonly List<Type> loadedTypes = new();

        public FileAggregateStore(ILogger<FileAggregateStore> logger, ICurrentUser currentUser, IUserCache cache)
        {
            this.rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Data");
            this.logger = logger;
            this.currentUser = currentUser;
            this.cache = cache;
        }

        public async Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            if (cache.ContainsKey(currentUser.Id, "data", aggregate.Id))
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
                cache.AddOrUpdate(currentUser.Id, "data", aggregate.Id, aggregate);
            }
            await this.SaveToFileAsync(aggregate);
        }

        public async Task<List<TAggregate>> GetAllAsync<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            await this.LoadAsync(typeof(TAggregate));
            return this.BrowseCache<TAggregate>(x => true);
        }

        public async Task<TAggregate> GetByAsync<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            await this.LoadAsync(typeof(TAggregate));
            var result = this.BrowseCache<TAggregate>(x => x.Id.Equals(id)).SingleOrDefault();
            return result ?? throw new AggregateNotFoundException(id);
        }

        public async Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            await this.LoadAsync(typeof(TAggregate));
            return this.BrowseCache<TAggregate>(x => ids.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        }

        public Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate) 
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            var file = this.GetFilePath(aggregate);
            cache.Remove(currentUser.Id, "data", aggregate.Id);
            File.Delete(file);
            return Task.FromResult(true);
        }

        public List<TAggregate> BrowseCache<TAggregate>(Func<TAggregate, bool> predicate)
            where TAggregate : class, IAggregateRoot
        {
            if (cache == null)
            {
                throw new SmabuException("Cache is null");
            }
            var result = cache.GetValues<TAggregate>(currentUser.Id, "data")
                .Where(predicate).ToList() ?? new List<TAggregate>();
            return result;
        }

        private async Task SaveToFileAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            string path = GetFilePath(aggregate);
            var json = AggregateJsonConverter.ConvertToJson(aggregate);

            await File.WriteAllTextAsync(path, json);
        }

        private async Task LoadAsync(Type aggregateType)
        {
            if (!loadedTypes.Contains(aggregateType))
            {
                string directory = BuildDirectoryPath(aggregateType);
                var fileNames = Directory.GetFiles(directory);
                this.logger.LogInformation("Read {0} files for type {1} ", fileNames.Length, aggregateType.Name);
                foreach (var fileName in fileNames)
                {
                    var file = await File.ReadAllTextAsync(fileName);
                    var aggregate = AggregateJsonConverter.ConvertFromJson(aggregateType, file);
                    if (aggregate != null && aggregate is IAggregateRoot<IEntityId> aggregateWithId)
                    {
                        cache.AddOrUpdate(currentUser.Id, "data", aggregateWithId.Id, aggregate);
                    }
                    else
                    {
                        this.logger.LogWarning("Invalid aggregate: {0}", fileName);
                    }
                }
                loadedTypes.Add(aggregateType);
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

using LIT.Smabu.Infrastructure.Exception;
using LIT.Smabu.Shared.Identity;
using LIT.Smabu.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class FileAggregateStore : IAggregateStore
    {
        private readonly string rootDirectory;
        private readonly ILogger<FileAggregateStore> logger;
        private readonly ICurrentUser currentUser;
        private readonly List<Type> loadedTypes = [];

        public FileAggregateStore(ILogger<FileAggregateStore> logger, ICurrentUser currentUser)
        {
            rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Smabu", "Data");
            this.logger = logger;
            this.currentUser = currentUser;
        }

        public async Task CreateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            aggregate.UpdateMeta(new AggregateMeta(1, DateTime.Now, currentUser.Id, currentUser.Name, null, null, null));
            await SaveToFileAsync(aggregate);
        }

        public async Task UpdateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var latestVersion = await GetByAsync(aggregate.Id);
            if (latestVersion != null)
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
            }
            await SaveToFileAsync(aggregate);
        }

        public async Task<IReadOnlyList<TAggregate>> GetAllAsync<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            return await LoadAsync<TAggregate>();
        }

        public async Task<TAggregate> GetByAsync<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var result = (await LoadAsync<TAggregate>([id])).FirstOrDefault();
            return result ?? throw new AggregateNotFoundException(id);
        }

        public async Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var allItems = await LoadAsync<TAggregate>(ids);
            return allItems.Where(x => ids.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
        }

        public Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var file = GetFilePath(aggregate);
            File.Delete(file);
            return Task.FromResult(true);
        }

        private async Task SaveToFileAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>
        {
            string path = GetFilePath(aggregate);
            var json = AggregateJsonConverter.ConvertToJson(aggregate);
            await File.WriteAllTextAsync(path, json);
        }

        private async Task<TAggregate[]> LoadAsync<TAggregate>(IEnumerable<IEntityId>? ids = null)
        {
            var result = new List<TAggregate>();
            var aggregateType = typeof(TAggregate);
            string directory = BuildDirectoryPath(aggregateType);
            var fileNames = Directory.GetFiles(directory, "*.json", SearchOption.TopDirectoryOnly);
            if (ids != null)
            {
                fileNames = fileNames.Where(x => ids.Any(y => x.Contains(y.Value.ToString()))).ToArray();
            }
            logger.LogInformation("Read {0} files for type {1} ", fileNames.Length, aggregateType.Name);
            foreach (var fileName in fileNames)
            {
                var fileContent = await File.ReadAllTextAsync(fileName);
                var aggregate = AggregateJsonConverter.ConvertFromJson(aggregateType, fileContent);
                if (aggregate != null && aggregate is TAggregate aggregateWithId)
                {
                    result.Add(aggregateWithId);
                }
                else
                {
                    logger.LogWarning("Invalid aggregate: {0}", fileName);
                }
            }
            return [.. result];
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
                logger.LogDebug("Directory created {0}", directory);
                Directory.CreateDirectory(directory);
            }
            return directory;
        }
    }
}

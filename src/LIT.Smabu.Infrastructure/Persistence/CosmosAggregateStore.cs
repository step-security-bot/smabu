using LIT.Smabu.Domain.SeedWork;
using LIT.Smabu.Infrastructure.Exceptions;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class CosmosAggregateStore(ICurrentUser currentUser, IConfiguration config, ILogger<CosmosAggregateStore> logger) : IAggregateStore
    {
        private const string AggregatesContainerId = "Aggregates";
        private static Container? container;
        private readonly ICurrentUser currentUser = currentUser;

        public async Task<bool> CreateAsync<TAggregate>(TAggregate aggregate)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            if (aggregate.Meta == null)
            {
                aggregate.UpdateMeta(AggregateMeta.CreateFirst(currentUser));
            }
            var container = await GetAggregatesContainerAsync();
            var entity = CreateEntity(aggregate);
            var response = await container.CreateItemAsync(entity, new PartitionKey(entity.PartitionKey));
            if (response.StatusCode != HttpStatusCode.Created)
            {
                logger.LogError("Creation of aggregate {type}/{id} failed: {code}", typeof(TAggregate).Name, aggregate.Id, response.StatusCode);
                throw new SmabuException($"Creation of aggregate '{aggregate.Id}' failed with code: {response.StatusCode}");
            }
            logger.LogInformation("Created aggregate {type}/{id} successfully", typeof(TAggregate).Name, aggregate.Id);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> UpdateAsync<TAggregate>(TAggregate aggregate)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var meta = aggregate.Meta!.Next(currentUser);
            aggregate.UpdateMeta(meta);
            var container = await GetAggregatesContainerAsync();
            var entity = CreateEntity(aggregate);
            var response = await container.ReplaceItemAsync(entity, entity.Id, new PartitionKey(entity.PartitionKey));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Updating aggregate {type}/{id} failed: {code}", typeof(TAggregate).Name, aggregate.Id, response.StatusCode);
                throw new SmabuException($"Updating aggregate '{aggregate.Id}' failed with code: {response.StatusCode}");
            }
            logger.LogInformation("Updated aggregate {type}/{id} successfully", typeof(TAggregate).Name, aggregate.Id);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var container = await GetAggregatesContainerAsync();
            var entity = CreateEntity(aggregate);
            var response = await container.DeleteItemAsync<TAggregate>(entity.Id, new PartitionKey(entity.PartitionKey));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Deleting aggregate {type}/{id} failed: {code}", typeof(TAggregate).Name, aggregate.Id, response.StatusCode);
                throw new SmabuException($"Deleting aggregate '{aggregate.Id}' failed with code: {response.StatusCode}");
            }
            logger.LogInformation("Deleted aggregate {type}/{id} successfully", typeof(TAggregate).Name, aggregate.Id);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<IReadOnlyList<TAggregate>> GetAllAsync<TAggregate>()
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var container = await GetAggregatesContainerAsync();
            var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{GetPartitionKey<TAggregate>()}'";
            QueryDefinition queryDefinition = new(sqlQueryText);
            var queryResultSetIterator = container.GetItemQueryIterator<CosmosEntity<TAggregate>>(queryDefinition);

            List<TAggregate> result = [];
            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (var item in currentResultSet)
                {
                    result.Add(item.Body);
                }
            }
            logger.LogInformation("Get all aggregates of type {type}: {count} items", typeof(TAggregate).Name, result.Count);
            return result;
        }

        public async Task<TAggregate> GetByAsync<TAggregate>(IEntityId<TAggregate> id)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var container = await GetAggregatesContainerAsync();
            var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{GetPartitionKey<TAggregate>()}' AND c.id = '{id.Value}'";

            QueryDefinition queryDefinition = new(sqlQueryText);
            var queryResultSetIterator = container.GetItemQueryIterator<CosmosEntity<TAggregate>>(queryDefinition);

            List<TAggregate> result = [];

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (var item in currentResultSet)
                {
                    result.Add(item.Body);
                }
            }
            logger.LogInformation("Get aggregate {type}/{id}", typeof(TAggregate).Name, id);
            return result[0];
        }

        public async Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            Dictionary<IEntityId<TAggregate>, TAggregate> result = [];
            if (ids.Any())
            {
                var container = await GetAggregatesContainerAsync();
                var formattedIds = ids.Distinct().Select(x => $"\"{x}\"").ToList();
                var sqlQueryText = $"SELECT * FROM c"
                    + $" WHERE c.partitionKey = '{GetPartitionKey<TAggregate>()}' AND c.id IN ({string.Join(',', formattedIds)})";

                QueryDefinition queryDefinition = new(sqlQueryText);
                var queryResultSetIterator = container.GetItemQueryIterator<CosmosEntity<TAggregate>>(queryDefinition);


                while (queryResultSetIterator.HasMoreResults)
                {
                    var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var item in currentResultSet)
                    {
                        result.Add(item.Body.Id, item.Body);
                    }
                }
            }
            logger.LogInformation("Get aggregates of type {type} by ids. Found {found}/{requested}", 
                typeof(TAggregate).Name, result.Count, ids.Distinct().Count());
            return result;
        }

        public async Task<IReadOnlyList<TAggregate>> ApplySpecification<TAggregate>(Specification<TAggregate> specification)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            IQueryable<TAggregate> queryable = (await this.GetAllAsync<TAggregate>()).AsQueryable();
            var result = Specifications.SpecificationEvaluator.GetQuery(queryable, specification).ToList();

            logger.LogInformation("Get aggregates of type {type} by specification '{specification}'", typeof(TAggregate).Name, specification.GetType().Name);
            return result;
        }

        private async Task<Container> GetAggregatesContainerAsync()
        {
            if (container == null)
            {
                var endpointUri = config["AzureAD:Cosmos:Endpoint"];
                var primaryKey = config["AzureAD:Cosmos:PrimaryKey"];
                var databaseId = config["AzureAD:Cosmos:DatabaseId"];
                var cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = "Smabu" });
                var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
                var database = databaseResponse.Database;
                var response = await database!.CreateContainerIfNotExistsAsync(AggregatesContainerId, "/partitionKey");
                container = response.Container;
            }
            return container;
        }

        private static string GetPartitionKey<TAggregate>() where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            return typeof(TAggregate).Name;
        }

        private static CosmosEntity<T> CreateEntity<T>(T aggregate) where T : class, IAggregateRoot<IEntityId<T>>
        {
            return new CosmosEntity<T>(aggregate.Id.Value.ToString(), aggregate, GetPartitionKey<T>());
        }

        public record CosmosEntity<T>
        {
            [JsonConstructor]
            public CosmosEntity(string id, T body, string partitionKey)
            {
                Id = id;
                Body = body;
                PartitionKey = partitionKey;
            }

            [JsonProperty("id")]
            public string Id { get; init; }

            [JsonProperty("body")]
            public T Body { get; init; }

            [JsonProperty("partitionKey")]
            public string PartitionKey { get; init; }
        }
    }
}

using LIT.Smabu.Shared.Contracts;
using LIT.Smabu.Shared.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class CosmosAggregateStore(ICurrentUser currentUser, IConfiguration config) : IAggregateStore
    {
        private const string AggregatesContainerId = "Aggregates";
        private static Container? container;
        private readonly ICurrentUser currentUser = currentUser;

        public async Task<bool> CreateAsync<TAggregate>(TAggregate aggregate)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            aggregate.UpdateMeta(AggregateMeta.CreateFirst(currentUser));
            var container = await GetAggregatesContainerAsync();
            var entity = CreateEntity(aggregate);
            var response = await container.CreateItemAsync(entity, new PartitionKey(entity.PartitionKey)); 
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
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            var container = await GetAggregatesContainerAsync();
            var entity = CreateEntity(aggregate);
            var response = await container.DeleteItemAsync<TAggregate>(entity.Id, new PartitionKey(entity.PartitionKey));
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
            return result[0];
        }

        public async Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
             where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>
        {
            Dictionary<IEntityId<TAggregate>, TAggregate> result = [];
            if (ids.Any())
            {
                var container = await GetAggregatesContainerAsync();
                var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{GetPartitionKey<TAggregate>()}' AND c.id IN ({string.Join(',', ids.Select(x => $"\"{x}\""))})";

                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
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
            return result;
        }

        private async Task<Container> GetAggregatesContainerAsync()
        {
            if (container == null)
            {
                var endpointUri = config["Azure:CosmosEndpoint"];
                var primaryKey = config["Azure:CosmosPrimaryKey"];
                var databaseId = config["Azure:CosmosDatabaseId"];
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

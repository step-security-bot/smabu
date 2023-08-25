using LIT.Smabu.Shared.Entities;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace LIT.Smabu.Infrastructure.DDD
{
    public static class AggregateJsonConverter
    {
        static readonly JsonSerializerSettings settings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,            
        };

        static AggregateJsonConverter()
        {

        }

        public static string ConvertToJson(IAggregateRoot aggregate)
        {
            var result = JsonConvert.SerializeObject(aggregate, settings);
            return result;
        }

        public static TAggregate ConvertToAggregate<TAggregate>(string json) where TAggregate : class, IAggregateRoot
        {
            var result = ConvertToAggregate(json, typeof(TAggregate)) as TAggregate;
            return result ?? throw new InvalidCastException($"Aggregate has to be type of {typeof(TAggregate)}");
        }

        public static object ConvertToAggregate(string json, Type aggregateType)
        {
            var result = JsonConvert.DeserializeObject(json, aggregateType, settings);
            return result ?? throw new InvalidCastException($"Aggregate has to be type of {aggregateType}");
        }
    }
}

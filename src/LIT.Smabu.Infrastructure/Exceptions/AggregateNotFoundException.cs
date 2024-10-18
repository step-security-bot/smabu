using LIT.Smabu.Shared;

namespace LIT.Smabu.Infrastructure.Exceptions
{
    public class AggregateNotFoundException(IEntityId id) : SmabuException($"Aggregate {id} not found.")
    {
    }
}

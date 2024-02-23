using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class AggregateNotFoundException(IEntityId id) : SmabuException($"Aggregate {id} not found.")
    {
    }
}

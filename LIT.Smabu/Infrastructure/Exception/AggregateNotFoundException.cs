using LIT.Smabu.Shared.Entities;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class AggregateNotFoundException : SmabuException
    {
        public AggregateNotFoundException(IEntityId id) : base($"Aggregate {id} not found.")
        {
        }
    }
}

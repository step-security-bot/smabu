using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class AggregateNotFoundException : SmabuException
    {
        public AggregateNotFoundException(IEntityId id) : base($"Aggregate {id} not found.")
        {
        }
    }
}

using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class AggregateNotFoundException : SmabuException
    {
        public AggregateNotFoundException(IEntityId id) : base($"Aggregate {id} not found.")
        {
        }
    }
}

using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class AggregateNotFoundException : SmabuException
    {
        public AggregateNotFoundException(IEntityId id) : base($"Aggregate {id} not found.")
        {
        }
    }
}

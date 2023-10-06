using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class EntityNotFoundException : SmabuException
    {
        public EntityNotFoundException(IEntityId id) : base($"Entity {id} not found.")
        {
        }
    }
}

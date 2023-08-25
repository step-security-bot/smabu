using LIT.Smabu.Shared.Entities;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class EntityNotFoundException : SmabuException
    {
        public EntityNotFoundException(IEntityId id) : base($"Entity {id} not found.")
        {
        }
    }
}

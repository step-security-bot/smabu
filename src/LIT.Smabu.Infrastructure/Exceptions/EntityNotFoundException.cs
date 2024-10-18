using LIT.Smabu.Shared;

namespace LIT.Smabu.Infrastructure.Exceptions
{
    public class EntityNotFoundException(IEntityId id) : SmabuException($"Entity {id} not found.")
    {
    }
}

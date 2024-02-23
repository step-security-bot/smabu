using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class EntityNotFoundException(IEntityId id) : SmabuException($"Entity {id} not found.")
    {
    }
}

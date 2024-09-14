using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.Errors
{
    public class DomainError(string message, IEntityId? entityId = null) : Exception(BuildMessage(message, entityId))
    {
        private static string BuildMessage(string message, IEntityId? entityId)
        {
            return message + (entityId != null ? $" ({entityId.GetType().Name}: {entityId})" : "");
        }
    }
}

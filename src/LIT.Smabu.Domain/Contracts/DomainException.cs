using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Contracts
{
    public class DomainException : Exception
    {
        public DomainException(string message, IEntityId entityId) : base(BuildMessage(message, entityId))
        {
        }

        private static string BuildMessage(string message, IEntityId? entityId)
        {
            return message + (entityId != null ? $" (EntityId: {entityId})" : "");
        }
    }
}

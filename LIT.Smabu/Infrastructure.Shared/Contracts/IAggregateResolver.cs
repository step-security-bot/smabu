using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IAggregateResolver
    {
        Dictionary<IEntityId, IAggregateRoot> ResolveByIds(IEntityId[] id);
    }
}

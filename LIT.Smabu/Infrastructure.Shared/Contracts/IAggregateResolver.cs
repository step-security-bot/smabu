using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IAggregateResolver
    {
        Task<Dictionary<IEntityId, IAggregateRoot>> ResolveByIdsAsync(IEntityId[] id);
    }
}

using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IMapperSettings
    {
        Task PostHandleAsync<TDest>(TDest dest);
        Task<Dictionary<IEntityId, IAggregateRoot>> ResolveAggregatesAsync(IEntityId[] entityIds);
    }
}

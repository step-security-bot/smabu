using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IAggregateStore
    {
        Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
        Task<List<TAggregate>> GetAsync<TAggregate>() where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<TAggregate> GetAsync<TAggregate>(IEntityId<TAggregate> id) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
        Task<List<TAggregate>> BrowseAsync<TAggregate>(Func<TAggregate, bool> predicate) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<Dictionary<TEntityId, TAggregate>> GetAsync<TAggregate, TEntityId>(List<TEntityId> ids) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : class, IEntityId<TAggregate>;
    }
}

using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.DDD
{
    public interface IAggregateStore
    {
        Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
        Task<List<TAggregate>> GetAllAsync<TAggregate>() where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<TAggregate> GetAsync<TAggregate>(IEntityId<TAggregate> id) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
        Task<List<TAggregate>> BrowseAsync<TAggregate>(Func<TAggregate, bool> predicate) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<Dictionary<TEntityId, TAggregate>> GetByIdsAsync<TAggregate, TEntityId>(List<TEntityId> ids) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : class, IEntityId<TAggregate>;
    }
}

using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IAggregateStore
    {
        Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) 
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;

        Task<List<TAggregate>> GetAllAsync<TAggregate>() 
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<TAggregate> GetByAsync<TAggregate>(IEntityId<TAggregate> id) 
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate) 
            where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
    }
}

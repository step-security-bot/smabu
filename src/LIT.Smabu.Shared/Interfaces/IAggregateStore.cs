namespace LIT.Smabu.Shared.Interfaces
{
    public interface IAggregateStore
    {
        Task CreateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task UpdateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<IReadOnlyList<TAggregate>> GetAllAsync<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<TAggregate> GetByAsync<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
    }
}

using LIT.Smabu.Domain.OfferAggregate.Specifications;

namespace LIT.Smabu.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task<bool> CreateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<bool> UpdateAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<IReadOnlyList<TAggregate>> GetAllAsync<TAggregate>()
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<TAggregate> GetByAsync<TAggregate>(IEntityId<TAggregate> id)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<Dictionary<IEntityId<TAggregate>, TAggregate>> GetByAsync<TAggregate>(IEnumerable<IEntityId<TAggregate>> ids)
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;

        Task<IReadOnlyList<TAggregate>> ApplySpecification<TAggregate>(Specification<TAggregate> specification) 
            where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
    }
}

using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Infrastructure.DDD
{
    public interface IAggregateStore
    {
        Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
        List<TAggregate> GetAll<TAggregate>() where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        TAggregate Get<TAggregate>(IEntityId<TAggregate> id) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        Task<bool> DeleteAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregateRoot<IEntityId<TAggregate>>;
        List<TAggregate> Browse<TAggregate>(Func<TAggregate, bool> predicate) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
        List<TAggregate> GetByIds<TAggregate, TEntityId>(List<TEntityId> ids) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : class, IEntityId<TAggregate>;
    }
}

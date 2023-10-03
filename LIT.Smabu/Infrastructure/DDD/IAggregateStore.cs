using LIT.Smabu.Shared.Entities;

namespace LIT.Smabu.Infrastructure.DDD
{
    public interface IAggregateStore
    {
        Task LoadAsync();
        Task AddOrUpdateAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId;
        List<TAggregate> GetAll<TAggregate, TEntityId>() where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : IEntityId;
        TAggregate Get<TAggregate, TEntityId>(TEntityId id) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : IEntityId;
        Task<bool> DeleteAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId;
        List<TAggregate> Browse<TAggregate, TEntityId>(Func<TAggregate, bool> predicate) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : IEntityId;
        List<TAggregate> GetByIds<TAggregate, TEntityId>(List<TEntityId> ids) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : IEntityId;
    }
}

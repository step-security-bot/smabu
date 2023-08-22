using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Infrastructure.DDD
{
    public interface IAggregateStore
    {
        void Load();
        Task AddOrUpdateAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId;
        Task<TAggregate> GetAsync<TAggregate, TEntityId>(TEntityId id) where TAggregate : class, IAggregateRoot<TEntityId> where TEntityId : IEntityId;
        Task<bool> DeleteAsync<TEntityId>(IAggregateRoot<TEntityId> aggregate) where TEntityId : IEntityId;
    }
}

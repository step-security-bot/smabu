using LIT.Smabu.Shared;

namespace LIT.Smabu.Infrastructure.Messaging
{
    public interface IDomainEventDispatcher
    {
        Task HandleDomainEventsAsync<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregateRoot<IEntityId<TAggregate>>;
    }
}
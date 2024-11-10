using LIT.Smabu.Shared;
using System.Transactions;

namespace LIT.Smabu.Domain.Shared
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId>
        where TEntityId : class, IEntityId
    {
        readonly List<IDomainEvent> _unhandledEvents = [];

        public AggregateMeta? Meta { get; set; }

        public void UpdateMeta(AggregateMeta aggregateMeta)
        {
            if (Meta == null || Meta.Version == aggregateMeta.Version - 1)
            {
                Meta = aggregateMeta;
            }
            else
            {
                throw new DomainException($"Erwartete Version ist {Meta.Version + 1} anstatt {aggregateMeta.Version}.", Id);
            }
        }

        public virtual Result Delete()
        {
            return Result.Success();
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents(bool cleanup = true)
        {
            var events = _unhandledEvents.ToList();
            if (cleanup)
            {
                _unhandledEvents.Clear();
            }
            return events;
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _unhandledEvents.Add(domainEvent);
        }
    }
}

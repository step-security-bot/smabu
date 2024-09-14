using LIT.Smabu.Domain.Errors;

namespace LIT.Smabu.Domain.SeedWork
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId>
        where TEntityId : class, IEntityId
    {
        public AggregateMeta? Meta { get; set; }

        public void UpdateMeta(AggregateMeta aggregateMeta)
        {
            if (Meta == null || Meta.Version == aggregateMeta.Version - 1)
            {
                Meta = aggregateMeta;
            }
            else
            {
                throw new DomainError($"Erwartete Version ist {Meta.Version + 1} anstatt {aggregateMeta.Version}.", Id);
            }
        }

        public virtual void Delete()
        {

        }
    }
}

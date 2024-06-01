using LIT.Smabu.Domain.Exceptions;

namespace LIT.Smabu.Domain.SeedWork
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId>
        where TEntityId : class, IEntityId
    {
        public new AggregateMeta? Meta
        {
            get
            {
                return base.Meta as AggregateMeta;
            }
            set
            {
                base.Meta = value;
            }
        }

        public void UpdateMeta(AggregateMeta aggregateMeta)
        {
            if (Meta == null || Meta.Version == aggregateMeta.Version - 1)
            {
                base.Meta = aggregateMeta;
            }
            else
            {
                throw new DomainException($"Erwartete Version ist {Meta.Version + 1} anstatt {aggregateMeta.Version}.", Id);
            }
        }

        public virtual void Delete()
        {

        }
    }
}

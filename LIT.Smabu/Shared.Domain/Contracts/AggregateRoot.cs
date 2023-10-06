using LIT.Smabu.Domain.Shared.Exceptions;

namespace LIT.Smabu.Domain.Shared.Contracts
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId>
        where TEntityId : class, IEntityId
    {
        public new IAggregateMeta? Meta
        {
            get
            {
                return base.Meta as IAggregateMeta;
            }
            set
            {
                base.Meta = value;
            }
        }

        public void UpdateMeta(IAggregateMeta aggregateMeta)
        {
            if (Meta == null || Meta.Version == aggregateMeta.Version - 1)
            {
                base.Meta = aggregateMeta;
            }
            else
            {
                throw new DomainException($"Erwartete Version ist {Meta.Version + 1} anstatt {aggregateMeta.Version}.");
            }
        }
    }
}

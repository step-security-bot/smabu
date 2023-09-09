using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.Entities
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId> where TEntityId : IEntityId
    {
        public new IAggregateMeta? Meta => base.Meta as IAggregateMeta;

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

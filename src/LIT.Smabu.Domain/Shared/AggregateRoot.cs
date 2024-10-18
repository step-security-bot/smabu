using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Shared
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
                throw new DomainException($"Erwartete Version ist {Meta.Version + 1} anstatt {aggregateMeta.Version}.", Id);
            }
        }

        public virtual Result Delete()
        {
            return Result.Success();
        }
    }
}

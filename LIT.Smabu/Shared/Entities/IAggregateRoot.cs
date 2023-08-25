using System;

namespace LIT.Smabu.Shared.Entities
{
    public interface IAggregateRoot<TEntityId> : IAggregateRoot, IEntity<TEntityId>
        where TEntityId : IEntityId
    {
    }

    public interface IAggregateRoot : IEntity
    {
        public new IAggregateMeta? Meta { get; }
        void UpdateMeta(IAggregateMeta aggregateMeta);
    }
}


using System;
namespace LIT.Smabu.Shared.Common
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


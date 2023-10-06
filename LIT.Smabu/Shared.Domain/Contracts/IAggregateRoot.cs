﻿namespace LIT.Smabu.Domain.Shared.Contracts
{
    public interface IAggregateRoot<out TEntityId> : IAggregateRoot, IEntity<TEntityId>
        where TEntityId : class, IEntityId
    {

    }

    public interface IAggregateRoot : IEntity
    {
        public new IAggregateMeta? Meta { get; }
        void UpdateMeta(IAggregateMeta aggregateMeta);
    }
}


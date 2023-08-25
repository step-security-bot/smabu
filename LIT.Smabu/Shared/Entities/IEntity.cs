using System;

namespace LIT.Smabu.Shared.Entities
{
    public interface IEntity<TEntityId> : IEntity where TEntityId : IEntityId
    {
        public TEntityId Id { get; }
    }

    public interface IEntity
    {
        public IEntityMeta? Meta { get; }
    }
}


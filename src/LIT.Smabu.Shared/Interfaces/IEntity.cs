using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Shared.Interfaces
{
    public interface IEntity<out TEntityId> : IEntity where TEntityId : IEntityId
    {
        public TEntityId Id { get; }
    }

    public interface IEntity
    {
        public EntityMeta? Meta { get; }
    }
}


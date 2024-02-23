using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Contracts
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public IEntityMeta? Meta { get; set; }
        public abstract TEntityId Id { get; }
    }
}

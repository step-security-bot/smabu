using LIT.Smabu.Shared.Contracts;
using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Contracts
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public EntityMeta? Meta { get; set; }
        public abstract TEntityId Id { get; }
    }
}

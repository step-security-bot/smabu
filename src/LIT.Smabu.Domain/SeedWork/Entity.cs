namespace LIT.Smabu.Domain.SeedWork
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public EntityMeta? Meta { get; set; }
        public abstract TEntityId Id { get; }
    }
}

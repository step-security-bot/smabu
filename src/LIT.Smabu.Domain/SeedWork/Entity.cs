namespace LIT.Smabu.Domain.SeedWork
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public abstract TEntityId Id { get; }
    }
}

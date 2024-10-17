namespace LIT.Smabu.Shared
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public abstract TEntityId Id { get; }
    }
}

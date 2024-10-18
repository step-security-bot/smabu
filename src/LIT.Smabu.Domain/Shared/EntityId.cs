using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Shared
{
    public abstract record EntityId<TEntity>(Guid Value) : IEntityId<TEntity> where TEntity : IEntity
    {
        public sealed override string ToString() => Value.ToString();
    }
}

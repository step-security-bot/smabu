using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Contracts
{
    public abstract record EntityId<TEntity>(Guid Value) : IEntityId<TEntity> where TEntity : IEntity
    {
        public sealed override string ToString() => Value.ToString();
    }
}

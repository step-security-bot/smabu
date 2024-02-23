using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Domain.Contracts
{
    public abstract class EntityId<TEntity>(Guid value) : IEntityId<TEntity> where TEntity : IEntity
    {
        public Guid Value { get; } = value;

        public static bool operator ==(EntityId<TEntity> obj1, EntityId<TEntity> obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(EntityId<TEntity> obj1, EntityId<TEntity> obj2) => !(obj1 == obj2);

        public bool Equals(Guid id)
        {
            return this.Value == id;
        }

        public override bool Equals(object? obj)
        {
            var target = obj as EntityId<TEntity>;
            return Value.Equals(target?.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString() => Value.ToString();
    }
}

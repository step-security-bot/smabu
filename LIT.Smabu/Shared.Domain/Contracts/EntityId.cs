namespace LIT.Smabu.Domain.Shared.Contracts
{
    public abstract class EntityId<TEntity> : IEntityId<TEntity> where TEntity : IEntity
    {
        public EntityId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
        public static bool operator ==(EntityId<TEntity> obj1, EntityId<TEntity> obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(EntityId<TEntity> obj1, EntityId<TEntity> obj2) => !(obj1 == obj2);

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

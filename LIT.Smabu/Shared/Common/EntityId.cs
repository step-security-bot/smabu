namespace LIT.Smabu.Shared.Common
{
    public abstract class EntityId<TEntity> : IEntityId<TEntity> where TEntity : IEntity
    {
        public EntityId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public override bool Equals(object? obj)
        {
            var target = obj as EntityId<TEntity>;
            return Value.Equals(target?.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}

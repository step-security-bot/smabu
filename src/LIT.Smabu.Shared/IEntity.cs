namespace LIT.Smabu.Shared
{
    public interface IEntity<out TEntityId> : IEntity where TEntityId : IEntityId
    {
        public TEntityId Id { get; }
    }

    public interface IEntity
    {

    }
}


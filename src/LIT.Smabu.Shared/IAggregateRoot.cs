namespace LIT.Smabu.Shared
{
    public interface IAggregateRoot<out TEntityId> : IAggregateRoot, IEntity<TEntityId>
        where TEntityId : class, IEntityId
    {

    }

    public interface IAggregateRoot : IEntity
    {
        public AggregateMeta? Meta { get; }
        void UpdateMeta(AggregateMeta aggregateMeta);
    }
}


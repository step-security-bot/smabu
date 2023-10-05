namespace LIT.Smabu.Shared.Domain.Contracts
{
    public interface IEntityId<TEntity> : IEntityId where TEntity : IEntity
    {

    }

    public interface IEntityId : IValueObject
    {
        Guid Value { get; }
    }
}

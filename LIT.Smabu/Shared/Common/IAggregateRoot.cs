using System;
namespace LIT.Smabu.Shared.Common
{
    public interface IAggregateRoot<TEntityId> : IAggregateRoot, IEntity<TEntityId>
        where TEntityId : IEntityId
    {

    }

    public interface IAggregateRoot : IEntity
    {

    }
}


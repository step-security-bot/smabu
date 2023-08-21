using System;
namespace LIT.Smabu.Shared.Common
{
    public interface IAggregateRoot<TEntityId> : IAggregateRoot
    {

    }

    public interface IAggregateRoot : IEntity
    {

    }
}


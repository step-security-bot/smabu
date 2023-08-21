using System;
namespace LIT.Smabu.Shared.Common
{
    public interface IEntity<TEntityId> : IEntity where TEntityId : IEntityId
    {
        public TEntityId Id { get; }
        public string DisplayName { get; }
    }

    public interface IEntity
    {

    }
}


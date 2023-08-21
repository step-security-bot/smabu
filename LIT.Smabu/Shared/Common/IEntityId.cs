using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Common
{
    public interface IEntityId<TEntity> : IEntityId where TEntity : IEntity
    {

    }

    public interface IEntityId : IValueObject
    {
        Guid Value { get; }
    }
}

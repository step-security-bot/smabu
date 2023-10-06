using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Domain.Shared.Contracts
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public IEntityMeta? Meta { get; set; }
        public abstract TEntityId Id { get; }
    }
}

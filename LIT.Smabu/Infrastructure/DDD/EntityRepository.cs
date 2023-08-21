using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.DDD
{
    public class EntityRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId> where TEntityId : IEntityId
    {
        public EntityRepository(IAggregateRepository aggregateRepository)
        {
                
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(TEntityId id)
        {
            throw new NotImplementedException();
        }
    }
}

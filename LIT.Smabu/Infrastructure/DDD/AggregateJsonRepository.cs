using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.DDD
{
    public class AggregateJsonRepository : IAggregateRepository
    {
        public Task AddOrUpdateAsync(IAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IAggregateRoot aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntitiesAsync<TEntity>() where TEntity : IEntity
        {
            throw new NotImplementedException();
        }
    }
}

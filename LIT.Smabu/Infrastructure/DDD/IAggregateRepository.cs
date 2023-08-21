using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.DDD
{
    public interface IAggregateRepository
    {
        Task AddOrUpdateAsync(IAggregateRoot aggregate);
        Task DeleteAsync(IAggregateRoot aggregate);
        Task<TEntity> GetEntitiesAsync<TEntity>() where TEntity : IEntity;
    }
}

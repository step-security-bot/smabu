using LIT.Smabu.Shared.BusinessDomain.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Common
{
    public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId> where TEntityId : IEntityId
    {

    }
}

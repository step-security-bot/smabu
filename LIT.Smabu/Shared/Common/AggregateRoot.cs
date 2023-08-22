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
        public new IAggregateMeta? Meta { get; private set; }

        public void UpdateMeta(IAggregateMeta aggregateMeta)
        {
            if (this.Meta == null || this.Meta.Version == aggregateMeta.Version - 1)
            {
                this.Meta = aggregateMeta;
            }
            else
            {
                throw new DomainException($"Erwartete Version ist {this.Meta.Version + 1} anstatt {aggregateMeta.Version}.");
            }
        }
    }
}

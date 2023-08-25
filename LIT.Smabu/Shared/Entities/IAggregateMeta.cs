using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Entities
{
    public interface IAggregateMeta : IEntityMeta
    {
        long Version { get; }
    }
}

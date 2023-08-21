using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Common
{
    public interface IAggregateMeta : IEntityMeta
    {
        long Version { get; }
    }
}

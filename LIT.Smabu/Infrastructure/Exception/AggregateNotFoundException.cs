using LIT.Smabu.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Infrastructure.Exception
{
    public class AggregateNotFoundException : SmabuException
    {
        public AggregateNotFoundException(IEntityId id) : base($"Aggregate {id} not found.")
        {
        }
    }
}

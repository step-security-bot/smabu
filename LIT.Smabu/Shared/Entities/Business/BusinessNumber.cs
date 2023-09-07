using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Entities.Business
{
    public abstract class BusinessNumber : SimpleValueObject<long>
    {
        public BusinessNumber(long value) : base(value)
        {
        }

        public abstract string ShortForm { get; }
        public abstract int Digits { get; }

        public string ToLongString() => $"{ShortForm}-{Value.ToString(new string('0', Digits))}";
    }
}

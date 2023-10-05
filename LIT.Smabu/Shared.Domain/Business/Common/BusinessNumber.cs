using LIT.Smabu.Shared.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIT.Smabu.Shared.Domain.Business.Common
{
    public abstract class BusinessNumber : SimpleValueObject<long>
    {
        public BusinessNumber(long value) : base(value)
        {
        }

        public abstract string ShortForm { get; }
        public abstract int Digits { get; }

        public override int CompareTo(object? obj) => obj is not null ? ToLongString().CompareTo(((BusinessNumber)obj).ToLongString()) : -1;

        public string ToLongString() => $"{ShortForm}-{Value.ToString(new string('0', Digits))}";
    }
}

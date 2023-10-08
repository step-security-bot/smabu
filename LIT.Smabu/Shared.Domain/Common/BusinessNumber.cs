using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
{
    public abstract record BusinessNumber : SimpleValueObject<long>
    {
        public BusinessNumber(long value) : base(value)
        {
        }

        public abstract string ShortForm { get; }
        public abstract int Digits { get; }

        public override int CompareTo(object? obj) => obj is not null ? ToLongString().CompareTo(((BusinessNumber)obj).ToLongString()) : -1;

        public string ToLongString() => $"{ShortForm}-{Value.ToString(new string('0', Digits))}";
        public string ToShortString() => $"{Value.ToString(new string('0', Digits))}";
    }
}

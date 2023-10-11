using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Common
{
    public abstract record BusinessNumber : SimpleValueObject<long>
    {
        private const string TempText = "TEMP";

        public BusinessNumber(long value) : base(value)
        {

        }

        public abstract string ShortForm { get; }
        public abstract int Digits { get; }

        protected virtual int TemporaryValue { get; } = 0;
        public bool IsTemporary => this.Value == TemporaryValue;

        public override int CompareTo(object? obj) => obj is not null ? ToLongString().CompareTo(((BusinessNumber)obj).ToLongString()) : -1;
        public string ToLongString() => $"{ShortForm}-{(IsTemporary ? TempText : ConvertValueToFormattedString())}";
        public string ToShortString() => $"{(IsTemporary ? TempText : ConvertValueToFormattedString())}";

        private string ConvertValueToFormattedString()
        {
            return Value.ToString(new string('0', Digits));
        }
    }
}

namespace LIT.Smabu.Domain.Shared
{
    public abstract record BusinessNumber : SimpleValueObject<long>
    {
        private const string TempText = "TEMP";

        protected BusinessNumber(long value) : base(value)
        {
            DisplayName = $"{ShortForm}-{(IsTemporary ? TempText : ConvertValueToFormattedString())}";
        }

        public abstract string ShortForm { get; }
        public abstract int Digits { get; }
        protected virtual int TemporaryValue { get; } = 0;
        public bool IsTemporary => Value == TemporaryValue;
        public string DisplayName { get; }

        public override int CompareTo(SimpleValueObject<long>? other) => other is not null ? DisplayName.CompareTo(((BusinessNumber)other).DisplayName) : -1;

        private string ConvertValueToFormattedString()
        {
            return Value.ToString(new string('0', Digits));
        }
    }
}

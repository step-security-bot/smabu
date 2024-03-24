namespace LIT.Smabu.Domain.Contracts
{
    public abstract record BusinessNumber(long Value) : SimpleValueObject<long>(Value)
    {
        private const string TempText = "TEMP";

        public abstract string ShortForm { get; }
        public abstract int Digits { get; }
        protected virtual int TemporaryValue { get; } = 0;
        public bool IsTemporary => Value == TemporaryValue;
        public string Long => $"{ShortForm}-{(IsTemporary ? TempText : ConvertValueToFormattedString())}";

        public override int CompareTo(object? obj) => obj is not null ? Long.CompareTo(((BusinessNumber)obj).Long) : -1;

        private string ConvertValueToFormattedString()
        {
            return Value.ToString(new string('0', Digits));
        }
    }
}

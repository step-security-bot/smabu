namespace LIT.Smabu.Domain.Contracts
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
        public bool IsTemporary => Value == TemporaryValue;

        public string Long => $"{ShortForm}-{(IsTemporary ? TempText : ConvertValueToFormattedString())}";

        public override int CompareTo(object? obj) => obj is not null ? Long.CompareTo(((BusinessNumber)obj).Long) : -1;

        private string ConvertValueToFormattedString()
        {
            return Value.ToString(new string('0', Digits));
        }
    }
}

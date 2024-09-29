namespace LIT.Smabu.Domain.Common
{
    public record TaxRate
    {
        public TaxRate(string name, decimal rate, string details)
        {
            Name = name;
            Rate = rate;
            Details = details;
        }

        public string Name { get; init; }
        public decimal Rate { get; init; }
        public string Details { get; init; }

        public static TaxRate[] GetAll()
        {
            return
            [
                new TaxRate ("Kleinunternehmer", 0, "Umsatzsteuer wird aufgrund der Befreiung für Kleinunternehmer gemäß § 19 Abs. 1 UStG nicht gesondert ausgewiesen.")
            ];
        }
    }
}

namespace LIT.Smabu.UseCases.Orders
{
    public record OrderReferenceItem<TId>(TId Id, string Name, DateOnly? Date, decimal? Amount)
    {
        internal static OrderReferenceItem<TId> Create(TId id, string name, DateOnly? date, decimal amount)
        {
            return new OrderReferenceItem<TId>(id, name, date, amount);
        }
    }
}

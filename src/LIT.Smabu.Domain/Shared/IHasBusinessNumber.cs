namespace LIT.Smabu.Domain.Shared
{
    public interface IHasBusinessNumber<T>
        where T : BusinessNumber
    {
        T Number { get; }
    }
}

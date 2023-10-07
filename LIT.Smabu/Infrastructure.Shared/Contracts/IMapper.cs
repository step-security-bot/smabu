using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IMapper
    {
        IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> source) where TDest : new();
        TDest Map<TSource, TDest>(TSource source) where TDest : new();
        TDest MapToValueObject<TSource, TDest>(TSource source) where TDest : IValueObject;
    }
}
using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Infrastructure.Shared.Contracts
{
    public interface IMapper
    {
        TDest Map<TSource, TDest>(TSource source) where TDest : new();
        TDest MapToValueObject<TSource, TDest>(TSource source) where TDest : IValueObject;
    }
}
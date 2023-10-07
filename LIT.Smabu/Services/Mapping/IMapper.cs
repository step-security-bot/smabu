namespace LIT.Smabu.Business.Service.Mapping
{
    public interface IMapper
    {
        TDest Map<TSource, TDest>(TSource source) where TDest : new();
        TDest MapToValueObject<TSource, TDest>(TSource source);
    }
}
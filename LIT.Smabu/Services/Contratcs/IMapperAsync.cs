namespace LIT.Smabu.Business.Service.Contratcs
{
    public interface IMapperAsync<TSource, TDest>
    {
        Task<TDest> MapAsync(TSource source);
    }
}

namespace LIT.Smabu.Business.Service.Contratcs
{
    public interface IAsyncMapper<TSource, TDest>
    {
        Task<TDest> MapAsync(TSource source);
    }
}

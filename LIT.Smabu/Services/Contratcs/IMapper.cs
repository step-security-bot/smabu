namespace LIT.Smabu.Business.Service.Contratcs
{
    public interface IMapper<TSource, TDest>
    {
        TDest Map(TSource source);
    }
}

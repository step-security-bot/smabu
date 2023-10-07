using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Shared.Contracts;

namespace LIT.Smabu.Business.Service.Contratcs
{
    public interface IManyAsyncMapper<TSource, TDest> : IAsyncMapper<TSource, TDest>
    {
        Task<Dictionary<IEntityId, TDest>> MapAsync(IEnumerable<TSource> source);
    }
}

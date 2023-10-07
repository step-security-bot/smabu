using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Business.Service.Contratcs
{
    public interface IMapperManyAsync<TSource, TDest> : IMapperAsync<TSource, TDest>
    {
        Task<Dictionary<IEntityId, TDest>> MapAsync(IEnumerable<TSource> source);
    }
}

using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.GetGroup
{
    public class GetCatalogGroupHandler(IAggregateStore store) : IQueryHandler<GetCatalogGroupQuery, CatalogGroupDTO>
    {
        public async Task<Result<CatalogGroupDTO>> Handle(GetCatalogGroupQuery request, CancellationToken cancellationToken)
        {
            var catalog = await store.GetByAsync(request.CatalogId);
            var groupResult = catalog.GetGroup(request.CatalogGroupId);
            return groupResult.IsSuccess
                ? CatalogGroupDTO.Create(groupResult.Value!)
                : Result<CatalogGroupDTO>.Failure(groupResult.Error);
        }
    }
}

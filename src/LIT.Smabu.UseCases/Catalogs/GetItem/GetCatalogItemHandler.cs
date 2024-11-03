using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.GetItem
{
    public class GetCatalogItemHandler(IAggregateStore store) : IQueryHandler<GetCatalogItemQuery, CatalogItemDTO>
    {
        public async Task<Result<CatalogItemDTO>> Handle(GetCatalogItemQuery request, CancellationToken cancellationToken)
        {
            var catalog = await store.GetByAsync(CatalogId.DefaultId);
            var itemResult = catalog.GetItem(request.Id);
            return itemResult.IsSuccess
            ? CatalogItemDTO.Create(itemResult.Value!)
            : itemResult.Error;            
        }
    }
}

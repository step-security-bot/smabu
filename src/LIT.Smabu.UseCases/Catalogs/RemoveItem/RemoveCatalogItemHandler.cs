using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.RemoveItem
{
    public class RemoveCatalogItemHandler(IAggregateStore store) : ICommandHandler<RemoveCatalogItemCommand>
    {
        public async Task<Result> Handle(RemoveCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var catalog = await store.GetByAsync(request.CatalogId);
            var result = catalog.RemoveItem(request.CatalogItemId);
            await store.UpdateAsync(catalog);
            return result;
        }
    }
}

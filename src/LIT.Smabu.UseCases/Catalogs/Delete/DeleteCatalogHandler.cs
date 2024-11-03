using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.Delete
{
    public class DeleteCatalogHandler(IAggregateStore store) : ICommandHandler<DeleteCatalogCommand>
    {
        public async Task<Result> Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
        {
            var catalog = await store.GetByAsync(request.Id);
            var result = catalog.Delete();
            await store.DeleteAsync(catalog);
            return result;
        }
    }
}

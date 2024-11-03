using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.AddGroup
{
    public class AddCatalogGroupHandler(IAggregateStore store) : ICommandHandler<AddCatalogGroupCommand>
    {
        public async Task<Result> Handle(AddCatalogGroupCommand request, CancellationToken cancellationToken)
        {
            var catalog = await store.GetByAsync(request.CatalogId);
            var groupResult = catalog.AddGroup(request.Id, request.Name, request.Description);
            return groupResult;
        }
    }
}

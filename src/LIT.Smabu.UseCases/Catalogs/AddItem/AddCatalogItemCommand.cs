using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.AddItem
{
    public record AddCatalogItemCommand(CatalogItemId CatalogItemId,
        CatalogId CatalogId, 
        CatalogGroupId CatalogGroupId, 
        string Name, string Description) : ICommand
    {

    }
}
using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.RemoveItem
{
    public record RemoveCatalogItemCommand(CatalogId CatalogId, CatalogItemId Id) : ICommand
    {

    }
}
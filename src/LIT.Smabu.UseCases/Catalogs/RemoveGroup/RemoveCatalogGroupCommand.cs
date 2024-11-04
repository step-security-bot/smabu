using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.RemoveGroup
{
    public record RemoveCatalogGroupCommand(CatalogGroupId CatalogGroupId, CatalogId CatalogId) : ICommand
    {

    }
}
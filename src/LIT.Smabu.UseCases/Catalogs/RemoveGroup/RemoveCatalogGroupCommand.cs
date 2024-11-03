using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.RemoveGroup
{
    public record RemoveCatalogGroupCommand(CatalogId CatalogId, CatalogGroupId Id) : ICommand
    {

    }
}
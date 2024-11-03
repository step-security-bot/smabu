using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.AddGroup
{
    public record AddCatalogGroupCommand(CatalogId CatalogId, CatalogGroupId Id, string Name, string Description) : ICommand
    {

    }
}
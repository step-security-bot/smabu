using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.Update
{
    public record UpdateCatalogCommand(CatalogId CatalogId, string Name) : ICommand
    {

    }
}
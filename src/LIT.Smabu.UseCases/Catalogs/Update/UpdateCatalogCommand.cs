using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.Update
{
    public record UpdateCatalogCommand(CatalogId Id, string Name) : ICommand
    {

    }
}
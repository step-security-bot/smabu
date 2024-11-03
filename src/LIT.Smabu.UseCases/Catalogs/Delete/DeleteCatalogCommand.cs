using LIT.Smabu.Domain.CatalogAggregate;
using LIT.Smabu.UseCases.Shared;

namespace LIT.Smabu.UseCases.Catalogs.Delete
{
    public record DeleteCatalogCommand(CatalogId Id) : ICommand
    {

    }
}
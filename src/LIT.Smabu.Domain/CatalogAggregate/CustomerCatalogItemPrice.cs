using LIT.Smabu.Domain.CustomerAggregate;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public record CustomerCatalogItemPrice(decimal Price, DateTime ValidFrom, CustomerId CustomerId) : CatalogItemPrice(Price, ValidFrom)
    {

    }
}

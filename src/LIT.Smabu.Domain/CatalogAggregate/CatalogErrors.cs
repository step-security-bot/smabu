
using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{

    public static class CatalogErrors
    {
        public static readonly Error NameEmpty = new("CatalogGroup.NameEmpty", "Name must no be empty.");
        public static readonly Error UnitEmpty = new("CatalogGroup.UnitEmpty", "Unit must no be empty.");
        public static readonly Error NoValidPrice = new("CatalogItem.NoValidPrice", "One valid price must be available at least.");
    }
}


using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public static class CatalogErrors
    {
        public static readonly Error NameEmpty = new("Catalog.NameEmpty", "Name must no be empty.");
        public static readonly Error NameAlreadyExists = new("Catalog.NameAlreadyExists", "Name already exists.");
        public static readonly Error UnitEmpty = new("Catalog.UnitEmpty", "Unit must no be empty.");
        public static readonly Error NoValidPrice = new("Catalog.NoValidPrice", "One valid price must be available at least.");
        public static readonly Error GroupNotFound = new("Catalog.GroupNotFound", "Group not found.");
    }
}

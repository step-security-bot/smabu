using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public class CatalogGroup : AggregateRoot<CatalogGroupId>
    {
        public CatalogGroup(CatalogGroupId id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static CatalogGroup Create(CatalogGroupId id, string name, string description)
        {
            return new CatalogGroup(id, name, description);
        }

        public override CatalogGroupId Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Result Update(string name, string description)
        {
            if (string.IsNullOrEmpty(name)) 
            {
                return CatalogErrors.NameEmpty;
            }
            Name = name;
            Description = description;

            return Result.Success();
        }

        override public Result Delete()
        {
            return base.Delete();
        }
    }
}

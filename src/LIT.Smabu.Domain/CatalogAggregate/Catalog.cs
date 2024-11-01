using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Domain.Common;

namespace LIT.Smabu.Domain.CatalogAggregate
{
    public class Catalog(CatalogId id, string name, List<CatalogGroup> groups) : AggregateRoot<CatalogId>
    {
        private readonly List<CatalogGroup> _groups = groups;

        public override CatalogId Id { get; } = id;
        public string Name { get; private set; } = name;
        public IReadOnlyList<CatalogGroup> Groups  => _groups;

        public static Catalog Create(CatalogId id, string name)
        {
            var catalog = new Catalog(id, name, []);
            return catalog;
        }

        public Result<CatalogGroup> AddGroup(CatalogGroupId id, string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return CatalogErrors.NameEmpty;
            }
            if (Groups.Any(g => g.Name == name))
            {
                return CatalogErrors.NameAlreadyExists;
            }
            var group = CatalogGroup.Create(id, Id, name, description);
            _groups.Add(group);
            return group;
        }

        public Result RemoveGroup(CatalogGroupId id)
        {
            var group = Groups.FirstOrDefault(g => g.Id == id);
            if (group == null)
            {
                return Result.Failure(CatalogErrors.GroupNotFound);
            }
            _groups.Remove(group);
            return Result.Success();
        }

        public Result UpdateGroup(CatalogGroupId id, string name, string description)
        {
            var group = Groups.FirstOrDefault(g => g.Id == id);
            if (group == null)
            {
                return Result.Failure(CatalogErrors.GroupNotFound);
            }
            group?.Update(name, description);
            return Result.Success();
        }
    }
}

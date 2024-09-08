using System.ComponentModel.DataAnnotations.Schema;

namespace LIT.Smabu.Domain.SeedWork
{
    [ComplexType]
    public record AggregateMeta : EntityMeta
    {
        public AggregateMeta(long version, DateTime createdOn, string createdById, string createdByName, DateTime? modifiedOn, string? modifiedById, string? modifiedByName)
            : base(createdOn, createdById, createdByName, modifiedOn, modifiedById, modifiedByName)
        {
            Version = version;
        }

        public long Version { get; init; }

        public static AggregateMeta CreateLegacy(ICurrentUser currentUser, DateTime createdOn)
        {
            return new AggregateMeta(1, createdOn, currentUser.Username, currentUser.Name, DateTime.Now, currentUser.Username, currentUser.Name);
        }

        public static AggregateMeta CreateFirst(ICurrentUser currentUser)
        {
            return new AggregateMeta(1, DateTime.Now, currentUser.Username, currentUser.Name, null, null, null);
        }

        public AggregateMeta Next(ICurrentUser currentUser)
        {
            return new AggregateMeta(Version + 1, CreatedOn, CreatedById, CreatedByName,
                DateTime.Now, currentUser.Username, currentUser.Name);
        }
    }
}

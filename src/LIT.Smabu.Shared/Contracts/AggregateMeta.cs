using LIT.Smabu.Shared.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIT.Smabu.Shared.Contracts
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

        public static AggregateMeta CreateFirst(ICurrentUser currentUser)
        {
            return new AggregateMeta(1, DateTime.Now, currentUser.Id, currentUser.Name, null, null, null);
        }

        public AggregateMeta Next(ICurrentUser currentUser)
        {
            return new AggregateMeta(this.Version + 1, this.CreatedOn, this.CreatedById, this.CreatedByName,
                DateTime.Now, currentUser.Id, currentUser.Name);
        }
    }
}

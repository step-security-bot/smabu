using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class AggregateMeta : EntityMeta, IAggregateMeta
    {
        public AggregateMeta(long version, DateTime createdOn, Guid createdById, string createdByName, DateTime? modifiedOn, Guid? modifiedById, string? modifiedByName)
            : base(createdOn, createdById, createdByName, modifiedOn, modifiedById, modifiedByName)
        {
            this.Version = version;
        }

        public long Version { get; }
    }
}

using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class AggregateMeta : EntityMeta, IAggregateMeta
    {
        public AggregateMeta(long version, DateTime createdOn, string createdById, string createdByName, DateTime? modifiedOn, string? modifiedById, string? modifiedByName)
            : base(createdOn, createdById, createdByName, modifiedOn, modifiedById, modifiedByName)
        {
            Version = version;
        }

        public long Version { get; }
    }
}

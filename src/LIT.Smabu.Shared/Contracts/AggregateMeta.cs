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
    }
}

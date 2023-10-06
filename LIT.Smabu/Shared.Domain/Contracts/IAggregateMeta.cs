namespace LIT.Smabu.Domain.Shared.Contracts
{
    public interface IAggregateMeta : IEntityMeta
    {
        long Version { get; }
    }
}

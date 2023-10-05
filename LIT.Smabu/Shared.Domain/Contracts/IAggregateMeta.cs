namespace LIT.Smabu.Shared.Domain.Contracts
{
    public interface IAggregateMeta : IEntityMeta
    {
        long Version { get; }
    }
}

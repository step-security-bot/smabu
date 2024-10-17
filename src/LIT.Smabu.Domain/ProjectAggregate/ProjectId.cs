using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.ProjectAggregate
{
    public record ProjectId(Guid Value) : EntityId<IProject>(Value);
}
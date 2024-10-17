using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.ProjectAggregate
{
    public record ProjectId(Guid Value) : EntityId<IProject>(Value);
}
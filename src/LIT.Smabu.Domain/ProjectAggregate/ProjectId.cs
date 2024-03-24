using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.ProjectAggregate
{
    public record ProjectId(Guid Value) : EntityId<IProject>(Value);
}
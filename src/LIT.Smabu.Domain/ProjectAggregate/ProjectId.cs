using LIT.Smabu.Domain.SeedWork;

namespace LIT.Smabu.Domain.ProjectAggregate
{
    public record ProjectId(Guid Value) : EntityId<IProject>(Value);
}
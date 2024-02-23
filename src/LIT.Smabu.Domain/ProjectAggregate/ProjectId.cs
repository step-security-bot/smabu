using LIT.Smabu.Domain.Contracts;

namespace LIT.Smabu.Domain.ProjectAggregate
{
    public class ProjectId(Guid value) : EntityId<IProject>(value);
}
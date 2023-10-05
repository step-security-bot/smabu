using LIT.Smabu.Shared.Domain.Contracts;

namespace LIT.Smabu.Shared.Domain.ProjectAggregate
{
    public class ProjectId : EntityId<IProject>
    {
        public ProjectId(Guid value) : base(value)
        {
        }
    }
}
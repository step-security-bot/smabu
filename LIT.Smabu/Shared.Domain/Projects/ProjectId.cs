using LIT.Smabu.Domain.Shared.Contracts;

namespace LIT.Smabu.Domain.Shared.Projects
{
    public class ProjectId : EntityId<IProject>
    {
        public ProjectId(Guid value) : base(value)
        {
        }
    }
}
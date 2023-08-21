using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Shared.BusinessDomain.Project
{
    public class ProjectId : EntityId<IProject>
    {
        public ProjectId(Guid value) : base(value)
        {
        }
    }
}
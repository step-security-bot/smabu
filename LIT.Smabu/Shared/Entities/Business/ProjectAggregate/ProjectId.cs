namespace LIT.Smabu.Shared.Entities.Business.ProjectAggregate
{
    public class ProjectId : EntityId<IProject>
    {
        public ProjectId(Guid value) : base(value)
        {
        }
    }
}
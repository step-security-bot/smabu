using System;
namespace LIT.Smabu.Shared.Common
{
    public interface IEntityMeta : IValueObject
    {
        public DateTime CreatedOn { get; }
        public Guid CreatedById { get; }
        public string CreatedByName { get; }
        public DateTime? ModifiedOn { get; }
        public Guid? ModifiedById { get; }
        public string? ModifiedByName { get; }
    }
}


using System;

namespace LIT.Smabu.Shared.Interfaces
{
    public interface IEntityMeta : IValueObject
    {
        public DateTime CreatedOn { get; }
        public string CreatedById { get; }
        public string CreatedByName { get; }
        public DateTime? ModifiedOn { get; }
        public string? ModifiedById { get; }
        public string? ModifiedByName { get; }
    }
}


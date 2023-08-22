using LIT.Smabu.Shared.Common;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class EntityMeta : IEntityMeta
    {
        public EntityMeta(DateTime createdOn, Guid createdById, string createdByName, DateTime? modifiedOn, Guid? modifiedById, string? modifiedByName)
        {

            CreatedOn = createdOn;
            CreatedById = createdById;
            CreatedByName = createdByName;

            if (modifiedOn != null)
            {
                if (string.IsNullOrEmpty(modifiedByName))
                {
                    throw new ArgumentException($"\"{nameof(modifiedByName)}\" kann nicht NULL oder leer sein.", nameof(createdByName));
                }
                if (modifiedById is null)
                {
                    throw new ArgumentNullException(nameof(modifiedById));
                }

                ModifiedOn = modifiedOn;
                ModifiedById = modifiedById;
                ModifiedByName = modifiedByName;
            }
        }

        public DateTime CreatedOn { get; }

        public Guid CreatedById { get; }

        public string CreatedByName { get; }

        public DateTime? ModifiedOn { get; }

        public Guid? ModifiedById { get; }

        public string? ModifiedByName { get; }
    }
}

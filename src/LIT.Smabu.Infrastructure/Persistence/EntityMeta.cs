using LIT.Smabu.Shared.Interfaces;

namespace LIT.Smabu.Infrastructure.Persistence
{
    public class EntityMeta : IEntityMeta
    {
        public EntityMeta(DateTime createdOn, string createdById, string createdByName, DateTime? modifiedOn, string? modifiedById, string? modifiedByName)
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

        public string CreatedById { get; }

        public string CreatedByName { get; }

        public DateTime? ModifiedOn { get; }

        public string? ModifiedById { get; }

        public string? ModifiedByName { get; }
    }
}

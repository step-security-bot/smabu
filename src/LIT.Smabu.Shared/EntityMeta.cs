using System.ComponentModel.DataAnnotations.Schema;

namespace LIT.Smabu.Shared
{
    [ComplexType]
    public record EntityMeta : IValueObject
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
                ArgumentNullException.ThrowIfNull(modifiedById);

                ModifiedOn = modifiedOn;
                ModifiedById = modifiedById;
                ModifiedByName = modifiedByName;
            }
        }

        public DateTime CreatedOn { get; init; }

        public string CreatedById { get; init; }

        public string CreatedByName { get; init; }

        public DateTime? ModifiedOn { get; init; }

        public string? ModifiedById { get; init; }

        public string? ModifiedByName { get; init; }
    }
}

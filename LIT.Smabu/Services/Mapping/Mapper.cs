using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using System.Reflection;

namespace LIT.Smabu.Business.Service.Mapping
{
    public class Mapper : IMapper
    {
        private readonly IAggregateResolver aggregateResolver;

        public Mapper(IAggregateResolver aggregateResolver)
        {
            this.aggregateResolver = aggregateResolver;
        }

        public TDest Map<TSource, TDest>(TSource source) where TDest : new()
        {
            var dest = new TDest();
            return this.Map(source, dest);
        }

        public TDest MapToValueObject<TSource, TDest>(TSource source)
        {
            throw new NotImplementedException();
        }

        private TDest Map<TSource, TDest>(TSource source, TDest dest) where TDest : new()
        {
            if (source != null)
            {
                var sourceProperties = source.GetType().GetProperties();
                var destProperties = dest!.GetType().GetProperties();

                foreach (var sourceProperty in sourceProperties)
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    var destProperty = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                    SetValueToDest(dest, sourceProperty, sourceValue, destProperty);

                    if (sourceValue is IEntityId entityId)
                    {
                        TryToResolveMatchingDto(dest, destProperties, sourceProperty, entityId);
                    }
                }
                return dest;
            }
            else
            {
                throw new ArgumentException("Source is null");
            }
        }

        private void SetValueToDest<TDest>(TDest dest, PropertyInfo sourceProperty, object? sourceValue, PropertyInfo? destProperty)
        {
            if (destProperty != null && sourceValue != null)
            {
                if (sourceProperty.PropertyType == destProperty.PropertyType)
                {
                    destProperty.SetValue(dest, sourceValue);
                }
                else
                {
                    TryToMap(dest, sourceProperty, sourceValue, destProperty);
                }
            }
            else
            {
                // Not found in dest
            }
        }

        private void TryToResolveMatchingDto<TDest>(TDest dest, PropertyInfo[] destProperties, PropertyInfo sourceProperty, IEntityId entityId)
        {
            var destPropertyDto = destProperties.FirstOrDefault(x => x.Name + "Id" == sourceProperty.Name);
            if (destPropertyDto != null)
            {
                var resolvedEntities = this.aggregateResolver.ResolveByIds(new[] { entityId });
                if (resolvedEntities.Count == 1)
                {
                    var resolvedEntity = resolvedEntities.First().Value;
                    var destDto = Activator.CreateInstance(destPropertyDto.PropertyType);
                    var resolvedEntityDto = Map(resolvedEntity, destDto);
                    destPropertyDto.SetValue(dest, resolvedEntityDto);
                }
                else
                {
                    // resolving not possible
                }
            }
        }

        private void TryToMap<TDest>(TDest dest, PropertyInfo sourceProperty, object sourceValue, PropertyInfo destProperty)
        {
            if (sourceValue != null)
            {
                var baseMethod = typeof(Mapper).GetMethod("Map")!;
                var genericMethod = baseMethod.MakeGenericMethod(sourceProperty.PropertyType, destProperty.PropertyType)!;
                var destValue = genericMethod.Invoke(this, new object[] { sourceValue });
                if (destValue != null)
                {
                    destProperty.SetValue(dest, destValue);
                }
            }
        }
    }
}

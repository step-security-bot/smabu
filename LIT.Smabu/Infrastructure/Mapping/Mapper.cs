using LIT.Smabu.Domain.Shared.Contracts;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace LIT.Smabu.Business.Service.Mapping
{
    //public class Mapper : IMapper
    //{
    //    private readonly ILogger<Mapper> logger;
    //    private readonly IMapperSettings settings;
    //    private readonly MethodInfo getSingleMethod;
    //    private readonly MethodInfo getEnumerableMethod;
    //    private static readonly Dictionary<Type, PropertyInfo[]> propertiesCache = new();

    //    public Mapper(ILogger<Mapper> logger, IMapperSettings settings)
    //    {
    //        this.logger = logger;
    //        this.settings = settings;
    //        this.getSingleMethod = typeof(Mapper).GetMethods().First(x => x.ToString() == "TDest Map[TSource,TDest](TSource)");
    //        this.getEnumerableMethod = typeof(Mapper).GetMethods().First(x => x.ToString() == "System.Collections.Generic.IEnumerable`1[TDest] Map[TSource,TDest](System.Collections.Generic.IEnumerable`1[TSource])");
    //    }

    //    public IEnumerable<TDest> Map<TSource, TDest>(IEnumerable<TSource> source) where TDest : new()
    //    {
    //        var result = new List<TDest>();
    //        foreach (var sourceItem in source)
    //        {
    //            var destItem = this.Map<TSource, TDest>(sourceItem);
    //            result.Add(destItem);
    //        }
    //        return result;
    //    }

    //    public TDest Map<TSource, TDest>(TSource source) where TDest : new()
    //    {
    //        var dest = new TDest();
    //        return this.Map(source, dest);
    //    }

    //    private TDest Map<TSource, TDest>(TSource source, TDest dest)
    //    {
    //        if (source != null && dest != null)
    //        {
    //            var sourceProperties = GetProperties(typeof(TSource));
    //            var destProperties = GetProperties(typeof(TDest));

    //            foreach (var sourceProperty in sourceProperties)
    //            {
    //                var sourceValue = sourceProperty.GetValue(source);
    //                var destProperty = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
    //                SetValueToDest(dest, sourceProperty, sourceValue, destProperty);
    //                if (sourceValue is IEntityId entityId)
    //                {
    //                    TryToResolveMatchingDto(dest, destProperties, sourceProperty, entityId);
    //                }
    //            }
    //            this.settings?.PostHandleAsync(dest).GetAwaiter().GetResult();
    //            return dest;
    //        }
    //        else
    //        {
    //            throw new ArgumentException("Source is null");
    //        }
    //    }

    //    public TDest MapToValueObject<TSource, TDest>(TSource source) where TDest : IValueObject
    //    {
    //        var destType = typeof(TDest);
    //        var sourceType = typeof(TSource);
    //        var destConstructor = destType.GetConstructors().FirstOrDefault()!;
    //        var sourceProperties = GetProperties(sourceType);
    //        var destConstructorValues = new List<object>();
    //        foreach (var destConstructorParameter in destConstructor.GetParameters())
    //        {
    //            var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name.Equals(destConstructorParameter.Name, StringComparison.OrdinalIgnoreCase));
    //            if (sourceProperty != null)
    //            {
    //                destConstructorValues.Add(sourceProperty.GetValue(source)!);
    //            }
    //        }
    //        try
    //        {
    //            var dest = (TDest)Activator.CreateInstance(destType, destConstructorValues.ToArray()!)!;
    //            return dest;
    //        }
    //        catch (Exception ex)
    //        {
    //            this.logger.LogError("Mapping to ValueObject not possible: {0}", ex.Message);
    //            throw;
    //        }
    //    }

    //    private void SetValueToDest(object dest, PropertyInfo sourceProperty, object? sourceValue, PropertyInfo? destProperty)
    //    {
    //        if (destProperty != null && sourceValue != null)
    //        {
    //            if (sourceProperty.PropertyType == destProperty.PropertyType)
    //            {
    //                destProperty.SetValue(dest, sourceValue);
    //            }
    //            else
    //            {
    //                TryToMap(dest!, sourceProperty, sourceValue, destProperty);
    //            }
    //        }
    //        else
    //        {
    //            // Not found in dest
    //        }
    //    }

    //    private void TryToResolveMatchingDto(object dest, PropertyInfo[] destProperties, PropertyInfo sourceProperty, IEntityId entityId)
    //    {
    //        var destPropertyDto = destProperties.FirstOrDefault(x => x.Name + "Id" == sourceProperty.Name);
    //        if (destPropertyDto != null)
    //        {
    //            var resolvedEntities = this.settings.ResolveAggregatesAsync(new[] { entityId }).GetAwaiter().GetResult();
    //            if (resolvedEntities.Count == 1)
    //            {
    //                var resolvedEntity = resolvedEntities.First().Value;
    //                var destDto = Activator.CreateInstance(destPropertyDto.PropertyType);
    //                var resolvedEntityDto = Map(resolvedEntity, destDto);
    //                destPropertyDto.SetValue(dest, resolvedEntityDto);
    //            }
    //            else
    //            {
    //                this.logger.LogError("Resolving entity '{0}' for dto '{1}' failed.", entityId, destPropertyDto.Name);
    //            }
    //        }
    //    }

    //    private void TryToMap(object dest, PropertyInfo sourceProperty, object sourceValue, PropertyInfo destProperty)
    //    {
    //        if (sourceValue != null)
    //        {
    //            MethodInfo genericMethod;
    //            if (sourceProperty.Name.Contains("List"))
    //            {
    //                genericMethod = this.getEnumerableMethod.MakeGenericMethod(sourceProperty.PropertyType, destProperty.PropertyType)!;
    //            }
    //            else
    //            {
    //                genericMethod = this.getSingleMethod.MakeGenericMethod(sourceProperty.PropertyType, destProperty.PropertyType)!;
    //            }
    //            var destValue = genericMethod.Invoke(this, new object[] { sourceValue });
    //            if (destValue != null)
    //            {
    //                destProperty.SetValue(dest, destValue);
    //            }
    //        }
    //    }

    //    private static PropertyInfo[] GetProperties(Type type)
    //    {
    //        if (!propertiesCache.ContainsKey(type))
    //        {
    //            propertiesCache.Add(type, type.GetProperties());
    //        }
    //        return propertiesCache[type];
    //    }
    //}
}

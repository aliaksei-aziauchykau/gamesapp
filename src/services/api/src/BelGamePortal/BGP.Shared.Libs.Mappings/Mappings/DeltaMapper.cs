using Microsoft.AspNetCore.OData.Deltas;

namespace BGP.Shared.Libs.Mappings;

public static class DeltaMapper
{
    public static Delta<TDestination> MapDelta<TSource, TDestination>(Delta<TSource> sourceDelta)
        where TSource : class
        where TDestination : class
    {
        // Create a new Delta for the destination type
        var destinationDelta = new Delta<TDestination>();

        // Iterate over changed properties in the source delta
        foreach (var propertyName in sourceDelta.GetChangedPropertyNames())
        {
            var sourcePropertyValue = sourceDelta.TryGetPropertyValue(propertyName, out var value) ? value : null;

            // Check if the property is complex (i.e., an object) and requires recursive mapping
            var sourcePropertyType = typeof(TSource).GetProperty(propertyName)?.PropertyType;
            if (sourcePropertyType != null && !sourcePropertyType.IsPrimitive && sourcePropertyType != typeof(string))
            {
                // If it's a complex type, handle recursive mapping
                if (sourcePropertyValue is Delta nestedSourceDelta)
                {
                    var mapMethod = typeof(DeltaMapper).GetMethod(nameof(MapDelta))
                        ?.MakeGenericMethod(sourcePropertyType, typeof(TDestination).GetProperty(propertyName)?.PropertyType);

                    var nestedMappedDelta = mapMethod?.Invoke(null, new[] { nestedSourceDelta });
                    destinationDelta.TrySetPropertyValue(propertyName, nestedMappedDelta);
                }
            }
            else
            {
                // For non-complex types, set the property value directly
                destinationDelta.TrySetPropertyValue(propertyName, sourcePropertyValue);
            }
        }

        return destinationDelta;
    }
}

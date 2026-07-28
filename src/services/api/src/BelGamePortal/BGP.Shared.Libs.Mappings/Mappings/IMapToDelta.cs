using AutoMapper;
using Microsoft.AspNetCore.OData.Deltas;

namespace BGP.Shared.Libs.Mappings.Mappings;

public interface IMapToDelta<T> : IMapTo<T> where T : class
{
    new IMappingExpression Mapping(Profile profile) { 
        var mapExpression = profile.CreateMap(typeof(Delta<>).MakeGenericType([GetType()]), typeof(Delta<T>));

        mapExpression.ConvertUsing((src, _) =>
        {
            var srcDelta = (IDelta)src;
            var deltaDestination = new Delta<T>();

            // Iterate over each changed property in the source Delta object
            foreach (var propertyName in srcDelta.GetChangedPropertyNames())
            {
                // Try to get the value from the source delta
                if (srcDelta.TryGetPropertyValue(propertyName, out var value))
                {
                    // Use AutoMapper to map the property value to the destination delta
                    deltaDestination.TrySetPropertyValue(propertyName, value);
                }
            }

            return deltaDestination;
        });

        return mapExpression;
    }
}

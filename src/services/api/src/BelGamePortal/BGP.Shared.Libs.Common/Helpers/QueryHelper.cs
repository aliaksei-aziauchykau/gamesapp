

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace BGP.Shared.Libs.Common.Helpers;

public static class QueryHelper
{
    public static IQueryable<T> FindById<T, TKey>(this IQueryable<T> query, TKey id) where T : class
    {
        var lambda = QueryHelper.GetEqualExpression<T, TKey>(id);

        // Apply the Where clause to the query
        return query.Where(lambda);
    }

    public static Expression<Func<T, bool>> GetEqualExpression<T, TKey>(TKey id) where T : class
    {

        var keyProperty = typeof(T)
            .GetProperties()
            .FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), false).Any());

        if (keyProperty == null)
        {
            throw new InvalidOperationException("No property with [Key] attribute found.");
        }

        // Create a parameter expression for the entity (e.g., "e => e")
        var parameter = Expression.Parameter(typeof(T), "x");

        // Create an expression for e.KeyProperty == id (e.g., "e => e.ContainerId == id")
        var propertyAccess = Expression.Property(parameter, keyProperty);
        var constantValue = Expression.Constant(id);

        // Make sure to handle the type mismatch between key property and id
        var equalsExpression = Expression.Equal(propertyAccess, Expression.Convert(constantValue, keyProperty.PropertyType));

        // Create the full lambda expression (e.g., "e => e.ContainerId == id")
        var lambda = Expression.Lambda<Func<T, bool>>(equalsExpression, parameter);

        return lambda;
    }

    public static PropertyInfo GetKeyProperty(Type type)
    {
        var keyProperty = type
            .GetProperties()
            .FirstOrDefault(prop => prop.GetCustomAttributes(typeof(KeyAttribute), false).Any());

        if (keyProperty == null)
        {
            throw new InvalidOperationException($"No property with [Key] attribute found. In {type.Name}");
        }

        return keyProperty;
    }
}

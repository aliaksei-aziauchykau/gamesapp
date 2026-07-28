using BGP.Shared.Libs.Common.Helpers;

namespace BGP.Shared.Libs.Domain.Entities;

public interface IHasKey<T>
{
    T Id
    {
        get
        {
            var type = GetType();
            var keyProperty = QueryHelper.GetKeyProperty(type);
            var idValue = keyProperty.GetValue(this);
            if (idValue is not T id) throw new InvalidOperationException($"The key property {keyProperty.Name} is not of type {typeof(T).Name}");
            return id;
        }
        set
        {
            var type = GetType();
            var keyProperty = QueryHelper.GetKeyProperty(type);
            keyProperty.SetValue(this, value);
        }
    }
}

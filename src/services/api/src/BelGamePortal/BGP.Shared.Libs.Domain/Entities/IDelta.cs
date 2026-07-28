namespace BGP.Shared.Libs.Domain.Entities;
public interface IDelta<T>
{
    T Patch(T entity);
}

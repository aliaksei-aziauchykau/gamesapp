namespace BGP.Shared.Modules.EntityApi;

public interface IEntityApiPath<TEntity>
{
    string ApiPath { get; }
}

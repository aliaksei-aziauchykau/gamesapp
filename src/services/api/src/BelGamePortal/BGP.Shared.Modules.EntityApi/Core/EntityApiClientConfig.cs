namespace BGP.Shared.Modules.EntityApi;

public class EntityApiClientConfig : IEntityApiClientConfig
{
    public string BaseUrl { get; set; } = string.Empty;

    public EntityDtoType EntityDtoType { get; set; } = EntityDtoType.Entity;
}

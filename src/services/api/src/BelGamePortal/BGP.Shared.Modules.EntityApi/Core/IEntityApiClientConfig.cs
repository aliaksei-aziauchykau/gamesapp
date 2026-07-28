namespace BGP.Shared.Modules.EntityApi;

public interface IEntityApiClientConfig
{
    public string BaseUrl { get; set; }

    public EntityDtoType EntityDtoType { get; set; }
}


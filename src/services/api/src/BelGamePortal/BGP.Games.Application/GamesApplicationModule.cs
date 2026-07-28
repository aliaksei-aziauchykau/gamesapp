using BGP.Games.Application.Modules.Games;
using BGP.Games.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Games.Application;

public class GamesApplicationModule
{
    private IServiceCollection services;

    public static GamesApplicationModule Create()
    {
        return new GamesApplicationModule();
    }

    public GamesApplicationModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public GamesApplicationModule AddModuleServices()
    {
        services.AddScoped<ICrudService<Game, Guid>, GamesCrudService>();

        return this;
    }

}

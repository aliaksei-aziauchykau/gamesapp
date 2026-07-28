using System.Reflection;
using AutoMapper.Internal;
using BGP.Shared.Libs.Common.Helpers;
using BGP.Shared.Libs.Mappings.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.MapCommands;

public class MapCommandsModule
{
    private IServiceCollection? _services;

    private IServiceCollection services => _services ?? throw new InvalidOperationException("Services not set");

    public static MapCommandsModule Create()
    {
        return new MapCommandsModule();
    }

    public MapCommandsModule AddServiceCollection(IServiceCollection services)
    {
        _services = services;

        return this;
    }

    public MapCommandsModule AddMapCommands(Assembly assembly, params Assembly[] assemblies)
    {
        assembly
            .Merge(assemblies)
            .ForEach((assembly) => services.RegisterMapCommands(assembly));

        return this;
    }

    public MapCommandsModule AddMappings(Assembly assembly, params Assembly[] assemblies)
    {
        assembly
            .Merge(assemblies)
            .ForEach((assembly) => services.AddAutoMapper(config =>
            {
                config.AddProfile(new MappingProfile(assembly));
            }));

        return this;
    }
}

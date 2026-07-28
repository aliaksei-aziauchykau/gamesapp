
using Microsoft.AspNetCore.Routing;
using Serilog;

namespace BGP.Shared.Libs.Api.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void LogEndpointsToConsole(this IEndpointRouteBuilder endpoints)
    {
        foreach (var dataSources in endpoints.DataSources)
        {
            foreach (var endpoint in dataSources.Endpoints)
            {
                var routeEndpoint = endpoint as RouteEndpoint;
                if (routeEndpoint != null)
                {
                    Log.Information($"Route: {routeEndpoint.RoutePattern.RawText}");
                }
            }
        }
    }

}

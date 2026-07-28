


using BGP.Shared.Libs.Common;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
namespace BGP.Shared.Modules.ODataVersioning;

public class ODataVersioningModule : DIModule<ODataVersioningModule>
{
    public ODataVersioningModule AddODataVersioningServices(IMvcBuilder mvcBuilder)
    {

        Services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            })

            .AddOData(options =>
            {
                options.AddRouteComponents("api/v{version:apiVersion}");
            })

            .AddODataApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";

                options.SubstituteApiVersionInUrl = true;
            });

        return this;
    }
}




using BGP.Shared.Libs.Common;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.Extensions.DependencyInjection;
namespace BGP.Shared.Modules.OData;

public class ODataModule : DIModule<ODataModule>
{
    public ODataModule AddODataServices(IMvcBuilder mvcBuilder)
    {

        mvcBuilder
            .AddNewtonsoftJson()
            .AddOData(options =>
            {
                options.EnableQueryFeatures();
                options.RouteOptions.EnableKeyInParenthesis = false;
                options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
                options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
                options.RouteOptions.EnableQualifiedOperationCall = false;
                options.RouteOptions.EnableUnqualifiedOperationCall = true;
            })
            .AddODataNewtonsoftJson();

        return this;
    }
}

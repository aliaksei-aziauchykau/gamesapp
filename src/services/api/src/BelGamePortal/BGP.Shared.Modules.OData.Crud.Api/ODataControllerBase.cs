using BGP.Shared.Libs.Microservice;
using MediatR;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.OData.Crud.Api;

public abstract class ODataControllerBase : ODataController
{
    private IProvideServiceProvider _serviceProvider;
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= _serviceProvider.ServiceProvider.CreateScope().ServiceProvider.GetRequiredService<ISender>();

    protected ODataControllerBase(IProvideServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}
